using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Infinite.TaxiBookingSystem.API.Models
{
    public class User:LoginModel
    {
        public int Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string Role { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
