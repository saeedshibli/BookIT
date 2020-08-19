using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookItt.Models
{
    public class ShoppingCart
    {
        public Book book { get; set; }
        public int Quantity { get; set; }
        public ShoppingCart(Book book,int Quantity)
        {
            this.book = book;
            this.Quantity = Quantity;
        }
    }
}