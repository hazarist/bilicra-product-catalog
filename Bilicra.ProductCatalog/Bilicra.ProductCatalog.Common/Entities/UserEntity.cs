using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bilicra.ProductCatalog.Common.Entities
{
    public class UserEntity : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Salt { get; set; }
    
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }

#nullable enable
        public string? PhoneNumber { get; set; }
#nullable disable

        [NotMapped]
        public string AccessToken { get; set; }
    }
}
