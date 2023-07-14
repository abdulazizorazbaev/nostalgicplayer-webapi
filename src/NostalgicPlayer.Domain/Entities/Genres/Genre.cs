using System.ComponentModel.DataAnnotations;

namespace NostalgicPlayer.Domain.Entities.Genres;

public class Genre : Auditable
{
    [MaxLength(50)]
    public string GenreName { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;

    public string Description { get; set;} = String.Empty;
}