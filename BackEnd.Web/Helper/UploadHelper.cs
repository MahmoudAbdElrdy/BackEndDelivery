using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Web.Helper
{
    public static class UploadHelper
    {
        // Upload Image,File To Existing Folder 
        public static string SaveFile(Microsoft.AspNetCore.Http.IFormFile formFile, string FristName)
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            List<string> _extentions = new List<string>() {  ".PNG", ".JPG", ".JPEG", ".BMP", ".GIF", ".OGG", //etc
                                                             ".WAV", ".MID", ".MIDI", ".WMA", ".MP3", ".RMA", //etc
                                                             ".AVI", ".MP4", ".DIVX", ".WMV" };
            try
            {
                var file = formFile;
                var folderName = Path.Combine("wwwroot/UploadFiles");
                // Check if directory exist or not
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {


                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var _imgname = FristName + DateTime.Now.Ticks;
                    // check valid extension
                    #region  check size

                    string extension = Path.GetExtension(file.FileName);

                    //extension= extension.Substring(1, extension.Length-1);
                    var size = file.Length;
                    if (!_extentions.Contains(extension.ToUpper()))
                    {
                        temp.Add("dbPath", "");
                        temp.Add("_ext", "");
                        temp.Add("stat", "File extension is not valid.");
                        //return new {path="",extention="",state= "File extension is not valid." };
                        return "!!امتداد الملف غير صحيح";
                    }

                    if (size > (1000 * 1024 * 1024))
                        return "حجم الملف اكبر من 5 ميجا بايت";
                    #endregion

                    // Updated To GetFileName By Elgendy
                    var _ext = Path.GetFileNameWithoutExtension(file.FileName);

                    //  var fullPath = Path.Combine(pathToSave, _imgname +_ext);
                    var filepath = Path.Combine(folderName, _imgname + extension);

                    using (var stream = new System.IO.FileStream(filepath, FileMode.Create))

                    {
                        file.CopyTo(stream);
                    }
                    string dbPath = "http://gssorapp-001-site1.htempurl.com/wwwroot/UploadFiles/" + _imgname + extension;
                    temp.Add("dbPath", dbPath);
                    temp.Add("_ext", _ext);
                    temp.Add("stat", "done");
                    //return new { path = dbPath, extention = _ext, state = "done." };

                    return dbPath;

                }
                else
                {
                    temp.Add("dbPath", "");
                    temp.Add("_ext", "");
                    temp.Add("stat", "BadRequest");
                    //return new { path = "", extention = "", state = "BadRequest" };

                    return "BadRequest";
                }
            }
            catch (Exception ex)
            {
                temp.Add("dbPath", "");
                temp.Add("_ext", "");
                temp.Add("stat", "Internal server error" + ex.ToString());
                //return new { path = "", extention = "", state = "Internal server error" + ex.ToString() };

                return "خطا في السيرفر";
            }
        }

    }
}
