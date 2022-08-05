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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;

        public LabelController(ILabelBL labelBL, FundooContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.labelBL = labelBL;
            this.fundooContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var labelNote = fundooContext.NotesTable.Where(r => r.NoteID == labelModel.NoteID).FirstOrDefault();
                if (labelNote.UserId == userid)
                {
                    var result = labelBL.CreateLabel(labelModel);
                    if (result != null)
                    {
                        return Ok(new { Success = true, Message = "Label created successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { Success = false, Message = "Label not created" });
                    }
                }
                else
                {
                    return Unauthorized(new { Success = false, Message = "Unauthorized User!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = labelBL.UpdateLabel(labelModel, labelID);
                if (result != null)
                {
                    return Ok(new { Success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return NotFound(new { Success = false, message = "Label Not Updated" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel(long labelID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var delete = labelBL.DeleteLabel(labelID, userId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Label Deleted Successfully" });
                }
                else
                {
                    return this.NotFound(new { Success = false, message = "Label not Deleted" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult GetAllLabels()
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var labels = labelBL.GetLabels(userid);
                if (labels != null)
                {
                    return this.Ok(new { Success = true, Message = " All labels found Successfully", data = labels });
                }
                else
                {
                    return this.NotFound(new { Success = false, Message = "No label found" });
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
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
            var cacheKey = "CollaboratorList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = fundooContext.LabelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
