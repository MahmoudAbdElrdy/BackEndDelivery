using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service.DTO.Country
{
   public class GeneralInformationDto
    {
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
      
    }
    public class ShowGeneralInformationDto 
    {
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; } 

    }
}
