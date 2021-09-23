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
        private Entities context;
        private EFUnitOfWork unitOfWork;

        public HomeController()
        {
            context = new Entities();
            unitOfWork = new EFUnitOfWork();
        }

        public ActionResult Index()
        {
            var testall = unitOfWork.GetRepository<STUDENT>().GetAll();
            var testsingle = unitOfWork.GetRepository<STUDENT>().GetByID(1);

            var testall_context = context.STUDENTs.ToList();
            var testsingle_context = context.STUDENTs.FirstOrDefault(s => s.Student_ID == 1);
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