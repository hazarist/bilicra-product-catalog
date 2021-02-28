using System;

namespace Bilicra.ProductCatalog.Common.Models.ProductModels
{
    public class ProductPriceConfirmPatchModel
    {
        public Guid Id { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
