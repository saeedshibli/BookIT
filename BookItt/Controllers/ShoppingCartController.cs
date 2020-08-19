using BookItt.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;

namespace BookItt.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderNow(int ?id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session["ShoppingCart"]==null)
            {
                List<ShoppingCart> lsCart = new List<ShoppingCart>
                {
                    new ShoppingCart(db.Books.Find(id),1)
                };
                Session["ShoppingCart"] = lsCart;
            }
            else
            {
                List<ShoppingCart> lsCart = (List<ShoppingCart>)Session["ShoppingCart"];
                int check = isExistingCheck(id);
                if (check == -1)
                    lsCart.Add(new ShoppingCart(db.Books.Find(id),1));
                else
                {
                    lsCart[check].Quantity++;
                }
                
                Session["ShoppingCart"] = lsCart;
            }
            return View("Index");
        }
        private int isExistingCheck(int ?id)
        {
            List<ShoppingCart> lsCart = (List<ShoppingCart>)Session["ShoppingCart"];
            for(int i=0;i<lsCart.Count;i++)
            {
                if (lsCart[i].book.BookID == id)
                    return i;
            }
            return -1;
        }
        public ActionResult DeleteItemCart(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int check = isExistingCheck(id);
            List<ShoppingCart> lsCart = (List<ShoppingCart>)Session["ShoppingCart"];
            lsCart.RemoveAt(check);
            return View("Index");
        }
        public ActionResult UpdateCart(FormCollection frc)
        {
            string[] quantites = frc.GetValues("Quantity");
            List<ShoppingCart> lstCart = (List<ShoppingCart>)Session["ShoppingCart"];
            for(int i=0;i<lstCart.Count;i++)
            {
                lstCart[i].Quantity = Convert.ToInt32(quantites[i]);
            }
            Session["ShoppingCart"] = lstCart;
            return View("Index");
        }

        public ActionResult CheckOut()
        {
        
            return View("CheckOut");
        }
        public ActionResult ProcessOrder(FormCollection frc)
        {
            List<ShoppingCart> lstCart = (List<ShoppingCart>)Session["ShoppingCart"];
            //1.save the order into order table
            
            var user2 = User.Identity.GetUserName();
            Session["email"] = user2;
            Order order = new Order()
            {
                CustomerEmail=user2,
                OrderDate=DateTime.Now,
                PaymentType="Cash",
                Status="Processing"
            };
            db.Orders.Add(order);
            db.SaveChanges();
            //2.savee the order detail into orderbook table
            foreach(ShoppingCart cart in lstCart)
            {
                OrderBook orderBook = new OrderBook() {
                    OrderID = order.OrderID,
                    BookID=cart.book.BookID,
                    Quantity=cart.Quantity,
                    price=cart.book.Price
                };
                db.OrdersBooks.Add(orderBook);
                db.SaveChanges();
            }
            
            int[] arr = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (ShoppingCart cart in lstCart)
            {
                if (cart.book.SectionID == 1) arr[0]++;
                if (cart.book.SectionID == 2) arr[1]++;
                if (cart.book.SectionID == 3) arr[2]++;
                if (cart.book.SectionID == 4) arr[3]++;
                if (cart.book.SectionID == 5) arr[4]++;
                if (cart.book.SectionID == 6) arr[5]++;
                if (cart.book.SectionID == 7) arr[6]++;
                if (cart.book.SectionID == 8) arr[7]++;
                if (cart.book.SectionID == 9) arr[8]++;

            }
            int max = arr[0];
            var place = 0;
            for (int i = 0; i < 8; i++)
            {
                if (arr[i + 1] > arr[i])
                {
                    max = arr[i + 1];
                    place = i + 1;
                }
            }
            Session["Section"] = place;
            


                //3. remove shopping cart session
                Session.Remove("ShoppingCart");
            var books = db.Books.Include(b => b.section);
            List<Book> book = books.ToList();
            Session["Books"] = book;
            return View("OrderSuccess");
    
        }
        public ActionResult OrderSucc()
        {

            var query = db.OrdersBooks.Include("Book").Include("Book.Section").GroupBy(p => p.Book.Name)
                                                     .Select(g => new { name = g.Key, count = g.Sum(w => w.Quantity) }).
                                                     OrderByDescending(x => x.count).
                                                     Take(3).
                                                     ToList();



            return View(query);
        }
       
    }
}