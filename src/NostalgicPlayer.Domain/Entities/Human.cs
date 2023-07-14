using System.ComponentModel.DataAnnotations;

namespace NostalgicPlayer.Domain.Entities;

public class Human : Auditable
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;

    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;

    public string ImagePath { get; set; } = String.Empty;
}