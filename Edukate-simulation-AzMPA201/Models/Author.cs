using Edukate_simulation_AzMPA201.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Edukate_simulation_AzMPA201.Models;

public class Author : BaseEntity
{
    [Required, MaxLength(256)]
    public string Fullname { get; set; } = string.Empty;
    public ICollection<Course> Courses { get; set; } = [];
}