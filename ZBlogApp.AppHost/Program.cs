
var builder = DistributedApplication.CreateBuilder(args);

// Add blazor server project
var blogDB = builder.AddSqlServer("ZBlogSqlServer")
    .AddDatabase("ZBlogDB");

// Add api service project and only accessible via internal endpoints
var apiService = builder.AddProject<Projects.ZBlogApp_ApiService>("apiservice")
    .WithReference(blogDB)
    .WaitFor(blogDB);

// Add blazor server project and make it accessible via external HTTP endpoints
var blazorServer = builder.AddProject<Projects.ZBlogApp_BlazorServer>("blazorserver")
    .WithExternalHttpEndpoints()
    .WithReference(blogDB)
    .WaitFor(blogDB);

// Add web frontend project and make it accessible via external HTTP endpoints
builder.AddProject<Projects.ZBlogApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

await builder.Build().RunAsync();
