using BackEnd.Service.DTO.Country;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.IService
{
  public  interface ICityServices
    {
        IResponseDTO Insert(CityDto entity);
        IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetAllByCountry(int CountryId, int pageNumber = 0, int pageSize = 0);
        IResponseDTO GetByIdAsync(int? id);
        IResponseDTO Delete(int id);
        IResponseDTO Update(CityDto entity);
        IResponseDTO Remove(CityDto entity);
    }
}
