using BackEnd.Service.DTO;
using BackEnd.Service.DTO.CategoriesDto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
    public interface ICategoryServices 
    {
        IResponseDTO Insert(CategoryDto entity);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(CategoryDto entity);
        IResponseDTO Remove(CategoryDto entity);
    }
}
