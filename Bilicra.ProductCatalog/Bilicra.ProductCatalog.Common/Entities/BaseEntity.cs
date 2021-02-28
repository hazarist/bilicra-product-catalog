using System;

namespace Bilicra.ProductCatalog.Common.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
#nullable enable
        public DateTime? LastUpdatedDate { get; set; }
#nullable disable
    }
}
