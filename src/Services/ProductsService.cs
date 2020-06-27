using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectRootNamespace.Api.DataAccess;
using ProjectRootNamespace.Api.DataAccess.Entities;
using ProjectRootNamespace.Api.Infrastructure;

namespace ProjectRootNamespace.Api.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductListDTO>> FindMany();
        Task<ProductDTO> Find(int id);
        Task<ProductDTO> Create(ProductCreateDTO model);
        Task Update(int id, ProductUpdateDTO model);
        Task Delete(int id);
    }

    public class ProductsService : BaseService<Product, int>, IProductsService
    {
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(
            MainDbContext dbContext,
            ILogger<ProductsService> logger) : base(dbContext)
        {
            _logger = logger;
        }

        public async Task<IEnumerable<ProductListDTO>> FindMany()
        {
            return await DbFindMany(x => new ProductListDTO(x));
        }

        public async Task<ProductDTO> Find(int id)
        {
            return new ProductDTO(await DbFind(x => x.Id == id));
        }

        public async Task<ProductDTO> Create(ProductCreateDTO model)
        {
            var dbRecord = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                Deleted = false,
            };

            return await Find((await DbCreate(dbRecord)).Id);
        }

        public async Task Update(int id, ProductUpdateDTO model)
        {
            var dbRecord = await DbFind(x => x.Id == id);

            dbRecord.Name = model.Name;
            dbRecord.Description = model.Description;
            dbRecord.Price = model.Price;
            dbRecord.Stock = model.Stock;

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

        public ProductDTO() { }

        public ProductDTO(Product source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            Price = source.Price;
            Stock = source.Stock;
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
