using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using System.Security.Claims;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            this.logger = logger;   
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);

                if (result != null)
                {
                    logger.LogInformation("Registeration Sucessfull");
                    return Ok(new { success = true, message = "Registration Successful", data = result });
                }
                else
                {
                    logger.LogError("Registeration Unsuccessfull");
                    return BadRequest(new { success = false, message = "Registration Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLoginModel)
        {
            try
            {
                var result = userBL.Login(userLoginModel);

                if (result != null)
                {
                    logger.LogInformation("Login Sucessfull");
                    return Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                {
                    logger.LogError("Registeration Unsuccessfull");
                    return BadRequest(new { success = false, message = "Login Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);

                if (result != null)
                {
                    logger.LogInformation("Email sent Successful");
                    return Ok(new { success = true, message = "Email sent Successful" });
                }
                else
                {
                    logger.LogError("Reset Email not send");
                    return BadRequest(new { success = false, message = "Reset Email not send" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [Route("ResetLink")]
        public IActionResult ResetLink(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetLink(Email, password, confirmPassword);

                if (result != null)
                {
                    logger.LogInformation("Password Reset Successful");
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    logger.LogError("Password Reset Unsuccessful");
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                logger.LogError(ToString());
                throw;
            }
        }
    }
}
