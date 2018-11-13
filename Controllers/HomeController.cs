using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "The University of Maryland University College has always been on the forefront of tying academic learning to real-world job applications. The UMUC Competency Tracker is the next step, allowing professors and administrators to directly link academic programs to their respective Learning Activities and Competencies. The UCT visualizes this, allowing faculty to better track problem areas and adjust coursework to better suit their goals-- and to make life for both students and faculty easier. ";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "UCT contacts.";

            return View();
        }
    }
}
