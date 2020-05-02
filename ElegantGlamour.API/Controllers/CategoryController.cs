using System.Threading.Tasks;
using ElegantGlamour.API.Dtos;
using ElegantGlamour.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElegantGlamour.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;

        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto category)
        {
            return Ok(await _categoryService.CreateCategory(category));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto category)
        {
            category.Id = id;
            return Ok(await _categoryService.UpdateCategory(category));
        }
    }
}