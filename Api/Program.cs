using Api.Data;
using Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<RouteOptions>(options =>
{
   options.LowercaseUrls = true;
});

builder.Services.AddDbContext<ApiDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<ICoordinatesService, CoordinatesService>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiDbContext>();
    var config = services.GetRequiredService<IConfiguration>();

    context.Database.EnsureCreated();
    context.ResetDb();
    context.PopulateDb(config);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
