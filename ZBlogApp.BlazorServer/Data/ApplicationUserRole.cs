using Microsoft.AspNetCore.Identity;

namespace ZBlogApp.BlazorServer.Data;
public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ApplicationRole Role { get; set; } = null!;
}
