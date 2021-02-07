using System;
using System.Collections.Generic;
using System.Text;
namespace BackEnd.Service
{
    public interface IResponseDTO
    {
        #region Public Properties
       // bool IsPassed { get; set; }
     
        string Message { get; set; }
        dynamic Data { get; set; }
        int Code { get; set; }
        int? totalRowCount { get; set; }
        #endregion
    }
    public class ResponseDTO : IResponseDTO
    {
        public ResponseDTO()
        {
           // IsPassed = false;
          
            Message = "";
        }
       // public bool IsPassed { get; set; }

    

        public string Message { get; set; } = "";
        public int Code { get; set; } = 400;

        public dynamic Data { get; set; }
        public int? totalRowCount { get; set; } = 0;

       //// public void Copy(ResponseDTO x)
       // {
       //    // IsPassed = x.IsPassed;

        //     Data = x.Data;
        //     Message = x.Message;
        // }
    }
}
