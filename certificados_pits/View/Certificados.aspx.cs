using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Threading.Tasks;
using Datos.Modelo;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Data;
//using System.Reflection.Metadata;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace certificados_pits.View
{
    public partial class Certificados : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    Modelo entity = new Modelo();
                    var documento = Request.QueryString["documento"];
                    var numero_contrato = Request.QueryString["numero_contrato"];

                    var persona = (from i in entity.Persona
                                   where i.documento == documento
                                   select i).FirstOrDefault();

                    var contrato = (from i in entity.Contrato
                                    where i.id_persona == persona.id && i.numero_contrato == numero_contrato
                                    select i).FirstOrDefault();

                    var convenio = (from i in entity.Convenio
                                    where i.id == contrato.id_convenio
                                    select i).FirstOrDefault();


                    using (MemoryStream ms = new MemoryStream())
                    {
                        Document document = new Document(PageSize.A4);
                        document.SetMargins(55f, 55f, 120f, 20f);
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);
                        iTextSharp.text.Image imageHeader = iTextSharp.text.Image.GetInstance(Server.MapPath("/images/nueva_cabecera.png"));
                        
                        imageHeader.SetAbsolutePosition(0f, 720f);
                        imageHeader.ScalePercent(58f);

                        iTextSharp.text.Image imageFooter = iTextSharp.text.Image.GetInstance(Server.MapPath("/images/pie_pagina2.png"));
                        imageFooter.SetAbsolutePosition(30f, 0f);
                        imageFooter.ScalePercent(58f);

                        document.Open();
                        document.Add(imageHeader);
                        document.Add(imageFooter);
                        //imageHeader.SetAbsolutePosition(0, 0);
                        //imageFooter.SetAbsolutePosition(0, 0);

                        //PdfContentByte cbhead = writer.DirectContent;
                        //PdfTemplate tp = cbhead.CreateTemplate(273, 95);
                        //tp.AddImage(imageHeader);

                        //PdfContentByte cbfoot = writer.DirectContent;
                        //PdfTemplate tpl = cbfoot.CreateTemplate(273, 95);
                        //tpl.AddImage(imageFooter);

                        //cbhead.AddTemplate(tp, 0, 842 - 95);
                        //cbfoot.AddTemplate(tpl, 595 - 273, 0);

                        BaseFont bf_title = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Font font_title = new Font(bf_title, 13, Font.BOLD);
                        Paragraph p_title = new Paragraph(null, font_title);
                        p_title.Add("DIRECCIÓN DE EXTENSIÓN Y PROYECCIÓN SOCIAL");
                        p_title.SetLeading(1, 1);
                        p_title.Alignment = Element.ALIGN_CENTER;
                        document.Add(p_title);

                        document.Add(Chunk.NEWLINE);

                        

                        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                        Font font = new Font(bf, 11, Font.NORMAL);
                        Paragraph p = new Paragraph(null,font);
                        p.Add("Que en virtud del Contrato Interadministrativo número " + convenio.codigo_convenio + ". El (La) Señor (a) " + persona.nombre + " con cédula " + persona.documento + ", subscribió un contrato por prestación de servicios (" + contrato.numero_contrato + ") desde el " + contrato.fecha_inicio.Value.Date.ToString("dd/MM/yyyy") + " hasta el " + contrato.fecha_fin.Value.Date.ToString("dd/MM/yyyy") + ".");
                        p.SetLeading(1, 1);
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        
                        document.Add(p);

                        document.Add(Chunk.NEWLINE);

                        p = new Paragraph(null, font);
                        p.Add("Honorarios mensuales " + contrato.honorarios_letras + "($" + contrato.honorarios.ToString() + ").");
                        p.SetLeading(1, 1);
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(p);

                        document.Add(Chunk.NEWLINE);

                        p = new Paragraph(null, font);
                        p.Add("Obeto: " + contrato.objeto + ".");
                        p.SetLeading(1, 1);
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(p);

                        document.Add(Chunk.NEWLINE);

                        p = new Paragraph(null, font);
                        p.Add("Actividades: " + contrato.actividades + ".");
                        p.SetLeading(1, 1);
                        p.Alignment = Element.ALIGN_JUSTIFIED;
                        document.Add(p);

                        document.Close();
                        writer.Close();
                        Response.ContentType = "pdf/application";
                        Response.AddHeader("content-disposition",
                        "attachment;filename= certificado_" + contrato.numero_contrato + ".pdf");
                        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    }


                    //Warning[] warnings;
                    //string[] streamIds;
                    //string mimeType = string.Empty;
                    //string encoding = "utf-8";
                    //string extension = string.Empty;

                    //Modelo entity = new Modelo();
                    //var documento = Request.QueryString["documento"];
                    //var numero_contrato = Request.QueryString["numero_contrato"];

                    //var persona = (from i in entity.Persona
                    //               where i.documento == documento
                    //               select i).FirstOrDefault();

                    //var contrato = (from i in entity.Contrato
                    //                where i.id_persona == persona.id && i.numero_contrato == numero_contrato
                    //                select i).FirstOrDefault();

                    //var convenio = (from i in entity.Convenio
                    //                where i.id == contrato.id_convenio
                    //                select i).FirstOrDefault();

                    //var ruta = "View/Reportes/CertificadoL.rdlc";
                    //ReporteCertificado.Reset();
                    //ReporteCertificado.LocalReport.DataSources.Clear();
                    //ReporteCertificado.ProcessingMode = ProcessingMode.Local;
                    //ReporteCertificado.LocalReport.ReportPath = ruta;

                    ////ReportParameter[] parameters = new ReportParameter[10];
                    ////parameters[0] = new ReportParameter("convenio", convenio.codigo_convenio);
                    ////parameters[1] = new ReportParameter("nombre", persona.nombre);
                    ////parameters[2] = new ReportParameter("documento", persona.documento);
                    ////parameters[3] = new ReportParameter("contrato", contrato.numero_contrato);
                    ////parameters[4] = new ReportParameter("fecha_inicio", contrato.fecha_inicio.ToString());
                    ////parameters[5] = new ReportParameter("fecha_fin", contrato.fecha_fin.ToString());
                    ////parameters[6] = new ReportParameter("honorarios", contrato.honorarios.ToString());
                    ////parameters[7] = new ReportParameter("objeto", contrato.objeto);
                    ////parameters[8] = new ReportParameter("actividades", contrato.actividades);
                    ////parameters[9] = new ReportParameter("fecha_generacion_certificado", "1 de diciembre de 2019");

                    ////ReporteCertificado.LocalReport.SetParameters(parameters);

                    //var texto_certificado = "Que en virtud del Contrato Interadministrativo número " + convenio.codigo_convenio + ". El (La) Señor (a)" + persona.nombre + " con cédula " + persona.documento + ", subscribió un contrato por prestación de servicios (" + contrato.honorarios.ToString() + ") desde el " + contrato.fecha_inicio.ToString() + " hasta el " + contrato.fecha_fin.ToString() + ".";
                    //var texto_organizado = GetTextJustify(texto_certificado, 85);

                    //DataTable dt2 = new DataTable();
                    //dt2.Columns.Add("texto");

                    //DataRow row;
                    //foreach (var item in texto_organizado)
                    //{
                    //    row = dt2.NewRow();
                    //    row["texto"] = item;
                    //    dt2.Rows.Add(row);
                    //}

                    //row = dt2.NewRow();
                    //row["texto"] = "El ingreso mensual es de($" + contrato.honorarios.ToString() + ")";
                    //dt2.Rows.Add(row);



                    //var texto_certificado_objeto = "Obeto: " + contrato.objeto + ".";
                    //var lista_texto_objeto = GetTextJustify(texto_certificado_objeto, 85);
                    //foreach (var item in lista_texto_objeto)
                    //{
                    //    row = dt2.NewRow();
                    //    row["texto"] = item;
                    //    dt2.Rows.Add(row);
                    //}

                    //var texto_certificado_actividades = "Actividades " + contrato.actividades + ".";
                    //var lista_texto_actividades = GetTextJustify(texto_certificado_actividades, 85);
                    //foreach (var item in lista_texto_actividades)
                    //{
                    //    row = dt2.NewRow();
                    //    row["texto"] = item;
                    //    dt2.Rows.Add(row);
                    //}

                    //row = dt2.NewRow();
                    //row["texto"] = "Cualquier inquietud con gusto será atendida en el 4480520 Ext 1052.";
                    //dt2.Rows.Add(row);

                    //row = dt2.NewRow();
                    //row["texto"] = "La presente certificación se expide a solicitud del interesado(a).";
                    //dt2.Rows.Add(row);


                    //ReportDataSource DataSource = new ReportDataSource();
                    //DataSource.Name = "DataSet2";
                    //DataSource.Value = dt2;
                    //ReporteCertificado.LocalReport.DataSources.Add(DataSource);



                    //DataTable dt = new DataTable();
                    //dt.Columns.Add("codigo_convenio");
                    //dt.Columns.Add("nombre");
                    //dt.Columns.Add("documento");
                    //dt.Columns.Add("numero_contrato");
                    //dt.Columns.Add("fecha_inicio");
                    //dt.Columns.Add("fecha_fin");
                    //dt.Columns.Add("honorarios");
                    //dt.Columns.Add("objeto");
                    //dt.Columns.Add("actividades");
                    //dt.Columns.Add("fecha_generacion_certificado");

                    //DataRow dr = dt.NewRow();
                    //dr["codigo_convenio"] = convenio.codigo_convenio;
                    //dr["nombre"] = persona.nombre;
                    //dr["documento"] = persona.documento;
                    //dr["numero_contrato"] = contrato.numero_contrato;
                    //dr["fecha_inicio"] = contrato.fecha_inicio.ToString();
                    //dr["fecha_fin"] = contrato.fecha_fin.ToString();
                    //dr["honorarios"] = contrato.honorarios.ToString();
                    //dr["objeto"] = contrato.objeto;
                    //dr["actividades"] = contrato.actividades;
                    //dr["fecha_generacion_certificado"] = "5 de diciembre de 2019";
                    //dt.Rows.Add(dr);

                    //ReportDataSource DataSource2 = new ReportDataSource();
                    //DataSource2.Name = "DataSet1";
                    //DataSource2.Value = dt;
                    //ReporteCertificado.LocalReport.DataSources.Add(DataSource2);

                    ////ReporteCertificado.DataBind();
                    ////ReporteCertificado.ShowPrintButton = true;
                    ////ReporteCertificado.LocalReport.Refresh();
                    //byte[] bytes = ReporteCertificado.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    //// byte[] bytes = viewer.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    //// Now that you have all the bytes representing the PDF report, buffer it and send it to the client.          
                    //// System.Web.HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Buffer = true;
                    //Response.Clear();
                    //Response.ContentType = mimeType;
                    //Response.AddHeader("content-disposition", "attachment; filename= certificado_" + contrato.numero_contrato + "." + extension);
                    //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
                    //Response.Flush(); // send it to the client to download  
                    //Response.End();
                }
            }
            catch (Exception exc)
            {

                throw exc;
            }

        }

        public static List<string> GetTextJustify(string text, int width)
        {
            string[] palabras = text.Split(' ');
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            int length = palabras.Length;
            List<string> resultado = new List<string>();
            for (int i = 0; i < length; i++)
            {
                sb1.AppendFormat("{0} ", palabras[i]);
                if (sb1.ToString().Length > width)
                {
                    resultado.Add(sb2.ToString());
                    sb1 = new StringBuilder();
                    sb2 = new StringBuilder();
                    i--;
                }
                else
                {
                    sb2.AppendFormat("{0} ", palabras[i]);
                }
            }
            resultado.Add(sb2.ToString());

            List<string> resultado2 = new List<string>();
            string temp;

            int index1, index2, salto;
            string target;
            int limite = resultado.Count;
            foreach (var item in resultado)
            {
                target = " ";
                temp = item.ToString().Trim();
                index1 = 0; index2 = 0; salto = 2;

                if (limite <= 1)
                {
                    resultado2.Add(temp);
                    break;
                }
                while (temp.Length <= width)
                {
                    if (temp.IndexOf(target, index2) < 0)
                    {
                        index1 = 0; index2 = 0;
                        target = target + " ";
                        salto++;
                    }
                    index1 = temp.IndexOf(target, index2);
                    temp = temp.Insert(temp.IndexOf(target, index2), " ");
                    index2 = index1 + salto;

                }
                limite--;
                resultado2.Add(temp);
            }
            return resultado2;
        }
    }
}