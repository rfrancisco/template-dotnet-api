using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using projectRootNamespace.Api.DataAccess;
using projectRootNamespace.Api.DataAccess.Entities;
using projectRootNamespace.Api.Infrastructure;

namespace projectRootNamespace.Api.Services
{
    public interface ISkusService
    {
        Task<SkuDTO> Find(int productId, int skuId);
        Task<PagedResultsDTO<SkuListDTO>> FindMany(int productId, string searchTerm = "", int page = 0, int pageSize = 0);
        Task<SkuDTO> Create(int productId, SkuCreateDTO dto);
        Task Update(int productId, int skuId, SkuUpdateDTO dto);
        Task Delete(int productId, int skuId);
    }

    public class SkusService : BaseService<Sku, int>, ISkusService
    {
        public SkusService(
            MainDbContext dbContext,
            IHttpContextAccessor httpContext) : base(dbContext, httpContext) { }

        public async Task<SkuDTO> Find(int productId, int skuId)
        {
            return new SkuDTO(await DbFind(x => x.Id == skuId && x.ProductId == productId));
        }

        public async Task<PagedResultsDTO<SkuListDTO>> FindMany(int productId, string searchTerm = "", int page = 0, int pageSize = 0)
        {
            return await DbFindMany(
                w => w.ProductId == productId && (string.IsNullOrEmpty(searchTerm) || w.Model.ToLower().Trim().Contains(searchTerm.ToLower().Trim())),
                x => new SkuListDTO(x),
                page,
                pageSize);
        }

        public async Task<SkuDTO> Create(int productId, SkuCreateDTO dto)
        {
            var dbRecord = new Sku()
            {
                ProductId = productId,
                Model = dto.Model.Trim(),
                Size = dto.Size,
                Price = dto.Price,
                Stock = dto.Stock
            };

            return await Find(productId, (await DbCreate(dbRecord)).Id);
        }

        public async Task Update(int productId, int skuId, SkuUpdateDTO dto)
        {
            var dbRecord = await DbFind(x => x.Id == skuId && x.ProductId == productId);
            dbRecord.Model = dto.Model.Trim();
            dbRecord.Size = dto.Size;
            dbRecord.Price = dto.Price;
            dbRecord.Stock = dto.Stock;

            await DbUpdate(dbRecord);
        }

        public async Task Delete(int productId, int skuId)
        {
            await DbDelete(x => x.Id == skuId && x.ProductId == productId);
        }
    }

    #region DTOs

    public class SkuDTO
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public SkuSize Size { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public int ProductId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

        public SkuDTO() { }

        public SkuDTO(Sku source)
        {
            Id = source.Id;
            Model = source.Model;
            Size = source.Size;
            Price = source.Price;
            Stock = source.Stock;
            ProductId = source.ProductId;
            CreatedBy = source.CreatedBy;
            CreatedOn = source.CreatedOn;
            UpdatedBy = source.UpdatedBy;
            UpdatedOn = source.UpdatedOn;
        }
    }

    public class SkuListDTO : SkuDTO
    {
        public SkuListDTO(Sku source) : base(source)
        {
        }
    }

    public class SkuCreateDTO
    {
        [Required]
        [MaxLength(64)]
        public string Model { get; set; }

        [Required]
        public SkuSize Size { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }

    public class SkuUpdateDTO
    {
        [Required]
        [MaxLength(64)]
        public string Model { get; set; }

        [Required]
        public SkuSize Size { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }

    #endregion
}
