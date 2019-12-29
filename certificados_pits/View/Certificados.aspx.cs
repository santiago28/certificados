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
using Datos.DTO;
using System.Globalization;

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


                        var header_footer_event = new HeaderFooter();
                        var concatenacion_catacteres = "";
                        using (MemoryStream ms = new MemoryStream())
                        {
                            Document document = new Document(PageSize.A4);
                            document.SetMargins(55f, 55f, 120f, 110f);
                            PdfWriter writer = PdfWriter.GetInstance(document, ms);

                            writer.PageEvent = header_footer_event;
                            header_footer_event.encabezado = parametros.Where(i => i.parametro == "encabezado").FirstOrDefault().valor;
                            header_footer_event.pie_pagina = parametros.Where(i => i.parametro == "pie_pagina").FirstOrDefault().valor;

                            document.Open();
                            var valida1 = writer.GetVerticalPosition(false);

                            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font = FontFactory.GetFont("Arial", size: 10);
                            Font font_table = FontFactory.GetFont("Arial", size: 8);
                            Paragraph p = new Paragraph(null, font);


                            BaseFont bf_title = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font_title = FontFactory.GetFont("Arial", size: 10, Font.BOLD);
                            Paragraph p_title = new Paragraph(null, font_title);

                            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                            Font font_bold = FontFactory.GetFont("Arial", size: 10, Font.BOLD);

                            
                            var fecha_actual = DateTime.Now.ToString("dd/MM/yyyy").Split('/');
                            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                            var nombre_mes = dtinfo.GetMonthName(int.Parse(fecha_actual[1]));
                            p = new Paragraph(null, font);
                            concatenacion_catacteres += "Medellín, " + nombre_mes + " " + fecha_actual[0] + " de " + fecha_actual[2];
                            p.Add("Medellín, " + nombre_mes + " " + fecha_actual[0] + " de "+ fecha_actual[2]);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            document.Add(Chunk.NEWLINE);

                            concatenacion_catacteres += parametros.Where(i => i.parametro == "titulo_principal").FirstOrDefault().valor;
                            p_title.Add(parametros.Where(i => i.parametro == "titulo_principal").FirstOrDefault().valor);
                            p_title.SetLeading(1, 1);
                            p_title.Alignment = Element.ALIGN_CENTER;
                            document.Add(p_title);

                            concatenacion_catacteres += Chunk.NEWLINE;
                            document.Add(Chunk.NEWLINE);

                            p_title = new Paragraph(null, font_title);
                            concatenacion_catacteres += parametros.Where(i => i.parametro == "titulo_secundario").FirstOrDefault().valor;
                            p_title.Add(parametros.Where(i => i.parametro == "titulo_secundario").FirstOrDefault().valor);
                            p_title.SetLeading(1, 1);
                            p_title.Alignment = Element.ALIGN_CENTER;
                            document.Add(p_title);
                            concatenacion_catacteres += Chunk.NEWLINE;
                            concatenacion_catacteres += Chunk.NEWLINE;
                            document.Add(Chunk.NEWLINE);
                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            concatenacion_catacteres += "Que en virtud del Contrato Interadministrativo número " + convenio.codigo_convenio + ". El (La) Señor (a) " + persona.nombre + " con cédula " + persona.documento + ", subscribió un contrato por prestación de servicios (" + contrato.numero_contrato + ") con una duración en días de " + contrato.duracion_dias + " en el año " + contrato.anio + ".";
                            p.Add("Que en virtud del Contrato Interadministrativo número " + convenio.codigo_convenio + ". El (La) Señor (a) " + persona.nombre + " con cédula " + persona.documento + ", subscribió un contrato por prestación de servicios (" + contrato.numero_contrato + ") con una duración de " + contrato.duracion_dias + " días en el año " + contrato.anio + ".");
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            concatenacion_catacteres += Chunk.NEWLINE;
                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            var numero = concatenacion_catacteres.Length;
                            concatenacion_catacteres += "Honorarios mensuales " + contrato.honorarios_letras.ToLower() + " ($" + contrato.honorarios.ToString() + ").";
                            var honorarios_conversion = String.Format("{0:#,#.00}", contrato.honorarios);
                            var h_letras = contrato.honorarios_letras.ToLower().Split(':');
                            p.Add("Honorarios mensuales" + h_letras[0] + " ($" + honorarios_conversion + ")"+ h_letras[1]);
                            
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);
                            concatenacion_catacteres += Chunk.NEWLINE;
                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            concatenacion_catacteres += "Objeto: " + contrato.objeto;
                            p.Add("Objeto: " + contrato.objeto);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);
                            concatenacion_catacteres += Chunk.NEWLINE;
                            document.Add(Chunk.NEWLINE);

                            var concatenacion_sin_actividades = concatenacion_catacteres;
                            concatenacion_catacteres += "Actividades: " + contrato.actividades;
                            
                            concatenacion_catacteres += Chunk.NEWLINE + parametros.Where(i => i.parametro == "texto_complementario").FirstOrDefault().valor + parametros.Where(i => i.parametro == "texto_expedicion").FirstOrDefault().valor + iTextSharp.text.Image.GetInstance(Server.MapPath("/UploadedFiles/" + parametros.Where(i => i.parametro == "firma").FirstOrDefault().valor)) + parametros.Where(i => i.parametro == "nombre_expide").FirstOrDefault().valor + parametros.Where(i => i.parametro == "cargo_expide").FirstOrDefault().valor;
                            //Prueba 1
                            var actividades = "Actividades: " + contrato.actividades;
                            var actividades_split = actividades.Split(' ');
                            var texto1 = "";
                            var texto2 = "";
                            bool texto_en_dos = false;
                            foreach (var item in actividades_split)
                            {
                                var validacion = concatenacion_sin_actividades += " " + item;
                                if (!texto_en_dos)
                                {
                                    texto1 += " " + item;
                                }
                                if (concatenacion_sin_actividades.Length >= 3600)
                                {
                                    texto_en_dos = true;
                                   
                                    texto2 += " " + item;
                                }
                            }
                            if (texto_en_dos)
                            {
                                p = new Paragraph(null, font);
                                p.Add(texto1);
                                p.SetLeading(1, 1);
                                p.Alignment = Element.ALIGN_JUSTIFIED;
                                document.Add(p);

                                document.NewPage();
                                p = new Paragraph(null, font);
                                p.Add(texto2);
                                p.SetLeading(1, 1);
                                p.Alignment = Element.ALIGN_JUSTIFIED;
                                document.Add(p);
                            }
                            else 

                           
                            if (concatenacion_catacteres.Length >= 3300 &&  !texto_en_dos)
                            {
                                //p = new Paragraph(null, font);
                                //var texto = "Actividades: " + contrato.actividades;
                                //int cantidad_espacios = texto.Count(Char.IsWhiteSpace);
                                //int mitad = (int)(cantidad_espacios / 2);
                                //var texto_partido = texto.Split(' ')[mitad];
                                //p.Add("Actividades: " + texto_partido[0]);
                                //p.SetLeading(1, 1);
                                //p.Alignment = Element.ALIGN_JUSTIFIED;
                                //document.Add(p);

                                //document.NewPage();

                                //p = new Paragraph(null, font);
                                //p.Add("Actividades: " + texto_partido[1]);
                                //p.SetLeading(1, 1);
                                //p.Alignment = Element.ALIGN_JUSTIFIED;
                                //document.Add(p);

                                var texto = "Actividades: " + contrato.actividades;
                                var texto_array = texto.Split(' ');
                                var cantidad_palabras = texto_array.Length;
                                int mitad = (int)(cantidad_palabras / 2);
                                string texto1_opcion2 = "";
                                string texto2_opcion2 = "";
                                for (int i = 0; i < mitad; i++)
                                {
                                    texto1_opcion2 += texto_array[i] + " ";
                                }

                                for (int i = mitad; i < cantidad_palabras; i++)
                                {
                                    texto2_opcion2 += texto_array[i] + " ";
                                }

                                p = new Paragraph(null, font);
                                p.Add(texto1_opcion2);
                                p.SetLeading(1, 1);
                                p.Alignment = Element.ALIGN_JUSTIFIED;
                                document.Add(p);

                                document.NewPage();

                                p = new Paragraph(null, font);
                                p.Add(texto2_opcion2);
                                p.SetLeading(1, 1);
                                p.Alignment = Element.ALIGN_JUSTIFIED;
                                document.Add(p);

                            }
                            else
                            {
                                p = new Paragraph(null, font);
                                p.Add("Actividades: " + contrato.actividades);
                                p.SetLeading(1, 1);
                                p.Alignment = Element.ALIGN_JUSTIFIED;
                                document.Add(p);
                            }

                            //p = new Paragraph(null, font);
                            //p.Add("Actividades: " + contrato.actividades);
                            //p.SetLeading(1, 1);
                            //p.Alignment = Element.ALIGN_JUSTIFIED;
                            //document.Add(p);
                            //p = new Paragraph(null, font);
                            //p.Add(texto1);
                            //p.SetLeading(1, 1);
                            //p.Alignment = Element.ALIGN_JUSTIFIED;
                            //document.Add(p);

                            document.Add(Chunk.NEWLINE);

                            p = new Paragraph(null, font);
                            p.Add(parametros.Where(i => i.parametro == "texto_complementario").FirstOrDefault().valor);
                            p.SetLeading(1, 1);
                            p.Alignment = Element.ALIGN_JUSTIFIED;
                            document.Add(p);

                            document.Add(Chunk.NEWLINE);
                            HeaderFooter hf = new HeaderFooter();
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

                            document.Add(Chunk.NEWLINE);
                            PdfPTable table = new PdfPTable(2);
                            table.HorizontalAlignment = 0;
                            table.WidthPercentage = 70f;
                            p = new Paragraph(null, font_table);
                            p.Add("Elaboró: " + parametros.Where(i => i.parametro == "elabora").FirstOrDefault().valor);
                            table.AddCell(p);
                            p = new Paragraph(null, font_table);
                            p.Add("Revisó: " + parametros.Where(i => i.parametro == "revisa").FirstOrDefault().valor);
                            table.AddCell(p);
                            document.Add(table);


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


    }



    public class HeaderFooter : PdfPageEventHelper
    {
        public string encabezado { get; set; }
        public string pie_pagina { get; set; }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //base.OnEndPage(writer, document);

            iTextSharp.text.Image imageHeader = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("/UploadedFiles/" + encabezado));

            var scalePercent = (((document.PageSize.Width / imageHeader.Width) * 100) - 4);

            imageHeader.SetAbsolutePosition(0f, 720f);
            imageHeader.ScalePercent(scalePercent);

            iTextSharp.text.Image imageFooter = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("/UploadedFiles/" + pie_pagina));
            var scalePercent2 = (((document.PageSize.Width / imageFooter.Width) * 100) - 4);
            imageFooter.SetAbsolutePosition(30f, 0f);
            imageFooter.ScalePercent(scalePercent2);

            document.Add(imageHeader);
            document.Add(imageFooter);
        }
    }
}