using DealerAPI.Persistence;
using DealerAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<DealerDbContext>(options => options
        .UseSqlite("Data Source=dealer.db")
        .UseSnakeCaseNamingConvention()
        .UseLazyLoadingProxies())
    .AddScoped<DealerService>()
    .AddScoped<DealerTypeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger(x =>
        {
            x.RouteTemplate = "dealer/swagger/{documentname}/swagger.json";
        })
        .UseSwaggerUI(x =>
        {
            x.RoutePrefix = "dealer/swagger";
        });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<DealerDbContext>();
    Console.WriteLine("can connect: " + dbContext.Database.CanConnect());

    dbContext?.Database.Migrate();
}

app.Run();
