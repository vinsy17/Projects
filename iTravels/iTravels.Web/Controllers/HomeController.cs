using Sample.Core.Repositories;
using Sample.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iTravels.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            this._userService = userService;
            //this._userRepository = userRepository;
        }

        public ActionResult Index()
        {
            var user=_userService.GetUser(new Guid());
            if (user != null) {

            }
            else {
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}