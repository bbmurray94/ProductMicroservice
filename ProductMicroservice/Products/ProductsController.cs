using Microsoft.AspNetCore.Mvc;

namespace ProductMicroservice.Products
{
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public ProductsController() { }


        [HttpGet]
        public async Task<ActionResult> Get() 
        {
            return Ok("Get list works");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id) 
        {
            return Ok("Get by id works");
        }
    }
}
