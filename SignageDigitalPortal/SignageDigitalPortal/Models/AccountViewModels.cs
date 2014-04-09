using System.ComponentModel.DataAnnotations;
using SignageDigitalPortal.Resources;
using SignageRepository.Resources;

namespace SignageDigitalPortal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(ResAccount), ErrorMessageResourceName = "REQUIRED_USERNAME")]
        [Display(Name = "FIELD_USERNAME", ResourceType = typeof(ResAccountRep))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ResAccount), ErrorMessageResourceName = "REQUIRED_PASSWORD")]
        [DataType(DataType.Password)]
        [Display(Name = "FIELD_PASSWORD", ResourceType = typeof(ResAccountRep))]
        public string Password { get; set; }

        [Display(Name = "FIELD_REMEMBER_ME", ResourceType = typeof(ResAccountRep))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
