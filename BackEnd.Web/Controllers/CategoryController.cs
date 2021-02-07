using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Service;
using BackEnd.Service.DTO;
using BackEnd.Service.DTO.CategoriesDto;
using BackEnd.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region privateFild
        private ICategoryServices ServicesCategory;
        #endregion

        #region CategoryController(IServicesCategory _ServicesCategory)
        public CategoryController(ICategoryServices _ServicesCategory)
        {
            ServicesCategory = _ServicesCategory;
        }
        #endregion

        #region Get : api/Category/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesCategory.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
      
        #region Get : api/Category/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesCategory.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/Category/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]CategoryDto model)
        {

            var result = ServicesCategory.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/Category/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesCategory.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/Category/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] CategoryDto model)
        {
            var result =  ServicesCategory.Insert(model);
            return result;
        }
        #endregion
    }
}
