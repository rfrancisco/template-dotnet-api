using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectRootNamespace.Api.Services;
using projectRootNamespace.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace projectRootNamespace.Api.Controllers
{
    [Route("/products/{productId}/skus")]
    [SwaggerTag("Products")]
    public class SkusController : BaseController
    {
        private readonly ISkusService _svc;

        public SkusController(ISkusService svc) =>
            _svc = svc;

        /// <summary>
        /// Get the list of skus.
        /// </summary>
        /// <param name="productId">The id of the parent the records belong to.</param>
        /// <param name="searchTerm">(Optional) Get records that match the specified value.</param>
        /// <param name="page">(Optional) The page of records to retrieve.</param>
        /// <param name="pageSize">(Optional) The number of records per page to retrieve.</param>
        /// <returns>The list of records found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the list of records found.</response>
        /// <response code="400">Bad request.</response>
        [HttpGet]
        public Task<PagedResultsDTO<SkuListDTO>> FindMany(int productId, [FromQuery] string searchTerm, [FromQuery] int page = 0, [FromQuery] int pageSize = 0) =>
             _svc.FindMany(productId, searchTerm, page, pageSize);

        /// <summary>
        /// Get the specified sku.
        /// </summary>
        /// <param name="productId">The id of the parent the record belons to.</param>
        /// <param name="skuId">The record unique identifier.</param>
        /// <returns>The record if found.</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="200">Return the record found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpGet("{skuId}")]
        public Task<SkuDTO> Find(int productId, int skuId) =>
             _svc.Find(productId, skuId);

        /// <summary>
        /// Create a new sku.
        /// </summary>
        /// <param name="productId">The id of the parent the record will belong to.</param>
        /// <param name="dto">The record creation dto.</param>
        /// <returns>The record that was created</returns>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="201">Return the record created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        public Task<SkuDTO> Create(int productId, SkuCreateDTO dto) =>
             _svc.Create(productId, dto);

        /// <summary>
        /// Updates an existing sku.
        /// </summary>
        /// <param name="productId">The id of the parent the record belons to.</param>
        /// <param name="skuId">The record unique identifier.</param>
        /// <param name="dto">The record update dto.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{skuId}")]
        public Task Update(int productId, int skuId, SkuUpdateDTO dto) =>
             _svc.Update(productId, skuId, dto);

        /// <summary>
        /// Deletes an existing sku.
        /// </summary>
        /// <param name="productId">The id of the parent the record belons to.</param>
        /// <param name="skuId">The record unique identifier.</param>
        /// <response code="401">Request is not unauthorized.</response>
        /// <response code="403">Operation is forbidden.</response>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpDelete("{skuId}")]
        public Task Delete(int productId, int skuId) =>
             _svc.Delete(productId, skuId);
    }
}
