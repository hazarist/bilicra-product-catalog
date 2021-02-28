using System;

namespace Bilicra.ProductCatalog.Common.Models.UserModels
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
    }
}
