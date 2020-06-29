using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectRootNamespace.Api.DataAccess;
using ProjectRootNamespace.Api.DataAccess.Entities;
using ProjectRootNamespace.Api.Infrastructure;
using ProjectRootNamespace.Api.Infrastructure.Exceptions;

namespace ProjectRootNamespace.Api.Services
{
    public interface IProductsService
    {
        Task<ProductDTO> Find(int id);
        Task<PagedResultsDTO<ProductListDTO>> FindMany(string searchTerm = "", int page = 0, int pageSize = 0);
        Task<ProductDTO> Create(ProductCreateDTO dto);
        Task Update(int id, ProductUpdateDTO dto);
        Task Delete(int id);
    }

    public class ProductsService : BaseService<Product, int>, IProductsService
    {
        public ProductsService(
            MainDbContext dbContext,
            IHttpContextAccessor httpContext) : base(dbContext, httpContext) { }

        public async Task<ProductDTO> Find(int id)
        {
            return new ProductDTO(await DbFind(x => x.Id == id));
        }

        public async Task<PagedResultsDTO<ProductListDTO>> FindMany(string searchTerm = "", int page = 0, int pageSize = 0)
        {
            return await DbFindMany(
                w => string.IsNullOrEmpty(searchTerm) || w.Name.ToLower().Trim().Contains(searchTerm.ToLower().Trim()),
                x => new ProductListDTO(x),
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
                Description = dto.Description.Trim(),
                Price = dto.Price,
                Stock = dto.Stock
            };

            return await Find((await DbCreate(dbRecord)).Id);
        }

        public async Task Update(int id, ProductUpdateDTO dto)
        {
            if (await DbExists(x => x.Name.ToLower().Trim() == dto.Name.ToLower().Trim() && x.Id != id))
                throw new ApiValidationException("DUPLICATE_NAME", "A record with the same name already exists");

            var dbRecord = await DbFind(x => x.Id == id);
            dbRecord.Name = dto.Name.Trim();
            dbRecord.Description = dto.Description.Trim();
            dbRecord.Price = dto.Price;
            dbRecord.Stock = dto.Stock;

            await DbUpdate(dbRecord);
        }

        public async Task Delete(int id)
        {
            await DbDelete(x => x.Id == id);
        }
    }

    #region DTOs

    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
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
            Price = source.Price;
            Stock = source.Stock;
            CreatedBy = source.CreatedBy;
            CreatedOn = source.CreatedOn;
            UpdatedBy = source.UpdatedBy;
            UpdatedOn = source.UpdatedOn;
        }
    }

    public class ProductListDTO : ProductDTO
    {
        public ProductListDTO(Product source) : base(source)
        {
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

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }

    public class ProductUpdateDTO
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }

    #endregion
}
