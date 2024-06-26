using GameStore.API.Dto;
using GameStore.API.Entities;

namespace GameStore.API.Mapping;

public static class GenreMapping
{
    public static GenreDto EntityToDto(this Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.Name
        );
    }
}