using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanedoLab.MS.Identity.API.Models
{
    public class UserLoginViewModel 
    {
        [Required(ErrorMessage = "Field {0} is required")]
        [EmailAddress(ErrorMessage = "Field {0} not in right format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string Password { get; set; }
    }

    public class UserRegistrationViewModel
    {
        [Required(ErrorMessage = "Field {0} is required")]
        [EmailAddress(ErrorMessage = "Field {0} not in right format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field {0} is required")]
        [StringLength(100, ErrorMessage = "Field {0} must be between {2} and {1} characters")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string PasswordConfirmation { get; set; }
    }

    public class UserAuthenticationViewModel 
    {
        public string AccessToken { get; set; }
        public DateTimeAuth DateTimeAuth { get; set; }        
        public UserToken UserToken { get; set; }
    }

    public class DateTimeAuth 
    {
        public DateTime CreatedAtUtc { get; set; }
        public double ExpirationInSeconds { get; set; }
        public DateTime ExpirationUtc { get; set; }
    }

    public class UserToken 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
