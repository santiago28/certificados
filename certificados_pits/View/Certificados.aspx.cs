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

                    if (documento != "" && numero_contrato != "")
                    {

                        var persona = (from i in entity.Persona
                                       where i.documento == documento
                                       select i).FirstOrDefault();

                        var contrato = (from i in entity.Contrato
                                        where i.id_persona == persona.id && i.numero_contrato == numero_contrato
                                        select i).FirstOrDefault();

                        var convenio = (from i in entity.Convenio
                                        where i.id == contrato.id_convenio
                                        select i).FirstOrDefault();

                        var parametros = (from i in entity.Parametros
                                          select i).ToList();

                        using (MemoryStream ms = new MemoryStream())
                        {
                            Document document = new Document(PageSize.A4);
                            document.SetMargins(55f, 55f, 120f, 20f);
                            PdfWriter writer = PdfWriter.GetInstance(document, ms);
                            iTextSharp.text.Image imageHeader = iTextSharp.text.Image.GetInstance(Server.MapPath("/UploadedFiles/"+ parametros.Where(i => i.parametro == "encabezado").FirstOrDefault().valor));

                            var scalePercent = (((document.PageSize.Width / imageHeader.Width) * 100) - 4);

                            imageHeader.SetAbsolutePosition(0f, 720f);
                            imageHeader.ScalePercent(scalePercent);

                            iTextSharp.text.Image imageFooter = iTextSharp.text.Image.GetInstance(Server.MapPath("/UploadedFiles/"+ parametros.Where(i => i.parametro == "pie_pagina").FirstOrDefault().valor));
                            var scalePercent2= (((document.PageSize.Width / imageFooter.Width) * 100) - 4);
                            imageFooter.SetAbsolutePosition(30f, 0f);
                            imageFooter.ScalePercent(scalePercent2);

                            document.Open();
                            //pruebas primera validación con la márgen
                            var valida1 = writer.GetVerticalPosition(false);
                            document.Add(imageHeader);
                            document.Add(imageFooter);

                            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font = new Font(bf, 11, Font.NORMAL);
                            Paragraph p = new Paragraph(null, font);


                            BaseFont bf_title = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font_title = new Font(bf_title, 13, Font.BOLD);
                            Paragraph p_title = new Paragraph(null, font_title);

                            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font_bold = new Font(bf, 11, Font.BOLD);

                            //p_title.Add("DIRECCIÓN DE EXTENSIÓN Y PROYECCIÓN SOCIAL");
                            p_title.Add(parametros.Where(i => i.parametro == "titulo_principal").FirstOrDefault().valor);
                            p_title.SetLeading(1, 1);
                            p_title.Alignment = Element.ALIGN_CENTER;
                            document.Add(p_title);


                            document.Add(Chunk.NEWLINE);

                            p_title = new Paragraph(null, font_title);
                            //p_title.Add("HACE CONSTAR");
                            p_title.Add(parametros.Where(i => i.parametro == "titulo_secundario").FirstOrDefault().valor);
                            p_title.SetLeading(1, 1);
                            p_title.Alignment = Element.ALIGN_CENTER;
                            document.Add(p_title);

                            document.Add(Chunk.NEWLINE);
                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
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
                            p.Add("Obeto: " + contrato.objeto);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            p.Add("Actividades: " + contrato.actividades);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            p.Add(parametros.Where(i => i.parametro == "texto_complementario").FirstOrDefault().valor);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            p.Add(parametros.Where(i => i.parametro == "texto_expedicion").FirstOrDefault().valor);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            iTextSharp.text.Image image_firma = iTextSharp.text.Image.GetInstance(Server.MapPath("/UploadedFiles/" + parametros.Where(i => i.parametro == "firma").FirstOrDefault().valor));
                            image_firma.ScalePercent(60f);
                            document.Add(image_firma);

                            p = new Paragraph(null, font_bold);
                            p.Add(parametros.Where(i => i.parametro == "nombre_expide").FirstOrDefault().valor);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            p = new Paragraph(null, font);
                            p.Add(parametros.Where(i => i.parametro == "cargo_expide").FirstOrDefault().valor);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            var tamanio = document.PageSize.Height;
                            var valida = writer.GetVerticalPosition(false);

                            document.Close();
                            writer.Close();
                            Response.ContentType = "pdf/application";
                            Response.AddHeader("content-disposition",
                            "attachment;filename= certificado_" + contrato.numero_contrato + ".pdf");
                            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                        }
                    }
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