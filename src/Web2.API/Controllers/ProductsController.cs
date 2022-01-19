using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web2.API.Models;

namespace Web2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private List<Product> products = new List<Product>()
        {
            new Product(){Id = 1, Cost = 9.99, Name = "Jambon 1kg", Quantity = 132}
        };
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {

            var produits = products;

            if (produits == null || produits.Count() == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(produits);
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.FirstOrDefault(_ => _.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody] Product product)
        {

            products.Add(product);
            return CreatedAtAction(nameof(Post), new { id = product.Id }, product);
        }

        [HttpPut]
        public ActionResult<Product> Put([FromBody] Product product)
        {

            var productToModify = products.FirstOrDefault(_ => _.Id == product.Id);
            if (productToModify != null)
            {
                return BadRequest("Le produit à modifier n'existe pas");
            }
            else
            {
                products.RemoveAll(_ => _.Id == product.Id);
                products.Add(product);
                return Ok(product);
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = products.FirstOrDefault(_ => _.Id == id);
            if (product == null)
            {
                return BadRequest("Le produit à supprimer n'existe pas.");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
