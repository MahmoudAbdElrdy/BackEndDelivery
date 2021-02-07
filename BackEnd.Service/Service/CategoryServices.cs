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
   public class CategoryServices : BaseServices, ICategoryServices
    {

        #region ServicesCategory(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public CategoryServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork, responseDTO, mapper)
        {


        }
        #endregion

        #region GetAll()
        public IResponseDTO GetAll(int pageNumber = 0, int pageSize = 0)
        {
            try
            {
                var result = _unitOfWork.Category.Get(x => x.IsDelete == false, page: pageNumber, Take: pageSize).ToList();
                if (result != null && result.Count > 0)
                {
                    var resultList = _mapper.Map<List<CategoryDto>>(result);
                    _response.Data = resultList;
                    _response.Code = 200;
                    _response.Message = "OK";
                    _response.totalRowCount = _unitOfWork.Category.Get(x => x.IsDelete == false).Count();
                }
                else
                {
                    _response.Data = null;
                    _response.Code = 200;
                    _response.Message = "No Data";
                    _response.totalRowCount = 0;
                }
            }
            catch (Exception ex)
            {
             
                _response.Data = null;
               _response.Code = 400;
                _response.Message = ex.Message;
                _response.totalRowCount = 0;
            }
            return _response;
        }
        #endregion

 
        #region Remove(CategoryDto model)
        public IResponseDTO Remove(CategoryDto model)
        {
            try
            {
                var DBmodel = _mapper.Map<Category>(model);
                _unitOfWork.Category.Delete(DBmodel);
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
                var DBmodel = _unitOfWork.Category.Get(x => x.Id == id && x.IsDelete == false).FirstOrDefault();
                if (DBmodel != null)
                {
                    var CategoryDto = _mapper.Map<CategoryDto>(DBmodel);
                    _response.Data = CategoryDto;
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

        #region InsertAsync(CategoryDto model)
        public  IResponseDTO Insert(CategoryDto model)
        {
            try
            {
                var Dto = _mapper.Map<Category>(model);
              //  Dto.CreationDate = DateTime.Now;

                var DBmodel =  _unitOfWork.Category.Insert(Dto);

                var save =  _unitOfWork.Save();

                if (save == "200")
                {
                    var CategoryDto = _mapper.Map<CategoryDto>(Dto);
                    _response.Data = CategoryDto;
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

        #region Update(CategoryDto model)
        public IResponseDTO Update(CategoryDto model)
        {
            try
            {
                
                var DbCategory = _mapper.Map<Category>(model);
                DbCategory.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Category.Update(DbCategory);
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

        #region Delete(CategoryDto model)
        public IResponseDTO Delete(int id)
        {
            try
            {
               
                var DbCategory = _unitOfWork.Category.GetByID(id);
                DbCategory.IsDelete = true;
                DbCategory.LastEditDate = DateTime.UtcNow.AddHours(2);
                _unitOfWork.Category.Update(DbCategory);
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

       

       
        public IResponseDTO GetAvailableCategoryWithSupCategory()
        {
            throw new NotImplementedException();
        }

       

       
        #endregion
    }
}
