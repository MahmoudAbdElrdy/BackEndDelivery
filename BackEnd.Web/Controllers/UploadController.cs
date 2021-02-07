using BackEnd.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost("UploadImage")]
        public IActionResult Upload()
        {
            ResponseDTO res;
            try
            {
                var name = Helper.UploadHelper.SaveFile(Request.Form.Files[0], "File");
                //string path = xx[0];
                res = new ResponseDTO()
                {
                    Code = 200,
                    Message = "",
                    Data = name,
                };
            }
            catch (Exception ex)
            {
                res = new ResponseDTO()
                {
                    Code = 400,
                    Message = "Error " + ex.Message,
                    Data = null,
                };
            }
            return Ok(res);
        }
    }
}
