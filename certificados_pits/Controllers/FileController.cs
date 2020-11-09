using Datos.Modelo;
using LogicaNegocio.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using LinqToExcel;
using System.Data.SqlClient;
using System.Data;
using System.Data.Entity.Migrations;

namespace certificados_pits.Controllers
{
    public class FileController : ApiController
    {

        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            Modelo entity = new Modelo();
            try
            {   
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var fileSavePath = string.Empty;

                    var docfiles = new List<string>();

                    var URLArchivo = "";

                    foreach (string file in httpRequest.Files)
                    {

                        var postedFile = httpRequest.Files[file];
                        //var filePath = HttpContext.Current.Server.MapPath("/UploadedFiles/");
                        var filePath = "C:/UploadedFiles/";

                        var GUID = Guid.NewGuid().ToString();

                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }

                        fileSavePath = Path.Combine(filePath, GUID + "." + postedFile.FileName.Split('.')[1]);

                        postedFile.SaveAs(fileSavePath);

                        docfiles.Add(filePath);

                        URLArchivo = "C:/UploadedFiles/" + GUID + "." + postedFile.FileName.Split('.')[1];


                        string e = Path.GetExtension(URLArchivo);
                        if (e != ".xlsx")
                        {
                            return Ok(new { success = false, message = "La extencion del archivo no es valida" });
                        }

                    }

                    var book = new ExcelQueryFactory(URLArchivo);

                    var hoja = book.GetWorksheetNames();
                    var resultado = (from i in book.Worksheet(hoja.FirstOrDefault())
                                     select i).ToList();


                    ContratoBl ContratoBl = new ContratoBl();
                    
                    List<Persona> oListPersona = new List<Persona>();
                    PersonaBl PersonaBl = new PersonaBl();
                    List<Array> oListContrato_malos = new List<Array>();

                    foreach (var values in resultado)
                    {

                        var convenio = ContratoBl.Consultar_Convenio(values[1]);

                        if (convenio == null)
                        {
                            Convenio oConvenio = new Convenio();
                            oConvenio.codigo_convenio = values[1];
                            //oConvenio.anio = int.Parse(values[3]);

                            int anio1 = 0;
                            if (int.TryParse(values[0], out anio1))
                            {
                                oConvenio.anio = anio1;
                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[0].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;

                            }

                            oConvenio.nombre = values[2];

                            convenio = ContratoBl.Crear_Convenio(oConvenio);
                        }

                        Persona oPersona = new Persona();
                        Contrato oContrato = new Contrato();
                        oPersona.documento = values[4];
                        var persona = PersonaBl.ConsultarPersona(oPersona);

                        oPersona.tipo_documento = values[3];
                        oPersona.documento = values[4];
                        oPersona.nombre = values[5];

                        if (persona == null)
                        {
                            persona = PersonaBl.CrearPersona(oPersona);
                        }

                        int anio = 0;
                        if (int.TryParse(values[0], out anio))
                        {
                            oContrato.anio = anio;
                        }
                        else
                        {
                            string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[0].ToString() };
                            oListContrato_malos.Add(datos);
                            continue;

                        }

                        var contrato = ContratoBl.ConsultarContrato(values[9], convenio.id,anio);

                        if (contrato == null)
                        {
                            oContrato.numero_contrato = values[9];
                            oContrato.objeto = values[7];
                            oContrato.actividades = values[8];

                            //validar formato de double de honorarios
                            double result = 0;
                            if (double.TryParse(values[10], out result))
                            {
                                oContrato.honorarios = result;
                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[10].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;

                            }


                            oContrato.honorarios_letras = values[11];
                            //oContrato.duracion_dias = double.Parse(values[23]);
                            double duracion_dias = 0;
                            if (double.TryParse(values[23], out duracion_dias))
                            {
                                oContrato.duracion_dias = duracion_dias;

                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[23].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }
                            DateTime fecha_inicio = new DateTime();
                            if (DateTime.TryParse(values[19], out fecha_inicio))
                            {
                                oContrato.fecha_inicio = fecha_inicio;

                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[19].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }

                            DateTime fecha_fin = new DateTime();
                            if (DateTime.TryParse(values[21], out fecha_fin))
                            {
                                oContrato.fecha_fin = fecha_fin;

                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[21].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }
                            //oContrato.fecha_fin = values[21];
                            oContrato.fecha_terminacion = values[21];
                            oContrato.id_persona = persona.id;
                            oContrato.id_convenio = convenio.id;

                            entity.Contrato.Add(oContrato);
                            entity.SaveChanges();
                        }
                        else {
                            contrato.fecha_terminacion = values[21];
                            contrato.objeto = values[7];
                            contrato.actividades = values[8];
                            DateTime fecha_fin = new DateTime();
                            if (DateTime.TryParse(values[21], out fecha_fin))
                            {
                                contrato.fecha_fin = fecha_fin;

                            }
                            else
                            {
                                string[] datos = { values[9].ToString(), values[0].ToString(), values[4].ToString(), values[5].ToString(), values[21].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }
                            entity.Contrato.AddOrUpdate(contrato);
                            entity.SaveChanges();
                        }

                    }


                    return Ok(new { success = true, path = URLArchivo, data= oListContrato_malos });

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