using System.ComponentModel.DataAnnotations;

namespace Edukate_simulation_AzMPA201.ViewModels.CourseViewModels;

public class CourseUpdateVM
{
    public int Id { get; set; }
    [Required, MaxLength(256)]
    public string Title { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    [Required, Range(0, 5)]
    public int Rating { get; set; }
    public int AuthorId { get; set; }
}
