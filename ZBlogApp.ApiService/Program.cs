using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZBlogApp.BlazorServer.Data;

var builder = WebApplication.CreateBuilder(args);

// Add 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DATABASE_CONNECTION_STRING")));

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
// Add OpenAPI services
builder.Services.AddOpenApi();

// Add Cors
builder.Services.AddCors(o => o.AddPolicy("Policy", builder => {
  builder.AllowAnyOrigin() // For anyone to access
    .AllowAnyMethod() // Allow any method
    .AllowAnyHeader(); // Allow any headers
}));

var app = builder.Build(); //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Use Cross Origin Resource Sharing
app.UseCors("Policy");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options => {
        options.Title = "ZBlogApp API";
        options.Theme = ScalarTheme.Kepler;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.ShowSidebar = true;
    });
}


// redireting to https
app.UseHttpsRedirection();


// Get all Articles
app.MapGet("/api/articles", async (ApplicationDbContext context) =>{
    var articles = await context.Article.ToListAsync();
    return Results.Ok(articles);
});

// Get all Users
app.MapGet("/api/users", async (ApplicationDbContext context) =>
{
    var users = await context.Users
        .Include(u => u.UserRoles) // Include UserRoles mapping table
        .ThenInclude(ur => ur.Role) // Include Role table
        .ToListAsync();

    var userList = users.Select(user => new
    {
        user.Id,
        user.FirstName,
        user.LastName,
        user.Email,
        Roles = user.UserRoles
            .Select(ur => ur.Role?.Name)
            .Where(roleName => roleName != null)
            .ToList()
    }).ToList();

    return Results.Ok(userList);
});

// Get all Roles
app.MapGet("/api/roles", async (ApplicationDbContext context) =>
{
    var roles = await context.Roles.ToListAsync();
    return Results.Ok(roles);
});


app.MapDefaultEndpoints();

await app.RunAsync();
