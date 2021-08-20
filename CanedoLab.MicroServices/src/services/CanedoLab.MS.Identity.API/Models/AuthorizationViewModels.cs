
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CanedoLab.MS.Identity.API.Models
{

    public class UserViewModel 
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }  
    

    public class UserClaimViewModel 
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class AddUserClaimViewModel 
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        public string ValueType { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
