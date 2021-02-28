using System.ComponentModel.DataAnnotations;

namespace Bilicra.ProductCatalog.Common.Models.AuthenticationModels
{
    public class AuthenticationPostModel
    {
        [Required(ErrorMessage="Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
