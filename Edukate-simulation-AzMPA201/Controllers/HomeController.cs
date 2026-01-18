using Edukate_simulation_AzMPA201.Data;
using Edukate_simulation_AzMPA201.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Edukate_simulation_AzMPA201.Controllers;
public class HomeController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {

        var courses = await _context.Courses.Select(x => new CourseGetVM()
        {
            Id = x.Id,
            Title = x.Title,
            ImagePath = x.ImagePath,
            Rating = x.Rating,
            AuthorId = x.AuthorId,
            AuthorFullname = x.Author.Fullname
        }).ToListAsync();



        return View(courses);
    }






}
