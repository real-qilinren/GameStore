namespace GameStore.API.Dto;

public record GameDetailDto(
    int Id, 
    string Name, 
    int Genre, 
    decimal Price, 
    DateOnly ReleaseDate);