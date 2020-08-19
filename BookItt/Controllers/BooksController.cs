using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookItt.Models;

namespace BookItt.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.section);
            return View(books.ToList());
        }

        public ActionResult UsersIndex()
        {
            var books = db.Books.Include(b => b.section).OrderBy(x => Guid.NewGuid());
            return View(books.ToList());
        }

        public PartialViewResult EcresingPrice()
        {
            List<Book> model = db.Books.Include(b=>b.section).OrderBy(x => x.Price).ToList();
            return PartialView("Ajaxss",model);
        }
        public PartialViewResult DecresingPrice()
        {
            List<Book> model = db.Books.Include(b => b.section).OrderByDescending(x => x.Price).ToList();
            return PartialView("Ajaxss", model);
        }





        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "BookID,Name,Price,Date,SectionID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Name", book.SectionID);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Name", book.SectionID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BookID,Name,Price,Date,SectionID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Name", book.SectionID);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string Genere ,string bookName,int ?Price)
        {
            List<Book> books = new List<Book>();
            if(Genere=="" && bookName==""&& Price==50)
            {
                return RedirectToAction("UsersIndex");
            }
            else if(Genere=="")
            {
                if (Price != 50 && bookName != "")
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {

                        if (b.Name == bookName && b.Price <= Price)
                            books.Add(b);
                    }

                }
                else if (Price == 50)
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {
                        if (b.Name.Contains(bookName))
                            books.Add(b);
                    }
                }
                else
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {
                        if (b.Price<=Price)
                            books.Add(b);
                    }
                }
                return View(books);

            }
            else if (bookName == "")
            {
                if (Price != 50 && Genere != "")
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {

                        if (b.section.Name == Genere&&b.Price<=Price)
                            books.Add(b);
                    }

                }
                else if (Price == 50)
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {

                        if (b.section.Name==Genere)
                            books.Add(b);
                    }
                }
                else
                {
                    foreach (Book b in db.Books.Include(b => b.section))
                    {
                        if (b.Price <= Price)
                            books.Add(b);
                    }
                }
            
                
                return View(books);
            }
            else if(Price==50)
            {
                foreach (Book b in db.Books.Include(b => b.section))
                {
                    if (b.section.Name==Genere && b.Name.Contains(bookName))
                        books.Add(b);
                }
                return View(books);
            }
            else
            {
                foreach (Book b in db.Books.Include(b => b.section))
                {
                    if (b.section.Name==Genere&& b.Name.Contains(bookName)&&b.Price<=Price)
                        books.Add(b);
                }
                return View(books);
            }
           //Book book =db.Books.Find()
           
        }
   

    }
}
