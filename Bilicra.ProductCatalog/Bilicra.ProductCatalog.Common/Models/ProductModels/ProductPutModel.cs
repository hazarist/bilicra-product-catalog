using Bilicra.ProductCatalog.Common.Enums;
using System;

namespace Bilicra.ProductCatalog.Common.Models.ProductModels
{
    public class ProductPutModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public CurrencyType Currency { get; set; }
    }
}
