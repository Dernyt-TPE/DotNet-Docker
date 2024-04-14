using Microsoft.AspNetCore.Mvc;
using StoreApp.Models;


namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product() { Id = 1, Name = "Shirt", Price = 499 },
            new Product() { Id = 2, Name = "Shoe", Price = 1200}
        };


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }


        // POST api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product item)
        {
            int nextId = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

            if (item == null)
            {
                return BadRequest("Invalid Request");
            }

            item.Id = nextId;

            _products.Add(item);
            return Ok("Product Added Successfully");
        }



        // PUT api/products/5
        [HttpPut("{id}")]
        //[HttpPut("UpdateDetails/{id}")]

        public IActionResult Update(int id, [FromBody] Product updateProduct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Product existingProduct = _products.FirstOrDefault(x => x.Id == id);

            if (existingProduct == null) return BadRequest("Failed to Update - Product Not Found");

            existingProduct.Name = updateProduct.Name;
            existingProduct.Price = updateProduct.Price;

            return Ok(existingProduct);
        }

        //DELETE api/products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var productToRemove = _products.FirstOrDefault(x => x.Id == id);
            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
                return Ok("Product Removed Successfully");
            }
            return NotFound("Product not Found");
        }
    }
}
