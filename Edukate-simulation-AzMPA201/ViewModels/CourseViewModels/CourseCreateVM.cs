using System.ComponentModel.DataAnnotations;

namespace Edukate_simulation_AzMPA201.ViewModels.CourseViewModels;

public class CourseCreateVM
{
    [Required, MaxLength(256)]
    public string Title { get; set; } = string.Empty;
    [Required]
    public IFormFile Image { get; set; } = null!;
    [Required, Range(0, 5)]
    public int Rating { get; set; }
    public int AuthorId { get; set; }
}