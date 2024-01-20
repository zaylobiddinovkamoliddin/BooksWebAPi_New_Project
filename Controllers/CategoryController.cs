
using BooksWepAPiDtos.CategoryDtos;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BooksWebAPi_New_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("/WithBooks")]
        public async Task<IActionResult> GetWithbooks()
        {
            var categories = await _categoryService.GetWithCategories();
            var json = JsonConvert.SerializeObject(categories, Formatting.Indented,
                                    new JsonSerializerSettings { 
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,   
                                    });

            return Ok(json);
        }
         
        [HttpPost]
        public async Task<IActionResult> Post(AddCategoryDto addCategoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.AddCategoryAsync(addCategoryDto);
                return Ok();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(category);
                return Ok();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }

    }
}
