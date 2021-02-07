using BackEnd.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class SubCategory:Base
    {
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
       // public bool? Active { get; set; }
         
    }
}
