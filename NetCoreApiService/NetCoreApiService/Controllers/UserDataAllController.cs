using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreApiService.Models;

namespace NetCoreApiService.Controllers
{
    [Route("user/data/all")]
    [ApiController]
    public class UserDataAllController : ControllerBase
    {
        private readonly ILogger _logger;

        public UserDataAllController(ILogger<UserDataAllController> logger)
        {
            _logger = logger;
        }

        // GET /user/data/all
        [HttpGet]
        public async Task<ActionResult<Users>> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "LİSTİNG ALL USERS DATA");
            try
            {
                using (var db = new WebApiContext())
                {
                    var list = (from t1 in db.Users
 
                                join detail in db.Products on t1.User_ID equals detail.Product_ID into Details
                                from m in Details.DefaultIfEmpty()
                                select new { t1.Username, t1.Products }).ToList();

                    _logger.LogInformation(LoggingEvents.ListItems, "GET LİST DATA Successful");
                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END GET ALL


        // GET /user/data/all/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id)
        {

            _logger.LogInformation(LoggingEvents.GetItem, "GET USER DATA ITEMS {ID}", id);
            try
            {
                using (var db = new WebApiContext())
                {
                    var list = (from t1 in db.Users
   
                                join detail in db.Products on t1.User_ID equals detail.Product_ID into Details
                                from m in Details.DefaultIfEmpty()
                                where(t1.User_ID==id)
                                select new { t1.Username, t1.Products }).ToList();

                    _logger.LogInformation(LoggingEvents.GetItem, "GET USER DATA ITEM {ID} Successful", id);
                    return Ok(list);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "GetById{ID} AN ERROR OCCURRED.");
                return null;
            }
        }// THE END GET


    }
}