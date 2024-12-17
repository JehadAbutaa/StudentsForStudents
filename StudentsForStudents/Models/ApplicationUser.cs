using Microsoft.AspNetCore.Identity;

namespace SmartLineSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool EmailConfirmed { get; set; }  // Flag to indicate if the email is confirmed
        public string EmailConfirmationToken { get; set; } = Guid.NewGuid().ToString();
        public bool IsLogedIn { get; set; }

        public string UserType { get; set; }
    
    }
}
