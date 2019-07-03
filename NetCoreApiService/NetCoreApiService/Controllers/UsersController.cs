using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NetCoreApiService.Models;

namespace NetCoreApiService.Controllers
{
    [Route("users/")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILogger _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        // GET /users/
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "LİSTİNG ALL USERS");
            try
            {
                using (var db = new WebApiContext())
                {
                    var list = db.Users.ToList();
                    _logger.LogInformation(LoggingEvents.ListItems, "GET LİST Successful");
                    return list;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }

        }// THE END GET ALL


        // GET /users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id)
        {

            _logger.LogInformation(LoggingEvents.GetItem, "GET ITEM {ID}", id);
            try
            {
                using (var db = new WebApiContext())
                {
                    var item = await db.Users.FindAsync(id);
                    if (item == null)
                    {
                        _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                        return NotFound();
                    }
                    _logger.LogInformation(LoggingEvents.GetItem, "GET ITEM {ID} Successful",id);
                    return item;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "GetById{ID} AN ERROR OCCURRED.");
                return null;
            }


        }// THE END GET


        // POST /users/
        [HttpPost]
        public async Task<ActionResult<Users>> Post(Users item)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "POST USER");
            try
            {
                using (var db = new WebApiContext())
                {
                    db.Users.Add(item);
                    await db.SaveChangesAsync();
                    _logger.LogInformation(LoggingEvents.InsertItem, "POST Successful");
                    return CreatedAtAction(nameof(Get), new { id = item.User_ID }, item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END POST



        // PUT /users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> Put(int id, Users item)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "PUT USER");
            try
            {
                if (id != item.User_ID)
                {

                    return BadRequest();
                }
    
                using (var db = new WebApiContext())
                {
                    db.Entry(item).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    _logger.LogInformation(LoggingEvents.UpdateItem, "PUT Successful");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END PUT



        // DELETE /users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteItem, "DELETE USER");
            try
            {
                  using (var db = new WebApiContext())
                    {
                        var item = await db.Users.FindAsync(id);

                        if (item == null)
                        {
                        _logger.LogInformation(LoggingEvents.GetItemNotFound, "DELETE NOT FOUND");
                        return NotFound();
                        }

                        db.Users.Remove(item);
                        await db.SaveChangesAsync();
                    _logger.LogInformation(LoggingEvents.DeleteItem, "DELETE Successful");
                    return NoContent();
                  }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END DELETE



    }
}
