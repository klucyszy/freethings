using System.Reflection;
using Freethings.Auctions;
using Freethings.Auctions.Presentation;
using Freethings.Offers;
using Freethings.Shared.Infrastructure;
using Freethings.Shared.Infrastructure.Authentication;
using Freethings.Shared.Infrastructure.Persistence;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(opts =>
{
    opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services
    .AddOffers(builder.Configuration)
    .AddAuctions(builder.Configuration)
    .AddModularSharedInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.ApplyMigrations();
    app.SeedWithSampleData();
}

app.MapAuctionsEndpoints();

app.UseAuth0();

app.UseHttpsRedirection();
app.UseExceptionHandler();


app.Run();