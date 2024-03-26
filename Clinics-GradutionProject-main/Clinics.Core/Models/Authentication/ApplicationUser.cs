using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//we would have used this model if we want to costimize the users table in Identity
namespace Clinics.Core.Models.Authentication
{
    
    public class ApplicationUser : IdentityUser
    {

        [Required, MaxLength(20)]
        public string FirstName { get; set; }
        [Required, MaxLength(20)]
        public string LastName { get; set; }
        
    }
}