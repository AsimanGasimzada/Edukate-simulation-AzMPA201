using Microsoft.AspNetCore.Mvc;

namespace Edukate_simulation_AzMPA201.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



}
