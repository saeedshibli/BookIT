using BookItt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace BookItt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult GetData()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var query = db.OrdersBooks.Include("Book").GroupBy(p => p.Book.Name)
                                                     .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).OrderByDescending(x => x.count).Take(5).ToList();


            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetData2()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var query2 = db.OrdersBooks.Include("Book").GroupBy(p => p.Book.section.Name)
                                                            .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).
                                                            OrderByDescending(x => x.count).
                                                            Take(5).
                                                            ToList();
            return Json(query2, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetData3()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var query = db.OrdersBooks.Include("Order").GroupBy(p => p.Order.CustomerEmail)
                                                     .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).
                                                     OrderByDescending(x => x.count).
                                                     Take(3).
                                                     ToList();



            return Json(query, JsonRequestBehavior.AllowGet);

        }
    }
}