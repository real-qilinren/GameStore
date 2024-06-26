using GameStore.API.Dto;
using GameStore.API.Entities;
using GameStore.API.Vo;

namespace GameStore.API.Mapping;

public static class GameMapping
{
    public static Game VoToEntity(this GameVo gameVo)
    {
        return new Game
        {
            Name = gameVo.Name,
            GenreId = gameVo.GenreId,
            Price = gameVo.Price,
            ReleaseDate = gameVo.ReleaseDate
        };
    }
    
    public static Game VoToEntity(this GameVo gameVo, int id)
    {
        return new Game
        {
            Id = id,
            Name = gameVo.Name,
            GenreId = gameVo.GenreId,
            Price = gameVo.Price,
            ReleaseDate = gameVo.ReleaseDate
        };
    }
    
    public static GameSummaryDto EntityToSummaryDto(this Game game)
    {
        return new GameSummaryDto (
            game.Id,
            game.Name,
            game.Genre!.Name,
            game.Price,
            game.ReleaseDate
        );
    }
    
    public static GameDetailDto EntityToDetailDto(this Game game)
    {
        return new GameDetailDto (
            game.Id,
            game.Name,
            game.GenreId,
            game.Price,
            game.ReleaseDate
        );
    }
}