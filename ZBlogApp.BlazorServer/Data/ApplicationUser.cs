using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace ZBlogApp.BlazorServer.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [DisplayName("First Name")]
    public required string FirstName { get; set; }

    [DisplayName("Last Name")]
    public required string LastName { get; set; }
    
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
}

