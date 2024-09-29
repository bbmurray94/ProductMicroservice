using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace ProductMicroservice.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            IEnumerable<Product> products = _productRepository.GetProducts();
            return new OkObjectResult(products);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id) 
        {
            Product product = _productRepository.GetProductById(id);
            return new OkObjectResult(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product) 
        {
            using (TransactionScope scope = new TransactionScope()) 
            {
                _productRepository.InsertProduct(product);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = product.Id}, product);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product) 
        {
            if (product == null) 
            {
                return NoContent();
            }

            using (TransactionScope scope = new TransactionScope())
            {
                _productRepository.UpdateProduct(product);
                scope.Complete();
                return new OkResult();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            _productRepository.DeleteProduct(id);
            return new OkResult();
        }
    }
}
