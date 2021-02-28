using Microsoft.AspNetCore.Http;

namespace Bilicra.ProductCatalog.Common.Models.ProductModels
{
    public class ProductPostPhotoModel
    {
        public IFormFile Photo { get; set; }
    }
}
