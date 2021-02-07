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
    public class CityController : ControllerBase
    {
        #region privateFild
        private ICityServices ServicesCity;
        #endregion

        #region CityController(IServicesCity _ServicesCity)
        public CityController(ICityServices _ServicesCity)
        {
            ServicesCity = _ServicesCity;
        }
        #endregion
        #region Get : api/City/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize = 0)
        {
            var result = ServicesCity.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
        #region Get : api/City/GetAll
        [HttpGet("GetAllByCountry")]
        public IResponseDTO GetAllByCountry(int CountryId, int pageNumber = 0, int pageSize = 0)
        {
            var result = ServicesCity.GetAllByCountry(CountryId,pageNumber, pageSize);
            return result;
        }
        #endregion
        #region Get : api/City/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesCity.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/City/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]CityDto model)
        {

            var result = ServicesCity.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/City/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesCity.Delete(id);
            return result;
        }
        #endregion

        #region Post : api/City/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] CityDto model)
        {
            var result =  ServicesCity.Insert(model);
            return result;
        }
        #endregion
    }
}
