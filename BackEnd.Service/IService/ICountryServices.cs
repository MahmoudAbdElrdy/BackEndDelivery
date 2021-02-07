using BackEnd.Service.DTO.Country;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
   public interface ICountryServices
    {
        IResponseDTO Insert(CountryDto entity); 
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(CountryDto entity);
        IResponseDTO Remove(CountryDto entity);
        IResponseDTO GetAllNationality(int pageNumber = 0, int pageSize = 0); 
    }
}
