using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookItt.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }

        [EmailAddress]
        public string CustomerEmail { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; }
        //public int OrderBookID { get; set; }

        //public OrderBook OrderBook { get; set; }

    }
}