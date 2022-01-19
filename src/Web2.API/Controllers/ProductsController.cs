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

        /// <summary>
        /// Premet l'obtention de la liste de tous les produits en inventaire chez RenaudMarché
        /// </summary>
        /// <remarks>Pas de remarques</remarks>  
        /// <response code="200">Liste complète des produits de RenaudMarché est trouvée et retournée</response>
        /// <response code="404">Liste complète des produits de RenaudMarché est introuvable</response>
        // GET: api/<ProductsController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {

            var produits = products;

            if (produits == null || produits.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(produits);
            }
        }

        /// <summary>
        /// Premet l'obtention d'un produit via son Id unique
        /// </summary>
        /// <remarks>Pas de remarques</remarks>  
        /// <response code="200">Le produit est trouvé et retourné</response>
        /// <response code="404">Le produit est introuvable</response>
        // GET: api/<ProductsController>/3
        [HttpGet("{id:int}")]
        public ActionResult<Product> Get(int id)
        {
            var product = products.FirstOrDefault(_ => _.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        /// <summary>
        /// Permet l'ajout d'un nouveau produit à la liste
        /// </summary>
        /// <remarks>Pas de remarques</remarks>  
        /// <response code="201">Le nouveau produit a été ajouté à la liste</response>
        /// <response code="400">Le produit à ajouter est invalide</response>
        // POST: api/<ProductsController>/2
        [HttpPost]
        public ActionResult<Product> Post([FromBody] Object productDto)
        {
            if(productDto as Product == null)
            {
                return BadRequest("Le produit à ajouter est invalide");
            }
            var prod = productDto as Product;
            products.Add(prod);
            return CreatedAtAction(nameof(Post), new { id = prod.Id }, prod);
        }

        /// <summary>
        /// Permet la modification d'un produit de la liste
        /// </summary>
        /// <remarks>Pas de remarques</remarks>  
        /// <response code="201">Le produit a été modifié avec succès</response>
        /// <response code="400">Le produit à modifier est invalide ou introuvable</response>
        // PUT: api/<ProductsController>/12
        [HttpPut]
        public ActionResult<Product> Put([FromBody] Object product)
        {
            var prod = product as Product;
            if(prod == null)
            {
                return BadRequest("Le produit à ajouter est invalide");
            }
            var productToModify = products.FirstOrDefault(_ => _.Id == prod.Id);
            if (productToModify != null)
            {
                return BadRequest("Le produit à modifier n'existe pas");
            }
            else
            {
                products.RemoveAll(_ => _.Id == prod.Id);
                products.Add(prod);
                return Ok(prod);
            }
        }

        /// <summary>
        /// Premet la suppression d'un produit
        /// </summary>
        /// <remarks>Pas de remarques</remarks>  
        /// <response code="200">Le produit a été supprimé avec succès</response>
        /// <response code="404">Le produit à supprimer est introuvable</response>
        // DELETE: api/<ProductsController>
        [HttpDelete("{id:int}")]
        public ActionResult<Product> Delete(int id)
        {
            var product = products.FirstOrDefault(_ => _.Id == id);
            if (product == null)
            {
                return NotFound("Le produit à supprimer n'existe pas.");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
