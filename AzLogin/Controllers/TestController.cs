using Microsoft.AspNetCore.Mvc;

namespace AzLogin.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            //Optionally, pass user information to the view
            var userName = User.Identity.Name;
            ViewBag.UserName = userName;
            return View();
        }
    }
}
