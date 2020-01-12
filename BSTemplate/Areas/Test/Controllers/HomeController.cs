using BS.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BSTemplate.Areas.Test.Controllers
{
    public class HomeController : Controller
    {
        // GET: Test/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}