using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Country
{
   public class CityDto
    {
        public string CityName { get; set; }
        public int Id { get; set; }
        public int? CountryId { get; set; }
    }
    public class CityWithCountryDto 
    {
        public string CityName { get; set; }
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
