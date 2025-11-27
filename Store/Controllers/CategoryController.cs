using Microsoft.AspNetCore.Mvc;
using Store.Entities;

namespace Store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {

        [HttpGet(Name = "Categories")]
        public IEnumerable<Category> Get()
        {
            return new List<Category>()
            {
                new Category { Id = 1, Name = "Shoes"},
                new Category { Id = 2, Name = "Clothes"},
                new Category { Id = 3, Name = "Shorts"}
            };
        }
    }
}