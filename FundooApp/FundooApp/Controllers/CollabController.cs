using BussinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public CollabController(ICollabBL collabBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.collabBL = collabBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult AddCollab(CollabModel collabModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var collab = fundooContext.NotesTable.Where(r => r.NoteID == collabModel.NoteID).FirstOrDefault();
                if (collab.UserId == userId)
                {
                    var result = collabBL.AddCollab(collabModel);
                    if (result != null)
                    {
                        return Ok(new { Success = true, message = "Collaboration Successfull", data = result });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, message = "Collaboration Unsuccessfull" });
                    }
                }
                else
                {
                    return Unauthorized(new { Success = false, message = "Failed Collaboration" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("Remove")]
        public IActionResult RemoveCollab(long collabID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = collabBL.RemoveCollab(collabID, userId);
                if (delete != null)
                {
                    return Ok(new { Success = true, message = "Collaboration Removed Successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Collaboration  Remove Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult GetCollab(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var notes = collabBL.GetCollab(noteId, userId);
                if (notes != null)
                {
                    return Ok(new { Success = true, message = "Collaborations Found Successfully", data = notes });

                }
                else
                {
                    return BadRequest(new { Success = false, message = "No Collaborations  Found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
            var cacheKey = "CollaboratorList";
            string serializedCollaboratorList;
            var CollaboratorList = new List<CollaboratorEntity>();
            var redisCollaboratorList = await distributedCache.GetAsync(cacheKey);
            if (redisCollaboratorList != null)
            {
                serializedCollaboratorList = Encoding.UTF8.GetString(redisCollaboratorList);
                CollaboratorList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollaboratorList);
            }
            else
            {
                CollaboratorList = fundooContext.CollaboratorTable.ToList();
                serializedCollaboratorList = JsonConvert.SerializeObject(CollaboratorList);
                redisCollaboratorList = Encoding.UTF8.GetBytes(serializedCollaboratorList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollaboratorList, options);
            }
            return Ok(CollaboratorList);
        }
    }
}
