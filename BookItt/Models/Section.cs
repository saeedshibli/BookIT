using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookItt.Models
{
    public class Section
    {
        
        public int SectionID { get; set; }
        [Display(Name = "Category")]
        public string Name  { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}