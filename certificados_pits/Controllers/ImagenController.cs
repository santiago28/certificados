using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace certificados_pits.Controllers
{
    public class ImagenController : ApiController
    {
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            try
            {
                var HttpRequest = HttpContext.Current.Request;
                if (HttpRequest.Files.Count > 0)
                {
                    var FileSavePath = string.Empty;
                    var Docfiles = new List<string>();
                    var URLArchivo = "";

                    foreach (string file in HttpRequest.Files)
                    {

                        var postedFile = HttpRequest.Files[file];
                        var filePath = HttpContext.Current.Server.MapPath("~/UploadedFiles/");

                        var GUID = Guid.NewGuid().ToString();

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        //FileSavePath = Path.Combine(filePath, GUID + "." + postedFile.FileName.Split('.')[1]);
                        var fecha_actual = DateTime.Now.ToShortDateString().Split('/'); //22/12/2019
                        var hora_actual = DateTime.Now.ToShortTimeString().Split(':'); //18-46
                        var FileName = postedFile.FileName.Split('.')[0] + fecha_actual[2] + fecha_actual[1] + fecha_actual[0] + hora_actual[0] + hora_actual[1] + "." + postedFile.FileName.Split('.')[1];
                        FileSavePath = Path.Combine(filePath, FileName);

                        postedFile.SaveAs(FileSavePath);

                        Docfiles.Add(filePath);

                        URLArchivo = FileName;
                    }
                    return Ok(new { success = true, path = URLArchivo });
                }
                else
                {
                    return Ok(new { success = false, message = "No File" });
                }
            }
            catch (Exception exc)
            {
                return Ok(new { success = false, message = exc.Message });
            }
        }

    }
}