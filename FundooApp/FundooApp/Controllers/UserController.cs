using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var result = userBL.Registration(userRegistrationModel);

                if(result != null)
                {
                    return Ok(new {success = true, message = "Registration Successfull", data = result});
                }
                else
                {
                    return BadRequest(new {success = false, message = "Registration Unsuccessfull" });
                }
            }
            catch(System.Exception)
            {
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
                    return Ok(new { success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login Unsuccessfull" });
                }
            }
            catch(System.Exception)
            {
                throw;
            }
        }
    }
}
