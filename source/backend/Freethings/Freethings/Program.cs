using System.Reflection;
using Freethings.Auctions;
using Freethings.Auctions.Presentation;
using Freethings.Shared.Infrastructure;
using Freethings.Shared.Infrastructure.Auth.Context;
using Freethings.Shared.Infrastructure.Authentication;
using Freethings.Shared.Infrastructure.Persistence;
using Freethings.Users;
using Freethings.Users.Presentation;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(opts =>
{
    opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services
    .AddAuctions(builder.Configuration)
    .AddUsers(builder.Configuration)
    .AddModularSharedInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
    app.SeedWithSampleData();
}

app.MapAuctionsEndpoints()
   .MapUsersEndpoints();

app
    .UseAppAuthentication()
    .UseCurrentUserContext()
    .UseHttpsRedirection()
    .UseExceptionHandler();

app.Run();