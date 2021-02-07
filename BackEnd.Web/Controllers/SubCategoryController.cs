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
    public class SubCategoryController : ControllerBase
    {
        #region privateFild
        private ISubCategoryServices ServicesSubCategory;
        #endregion

        #region SubCategoryController(IServicesSubCategory _ServicesSubCategory)
        public SubCategoryController(ISubCategoryServices _ServicesSubCategory)
        {
            ServicesSubCategory = _ServicesSubCategory;
        }
        #endregion

        #region Get : api/SubCategory/GetAll
        [HttpGet("GetPage")]
        public IResponseDTO GetPage(int pageNumber = 0, int pageSize =0)
        {
            var result = ServicesSubCategory.GetAll(pageNumber, pageSize);
            return result;
        }
        #endregion
      
        #region Get : api/SubCategory/GetById
        [HttpGet("GetById")]
        public IResponseDTO GetById(int id)
        {
            var result = ServicesSubCategory.GetByIdAsync(id);
            return result;
        }
        #endregion

        #region Put : api/SubCategory/Update
        [HttpPut("Update")]
        public IResponseDTO Update([FromBody]SubCategoryDto model)
        {

            var result = ServicesSubCategory.Update(model);
            return result;
        }
        #endregion

        #region Delete : api/SubCategory/Delete
        [HttpDelete("Delete")]
        public IResponseDTO Delete(int id)
        {
            var result = ServicesSubCategory.Delete(id);
            return result;
        }
        #endregion

       
        #region Post : api/SubCategory/SaveNew
        [HttpPost("SaveNew")]
        public IResponseDTO SaveNew([FromBody] SubCategoryDto model)
        {
            var result =  ServicesSubCategory.Insert(model);
            return result;
        }
        #endregion
        #region Get : api/City/GetAll
        [HttpGet("GetAllByCategory")]
        public IResponseDTO GetAllByCategory(int CategoryId, int pageNumber = 0, int pageSize = 0)
        {
            var result = ServicesSubCategory.GetAllByCategory(CategoryId, pageNumber, pageSize);
            return result;
        }
        #endregion
    }
}
