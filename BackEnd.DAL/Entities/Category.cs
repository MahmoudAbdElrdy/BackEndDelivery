using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Category:Base
    {
        public string CategoryName{ get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
