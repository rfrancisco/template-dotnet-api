using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using projectRootNamespace.Api.DataAccess;
using projectRootNamespace.Api.DataAccess.Entities;
using projectRootNamespace.Api.Infrastructure;
using projectRootNamespace.Api.Infrastructure.Exceptions;

namespace projectRootNamespace.Api.Services
{
    public interface IProductsService
    {
        Task<ProductDTO> Find(int productId);
        Task<PagedResultsDTO<ProductListDTO>> FindMany(string searchTerm = "", int page = 0, int pageSize = 0);
        Task<ProductDTO> Create(ProductCreateDTO dto);
        Task Update(int productId, ProductUpdateDTO dto);
        Task Delete(int productId);
    }

    public class ProductsService : BaseService<Product, int>, IProductsService
    {
        public ProductsService(
            MainDbContext dbContext,
            IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
            _baseQuery = _baseQuery.Include(x => x.Skus);
        }

        public async Task<ProductDTO> Find(int productId)
        {
            return new ProductDTO(await DbFind(x => x.Id == productId));
        }

        public async Task<PagedResultsDTO<ProductListDTO>> FindMany(string searchTerm = "", int page = 0, int pageSize = 0)
        {
            return await DbFindMany(
                w => string.IsNullOrEmpty(searchTerm) || w.Name.ToLower().Trim().Contains(searchTerm.ToLower().Trim()),
                x => new ProductListDTO(x, x.Skus.Count),
                page,
                pageSize);
        }

        public async Task<ProductDTO> Create(ProductCreateDTO dto)
        {
            if (await DbExists(x => x.Name.ToLower().Trim() == dto.Name.ToLower().Trim()))
                throw new ApiValidationException("DUPLICATE_NAME", "A record with the same name already exists");

            var dbRecord = new Product()
            {
                Name = dto.Name.Trim(),
                Description = dto.Description.Trim()
            };

            return await Find((await DbCreate(dbRecord)).Id);
        }

        public async Task Update(int productId, ProductUpdateDTO dto)
        {
            if (await DbExists(x => x.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && x.Id != productId))
                throw new ApiValidationException("DUPLICATE_NAME", "A record with the same name already exists");

            var dbRecord = await DbFind(x => x.Id == productId);
            dbRecord.Name = dto.Name.Trim();
            dbRecord.Description = dto.Description.Trim();

            await DbUpdate(dbRecord);
        }

        public async Task Delete(int productId)
        {
            await DbDelete(x => x.Id == productId);
        }
    }

    #region DTOs

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public ProductDTO() { }

        public ProductDTO(Product source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            CreatedBy = source.CreatedBy;
            CreatedOn = source.CreatedOn;
            UpdatedBy = source.UpdatedBy;
            UpdatedOn = source.UpdatedOn;
        }
    }

    public class ProductListDTO : ProductDTO
    {
        public int NumOfSkus { get; set; }

        public ProductListDTO(Product source, int numOfSkus) : base(source)
        {
            NumOfSkus = numOfSkus;
        }
    }

    public class ProductCreateDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
    }

    public class ProductUpdateDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }
    }

    #endregion
}
