using Microsoft.AspNetCore.Mvc;
using RoleTopMVC.ViewModels;

namespace RoleTopMVC.Controllers
{
    public class AlbumController : Controller
    {
        public IActionResult Index()
        {
            return View(new BaseViewModel()
            {
                NomeView = "Album"
            });
        }
    }
}