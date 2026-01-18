using Edukate_simulation_AzMPA201.Data;
using Edukate_simulation_AzMPA201.Models;
using Edukate_simulation_AzMPA201.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Edukate_simulation_AzMPA201.Areas.Admin.Controllers;
[Area("Admin")]
public class CourseController : Controller
{

    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public CourseController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "img");
    }

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

        //Select * from Courses
        //Join Authors 
        //On Authors.Id=Courses.AuthorId

        return View(courses);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await SendAuthorsWithViewBag();

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateVM vm)
    {
        await SendAuthorsWithViewBag();

        if (!ModelState.IsValid)
            return View(vm);


        var isExistAuthor = await _context.Authors.AnyAsync(x => x.Id == vm.AuthorId);

        if (!isExistAuthor)
        {
            ModelState.AddModelError("AuthorId", "This author is not found");
            return View(vm);
        }

        if (vm.Image.Length > 2 * 1024 * 1024)
        {
            ModelState.AddModelError("Image", "Faylin maximum olcusu 2 mb olmalidir");
            return View(vm);
        }




        if (!vm.Image.ContentType.Contains("image"))
        {

            ModelState.AddModelError("Image", "Yalniz sekil formatinda data yukleyin");
            return View(vm);
        }

        string uniqueFileName = Guid.NewGuid().ToString() + vm.Image.FileName;


        string path = Path.Combine(_folderPath, uniqueFileName);

        using FileStream stream = new(path, FileMode.Create);

        await vm.Image.CopyToAsync(stream);



        Course course = new()
        {
            Title = vm.Title,
            Rating = vm.Rating,
            AuthorId = vm.AuthorId,
            ImagePath = uniqueFileName
        };


        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }


    public async Task<IActionResult> Delete(int id)
    {
        var course = await _context.Courses.FindAsync(id);

        if (course is null)
            return NotFound();

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();


        string existImagePath = Path.Combine(_folderPath, course.ImagePath);


        if (System.IO.File.Exists(existImagePath))
            System.IO.File.Delete(existImagePath);


        return RedirectToAction("Index");
    }



    public async Task<IActionResult> Detail(int id)
    {
        var course = await _context.Courses.Select(x => new CourseGetVM()
        {
            Id = x.Id,
            AuthorFullname = x.Author.Fullname,
            AuthorId = x.AuthorId,
            ImagePath = x.ImagePath,
            Rating = x.Rating,
            Title = x.Title


        }).FirstOrDefaultAsync(x => x.Id == id);


        if (course is null)
            return NotFound();

        return View(course);
    }



    public async Task<IActionResult> Update(int id)
    {

        var course = await _context.Courses.Select(x => new CourseUpdateVM()
        {
            Id = x.Id,
            AuthorId = x.AuthorId,
            Rating = x.Rating,
            Title = x.Title
        }).FirstOrDefaultAsync(x => x.Id == id);

        if (course is null)
            return NotFound();

        await SendAuthorsWithViewBag();



        return View(course);
    }

    [HttpPost]
    public async Task<IActionResult> Update(CourseUpdateVM vm)
    {
        await SendAuthorsWithViewBag();

        if (!ModelState.IsValid)
            return View(vm);


        var isExistAuthor = await _context.Authors.AnyAsync(x => x.Id == vm.AuthorId);

        if (!isExistAuthor)
        {
            ModelState.AddModelError("AuthorId", "This author is not found");
            return View(vm);
        }

        if (vm.Image is not null)
        {

            if (vm.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Faylin maximum olcusu 2 mb olmalidir");
                return View(vm);
            }

            if (!vm.Image.ContentType.Contains("image"))
            {

                ModelState.AddModelError("Image", "Yalniz sekil formatinda data yukleyin");
                return View(vm);
            }
        }

        var existCourse = await _context.Courses.FindAsync(vm.Id);

        if (existCourse is null)
            return BadRequest();



        existCourse.Title = vm.Title;
        existCourse.AuthorId = vm.AuthorId;
        existCourse.Rating = vm.Rating;

        //if(vm.Image is not null)
        if (vm.Image is { })
        {
            string uniqueFileName = Guid.NewGuid().ToString() + vm.Image.FileName;


            string path = Path.Combine(_folderPath, uniqueFileName);

            using FileStream stream = new(path, FileMode.Create);

            await vm.Image.CopyToAsync(stream);



            string existImagePath = Path.Combine(_folderPath, existCourse.ImagePath);

            if (System.IO.File.Exists(existImagePath))
                System.IO.File.Delete(existImagePath);


            existCourse.ImagePath = uniqueFileName;

        }

        _context.Courses.Update(existCourse);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }
    private async Task SendAuthorsWithViewBag()
    {
        var authors = await _context.Authors.Select(x => new SelectListItem() { Text = x.Fullname, Value = x.Id.ToString() }).ToListAsync();
        ViewBag.Authors = authors;
    }

}


//image/jpg,