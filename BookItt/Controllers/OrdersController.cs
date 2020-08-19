using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookItt.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace BookItt.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }
        public ActionResult Index2(int ?page)
        {
            var pageNumber = page ?? 1;
            var pageSize = 5;
            var orderList = db.Orders.OrderByDescending(x => x.OrderID).ToPagedList(pageNumber, pageSize);
            return View(orderList);

        }
        //mine
        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        // GET: Orders/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,OrderDate,PaymentType,Status,CustomerEmail")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,OrderDate,PaymentType,Status,CustomerEmail")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("OrderHistory");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult OrderHistory()
        {
            List<Order> orders = new List<Order>();
            var user = User.Identity.GetUserName();
            foreach(Order o in db.Orders)
            {
                if (o.CustomerEmail == user)
                {
                    orders.Add(o);
                }
            }
            return View(orders);

        }
        public ActionResult Search(int ? OrderID, DateTime ? OrderDate, string Status)
        {
            List<Order> orders = new List<Order>();
            var user = User.Identity.GetUserName();
            if (OrderID == null && OrderDate == null && Status == "")
            {
                return RedirectToAction("OrderHistory");
            }
            if (OrderID == null)
            {
                if (Status == "")
                {
                    foreach (Order o in db.Orders)
                    {
                        if (o.OrderDate.Date==OrderDate && o.CustomerEmail == user)
                            orders.Add(o);
                    }
                }
                else
                {
                    foreach (Order o in db.Orders.Include(b=>b.OrderBooks))
                    {
                        if (o.Status.Contains(Status) && o.CustomerEmail == user)
                            orders.Add(o);
                    }
                }
                return View(orders);

            }
            if (OrderDate == null)
            {
                if (Status == "")
                {
                    foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                    {
                        if (o.OrderID==OrderID && o.CustomerEmail == user)
                            orders.Add(o);
                    }
                }
                else
                {
                    foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                    {
                        if (o.Status.Contains(Status) && o.CustomerEmail == user)
                            orders.Add(o);
                    }
                }


                return View(orders);
            }
            if (Status == "")
            {
                foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                {
                    if (o.OrderDate.Equals(OrderDate) && o.OrderID == OrderID && o.CustomerEmail == user)
                        orders.Add(o);
                }
                return View(orders);
            }
            else
            {
                foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                {
                    if (o.OrderDate.Equals(OrderDate) && o.OrderID == OrderID && o.Status.Contains(Status) && o.CustomerEmail == user)
                        orders.Add(o);
                }
                return View(orders);
            }
            //Book book =db.Books.Find()

        }

        [Authorize(Roles = "Admin")]
        public ActionResult SearchAdmin(int? OrderID, DateTime? OrderDate, string Email)
        {
            List<Order> orders = new List<Order>();
       
            if (OrderID == null && OrderDate == null && Email == "")
            {
                return RedirectToAction("Index");
            }
            if (OrderID == null)
            {
                if (Email == "")
                {
                    foreach (Order o in db.Orders)
                    {
                        if (o.OrderDate.Date == OrderDate )
                            orders.Add(o);
                    }
                }
                else
                {
                    foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                    {
                        if (o.CustomerEmail.Contains(Email) )
                            orders.Add(o);
                    }
                }
                return View(orders);

            }
            if (OrderDate == null)
            {
                if (Email == "")
                {
                    foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                    {
                        if (o.OrderID == OrderID )
                            orders.Add(o);
                    }
                }
                else
                {
                    foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                    {
                        if (o.CustomerEmail.Contains(Email))
                            orders.Add(o);
                    }
                }


                return View(orders);
            }
            if (Email == "")
            {
                foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                {
                    if (o.OrderDate.Equals(OrderDate) && o.OrderID == OrderID )
                        orders.Add(o);
                }
                return View(orders);
            }
            else
            {
                foreach (Order o in db.Orders.Include(b => b.OrderBooks))
                {
                    if (o.OrderDate.Equals(OrderDate) && o.OrderID == OrderID && o.CustomerEmail.Contains(Email))
                        orders.Add(o);
                }
                return View(orders);
            }
            //Book book =db.Books.Find()

        }
    }
}
