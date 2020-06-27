using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ProjectRootNamespace.Api.Services;
using ProjectRootNamespace.Api.Infrastructure;

namespace ProjectRootNamespace.Api.Controllers
{
    [Route("/products")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService _svc;

        public ProductsController(IProductsService svc) =>
            _svc = svc;

        /// <summary>
        /// Get the list of products.
        /// </summary>
        /// <param name="page">(Optional) The page of records to retrieve.</param>
        /// <param name="pageSize">(Optional) The number of records per page to retrieve.</param>
        /// <returns>The list of records found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the list of records found.</response>
        /// <response code="400">Bad request.</response>
        [HttpGet]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<PagedResultsDTO<ProductListDTO>> FindMany([FromQuery] int page = 0, [FromQuery] int pageSize = 0) =>
            await _svc.FindMany(page, pageSize);

        /// <summary>
        /// Get the specified product.
        /// </summary>
        /// <param name="id">The record unique identifier.</param>
        /// <returns>The record if found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the record found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<ActionResult<ProductDTO>> Find(int id) =>
            await _svc.Find(id);

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="model">The record creation model.</param>
        /// <returns>The record that was created</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="201">Return the record created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task<ProductDTO> Create(ProductCreateDTO model) =>
            await _svc.Create(model);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The record unique identifier.</param>
        /// <param name="model">The record update model.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task Update(int id, ProductUpdateDTO model) =>
            await _svc.Update(id, model);

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="id">The record unique identifier.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "Products" })]
        public async Task Delete(int id) =>
            await _svc.Delete(id);
    }
}
