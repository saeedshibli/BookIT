using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BookItt.Models;

namespace BookItt.Models
{
    public class OrderBook :DbContext
    {
        [Key][Column(Order=0)]
        public int OrderID { get; set; }
        [Key][Column(Order = 1)]
        public int BookID { get; set; }
        public double price { get; set; }
        public int Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Book Book { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Book> Books { get; set; }
    }
}