using Edukate_simulation_AzMPA201.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Edukate_simulation_AzMPA201.Models;

public class Course : BaseEntity
{
    [Required, MaxLength(256)]
    public string Title { get; set; } = string.Empty;
    [Required, MaxLength(512)]
    public string ImagePath { get; set; } = string.Empty;
    [Required]
    public int Rating { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}
