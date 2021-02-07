using AutoMapper;
using BackEnd.BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Service
{
    public class BaseServices
    {
        #region protected
        protected IUnitOfWork _unitOfWork;
        protected IResponseDTO _response;
        protected IMapper _mapper;
        #endregion

        #region BaseServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        public BaseServices(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = responseDTO;       
        }
        #endregion 
    }
}
