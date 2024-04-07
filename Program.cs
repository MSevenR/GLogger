using GLogger.Api.IgdbApi;
using GLogger.Api.IgdbApi.Endpoints;
using GLogger.Controllers;
using GLogger.Database;
using GLogger.Database.MongoDB;
using GLogger.TwitchApi;
using Microsoft.AspNetCore.Mvc;
using TwitchApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuration Section
var configuration = builder.Configuration;

//Twitch
var twitchApiConfig = configuration.GetSection("TwitchApiConfig").Get<TwitchApiConfig>();
var twitchClient = twitchApiConfig != null ? new TwitchApiClient(twitchApiConfig.Value) : null;
if (twitchClient == null) {
    throw new Exception("Twitch API configuration is missing");
}

//MongoDB
var mongodbConfig = configuration.GetSection("MongoDb").Get<DatabaseConfig>();
var mongodbClient = mongodbConfig != null ? new MongoDbClient(mongodbConfig.Value) : null;
if (mongodbClient != null) {
    builder.Services.AddSingleton(mongodbClient);
}

//IGDB
var igdbApiConfig = configuration.GetSection("IgdbApi").Get<IgdbConfig>();
var igdbClient = igdbApiConfig != null ? new IgdbClient(twitchClient, igdbApiConfig) : null;
if (igdbClient != null) {
    builder.Services.AddSingleton(igdbClient);
}

//Controllers
builder.Services.AddScoped<GameController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapPost("/games/gamesummary", (string gameName, [FromServices] GameController gameController) =>
{
    var gameList = gameController.GameSummarySearchByName(gameName, true);
    return gameList;
})
.WithName("FindGameSummary")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
