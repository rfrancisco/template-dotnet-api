using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProjectName.Api.DataAccess;
using ProjectName.Api.DataAccess.Entities;
using ProjectName.Api.Infrastructure;

namespace ProjectName.Api.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductListModel>> GetMany();
        Task<ProductModel> Get(int productId);
        Task<ProductModel> Create(ProductCreateModel model);
        Task Update(int productId, ProductUpdateModel model);
        Task Delete(int productId);
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

        public async Task<IEnumerable<ProductListModel>> GetMany()
        {
            return await DbGetMany(x => new ProductListModel(x));
        }

        public async Task<ProductModel> Get(int productId)
        {
            return new ProductModel(await DbGet(x => x.Id == productId));
        }

        public async Task<ProductModel> Create(ProductCreateModel model)
        {
            var dbRecord = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Stock = model.Stock,
                Deleted = false,
            };

            return await Get((await DbCreate(dbRecord)).Id);
        }

        public async Task Update(int productId, ProductUpdateModel model)
        {
            var dbRecord = await DbGet(x => x.Id == productId);

            dbRecord.Name = model.Name;
            dbRecord.Description = model.Description;
            dbRecord.Price = model.Price;
            dbRecord.Stock = model.Stock;

            await DbUpdate(dbRecord);
        }

        public async Task Delete(int productId)
        {
            await DbDelete(x => x.Id == productId);
        }
    }

    #region Models

    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        public ProductModel() { }

        public ProductModel(Product source)
        {
            Id = source.Id;
            Name = source.Name;
            Description = source.Description;
            Price = source.Price;
            Stock = source.Stock;
        }
    }

    public class ProductListModel : ProductModel
    {
        public ProductListModel(Product source) : base(source)
        {
        }
    }

    public class ProductCreateModel
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

    public class ProductUpdateModel
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
