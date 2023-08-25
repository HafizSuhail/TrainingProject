using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Stanford_University
{
    [Authorize]
    public class HomeController : Controller
    {
       
        public IActionResult Home()
        {
            //string username = HttpContext.Session.GetString("Username");

            //string userName = HttpContext.User.Identity.Name

            string userName = HttpContext.User.Identity.Name;

            return View("Homepage", userName);
        }
    }
}
