using BackEnd.Service.DTO.CategoriesDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public  interface ISubCategoryServices
    {
        IResponseDTO Insert(SubCategoryDto entity);
        IResponseDTO GetAllByCategory(int CategoryId, int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(SubCategoryDto entity);
        IResponseDTO Remove(SubCategoryDto entity);
    }
}
