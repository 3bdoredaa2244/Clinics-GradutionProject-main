using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinics.Core.Models.Authentication
{
    public class ResetPassword
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string NewPassword { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string ConfirmPassword { get; set; }
    }
}
