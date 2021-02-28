using Bilicra.ProductCatalog.Common.Models.ProductModels;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Bilicra.ProductCatalog.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductModel> GetByIdAsync(Guid id);
        Task<List<ProductModel>> GetAllAsync();
        Task<List<ProductModel>> GetAllAsync(string name);
        Task<ProductModel> CreateAsync(ProductPostModel product);
        Task<ProductModel> UpdateAsync(ProductPutModel product);
        Task<ProductModel> DeleteAsync(Guid id);
        Task<IWorkbook> ExportExcelAsync();
        Task<ProductModel> PriceConfirmPatchAsync(Guid id);
        Task<ProductModel> UploadPhotoAsync(Guid productId, ProductPostPhotoModel photo);
    }
}
