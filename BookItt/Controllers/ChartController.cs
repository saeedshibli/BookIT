using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookItt.Models;
using System.Web.Helpers;

namespace BookItt.Controllers
{
    public class ChartController : Controller
    {

        // GET: Chart
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult GetData()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var query = db.OrdersBooks.Include("Book").GroupBy(p => p.Book.Name)
                                                     .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).
                                                     OrderByDescending(x => x.count).
                                                     Take(5).
                                                     ToList();



            return Json(query, JsonRequestBehavior.AllowGet);

        }
    }
}