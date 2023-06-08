using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Knila.BookStore.WebAPI.Models.Authentication
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "The Email address field is required.")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "The Email address must be at least {2} characters long.", MinimumLength = 6)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(35, ErrorMessage = "The Password must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password *")]
        public string Password { get; set; }
    }
}