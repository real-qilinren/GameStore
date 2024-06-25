using GameStore.API.Vo;

namespace GameStore.API.Endpoints;
using GameStore.API.Dto;

public static class GameEndPoints
{
    
    const string getGameEndpointName = "GetGame";

    private static readonly List<GameDto> games =
    [
        new GameDto(1, "Game Name", "Genre", 59.99m, new DateOnly(2024, 6, 25)),
        new GameDto(2, "Game Name 2", "Genre 2", 29.99m, new DateOnly(2024, 6, 26)),
        new GameDto(3, "Game Name 3", "Genre 3", 39.99m, new DateOnly(2024, 6, 27)),
        new GameDto(4, "Game Name 4", "Genre 4", 49.99m, new DateOnly(2024, 6, 28)),
        new GameDto(4, "Game Name 5", "Genre 6", 19.99m, new DateOnly(2024, 6, 28))
    ];
    
    public static RouteGroupBuilder MapGamesEndPoints (this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();
        
        // GET /games
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int id) =>
            { 
                GameDto? game = games.Find(game => game.Id == id);
        
                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithName(getGameEndpointName);

        // POST /games
        group.MapPost("/", (GameVo game) =>
        {

            var newGame = new GameDto(games.Count + 1, game.Name, game.Genre, game.Price, game.ReleaseDate);
            games.Add(newGame);
            return Results.CreatedAtRoute(getGameEndpointName, new { id = newGame.Id }, newGame);
        });

        // PUT /games
        group.MapPut("/{id}", (int id, GameVo updateGame) =>
        {
            var idx = games.FindIndex(game => game.Id == id);

            if (idx == -1)
            {
                return Results.NotFound();
            }

            games[idx] = new GameDto(
                id,
                updateGame.Name,
                updateGame.Genre,
                updateGame.Price,
                updateGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);
    
            return Results.NoContent();
        });

        return group;
    }
    
}