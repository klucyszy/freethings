using System.Reflection;
using Freethings.Auctions;
using Freethings.Auctions.Presentation;
using Freethings.Offers;
using Freethings.Offers.Presentation;
using Freethings.Shared.Infrastructure;

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
}

app.MapOffersEndpoints();
app.MapAuctionsEndpoints();

app.UseHttpsRedirection();

app.Run();