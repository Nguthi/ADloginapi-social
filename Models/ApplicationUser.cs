using Microsoft.AspNetCore.Identity;

namespace ADloginAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Additional properties can be added here
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
