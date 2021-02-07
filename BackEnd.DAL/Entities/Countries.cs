using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
   public class Country: Base
    {
        public string  CountryName { get; set; }
        public  ICollection<City> City { get; set; }
      
    }
}
