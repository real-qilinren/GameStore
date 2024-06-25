using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Vo;

public record GameVo(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(20)] string Genre,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);