using System.Reflection;
using Freethings.Offers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(opts =>
{
    opts.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddOffers();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapOffersEndpoints();

app.UseHttpsRedirection();

app.Run();