using Bilicra.ProductCatalog.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bilicra.ProductCatalog.Common.Models.ProductModels
{
    public class ProductPostModel
    {
        [Required(ErrorMessage = "Please enter product code")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please enter product Name")]
        public string Name { get; set; }
        public string Photo { get; set; }
        [Required(ErrorMessage = "Please enter product price")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter product currency")]
        public CurrencyType Currency { get; set; }
    }
}
