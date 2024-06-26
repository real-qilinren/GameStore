using GameStore.API.Data;
using GameStore.API.Entities;
using GameStore.API.Mapping;
using GameStore.API.Vo;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;
using GameStore.API.Dto;

public static class GameEndPoints
{
    
    const string GetGameEndpointName = "GetGame";
    
    public static RouteGroupBuilder MapGamesEndPoints (this WebApplication app)
    {
        var group = app.MapGroup("games")
            .WithParameterValidation();
        
        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                .Include(game => game.Genre)
                .Select(game => game.EntityToSummaryDto())
                .AsNoTracking()
                .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
        
                return game is null ? Results.NotFound() : Results.Ok(game.EntityToDetailDto());
            })
            .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (GameVo gameVo, GameStoreContext dbContext) =>
        {
            Game newGame = gameVo.VoToEntity();

            dbContext.Games.Add(newGame);
            await dbContext.SaveChangesAsync();
            
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = newGame.Id }, newGame.EntityToDetailDto());
        });

        // PUT /games
        group.MapPut("/{id}", async (int id, GameVo updateGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingGame)
                .CurrentValues
                .SetValues(updateGame.VoToEntity(id));
            
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                .Where(game => game.Id == id)
                .ExecuteDeleteAsync();
    
            return Results.NoContent();
        });

        return group;
    }
    
}