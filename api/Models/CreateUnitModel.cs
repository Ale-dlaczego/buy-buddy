using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class CreateUnitModel
{
    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public required string Name { get; set; }
}