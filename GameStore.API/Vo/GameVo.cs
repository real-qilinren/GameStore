using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Vo;

public record GameVo(
    [Required] [StringLength(50)] string Name,
    int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);