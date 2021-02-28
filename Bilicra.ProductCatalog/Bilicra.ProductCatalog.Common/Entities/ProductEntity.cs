using Bilicra.ProductCatalog.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bilicra.ProductCatalog.Common.Entities
{
    public class ProductEntity : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
#nullable enable
        public string? Photo { get; set; }
#nullable disable
        [Required]
        public decimal Price { get; set; }
        [Required]
        public CurrencyType Currency { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
