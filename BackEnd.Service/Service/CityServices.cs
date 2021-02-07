using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.Country;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class CityServices : BaseServices, ICityServices
    {

        #region ServicesCity(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public CityServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion


        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.City.Get(x => x.IsDelete == false, includeProperties: "Country", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<CityWithCountryDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
        #region GetAll()
        public IResponseDTO GetAllByCountry(int CountryId, int pageNumber = 0, int pageSize = 0) 
        {
            try
            {
                var result = _unitOfWork.City.Get(x => x.IsDelete == false&&x.CountryId== CountryId, includeProperties: "Country", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<CityWithCountryDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Remove(CityDto model)
        public IResponseDTO Remove(CityDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<City>(model);
                _unitOfWork.City.Delete(DBmodel);
                var save = _unitOfWork.Save();
                if (save == "200")
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Message = save;
                    _response.Code = 400;

                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region GetByIdAsync(Guid? id)
        public IResponseDTO GetByIdAsync(int? id)
        {
            try
            {
                var DBmodel = _unitOfWork.City.Get(x => x.Id == id && x.IsDelete == false, includeProperties: "Country").FirstOrDefault();
                if (DBmodel != null)
                {
                    var CityDto = _mapper.Map<CityWithCountryDto>(DBmodel);
                    _response.Data = CityDto;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "Not Data";
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region InsertAsync(CityDto model)
        public IResponseDTO Insert(CityDto model)
        {
            try
            {
                var Dto = _mapper.Map<City>(model);
                //  Dto.CreationDate = DateTime.Now;

                var DBmodel = _unitOfWork.City.Insert(Dto);

                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    var CityDto = _mapper.Map<CityDto>(Dto); ;
                    _response.Data = CityDto;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;
                    _response.Message = save;
                    _response.Code = 400;

                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Update(CityDto model)
        public IResponseDTO Update(CityDto model)
        {
            try
            {

                var DbCity = _mapper.Map<City>(model);
                DbCity.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.City.Update(DbCity);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = model;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;

                    _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion

        #region Delete(CityDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {

                var DbCity = _unitOfWork.City.GetByID(id);
                DbCity.IsDelete = true;
                DbCity.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.City.Update(DbCity);
                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    _response.Data = id;
                    _response.Code = 200;
                    _response.Message = "OK";
                }
                else
                {
                    _response.Data = null;

                    _response.Code = 400;
                    _response.Message = save;
                }
            }
            catch (Exception ex)
            {

                _response.Data = null;
                _response.Code = 400;
                _response.Message = ex.Message;
            }
            return _response;
        }
        #endregion
    }
}
