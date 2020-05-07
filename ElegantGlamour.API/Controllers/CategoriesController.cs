using System.Threading.Tasks;
using ElegantGlamour.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElegantGlamour.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;

        }

        [HttpGet("")]
        public ActionResult GetCategories()
        {
            return NoContent();
        }
    }
}