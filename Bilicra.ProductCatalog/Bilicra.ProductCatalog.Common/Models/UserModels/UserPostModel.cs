using System.ComponentModel.DataAnnotations;

namespace Bilicra.ProductCatalog.Common.Models.UserModels
{
    public class UserPostModel
    {
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter email")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
