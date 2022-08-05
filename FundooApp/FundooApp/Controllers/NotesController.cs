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
    [Authorize]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL notesBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = notesBL.CreateNote(userId, notesModel);
                if (result != null)
                {
                    return Ok(new { sucess = true, message = "Note Created Successful", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Note Created Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNotes(NotesModel notesModel, long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
                var result = notesBL.UpdateNote(notesModel, NoteId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Notes Found" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
                var delete = notesBL.DeleteNotes(NoteId);
                if (delete != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes Delete Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Retrieve")]
        public IActionResult ReadNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
                var result = notesBL.ReadNotes(userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes Update Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Pin")]
        public IActionResult Pinned(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = notesBL.Pinned(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note Unpinned successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Trash")]
        public IActionResult Trashed(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = notesBL.Trashed(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Trash Successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note UnTrash successfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archieve(long NoteID)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var result = notesBL.Archieve(NoteID, userID);
                if (result == true)
                {
                    return Ok(new { success = true, message = "Note Archive Successfully" });
                }
                else if (result == false)
                {
                    return Ok(new { success = true, message = "Note Archive Unsuccessfully." });
                }
                return BadRequest(new { success = false, message = "Cannot perform operation." });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Color")]
        public IActionResult ColourNote(long NoteId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(r => r.Type == "userID").Value);
                var colors = notesBL.ColourNote(NoteId, color);
                if (colors != null)
                {
                    return Ok(new { Success = true, message = "Added Colour Successfully", data = colors });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Added Colour Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Image")]
        public IActionResult Imaged(long noteId, IFormFile image)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = notesBL.Imaged(noteId, userID, image);
                if (result != null)
                {
                    return Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessfully", Data = result });
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
            long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "userID").Value);
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}
