using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DAL.Entities
{
  public  class City : Base 
    {
        public string CityName { get; set; }
        public int? CountryId{ get; set; } 
       public Country Country { get; set; }

    }
}
