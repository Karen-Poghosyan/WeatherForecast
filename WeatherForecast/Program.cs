using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherForecast.BLL.Services;
using WeatherForecast.BLL.Services.Hosted;
using WeatherForecast.Core.Abstractions.BLL;
using WeatherForecast.Core.Abstractions.DAL.Repositories;
using WeatherForecast.Core.Abstractions.DAL.Repositories.Base;
using WeatherForecast.DAL.DataAccess;
using WeatherForecast.DAL.Repositories;
using WeatherForecast.DAL.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Host.ConfigureServices((hostContext, services) =>
{
    services.AddSingleton<IConfiguration>(builder.Configuration);
});

#region Repositories
builder.Services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IWeatherObservationRepository, WeatherObservationRepository>();
#endregion
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddHostedService<AutoRemoveForecastsJob>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
