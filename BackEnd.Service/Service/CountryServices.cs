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
   public class CountryServices : BaseServices, ICountryServices
    {

        #region ServicesCountry(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public CountryServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Country.Get(x => x.IsDelete == false, page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<CountryDto>>(result);
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

 
        #region Remove(CountryDto model)
        public IResponseDTO Remove(CountryDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Country>(model);
                _unitOfWork.Country.Delete(DBmodel);
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
                var DBmodel = _unitOfWork.Country.Get(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var CountryDto = _mapper.Map<CountryDto>(DBmodel);
                    _response.Data = CountryDto;
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

        #region InsertAsync(CountryDto model)
        public  IResponseDTO Insert(CountryDto model)
        {
            try
            {
                var Dto = _mapper.Map<Country>(model);
              //  Dto.CreationDate = DateTime.Now;

                var DBmodel =  _unitOfWork.Country.Insert(Dto);

                var save =  _unitOfWork.Save();

                if (save == "200")
                {
                    var CountryDto = _mapper.Map<CountryDto>(Dto);
                    _response.Data = CountryDto;
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

        #region Update(CountryDto model)
        public IResponseDTO Update(CountryDto model)
        {
            try
            {
                
                var DbCountry = _mapper.Map<Country>(model);
                DbCountry.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Country.Update(DbCountry);
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

        #region Delete(CountryDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbCountry = _unitOfWork.Country.GetByID(id);
                DbCountry.IsDelete = true;
                DbCountry.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Country.Update(DbCountry);
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

        public IResponseDTO GetAllNationality(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Repository<Nationality>().Get(x => x.IsDelete == false, page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<NationalityDto>>(result);
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
    }
}
