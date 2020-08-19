using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BookItt.Models
{
    public class Book
    {
        public string Name { get; set; }
        public int BookID { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Display(Name = "Date Of Publish")]
        public string Date { get; set; }
        public int SectionID { get; set; }
        
        public Section section { get; set; }
      
        public string Image { get; set; }
        public virtual ICollection<OrderBook> OrderBooks { get; set; }

    }
}