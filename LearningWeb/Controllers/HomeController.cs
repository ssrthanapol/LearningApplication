using LearningWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningWeb.Controllers
{
    public class HomeController : Controller
    {
        private EFUnitOfWork unitOfWork;

        public HomeController()
        {
            unitOfWork = new EFUnitOfWork();
        }

        public ActionResult Index()
        {
            var testall = unitOfWork.GetRepository<STUDENT>().GetAll();
            var testsingle = unitOfWork.GetRepository<STUDENT>().GetByID(1);
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