using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO;
using BackEnd.Service.DTO.Country;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        #region privateFild
        private ICountryServices ServicesCountry;
        #endregion

        #region CountryController(IServicesCountry _ServicesCountry)
        public CountryController(ICountryServices _ServicesCountry)
        {
            ServicesCountry = _ServicesCountry;
        }
        #endregion

        #region Get : api/Country/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesCountry.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
        #region Get : api/Country/GetAll
        [HttpGet("GetAllNationality")]
        public IResponseDTO GetAll()
        {
            var result = ServicesCountry.GetAllNationality(0,0);
            return result;
        }
        #endregion


        #region Get : api/Country/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesCountry.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Country/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]CountryDto model)
        {

            var result = ServicesCountry.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Country/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesCountry.Delete(id);
            return result;
        }
        #endregion

        //#region Put : api/Country/Remove
        //[HttpPut("Remove")]
        //public IResponseDTO Remove([FromBody] CountryDto model)
        //{
        //    var result = ServicesCountry.Remove(model);
        //    return result;
        //}
        //#endregion

        #region Post : api/Country/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] CountryDto model)
        {
            var result =  ServicesCountry.Insert(model);
            return result;
        }
        #endregion
    }
}
