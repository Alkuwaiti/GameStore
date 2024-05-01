using GameStore.api.DTOs;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = new()
{
    new GameDto(1, "The Witcher 3: Wild Hunt", "Action RPG", 29.99m, new DateOnly(2015, 5, 19)),
    new GameDto(2, "Grand Theft Auto V", "Action-Adventure", 39.99m, new DateOnly(2013, 9, 17)),
    new GameDto(3, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 49.99m, new DateOnly(2017, 3, 3)),
    new GameDto(4, "Red Dead Redemption 2", "Action-Adventure", 59.99m, new DateOnly(2018, 10, 26)),
    new GameDto(5, "Overwatch", "First-Person Shooter", 19.99m, new DateOnly(2016, 5, 24))
};

app.MapGet("games", () => games);

app.MapGet("/", () => "Hello World!");

app.Run();
