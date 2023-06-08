using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Knila.BookStore.WebAPI.Models.Authentication
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [Display(Name = "Your Name *")]
        [StringLength(30, ErrorMessage = "The Name must be at least {2} characters long.", MinimumLength = 6)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email address field is required.")]
        [Display(Name = "Email address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, ErrorMessage = "The Email address must be at least {2} characters long.", MinimumLength = 6)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$",
            ErrorMessage = @"Your password should contains a lowercase.
                           <br>Should contains a uppercase.
                           <br>Should contains a number.
                           <br>Should contains a special character(eg. ! @ # $ % &).
                           <br>Should be 8-25 characters long.")]
        [StringLength(35, ErrorMessage = "The Password must be at least {2} characters long.", MinimumLength = 6)]
        [Display(Name = "Password *")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm password field is required.")]
        [Compare("Password", ErrorMessage = "Password mismatch, Please try again.")]
        [Display(Name = "Confirm Password *")]
        public string ConfirmPassword { get; set; }

        //Range(typeof(bool), "false", "false", ErrorMessage = "Please agree with our Privacy And Security policy.")]
        public bool PrivacyAndSecurity { get; set; }

        //[Range(typeof(bool), "false", "false", ErrorMessage = "Please agree with our Terms Of Service.")]
        public bool TermsOfService { get; set; }
    }
}