using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace ZBlogApp.BlazorServer.Data;
public class ApplicationRole : IdentityRole
{
    public ApplicationRole() : base() { }

    public ApplicationRole(string roleName) : base(roleName) { }

    public ApplicationRole(string roleName, string description, DateTime createdDate)
      : base(roleName)
    {
        base.Name = roleName;

        this.Description = description;
        this.CreatedDate = createdDate;
    }

    public string? Description { get; set; }

    [DisplayName("Create Date")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();

}