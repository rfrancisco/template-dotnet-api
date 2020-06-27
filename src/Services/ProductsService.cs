using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ProjectRootNamespace.Api.DataAccess;
using ProjectRootNamespace.Api.DataAccess.Entities;
using ProjectRootNamespace.Api.Infrastructure;

namespace ProjectRootNamespace.Api.Services
{
    public interface IProductsService
    {
        Task<ProductDTO> Find(int id);
        Task<PagedResultsDTO<ProductListDTO>> FindMany(int page = 0, int pageSize = 0);
        Task<ProductDTO> Create(ProductCreateDTO model);
        Task Update(int id, ProductUpdateDTO model);
        Task Delete(int id);
    }

    public class ProductsService : BaseService<Product, int>, IProductsService
    {
        public ProductsService(MainDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProductDTO> Find(int id)
        {
            return new ProductDTO(await DbFind(x => x.Id == id));
        }

        public async Task<PagedResultsDTO<ProductListDTO>> FindMany(int page = 0, int pageSize = 0)
        {
            return await DbFindMany(x => new ProductListDTO(x), page, pageSize);
        }

        public async Task<ProductDTO> Create(ProductCreateDTO model)
        {
            if (await DbExists(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim()))
                throw new ApiValidationException("DUPLICATE_NAME", "A record with the same name already exists");

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
            if (await DbExists(x => x.Name.ToLower().Trim() == model.Name.ToLower().Trim() && x.Id != id))
                throw new ApiValidationException("DUPLICATE_NAME", "A record with the same name already exists");

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
