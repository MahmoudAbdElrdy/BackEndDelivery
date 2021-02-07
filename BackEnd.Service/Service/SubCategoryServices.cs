using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Entities;
using BackEnd.Service.DTO.CategoriesDto;
using BackEnd.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class SubCategoryServices : BaseServices, ISubCategoryServices
    {

        #region ServicesSubCategory(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public SubCategoryServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion


        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.SubCategory.Get(x => x.IsDelete == false, includeProperties: "Category", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowSubCategoryDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                    _response.totalRowCount = _unitOfWork.SubCategory.Get(x => x.IsDelete == false).Count();

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
        public IResponseDTO GetAllByCategory(int CategoryId, int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.SubCategory.Get(x => x.IsDelete == false && x.CategoryId == CategoryId, includeProperties: "Category", page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<ShowSubCategoryDto>>(result);
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

        #region Remove(SubCategoryDto model)
        public IResponseDTO Remove(SubCategoryDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<SubCategory>(model);
                _unitOfWork.SubCategory.Delete(DBmodel);
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
                var DBmodel = _unitOfWork.SubCategory.Get(x => x.Id == id && x.IsDelete == false, includeProperties: "Category").FirstOrDefault();
                if (DBmodel != null)
                {
                    var SubCategoryDto = _mapper.Map<ShowSubCategoryDto>(DBmodel);
                    _response.Data = SubCategoryDto;
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

        #region InsertAsync(SubCategoryDto model)
        public IResponseDTO Insert(SubCategoryDto model)
        {
            try
            {
                var Dto = _mapper.Map<SubCategory>(model);
                //  Dto.CreationDate = DateTime.Now;

                var DBmodel = _unitOfWork.SubCategory.Insert(Dto);

                var save = _unitOfWork.Save();

                if (save == "200")
                {
                    var SubCategoryDto = _mapper.Map<SubCategoryDto>(Dto); ;
                    _response.Data = SubCategoryDto;
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

        #region Update(SubCategoryDto model)
        public IResponseDTO Update(SubCategoryDto model)
        {
            try
            {

                var DbSubCategory = _mapper.Map<SubCategory>(model);
                DbSubCategory.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.SubCategory.Update(DbSubCategory);
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

        #region Delete(SubCategoryDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {

                var DbSubCategory = _unitOfWork.SubCategory.GetByID(id);
                DbSubCategory.IsDelete = true;
                DbSubCategory.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.SubCategory.Update(DbSubCategory);
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
