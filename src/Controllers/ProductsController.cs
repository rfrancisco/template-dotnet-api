using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ProjectName.Api.Services;
using ProjectName.Api.Infrastructure;

namespace ProjectName.Api.Controllers
{
    [Route("/products")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService _svc;

        public ProductsController(IProductsService svc)
        {
            _svc = svc;
        }

        /// <summary>
        /// Get the list of products.
        /// </summary>
        /// <returns>The list of products found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the list of product found.</response>
        /// <response code="400">Bad request.</response>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<IEnumerable<ProductListModel>> GetMany()
        {
            return await _svc.GetMany();
        }

        /// <summary>
        /// Get the specified product.
        /// </summary>
        /// <param name="productId">The product unique identifier.</param>
        /// <returns>The product if found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the list of product found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpGet("{productId}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<ActionResult<ProductModel>> Get(int productId)
        {
            return await _svc.Get(productId);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="model">The product creation model.</param>
        /// <returns>The product that was created</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="201">Return the product created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<ProductModel> Create(ProductCreateModel model)
        {
            return await _svc.Create(model);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">The product unique identifier.</param>
        /// <param name="model">The product update model.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{productId}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task Update(int productId, ProductUpdateModel model)
        {
            await _svc.Update(productId, model);
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="productId">The product unique identifier.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpDelete("{productId}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task Delete(int productId)
        {
            await _svc.Delete(productId);
        }
    }
}
