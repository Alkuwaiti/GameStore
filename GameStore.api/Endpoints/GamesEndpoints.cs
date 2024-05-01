using GameStore.api.DTOs;

namespace GameStore.api.Endpoints;
public static class GamesEndpoints
{
    private static readonly List<GameDto> games = new()
    {
        new GameDto(1, "The Witcher 3: Wild Hunt", "Action RPG", 29.99m, new DateOnly(2015, 5, 19)),
        new GameDto(2, "Grand Theft Auto V", "Action-Adventure", 39.99m, new DateOnly(2013, 9, 17)),
        new GameDto(3, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 49.99m, new DateOnly(2017, 3, 3)),
        new GameDto(4, "Red Dead Redemption 2", "Action-Adventure", 59.99m, new DateOnly(2018, 10, 26)),
        new GameDto(5, "Overwatch", "First-Person Shooter", 19.99m, new DateOnly(2016, 5, 24))
    };

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        const string GetGameEndpointName = "GetGame";

        var group = app.MapGroup("games").WithParameterValidation();

        group.MapGet("/", () => games);

        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);

            return game is null ? Results.NotFound() : Results.Ok(game);

        }

        ).WithName(GetGameEndpointName);

        // POST
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new GameDto(
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(id, updatedGame.Name, updatedGame.Genre, updatedGame.Price, updatedGame.ReleaseDate);

            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });

        return group;
    }
}
