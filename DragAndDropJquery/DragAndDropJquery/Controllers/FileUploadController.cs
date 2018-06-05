using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DragAndDropJquery.Controllers
{
    public class FileUploadController : Controller
    {
        private const string TempPath = @"E:\Start Up\DragAndDropJquery\DragAndDropJquery\Video";

        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload(FormCollection fc)
        {
            string un = fc["username"];
            bool isSavedSuccessfully = true;
            string fName = string.Empty,extension=string.Empty;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    fName = file.FileName;
                    extension = Path.GetExtension(file.FileName);
                    if (file != null && file.ContentLength > 0)
                    {
                        var path = Path.Combine(Server.MapPath("~/Video"));
                        string pathString = System.IO.Path.Combine(path.ToString());
                        var fileName1 = new Guid().ToString();//Path.GetFileName(file.FileName + );
                        bool isExists = System.IO.Directory.Exists(pathString);
                        if (!isExists) System.IO.Directory.CreateDirectory(pathString);
                        var uploadpath = string.Format("{0}\\{1}", pathString, fileName1);
                        file.SaveAs(uploadpath);
                    }
                }
            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }
            if (isSavedSuccessfully)
            {
                return Json(new
                {
                    Message = fName
                });
            }
            else
            {
                return Json(new
                {
                    Message = "Error in saving file"
                });
            }
        }

        //[HttpPost]
        //public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        //{
        //    foreach (HttpPostedFileBase file in Request.Files)
        //    {
        //        string filePath = Path.Combine(TempPath, file.FileName);
        //        System.IO.File.WriteAllBytes(filePath, ReadData(file.InputStream));
        //    }

        //    return Json("All files have been successfully stored.");
        //}

        //private byte[] ReadData(Stream stream)
        //{
        //    byte[] buffer = new byte[16 * 1024];

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }

        //        return ms.ToArray();
        //    }
        //}
    }
}