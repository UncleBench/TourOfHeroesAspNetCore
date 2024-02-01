using Microsoft.EntityFrameworkCore;
using TourOfHeroes.Server.Persistence;
using TourOfHeroes.Server.Services.Heroes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IHeroService, HeroService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerDocument(options =>
    {
        options.PostProcess = document =>
        {
            document.Info.Version = "v1";
            document.Info.Title = "Tour of Heroes API";
            document.Info.Description = "REST API for Tour of Heroes";
        };
    });

builder.Services.AddDbContext<TourOfHeroesDbContext>(options => {
    options.UseSqlite("Data Source=TourOfHeroes.db");
});

// Generate lowercase URLs and query strings
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();
