namespace Edukate_simulation_AzMPA201.ViewModels.CourseViewModels;

public class CourseGetVM
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int AuthorId { get; set; }
    public string AuthorFullname { get; set; } = string.Empty;
}
