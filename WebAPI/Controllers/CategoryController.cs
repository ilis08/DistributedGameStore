﻿using ApplicationService.Contracts;
using ApplicationService.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Controller to work with Categories
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : HomeController
    {
        private readonly ICategoryManagementService categoryService;
        private readonly ILogger<CategoryController> logger;

        public CategoryController(ICategoryManagementService _service, ILogger<CategoryController> _logger)
        {
            categoryService = _service;
            logger = _logger;
        }


        /// <summary>
        /// Returns all Categories if query==null, or if query!=null return Categories which contains 'query' 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("[action]")]
        public async Task<IActionResult> Get(string query)
        {
            var result = await categoryService.GetAsync(query);

            if (result.Any())
            {
                logger.Log(LogLevel.Information, "Succesfully getting list of categories");
                return Ok(result);
            }
            else
            {
                logger.Log(LogLevel.Error, "Cannot load a categories");
                return NoContent();
            }
        }

        [HttpGet("[action]/{id:int}", Name = "CategoryById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await categoryService.GetByIdAsync(id);

            logger.Log(LogLevel.Information, "Succesfully getting a category");
            return Ok(result);
        }

        [Authorize("Admin, Market")]
        [Route("[action]")]
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Save([FromForm]CategoryDTO category)
        {
            var categoryToReturn = await categoryService.SaveAsync(category);

            return CreatedAtRoute("CategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [Authorize("Admin, Market")]
        [Route("[action]")]
        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Update([FromBody] CategoryDTO category)
        {
            var categoryToReturn = await categoryService.UpdateAsync(category);

            return CreatedAtRoute("CategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [Authorize("Admin, Market")]
        [Route("[action]/{id:int}")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.DeleteAsync(id);

            logger.Log(LogLevel.Information,"Category was deleted");

            return Ok("Category was deleted succesfully");
        }     
    }
}
