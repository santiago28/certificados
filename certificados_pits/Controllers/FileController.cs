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



                    //DataTable dt2 = new DataTable();
                    //dt2.Columns.Add("codigo_convenio");
                    //dt2.Columns.Add("numero_contrato");
                    //dt2.Columns.Add("anio");
                    //dt2.Columns.Add("tipo_documento");
                    //dt2.Columns.Add("documento");
                    //dt2.Columns.Add("nombre");
                    //dt2.Columns.Add("objeto");
                    //dt2.Columns.Add("actividades");
                    //dt2.Columns.Add("honorarios");
                    //dt2.Columns.Add("honorarios_letras");
                    //dt2.Columns.Add("duracion_dias");
                    //dt2.Columns.Add("fecha_inicio");
                    //dt2.Columns.Add("fecha_real_terminacion");


                    //foreach (var values in resultado)
                    //{
                    //    DataRow row;
                    //    row = dt2.NewRow();
                    //    var d = values[0];
                    //    row["codigo_convenio"] = values[0];
                    //    row["numero_contrato"] = values[2];
                    //    row["anio"] = values[3];
                    //    row["tipo_documento"] = values[4];
                    //    row["documento"] = values[5];
                    //    row["nombre"] = values[6];
                    //    row["objeto"] = values[7];
                    //    row["actividades"] = values[8];
                    //    row["honorarios"] = values[10];
                    //    row["honorarios_letras"] = values[11];
                    //    row["duracion_dias"] = values[17];
                    //    row["fecha_inicio"] = values[18];
                    //    row["fecha_real_terminacion"] = values[21];
                    //    dt2.Rows.Add(row);
                    //}

                    ////Se define el parametro tipo estructura para enviar el datatable como parametro al procedimiento almacenado
                    //var parametro = new SqlParameter("@datos", System.Data.SqlDbType.Structured);
                    //parametro.Value = dt2;
                    //parametro.TypeName = "dbo.DatosExcel";

                    //using (entity = new Modelo())
                    //{
                    //    entity.Database.CommandTimeout = 0;
                    //    entity.Database.ExecuteSqlCommand("exec spInsertar_DatosExcel @datos", parametro);

                    //}


                    List<Persona> oListPersona = new List<Persona>();
                    PersonaBl PersonaBl = new PersonaBl();
                    List<Array> oListContrato_malos = new List<Array>();

                    foreach (var values in resultado)
                    {

                        var convenio = ContratoBl.Consultar_Convenio(values[0]);

                        if (convenio == null)
                        {
                            Convenio oConvenio = new Convenio();
                            oConvenio.codigo_convenio = values[0];
                            //oConvenio.anio = int.Parse(values[3]);

                            int anio = 0;
                            if (int.TryParse(values[3], out anio))
                            {
                                oConvenio.anio = anio;
                            }
                            else
                            {
                                string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[3].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;

                            }

                            oConvenio.nombre = values[1];

                            convenio = ContratoBl.Crear_Convenio(oConvenio);
                        }

                        Persona oPersona = new Persona();
                        Contrato oContrato = new Contrato();
                        oPersona.documento = values[5];
                        var persona = PersonaBl.ConsultarPersona(oPersona);

                        oPersona.tipo_documento = values[4];
                        oPersona.documento = values[5];
                        oPersona.nombre = values[6];

                        if (persona == null)
                        {
                            persona = PersonaBl.CrearPersona(oPersona);
                        }

                        var contrato = ContratoBl.ConsultarContrato(values[2], convenio.id, int.Parse(values[3]));
                        if (contrato == null)
                        {
                            oContrato.numero_contrato = values[2];
                            oContrato.objeto = values[7];
                            oContrato.actividades = values[8];

                            double result = 0;
                            if (double.TryParse(values[10], out result))
                            {
                                oContrato.honorarios = result;
                            }
                            else
                            {
                                string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[10].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;

                            }

                            int anio = 0;
                            if (int.TryParse(values[3], out anio))
                            {
                                oContrato.anio = anio;
                            }
                            else
                            {
                                string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[3].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;

                            }

                            oContrato.honorarios_letras = values[11];
                            //oContrato.duracion_dias = double.Parse(values[17]);
                            double duracion_dias = 0;
                            if (double.TryParse(values[17], out duracion_dias))
                            {
                                oContrato.duracion_dias = duracion_dias;

                            }
                            else
                            {
                                string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[17].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }
                            DateTime fecha_inicio = new DateTime();
                            if (DateTime.TryParse(values[18], out fecha_inicio))
                            {
                                oContrato.fecha_inicio = fecha_inicio;

                            }
                            else
                            {
                                string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[18].ToString() };
                                oListContrato_malos.Add(datos);
                                continue;
                            }


                            //DateTime fecha_fin = new DateTime();
                            //if (DateTime.TryParse(values[21], out fecha_fin))
                            //{
                            //    oContrato.fecha_fin = fecha_fin;

                            //}
                            //else
                            //{
                            //    string[] datos = { values[2].ToString(), values[3].ToString(), values[5].ToString(), values[6].ToString(), values[21].ToString() };
                            //    oListContrato_malos.Add(datos);
                            //    continue;
                            //}


                            //oContrato.fecha_terminacion = DateTime.Parse(values[21]);
                            oContrato.fecha_fin = values[21];
                            oContrato.fecha_terminacion = values[21];
                            oContrato.id_persona = persona.id;
                            oContrato.id_convenio = convenio.id;

                            entity.Contrato.Add(oContrato);
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