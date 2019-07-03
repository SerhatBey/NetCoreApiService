using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetCoreApiService.Models;

namespace NetCoreApiService.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ILogger _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        // GET /products/
        [HttpGet]
        public IEnumerable<Products> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "LİSTİNG ALL PRODUCT");
            try
            {
                    using (var db = new WebApiContext())
                {
                    var list = db.Products.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END GET ALL


        // GET /products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "GET ITEM {ID}", id);
            try
            {
                using (var db = new WebApiContext())
                {
                    var item = await db.Products.FindAsync(id);
                    if (item == null)
                    {
                        _logger.LogWarning(LoggingEvents.GetItemNotFound, "GetById({ID}) NOT FOUND", id);
                        return NotFound();
                    }
                    _logger.LogInformation(LoggingEvents.GetItem, "GET ITEM PRODUCT {ID} Successful", id);
                    return item;
                 }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "GetById{ID} AN ERROR OCCURRED.");
                return null;
            }
        }// THE END GET 


        // POST /products/
        [HttpPost]
        public async Task<ActionResult<Users>> Post(Products item)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, "POST PRODUCT");
            try
            {
                using (var db = new WebApiContext())
                {
                    db.Products.Add(item);
                    await db.SaveChangesAsync();
                    _logger.LogInformation(LoggingEvents.InsertItem, "POST Successful");
                    return CreatedAtAction(nameof(Get), new { id = item.Product_ID }, item);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "AN ERROR OCCURRED.");
                return null;
            }
        }// THE END POST



        // PUT /products/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> Put(int id, Products item)
        {
            _logger.LogInformation(LoggingEvents.UpdateItem, "PUT PRODUCT");
            try
            {
                    if (id != item.Product_ID)
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



        // DELETE /products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> Delete(int id)
        {
            _logger.LogInformation(LoggingEvents.DeleteItem, "DELETE PRODUCT");
            try
            {
                using (var db = new WebApiContext())
                {
                    var item = await db.Products.FindAsync(id);

                    if (item == null)
                    {
                        _logger.LogInformation(LoggingEvents.GetItemNotFound, "DELETE NOT FOUND");
                        return NotFound();
                    }

                    db.Products.Remove(item);
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