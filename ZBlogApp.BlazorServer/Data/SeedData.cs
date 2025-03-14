using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ZBlogApp.BlazorServer.Data;
public static class SeedData
{
    public static async Task Initialize(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager) 
    {
        await context.Database.MigrateAsync(); // ensure schema is up-to-date

        string adminRole = "admin";
        string memberRole = "contributor";
        string password4all = "P@$$w0rd";

        if (await roleManager.FindByNameAsync(adminRole) == null) 
        {
            await roleManager.CreateAsync(new ApplicationRole(adminRole));
        }

        if (await roleManager.FindByNameAsync(memberRole) == null) 
        {
            await roleManager.CreateAsync(new ApplicationRole(memberRole));
        }

        if (await userManager.FindByNameAsync("a@a.a") == null){
            var user = new ApplicationUser {
                UserName = "a@a.a",
                Email = "a@a.a",
                PhoneNumber = "6902341234",
                FirstName = "James",
                LastName = "Smith"
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, adminRole);

                var role = await roleManager.FindByNameAsync(adminRole);
                if (role != null) {
                    context.UserRoles.Add(new ApplicationUserRole { UserId = user.Id, RoleId = role.Id });
                    await context.SaveChangesAsync();
                }
            }
        }

        if (await userManager.FindByNameAsync("c@c.c") == null) {
            var user = new ApplicationUser {
                UserName = "c@c.c",
                Email = "c@c.c",
                PhoneNumber = "7788951456",
                FirstName = "John",
                LastName = "Doe"
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) {
                await userManager.AddPasswordAsync(user, password4all);
                await userManager.AddToRoleAsync(user, memberRole);

                var role = await roleManager.FindByNameAsync(memberRole);
                if (role != null) {
                    context.UserRoles.Add(new ApplicationUserRole { UserId = user.Id, RoleId = role.Id });
                    await context.SaveChangesAsync();
                }
            }
        }

    }
}