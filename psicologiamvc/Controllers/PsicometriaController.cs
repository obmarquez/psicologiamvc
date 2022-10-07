using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class PsicometriaController : Controller  // Cat_controlador: 25
    {
        [FiltroPermisos]
        // GET: Psicometria
        public ActionResult IndexPsicometria(string cadena = "")  // Cat_controlador_Acciones: 57
        {
            if (Session["Id_Usuario"] != null)
            {
                if (cadena.Trim() != "")
                    return View(psicometria.getPsico(cadena).ToList());
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult Indexterman(string fecha01 = "", string fecha02 = "", string opcionTest = "")
        {
            if(Session["Id_Usuario"] != null)
            {
                List<SelectListItem> itemsTests = new List<SelectListItem>();
                itemsTests.Add(new SelectListItem { Text = "Raven", Value = "1" });
                itemsTests.Add(new SelectListItem { Text = "MMPI-2", Value = "2" });
                itemsTests.Add(new SelectListItem { Text = "Terman", Value = "3" });
                itemsTests.Add(new SelectListItem { Text = "Cleaver", Value = "4" });
                ViewBag.tests = itemsTests;

                if (fecha01.Trim() != "" && fecha02.Trim() != "" && opcionTest.Trim() != "")
                {
                    ViewBag.laopcion = opcionTest;
                    return View(Consultas.obtenerListaTests(fecha01, fecha02, opcionTest).ToList());
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult printTerman(int sIdH)
        {
            var datosTerman = Terman.getTerman(sIdH).FirstOrDefault();

            var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
            var fontDato = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);

            var fonEtiTer = FontFactory.GetFont("Arial", 8, Font.BOLD, BaseColor.BLACK);
            var fonEtitDat = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLACK);

            MemoryStream ms = new MemoryStream();

            Document elTerman = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwTerman = PdfWriter.GetInstance(elTerman, ms);

            pwTerman.PageEvent = new HeaderTerman();

            elTerman.Open();

            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            elTerman.Add(imagen);

            Paragraph saltos = new Paragraph();
            saltos.Add(Chunk.NEWLINE);
            elTerman.Add(saltos);

            #region Datos Generales
            PdfPTable tblDatos = new PdfPTable(6)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] widthTerman = new float[6] { 80, 220, 80, 50, 80, 50 };
            tblDatos.SetWidths(widthTerman);
            tblDatos.HorizontalAlignment = 0;
            tblDatos.SpacingBefore = 10f;
            tblDatos.SpacingAfter = 10f;
            tblDatos.DefaultCell.Border = 0;

            PdfPCell celNombreTit = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
            celNombreTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celNombreTit.BorderWidth = 0;
            tblDatos.AddCell(celNombreTit);

            PdfPCell celNombredato = new PdfPCell(new Phrase(datosTerman.nombre, fontDato));
            celNombredato.HorizontalAlignment = Element.ALIGN_CENTER;
            celNombredato.BorderWidth = 0;
            celNombredato.BorderWidthBottom = 1;
            tblDatos.AddCell(celNombredato);

            PdfPCell celEdadTit = new PdfPCell(new Phrase("Edad:", fonEiqueta));
            celEdadTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celEdadTit.BorderWidth = 0;
            tblDatos.AddCell(celEdadTit);

            PdfPCell celEdaddato = new PdfPCell(new Phrase(datosTerman.edad, fontDato));
            celEdaddato.HorizontalAlignment = Element.ALIGN_CENTER;
            celEdaddato.BorderWidth = 0;
            celEdaddato.BorderWidthBottom = 1;
            tblDatos.AddCell(celEdaddato);

            PdfPCell celSexoTit = new PdfPCell(new Phrase("Sexo:", fonEiqueta));
            celSexoTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celSexoTit.BorderWidth = 0;
            tblDatos.AddCell(celSexoTit);

            PdfPCell celSexodato = new PdfPCell(new Phrase(datosTerman.sexo, fontDato));
            celSexodato.HorizontalAlignment = Element.ALIGN_CENTER;
            celSexodato.BorderWidth = 0;
            celSexodato.BorderWidthBottom = 1;
            tblDatos.AddCell(celSexodato);

            PdfPCell celPuestoTit = new PdfPCell(new Phrase("Puesto:", fonEiqueta));
            celPuestoTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celPuestoTit.BorderWidth = 0;
            tblDatos.AddCell(celPuestoTit);

            PdfPCell celPuestodato = new PdfPCell(new Phrase(datosTerman.puesto, fontDato));
            celPuestodato.HorizontalAlignment = Element.ALIGN_CENTER;
            celPuestodato.BorderWidth = 0;
            celPuestodato.BorderWidthBottom = 1;
            tblDatos.AddCell(celPuestodato);

            PdfPCell celFechaTit = new PdfPCell(new Phrase("Fecha Alta:", fonEiqueta));
            celFechaTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celFechaTit.BorderWidth = 0;
            tblDatos.AddCell(celFechaTit);

            PdfPCell celFechadato = new PdfPCell(new Phrase(datosTerman.fecha, fontDato));
            celFechadato.HorizontalAlignment = Element.ALIGN_CENTER;
            celFechadato.BorderWidth = 0;
            celFechadato.BorderWidthBottom = 1;
            tblDatos.AddCell(celFechadato);

            PdfPCell celVacioTit = new PdfPCell(new Phrase("", fonEiqueta));
            celVacioTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celVacioTit.BorderWidth = 0;
            tblDatos.AddCell(celVacioTit);

            PdfPCell celVacioTit2 = new PdfPCell(new Phrase("", fonEiqueta));
            celVacioTit2.HorizontalAlignment = Element.ALIGN_LEFT;
            celVacioTit2.BorderWidth = 0;
            tblDatos.AddCell(celVacioTit2);

            elTerman.Add(tblDatos);
            #endregion

            #region tlbTerman
            PdfPTable tblTerman = new PdfPTable(12)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] wdtTerman = new float[12] { 47, 47, 47, 47, 47, 47, 47, 47, 46, 46, 46, 46 };
            tblTerman.SetWidths(wdtTerman);
            tblTerman.HorizontalAlignment = 0;
            tblTerman.SpacingBefore = 10f;
            tblTerman.SpacingBefore = 10f;
            tblTerman.DefaultCell.Border = 1;

            //------------------------------------------------------------------------------------------- Titulos 1
            PdfPCell cel_1 = new PdfPCell(new Phrase("R", fonEtiTer));
            cel_1.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1.UseAscender = true;
            tblTerman.AddCell(cel_1);

            PdfPCell cel_2 = new PdfPCell(new Phrase("I", fonEtiTer));
            cel_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2.UseAscender = true;
            tblTerman.AddCell(cel_2);

            PdfPCell cel_3 = new PdfPCell(new Phrase("II", fonEtiTer));
            cel_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3.UseAscender = true;
            tblTerman.AddCell(cel_3);

            PdfPCell cel_4 = new PdfPCell(new Phrase("III", fonEtiTer));
            cel_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4.UseAscender = true;
            tblTerman.AddCell(cel_4);

            PdfPCell cel_5 = new PdfPCell(new Phrase("IV", fonEtiTer));
            cel_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5.UseAscender = true;
            tblTerman.AddCell(cel_5);

            PdfPCell cel_6 = new PdfPCell(new Phrase("V", fonEtiTer));
            cel_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6.UseAscender = true;
            tblTerman.AddCell(cel_6);

            PdfPCell cel_7 = new PdfPCell(new Phrase("VI", fonEtiTer));
            cel_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7.UseAscender = true;
            tblTerman.AddCell(cel_7);

            PdfPCell cel_8 = new PdfPCell(new Phrase("VII", fonEtiTer));
            cel_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8.UseAscender = true;
            tblTerman.AddCell(cel_8);

            PdfPCell cel_9 = new PdfPCell(new Phrase("VIII", fonEtiTer));
            cel_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9.UseAscender = true;
            tblTerman.AddCell(cel_9);

            PdfPCell cel_10 = new PdfPCell(new Phrase("IX", fonEtiTer));
            cel_10.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10.UseAscender = true;
            tblTerman.AddCell(cel_10);

            PdfPCell cel_11 = new PdfPCell(new Phrase("X", fonEtiTer));
            cel_11.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11.UseAscender = true;
            tblTerman.AddCell(cel_11);

            PdfPCell cel_12 = new PdfPCell(new Phrase("G", fonEtiTer));
            cel_12.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12.UseAscender = true;
            tblTerman.AddCell(cel_12);

            //------------------------------------------------------------------------------------------- Titulos 2
            PdfPCell cel_1_2 = new PdfPCell(new Phrase("RANGO", fonEtitDat));
            cel_1_2.Rotation = 270;
            cel_1_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_2.UseAscender = true;
            tblTerman.AddCell(cel_1_2);

            PdfPCell cel_2_2 = new PdfPCell(new Phrase("INFORMACION\n(Información o conocimientos)", fonEtitDat));
            cel_2_2.Rotation = 270;
            cel_2_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_2.UseAscender = true;
            tblTerman.AddCell(cel_2_2);

            PdfPCell cel_3_2 = new PdfPCell(new Phrase("JUICIO\n(Comprensión)", fonEtitDat));
            cel_3_2.Rotation = 270;
            cel_3_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_2.UseAscender = true;
            tblTerman.AddCell(cel_3_2);

            PdfPCell cel_4_2 = new PdfPCell(new Phrase("VOCABULARIO\n(Significado de palabras)", fonEtitDat));
            cel_4_2.Rotation = 270;
            cel_4_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_2.UseAscender = true;
            tblTerman.AddCell(cel_4_2);

            PdfPCell cel_5_2 = new PdfPCell(new Phrase("SÍNTESIS\n(Sección lógica)", fonEtitDat));
            cel_5_2.Rotation = 270;
            cel_5_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_2.UseAscender = true;
            tblTerman.AddCell(cel_5_2);

            PdfPCell cel_6_2 = new PdfPCell(new Phrase("CONCENTRACION\n(Aritméticas)", fonEtitDat));
            cel_6_2.Rotation = 270;
            cel_6_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_2.UseAscender = true;
            tblTerman.AddCell(cel_6_2);

            PdfPCell cel_7_2 = new PdfPCell(new Phrase("ANÁLSIS\n(Juicio práctico)", fonEtitDat));
            cel_7_2.Rotation = 270;
            cel_7_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_2.UseAscender = true;
            tblTerman.AddCell(cel_7_2);

            PdfPCell cel_8_2 = new PdfPCell(new Phrase("ABSTRACCION\n(Analogías)", fonEtitDat));
            cel_8_2.Rotation = 270;
            cel_8_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_2.UseAscender = true;
            tblTerman.AddCell(cel_8_2);

            PdfPCell cel_9_2 = new PdfPCell(new Phrase("PLANEACIÓN\n(Ordenamiento de frases)", fonEtitDat));
            cel_9_2.Rotation = 270;
            cel_9_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_2.UseAscender = true;
            tblTerman.AddCell(cel_9_2);

            PdfPCell cel_10_2 = new PdfPCell(new Phrase("ORGANIZACIÓN\n(Clasificación)", fonEtitDat));
            cel_10_2.Rotation = 270;
            cel_10_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_2.UseAscender = true;
            tblTerman.AddCell(cel_10_2);

            PdfPCell cel_11_2 = new PdfPCell(new Phrase("ATENCIÓN\n(Seriación)", fonEtitDat));
            cel_11_2.Rotation = 270;
            cel_11_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_2.UseAscender = true;
            tblTerman.AddCell(cel_11_2);

            PdfPCell cel_12_2 = new PdfPCell(new Phrase("GENERAL", fonEtitDat));
            cel_12_2.Rotation = 270;
            cel_12_2.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_2.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_2.UseAscender = true;
            tblTerman.AddCell(cel_12_2);

            //------------------------------------------------------------------------------------------- SOB
            PdfPCell cel_1_3 = new PdfPCell(new Phrase("SOB", fonEtiTer));
            cel_1_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_3.UseAscender = true;
            cel_1_3.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_3);

            PdfPCell cel_2_3 = new PdfPCell(new Phrase("16", fonEtiTer));
            cel_2_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_3.UseAscender = true;
            if(datosTerman.serie1 >= 16)
                cel_2_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_3);

            PdfPCell cel_3_3 = new PdfPCell(new Phrase("22", fonEtiTer));
            cel_3_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_3.UseAscender = true;
            if (datosTerman.serie2 >= 22)
                cel_3_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_3);

            PdfPCell cel_4_3 = new PdfPCell(new Phrase("29 - 30", fonEtiTer));
            cel_4_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_3.UseAscender = true;
            if (datosTerman.serie3 >= 29)
                cel_4_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_3);

            PdfPCell cel_5_3 = new PdfPCell(new Phrase("18", fonEtiTer));
            cel_5_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_3.UseAscender = true;
            if (datosTerman.serie4 >= 18)
                cel_5_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_3);

            PdfPCell cel_6_3 = new PdfPCell(new Phrase("24", fonEtiTer));
            cel_6_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_3.UseAscender = true;
            if (datosTerman.serie5 >= 24)
                cel_6_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_3);

            PdfPCell cel_7_3 = new PdfPCell(new Phrase("20", fonEtiTer));
            cel_7_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_3.UseAscender = true;
            if (datosTerman.serie6 >= 20)
                cel_7_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_3);

            PdfPCell cel_8_3 = new PdfPCell(new Phrase("19 - 20", fonEtiTer));
            cel_8_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_3.UseAscender = true;
            if (datosTerman.serie7 >= 19)
                cel_8_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_3);

            PdfPCell cel_9_3 = new PdfPCell(new Phrase("17", fonEtiTer));
            cel_9_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_3.UseAscender = true;
            if (datosTerman.serie8 >= 17)
                cel_9_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_3);

            PdfPCell cel_10_3 = new PdfPCell(new Phrase("18", fonEtiTer));
            cel_10_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_3.UseAscender = true;
            if (datosTerman.serie9 >= 18)
                cel_10_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_3);

            PdfPCell cel_11_3 = new PdfPCell(new Phrase("20 - 22", fonEtiTer));
            cel_11_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_3.UseAscender = true;
            if (datosTerman.serie10 >= 20)
                cel_11_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_3);

            PdfPCell cel_12_3 = new PdfPCell(new Phrase("140 - 141", fonEtiTer));
            cel_12_3.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_3.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_3.UseAscender = true;
            if (datosTerman.grantotal >= 140)
                cel_12_3.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_3);

            //------------------------------------------------------------------------------------------- SUP
            PdfPCell cel_1_4 = new PdfPCell(new Phrase("SUP", fonEtiTer));
            cel_1_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_4.UseAscender = true;
            cel_1_4.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_4);

            PdfPCell cel_2_4 = new PdfPCell(new Phrase("15", fonEtiTer));
            cel_2_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_4.UseAscender = true;
            if (datosTerman.serie1 == 15)
                cel_2_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_4);

            PdfPCell cel_3_4 = new PdfPCell(new Phrase("20", fonEtiTer));
            cel_3_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_4.UseAscender = true;
            if (datosTerman.serie2 == 20)
                cel_3_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_4);

            PdfPCell cel_4_4 = new PdfPCell(new Phrase("27 -28", fonEtiTer));
            cel_4_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_4.UseAscender = true;
            if (datosTerman.serie3 >= 27 && datosTerman.serie3 <= 28)
                cel_4_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_4);

            PdfPCell cel_5_4 = new PdfPCell(new Phrase("16 - 17", fonEtiTer));
            cel_5_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_4.UseAscender = true;
            if (datosTerman.serie4 >= 16 && datosTerman.serie4 <= 17)
                cel_5_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_4);

            PdfPCell cel_6_4 = new PdfPCell(new Phrase("22", fonEtiTer));
            cel_6_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_4.UseAscender = true;
            if (datosTerman.serie5 == 22)
                cel_6_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_4);

            PdfPCell cel_7_4 = new PdfPCell(new Phrase("18 - 19", fonEtiTer));
            cel_7_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_4.UseAscender = true;
            if (datosTerman.serie6 >= 18 && datosTerman.serie6 <= 19)
                cel_7_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_4);

            PdfPCell cel_8_4 = new PdfPCell(new Phrase("18", fonEtiTer));
            cel_8_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_4.UseAscender = true;
            if (datosTerman.serie7 == 18)
                cel_8_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_4);

            PdfPCell cel_9_4 = new PdfPCell(new Phrase("15 - 16", fonEtiTer));
            cel_9_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_4.UseAscender = true;
            if (datosTerman.serie8 >= 15 && datosTerman.serie8 <= 16)
                cel_9_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_4);

            PdfPCell cel_10_4 = new PdfPCell(new Phrase("17", fonEtiTer));
            cel_10_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_4.UseAscender = true;
            if (datosTerman.serie9 == 17)
                cel_10_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_4);

            PdfPCell cel_11_4 = new PdfPCell(new Phrase("18", fonEtiTer));
            cel_11_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_4.UseAscender = true;
            if (datosTerman.serie10 == 18)
                cel_11_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_4);

            PdfPCell cel_12_4 = new PdfPCell(new Phrase("120 - 139", fonEtiTer));
            cel_12_4.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_4.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_4.UseAscender = true;
            if (datosTerman.grantotal >= 120 && datosTerman.grantotal <= 139)
                cel_12_4.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_4);

            //------------------------------------------------------------------------------------------- TMA
            PdfPCell cel_1_5 = new PdfPCell(new Phrase("TMA", fonEtiTer));
            cel_1_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_5.UseAscender = true;
            cel_1_5.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_5);

            PdfPCell cel_2_5 = new PdfPCell(new Phrase("14", fonEtiTer));
            cel_2_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_5.UseAscender = true;
            if (datosTerman.serie1 == 14)
                cel_2_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_5);

            PdfPCell cel_3_5 = new PdfPCell(new Phrase("18", fonEtiTer));
            cel_3_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_5.UseAscender = true;
            if (datosTerman.serie2 == 18)
                cel_3_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_5);

            PdfPCell cel_4_5 = new PdfPCell(new Phrase("23 - 26", fonEtiTer));
            cel_4_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_5.UseAscender = true;
            if (datosTerman.serie3 >= 23 && datosTerman.serie3 <= 26)
                cel_4_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_5);

            PdfPCell cel_5_5 = new PdfPCell(new Phrase("14 - 15", fonEtiTer));
            cel_5_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_5.UseAscender = true;
            if (datosTerman.serie4 >= 14 && datosTerman.serie4 <= 15)
                cel_5_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_5);

            PdfPCell cel_6_5 = new PdfPCell(new Phrase("15 - 18", fonEtiTer));
            cel_6_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_5.UseAscender = true;
            if (datosTerman.serie5 >= 15 && datosTerman.serie5 <= 18)
                cel_6_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_5);

            PdfPCell cel_7_5 = new PdfPCell(new Phrase("15 - 17", fonEtiTer));
            cel_7_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_5.UseAscender = true;
            if (datosTerman.serie6 >= 15 && datosTerman.serie6 <= 17)
                cel_7_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_5);

            PdfPCell cel_8_5 = new PdfPCell(new Phrase("16 - 17", fonEtiTer));
            cel_8_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_5.UseAscender = true;
            if (datosTerman.serie7 >= 16 && datosTerman.serie7 <= 17)
                cel_8_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_5);

            PdfPCell cel_9_5 = new PdfPCell(new Phrase("13 - 14", fonEtiTer));
            cel_9_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_5.UseAscender = true;
            if (datosTerman.serie8 >= 13 && datosTerman.serie8 <= 14)
                cel_9_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_5);

            PdfPCell cel_10_5 = new PdfPCell(new Phrase("16", fonEtiTer));
            cel_10_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_5.UseAscender = true;
            if (datosTerman.serie9 == 16)
                cel_10_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_5);

            PdfPCell cel_11_5 = new PdfPCell(new Phrase("16", fonEtiTer));
            cel_11_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_5.UseAscender = true;
            if (datosTerman.serie10 == 16)
                cel_11_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_5);

            PdfPCell cel_12_5 = new PdfPCell(new Phrase("110 - 119", fonEtiTer));
            cel_12_5.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_5.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_5.UseAscender = true;
            if (datosTerman.grantotal >= 110 && datosTerman.grantotal <= 119)
                cel_12_5.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_5);

            //------------------------------------------------------------------------------------------- TM
            PdfPCell cel_1_6 = new PdfPCell(new Phrase("TM", fonEtiTer));
            cel_1_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_6.UseAscender = true;
            cel_1_6.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_6);

            PdfPCell cel_2_6 = new PdfPCell(new Phrase("12 - 13", fonEtiTer));
            cel_2_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_6.UseAscender = true;
            if (datosTerman.serie1 >= 12 && datosTerman.serie1 <= 13)
                cel_2_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_6);

            PdfPCell cel_3_6 = new PdfPCell(new Phrase("12 - 16", fonEtiTer));
            cel_3_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_6.UseAscender = true;
            if (datosTerman.serie2 >= 12 && datosTerman.serie2 <= 16)
                cel_3_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_6);

            PdfPCell cel_4_6 = new PdfPCell(new Phrase("14 - 22", fonEtiTer));
            cel_4_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_6.UseAscender = true;
            if (datosTerman.serie3 >= 14 && datosTerman.serie3 <= 22)
                cel_4_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_6);

            PdfPCell cel_5_6 = new PdfPCell(new Phrase("10 - 13", fonEtiTer));
            cel_5_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_6.UseAscender = true;
            if (datosTerman.serie4 >= 10 && datosTerman.serie4 <= 13)
                cel_5_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_6);

            PdfPCell cel_6_6 = new PdfPCell(new Phrase("12 - 14", fonEtiTer));
            cel_6_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_6.UseAscender = true;
            if (datosTerman.serie5 >= 12 && datosTerman.serie5 <= 14)
                cel_6_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_6);

            PdfPCell cel_7_6 = new PdfPCell(new Phrase("9 - 14", fonEtiTer));
            cel_7_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_6.UseAscender = true;
            if (datosTerman.serie6 >= 9 && datosTerman.serie6 <= 14)
                cel_7_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_6);

            PdfPCell cel_8_6 = new PdfPCell(new Phrase("9 - 15", fonEtiTer));
            cel_8_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_6.UseAscender = true;
            if (datosTerman.serie7 >= 9 && datosTerman.serie7 <= 15)
                cel_8_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_6);

            PdfPCell cel_9_6 = new PdfPCell(new Phrase("8 - 12", fonEtiTer));
            cel_9_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_6.UseAscender = true;
            if (datosTerman.serie8 >= 8 && datosTerman.serie8 <= 12)
                cel_9_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_6);

            PdfPCell cel_10_6 = new PdfPCell(new Phrase("10 - 15", fonEtiTer));
            cel_10_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_6.UseAscender = true;
            if (datosTerman.serie9 >= 10 && datosTerman.serie9 <= 15)
                cel_10_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_6);

            PdfPCell cel_11_6 = new PdfPCell(new Phrase("10 - 14", fonEtiTer));
            cel_11_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_6.UseAscender = true;
            if (datosTerman.serie10 >= 10 && datosTerman.serie10 <= 14)
                cel_11_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_6);

            PdfPCell cel_12_6 = new PdfPCell(new Phrase("90 - 109", fonEtiTer));
            cel_12_6.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_6.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_6.UseAscender = true;
            if (datosTerman.grantotal >= 90 && datosTerman.grantotal <= 109)
                cel_12_6.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_6);

            //------------------------------------------------------------------------------------------- TMB
            PdfPCell cel_1_7 = new PdfPCell(new Phrase("TMB", fonEtiTer));
            cel_1_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_7.UseAscender = true;
            cel_1_7.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_7);

            PdfPCell cel_2_7 = new PdfPCell(new Phrase("10 - 11", fonEtiTer));
            cel_2_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_7.UseAscender = true;
            if (datosTerman.serie1 >= 10 && datosTerman.serie1 <= 11)
                cel_2_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_7);

            PdfPCell cel_3_7 = new PdfPCell(new Phrase("10", fonEtiTer));
            cel_3_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_7.UseAscender = true;
            if (datosTerman.serie2 == 10)
                cel_3_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_7);

            PdfPCell cel_4_7 = new PdfPCell(new Phrase("12 - 13", fonEtiTer));
            cel_4_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_7.UseAscender = true;
            if (datosTerman.serie3 >= 12 && datosTerman.serie3 <= 13)
                cel_4_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_7);

            PdfPCell cel_5_7 = new PdfPCell(new Phrase("7 - 9", fonEtiTer));
            cel_5_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_7.UseAscender = true;
            if (datosTerman.serie4 >= 7 && datosTerman.serie4 <= 9)
                cel_5_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_7);

            PdfPCell cel_6_7 = new PdfPCell(new Phrase("8 - 10", fonEtiTer));
            cel_6_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_7.UseAscender = true;
            if (datosTerman.serie5 >= 8 && datosTerman.serie5 <= 10)
                cel_6_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_7);

            PdfPCell cel_7_7 = new PdfPCell(new Phrase("7 - 8", fonEtiTer));
            cel_7_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_7.UseAscender = true;
            if (datosTerman.serie6 >= 7 && datosTerman.serie6 <= 8)
                cel_7_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_7);

            PdfPCell cel_8_7 = new PdfPCell(new Phrase("6 - 8", fonEtiTer));
            cel_8_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_7.UseAscender = true;
            if (datosTerman.serie7 >= 6 && datosTerman.serie7 <= 8)
                cel_8_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_7);

            PdfPCell cel_9_7 = new PdfPCell(new Phrase("7", fonEtiTer));
            cel_9_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_7.UseAscender = true;
            if (datosTerman.serie8 == 7)
                cel_9_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_7);

            PdfPCell cel_10_7 = new PdfPCell(new Phrase("9", fonEtiTer));
            cel_10_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_7.UseAscender = true;
            if (datosTerman.serie9 == 9)
                cel_10_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_7);

            PdfPCell cel_11_7 = new PdfPCell(new Phrase("8", fonEtiTer));
            cel_11_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_7.UseAscender = true;
            if (datosTerman.serie10 == 8)
                cel_11_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_7);

            PdfPCell cel_12_7 = new PdfPCell(new Phrase("80 - 89", fonEtiTer));
            cel_12_7.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_7.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_7.UseAscender = true;
            if (datosTerman.grantotal >= 80 && datosTerman.grantotal <= 89)
                cel_12_7.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_7);

            //------------------------------------------------------------------------------------------- INF
            PdfPCell cel_1_8 = new PdfPCell(new Phrase("INF", fonEtiTer));
            cel_1_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_8.UseAscender = true;
            cel_1_8.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_8);

            PdfPCell cel_2_8 = new PdfPCell(new Phrase("8 - 9", fonEtiTer));
            cel_2_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_8.UseAscender = true;
            if (datosTerman.serie1 >= 8 && datosTerman.serie1 <= 9)
                cel_2_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_8);

            PdfPCell cel_3_8 = new PdfPCell(new Phrase("8", fonEtiTer));
            cel_3_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_8.UseAscender = true;
            if (datosTerman.serie2 == 8)
                cel_3_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_8);

            PdfPCell cel_4_8 = new PdfPCell(new Phrase("8 - 11", fonEtiTer));
            cel_4_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_8.UseAscender = true;
            if (datosTerman.serie3 >= 8 && datosTerman.serie3 <= 11)
                cel_4_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_8);

            PdfPCell cel_5_8 = new PdfPCell(new Phrase("6", fonEtiTer));
            cel_5_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_8.UseAscender = true;
            if (datosTerman.serie4 == 6)
                cel_5_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_8);

            PdfPCell cel_6_8 = new PdfPCell(new Phrase("6", fonEtiTer));
            cel_6_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_8.UseAscender = true;
            if (datosTerman.serie5 == 6)
                cel_6_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_8);

            PdfPCell cel_7_8 = new PdfPCell(new Phrase("5 - 6", fonEtiTer));
            cel_7_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_8.UseAscender = true;
            if (datosTerman.serie6 >= 5 && datosTerman.serie6 <= 6)
                cel_7_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_8);

            PdfPCell cel_8_8 = new PdfPCell(new Phrase("5", fonEtiTer));
            cel_8_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_8.UseAscender = true;
            if (datosTerman.serie7 == 5)
                cel_8_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_8);

            PdfPCell cel_9_8 = new PdfPCell(new Phrase("6", fonEtiTer));
            cel_9_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_8.UseAscender = true;
            if (datosTerman.serie8 == 6)
                cel_9_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_8);

            PdfPCell cel_10_8 = new PdfPCell(new Phrase("7 - 8", fonEtiTer));
            cel_10_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_8.UseAscender = true;
            if (datosTerman.serie9 >= 7 && datosTerman.serie9 <= 8)
                cel_10_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_8);

            PdfPCell cel_11_8 = new PdfPCell(new Phrase("6", fonEtiTer));
            cel_11_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_8.UseAscender = true;
            if (datosTerman.serie10 == 6)
                cel_11_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_8);

            PdfPCell cel_12_8 = new PdfPCell(new Phrase("70 - 79", fonEtiTer));
            cel_12_8.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_8.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_8.UseAscender = true;
            if (datosTerman.grantotal >= 70 && datosTerman.grantotal <= 79)
                cel_12_8.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_8);

            //------------------------------------------------------------------------------------------- DEF
            PdfPCell cel_1_9 = new PdfPCell(new Phrase("DEF", fonEtiTer));
            cel_1_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_1_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_1_9.UseAscender = true;
            cel_1_9.FixedHeight = 20f;
            tblTerman.AddCell(cel_1_9);

            PdfPCell cel_2_9 = new PdfPCell(new Phrase("0 - 7", fonEtiTer));
            cel_2_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_2_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_2_9.UseAscender = true;
            if (datosTerman.serie1 <= 7)
                cel_2_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_2_9);

            PdfPCell cel_3_9 = new PdfPCell(new Phrase("0 - 6", fonEtiTer));
            cel_3_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_3_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_3_9.UseAscender = true;
            if (datosTerman.serie2 <= 6)
                cel_3_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_3_9);

            PdfPCell cel_4_9 = new PdfPCell(new Phrase("0 - 7", fonEtiTer));
            cel_4_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_4_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_4_9.UseAscender = true;
            if (datosTerman.serie3 <= 7)
                cel_4_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_4_9);

            PdfPCell cel_5_9 = new PdfPCell(new Phrase("0 - 5", fonEtiTer));
            cel_5_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_5_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_5_9.UseAscender = true;
            if (datosTerman.serie4 <= 5)
                cel_5_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_5_9);

            PdfPCell cel_6_9 = new PdfPCell(new Phrase("0 - 5", fonEtiTer));
            cel_6_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_6_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_6_9.UseAscender = true;
            if (datosTerman.serie5 <= 5)
                cel_6_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_6_9);

            PdfPCell cel_7_9 = new PdfPCell(new Phrase("0 - 4", fonEtiTer));
            cel_7_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_7_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_7_9.UseAscender = true;
            if (datosTerman.serie6 <= 4)
                cel_7_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_7_9);

            PdfPCell cel_8_9 = new PdfPCell(new Phrase("0 - 4", fonEtiTer));
            cel_8_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_8_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_8_9.UseAscender = true;
            if (datosTerman.serie7 <= 4)
                cel_8_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_8_9);

            PdfPCell cel_9_9 = new PdfPCell(new Phrase("0 - 5", fonEtiTer));
            cel_9_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_9_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_9_9.UseAscender = true;
            if (datosTerman.serie8 <= 5)
                cel_9_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_9_9);

            PdfPCell cel_10_9 = new PdfPCell(new Phrase("0 - 6", fonEtiTer));
            cel_10_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_10_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_10_9.UseAscender = true;
            if (datosTerman.serie9 <= 6)
                cel_10_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_10_9);

            PdfPCell cel_11_9 = new PdfPCell(new Phrase("0 - 4", fonEtiTer));
            cel_11_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_11_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_11_9.UseAscender = true;
            if (datosTerman.serie10 <= 4)
                cel_11_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_11_9);

            PdfPCell cel_12_9 = new PdfPCell(new Phrase("69", fonEtiTer));
            cel_12_9.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_12_9.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_12_9.UseAscender = true;
            if (datosTerman.grantotal <= 69)
                cel_12_9.BackgroundColor = new iTextSharp.text.BaseColor(146, 43, 33);
            tblTerman.AddCell(cel_12_9);

            elTerman.Add(tblTerman);
            #endregion

            #region totales
            PdfPTable tblTotales = new PdfPTable(2)
            {
                TotalWidth = 150,
                LockedWidth = true
            };
            float[] widthTotales = new float[2] { 90, 60 };
            tblTotales.SetWidths(widthTotales);
            //tblTotales.DefaultCell.BorderWidth = 0;
            tblTotales.SpacingBefore = 10f;
            tblTotales.SpacingAfter = 10f;

            PdfPCell celTit_Total = new PdfPCell(new Phrase("SERIE", fonEtiTer));
            celTit_Total.HorizontalAlignment = Element.ALIGN_CENTER;
            celTit_Total.VerticalAlignment = Element.ALIGN_MIDDLE;
            celTit_Total.UseAscender = true;
            celTit_Total.BorderWidth = 0;
            celTit_Total.FixedHeight = 20f;
            tblTotales.AddCell(celTit_Total);

            PdfPCell celTit_Puntaje = new PdfPCell(new Phrase("PUNTAJE", fonEtiTer));
            celTit_Puntaje.HorizontalAlignment = Element.ALIGN_CENTER;
            celTit_Puntaje.VerticalAlignment = Element.ALIGN_MIDDLE;
            celTit_Puntaje.UseAscender = true;
            celTit_Puntaje.BorderWidth = 0;
            tblTotales.AddCell(celTit_Puntaje);

            PdfPCell cel_I = new PdfPCell(new Phrase("I. INFORMACION", fonEtitDat));
            cel_I.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_I.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_I.UseAscender = true;
            cel_I.BorderWidth = 0;
            cel_I.FixedHeight = 15f;
            tblTotales.AddCell(cel_I);

            PdfPCell cel_I_P = new PdfPCell(new Phrase(datosTerman.serie1.ToString(), fonEtiTer));
            cel_I_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_I_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_I_P.UseAscender = true;
            cel_I_P.BorderWidth = 1;
            tblTotales.AddCell(cel_I_P);

            PdfPCell cel_II = new PdfPCell(new Phrase("II. JUICIO", fonEtitDat));
            cel_II.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_II.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_II.UseAscender = true;
            cel_II.BorderWidth = 0;
            cel_II.FixedHeight = 15f;
            tblTotales.AddCell(cel_II);

            PdfPCell cel_II_P = new PdfPCell(new Phrase(datosTerman.serie2.ToString(), fonEtiTer));
            cel_II_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_II_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_II_P.UseAscender = true;
            cel_II_P.BorderWidth = 1;
            tblTotales.AddCell(cel_II_P);

            PdfPCell cel_III = new PdfPCell(new Phrase("III. VOCABULARIO", fonEtitDat));
            cel_III.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_III.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_III.UseAscender = true;
            cel_III.BorderWidth = 0;
            cel_III.FixedHeight = 15f;
            tblTotales.AddCell(cel_III);

            PdfPCell cel_III_P = new PdfPCell(new Phrase(datosTerman.serie3.ToString(), fonEtiTer));
            cel_III_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_III_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_III_P.UseAscender = true;
            cel_III_P.BorderWidth = 1;
            tblTotales.AddCell(cel_III_P);

            PdfPCell cel_IV = new PdfPCell(new Phrase("IV. SINTESIS", fonEtitDat));
            cel_IV.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_IV.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_IV.UseAscender = true;
            cel_IV.BorderWidth = 0;
            cel_IV.FixedHeight = 15f;
            tblTotales.AddCell(cel_IV);

            PdfPCell cel_IV_P = new PdfPCell(new Phrase(datosTerman.serie4.ToString(), fonEtiTer));
            cel_IV_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_IV_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_IV_P.UseAscender = true;
            cel_IV_P.BorderWidth = 1;
            tblTotales.AddCell(cel_IV_P);

            PdfPCell cel_V = new PdfPCell(new Phrase("V. CONCENTRACION", fonEtitDat));
            cel_V.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_V.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_V.UseAscender = true;
            cel_V.BorderWidth = 0;
            cel_V.FixedHeight = 15f;
            tblTotales.AddCell(cel_V);

            PdfPCell cel_V_P = new PdfPCell(new Phrase(datosTerman.serie5.ToString(), fonEtiTer));
            cel_V_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_V_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_V_P.UseAscender = true;
            cel_V_P.BorderWidth = 1;
            tblTotales.AddCell(cel_V_P);

            PdfPCell cel_VI = new PdfPCell(new Phrase("VI. ANALISIS", fonEtitDat));
            cel_VI.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_VI.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VI.UseAscender = true;
            cel_VI.BorderWidth = 0;
            cel_VI.FixedHeight = 15f;
            tblTotales.AddCell(cel_VI);

            PdfPCell cel_VI_P = new PdfPCell(new Phrase(datosTerman.serie6.ToString(), fonEtiTer));
            cel_VI_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_VI_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VI_P.UseAscender = true;
            cel_VI_P.BorderWidth = 1;
            tblTotales.AddCell(cel_VI_P);

            PdfPCell cel_VII = new PdfPCell(new Phrase("VII. ABSTRACCION", fonEtitDat));
            cel_VII.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_VII.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VII.UseAscender = true;
            cel_VII.BorderWidth = 0;
            cel_VII.FixedHeight = 15f;
            tblTotales.AddCell(cel_VII);

            PdfPCell cel_VII_P = new PdfPCell(new Phrase(datosTerman.serie7.ToString(), fonEtiTer));
            cel_VII_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_VII_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VII_P.UseAscender = true;
            cel_VII_P.BorderWidth = 1;
            tblTotales.AddCell(cel_VII_P);

            PdfPCell cel_VIII = new PdfPCell(new Phrase("VIII. PLANEACION", fonEtitDat));
            cel_VIII.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_VIII.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VIII.UseAscender = true;
            cel_VIII.BorderWidth = 0;
            cel_VIII.FixedHeight = 15f;
            tblTotales.AddCell(cel_VIII);

            PdfPCell cel_VIII_P = new PdfPCell(new Phrase(datosTerman.serie8.ToString(), fonEtiTer));
            cel_VIII_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_VIII_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_VIII_P.UseAscender = true;
            cel_VIII_P.BorderWidth = 1;
            tblTotales.AddCell(cel_VIII_P);

            PdfPCell cel_IX = new PdfPCell(new Phrase("IX. ORGANIZACION", fonEtitDat));
            cel_IX.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_IX.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_IX.UseAscender = true;
            cel_IX.BorderWidth = 0;
            cel_IX.FixedHeight = 15f;
            tblTotales.AddCell(cel_IX);

            PdfPCell cel_IX_P = new PdfPCell(new Phrase(datosTerman.serie9.ToString(), fonEtiTer));
            cel_IX_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_IX_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_IX_P.UseAscender = true;
            cel_IX_P.BorderWidth = 1;
            tblTotales.AddCell(cel_IX_P);

            PdfPCell cel_X = new PdfPCell(new Phrase("X. ATENCION", fonEtitDat));
            cel_X.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_X.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_X.UseAscender = true;
            cel_X.BorderWidth = 0;
            cel_X.FixedHeight = 15f;
            tblTotales.AddCell(cel_X);

            PdfPCell cel_X_P = new PdfPCell(new Phrase(datosTerman.serie10.ToString(), fonEtiTer));
            cel_X_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_X_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_X_P.UseAscender = true;
            cel_X_P.BorderWidth = 1;
            tblTotales.AddCell(cel_X_P);

            PdfPCell cel_GT = new PdfPCell(new Phrase("G TOTAL", fonEtitDat));
            cel_GT.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_GT.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_GT.UseAscender = true;
            cel_GT.BorderWidth = 0;
            cel_GT.FixedHeight = 15f;
            tblTotales.AddCell(cel_GT);

            PdfPCell cel_GT_P = new PdfPCell(new Phrase(datosTerman.grantotal.ToString(), fonEtiTer));
            cel_GT_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_GT_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_GT_P.UseAscender = true;
            cel_GT_P.BorderWidth = 1;
            tblTotales.AddCell(cel_GT_P);

            PdfPCell cel_CI = new PdfPCell(new Phrase("CI", fonEtitDat));
            cel_CI.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_CI.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_CI.UseAscender = true;
            cel_CI.BorderWidth = 0;
            cel_CI.FixedHeight = 15f;
            tblTotales.AddCell(cel_CI);

            PdfPCell cel_CI_P = new PdfPCell(new Phrase(datosTerman.ci.ToString(), fonEtiTer));
            cel_CI_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_CI_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_CI_P.UseAscender = true;
            cel_CI_P.BorderWidth = 1;
            tblTotales.AddCell(cel_CI_P);

            PdfPCell cel_inter = new PdfPCell(new Phrase("INTERPRETACION CI", fonEtitDat));
            cel_inter.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_inter.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_inter.UseAscender = true;
            cel_inter.BorderWidth = 0;
            cel_inter.FixedHeight = 15f;
            tblTotales.AddCell(cel_inter);

            PdfPCell cel_INTER_P = new PdfPCell(new Phrase(datosTerman.interpretacionci.ToString(), fonEtiTer));
            cel_INTER_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_INTER_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_INTER_P.UseAscender = true;
            cel_INTER_P.BorderWidth = 1;
            tblTotales.AddCell(cel_INTER_P);

            PdfPCell cel_RANGO = new PdfPCell(new Phrase("RANGO CI", fonEtitDat));
            cel_RANGO.HorizontalAlignment = Element.ALIGN_RIGHT;
            cel_RANGO.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_RANGO.UseAscender = true;
            cel_RANGO.BorderWidth = 0;
            cel_RANGO.FixedHeight = 15f;
            tblTotales.AddCell(cel_RANGO);

            PdfPCell cel_RANGO_P = new PdfPCell(new Phrase(datosTerman.rangoci.ToString(), fonEtiTer));
            cel_RANGO_P.HorizontalAlignment = Element.ALIGN_CENTER;
            cel_RANGO_P.VerticalAlignment = Element.ALIGN_MIDDLE;
            cel_RANGO_P.UseAscender = true;
            cel_RANGO_P.BorderWidth = 1;
            tblTotales.AddCell(cel_RANGO_P);

            elTerman.Add(tblTotales);

            #endregion

            elTerman.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");

        }

        public ActionResult printRaven(int sIdH)
        {
            var datosRaven = Raven.getRaven(sIdH).FirstOrDefault();

            var fonEiqueta = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);
            var fontDato = FontFactory.GetFont("Arial", 9, Font.NORMAL, BaseColor.BLACK);
            //var altoCelda = 15f;

            MemoryStream ms = new MemoryStream();

            Document elRaven = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwRaven = PdfWriter.GetInstance(elRaven, ms);

            pwRaven.PageEvent = new HeaderRaven();

            elRaven.Open();

            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            elRaven.Add(imagen);

            Paragraph saltos = new Paragraph();
            saltos.Add(Chunk.NEWLINE);
            elRaven.Add(saltos);

            #region Datos Generales
            PdfPTable tblDatos = new PdfPTable(2)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] widthTerman = new float[2] { 130,  430};
            tblDatos.SetWidths(widthTerman);
            tblDatos.HorizontalAlignment = 0;
            tblDatos.SpacingBefore = 10f;
            tblDatos.SpacingAfter = 10f;
            tblDatos.DefaultCell.Border = 0;

            PdfPCell celNombreTit = new PdfPCell(new Phrase("Nombre:", fonEiqueta));
            celNombreTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celNombreTit.BorderWidth = 0;
            tblDatos.AddCell(celNombreTit);

            PdfPCell celNombredato = new PdfPCell(new Phrase(datosRaven.evaluado, fontDato));
            celNombredato.HorizontalAlignment = Element.ALIGN_CENTER;
            celNombredato.BorderWidth = 0;
            celNombredato.BorderWidthBottom = 1;
            tblDatos.AddCell(celNombredato);

            PdfPCell celFechaTit = new PdfPCell(new Phrase("Fecha de Nacimiento:", fonEiqueta));
            celFechaTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celFechaTit.BorderWidth = 0;
            tblDatos.AddCell(celFechaTit);

            PdfPCell celFechaDato = new PdfPCell(new Phrase(datosRaven.nacimiento, fontDato));
            celFechaDato.HorizontalAlignment = Element.ALIGN_CENTER;
            celFechaDato.BorderWidth = 0;
            celFechaDato.BorderWidthBottom = 1;
            tblDatos.AddCell(celFechaDato);

            PdfPCell celEdadTit = new PdfPCell(new Phrase("Edad:", fonEiqueta));
            celEdadTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celEdadTit.BorderWidth = 0;
            tblDatos.AddCell(celEdadTit);

            PdfPCell celEdaddato = new PdfPCell(new Phrase(datosRaven.edad + " Años.", fontDato));
            celEdaddato.HorizontalAlignment = Element.ALIGN_CENTER;
            celEdaddato.BorderWidth = 0;
            celEdaddato.BorderWidthBottom = 1;
            tblDatos.AddCell(celEdaddato);

            PdfPCell celHoraInicio = new PdfPCell(new Phrase("Hora inicio:", fonEiqueta));
            celHoraInicio.HorizontalAlignment = Element.ALIGN_LEFT;
            celHoraInicio.BorderWidth = 0;
            tblDatos.AddCell(celHoraInicio);

            PdfPCell celHoraInicioDatis = new PdfPCell(new Phrase(datosRaven.horainicio, fontDato));
            celHoraInicioDatis.HorizontalAlignment = Element.ALIGN_CENTER;
            celHoraInicioDatis.BorderWidth = 0;
            celHoraInicioDatis.BorderWidthBottom = 1;
            tblDatos.AddCell(celHoraInicioDatis);

            PdfPCell celHoraFinal = new PdfPCell(new Phrase("Hora final:", fonEiqueta));
            celHoraFinal.HorizontalAlignment = Element.ALIGN_LEFT;
            celHoraFinal.BorderWidth = 0;
            tblDatos.AddCell(celHoraFinal);

            PdfPCell celHoraFinalDato = new PdfPCell(new Phrase(datosRaven.horafin, fontDato));
            celHoraFinalDato.HorizontalAlignment = Element.ALIGN_CENTER;
            celHoraFinalDato.BorderWidth = 0;
            celHoraFinalDato.BorderWidthBottom = 1;
            tblDatos.AddCell(celHoraFinalDato);

            PdfPCell celSexoTit = new PdfPCell(new Phrase("Género:", fonEiqueta));
            celSexoTit.HorizontalAlignment = Element.ALIGN_LEFT;
            celSexoTit.BorderWidth = 0;
            tblDatos.AddCell(celSexoTit);

            PdfPCell celSexodato = new PdfPCell(new Phrase(datosRaven.sexo, fontDato));
            celSexodato.HorizontalAlignment = Element.ALIGN_CENTER;
            celSexodato.BorderWidth = 0;
            celSexodato.BorderWidthBottom = 1;
            tblDatos.AddCell(celSexodato);

            elRaven.Add(tblDatos);
            #endregion

            #region tablaResultado
            PdfPTable tblRes = new PdfPTable(5)
            {
                TotalWidth = 560,
                LockedWidth = true
            };
            float[] widthRaven = new float[5] { 112, 112, 112, 112, 112 };
            tblRes.SetWidths(widthRaven);
            tblRes.HorizontalAlignment = 0;
            tblRes.SpacingAfter = 10f;
            tblRes.SpacingBefore = 10f;
            //tblRes.DefaultCell.Border = 10;

            PdfPCell celSerA = new PdfPCell(new Phrase("A", fonEiqueta));
            celSerA.HorizontalAlignment = Element.ALIGN_CENTER;
            celSerA.BorderWidth = 1;
            tblRes.AddCell(celSerA);

            PdfPCell celSerB = new PdfPCell(new Phrase("B", fonEiqueta));
            celSerB.HorizontalAlignment = Element.ALIGN_CENTER;
            celSerB.BorderWidth = 1;
            tblRes.AddCell(celSerB);

            PdfPCell celSerC = new PdfPCell(new Phrase("C", fonEiqueta));
            celSerC.HorizontalAlignment = Element.ALIGN_CENTER;
            celSerC.BorderWidth = 1;
            tblRes.AddCell(celSerC);

            PdfPCell celSerD = new PdfPCell(new Phrase("D", fonEiqueta));
            celSerD.HorizontalAlignment = Element.ALIGN_CENTER;
            celSerD.BorderWidth = 1;
            tblRes.AddCell(celSerD);

            PdfPCell celSerE = new PdfPCell(new Phrase("E", fonEiqueta));
            celSerE.HorizontalAlignment = Element.ALIGN_CENTER;
            celSerE.BorderWidth = 1;
            tblRes.AddCell(celSerE);

            #region Calificacion Raven
            int ra1, ra2, ra3, ra4, ra5, ra6, ra7, ra8, ra9, ra10, ra11, ra12, totalA;
            int rb1, rb2, rb3, rb4, rb5, rb6, rb7, rb8, rb9, rb10, rb11, rb12, totalB;
            int rc1, rc2, rc3, rc4, rc5, rc6, rc7, rc8, rc9, rc10, rc11, rc12, totalC;
            int rd1, rd2, rd3, rd4, rd5, rd6, rd7, rd8, rd9, rd10, rd11, rd12, totalD;
            int re1, re2, re3, re4, re5, re6, re7, re8, re9, re10, re11, re12, totalE;
            int percentil = 0, totalGral;
            string rango = string.Empty, diagnostico = string.Empty;

            ra1 = datosRaven.serie_a.Substring(0, 1) == "4" ? 1 : 0;
            ra2 = datosRaven.serie_a.Substring(1, 1) == "5" ? 1 : 0;
            ra3 = datosRaven.serie_a.Substring(2, 1) == "1" ? 1 : 0;
            ra4 = datosRaven.serie_a.Substring(3, 1) == "2" ? 1 : 0;
            ra5 = datosRaven.serie_a.Substring(4, 1) == "6" ? 1 : 0;
            ra6 = datosRaven.serie_a.Substring(5, 1) == "3" ? 1 : 0;
            ra7 = datosRaven.serie_a.Substring(6, 1) == "6" ? 1 : 0;
            ra8 = datosRaven.serie_a.Substring(7, 1) == "2" ? 1 : 0;
            ra9 = datosRaven.serie_a.Substring(8, 1) == "1" ? 1 : 0;
            ra10 = datosRaven.serie_a.Substring(9, 1) == "3" ? 1 : 0;
            ra11 = datosRaven.serie_a.Substring(10, 1) == "4" ? 1 : 0;
            ra12 = datosRaven.serie_a.Substring(11, 1) == "5" ? 1 : 0;

            rb1 = datosRaven.serie_b.Substring(0, 1) == "2" ? 1 : 0;
            rb2 = datosRaven.serie_b.Substring(1, 1) == "6" ? 1 : 0;
            rb3 = datosRaven.serie_b.Substring(2, 1) == "1" ? 1 : 0;
            rb4 = datosRaven.serie_b.Substring(3, 1) == "2" ? 1 : 0;
            rb5 = datosRaven.serie_b.Substring(4, 1) == "1" ? 1 : 0;
            rb6 = datosRaven.serie_b.Substring(5, 1) == "3" ? 1 : 0;
            rb7 = datosRaven.serie_b.Substring(6, 1) == "5" ? 1 : 0;
            rb8 = datosRaven.serie_b.Substring(7, 1) == "6" ? 1 : 0;
            rb9 = datosRaven.serie_b.Substring(8, 1) == "4" ? 1 : 0;
            rb10 = datosRaven.serie_b.Substring(9, 1) == "3" ? 1 : 0;
            rb11 = datosRaven.serie_b.Substring(10, 1) == "4" ? 1 : 0;
            rb12 = datosRaven.serie_b.Substring(11, 1) == "5" ? 1 : 0;

            rc1 = datosRaven.serie_c.Substring(0, 1) == "8" ? 1 : 0;
            rc2 = datosRaven.serie_c.Substring(1, 1) == "2" ? 1 : 0;
            rc3 = datosRaven.serie_c.Substring(2, 1) == "3" ? 1 : 0;
            rc4 = datosRaven.serie_c.Substring(3, 1) == "8" ? 1 : 0;
            rc5 = datosRaven.serie_c.Substring(4, 1) == "7" ? 1 : 0;
            rc6 = datosRaven.serie_c.Substring(5, 1) == "4" ? 1 : 0;
            rc7 = datosRaven.serie_c.Substring(6, 1) == "5" ? 1 : 0;
            rc8 = datosRaven.serie_c.Substring(7, 1) == "1" ? 1 : 0;
            rc9 = datosRaven.serie_c.Substring(8, 1) == "7" ? 1 : 0;
            rc10 = datosRaven.serie_c.Substring(9, 1) == "6" ? 1 : 0;
            rc11 = datosRaven.serie_c.Substring(10, 1) == "1" ? 1 : 0;
            rc12 = datosRaven.serie_c.Substring(11, 1) == "2" ? 1 : 0;

            rd1 = datosRaven.serie_d.Substring(0, 1) == "3" ? 1 : 0;
            rd2 = datosRaven.serie_d.Substring(1, 1) == "4" ? 1 : 0;
            rd3 = datosRaven.serie_d.Substring(2, 1) == "3" ? 1 : 0;
            rd4 = datosRaven.serie_d.Substring(3, 1) == "7" ? 1 : 0;
            rd5 = datosRaven.serie_d.Substring(4, 1) == "8" ? 1 : 0;
            rd6 = datosRaven.serie_d.Substring(5, 1) == "6" ? 1 : 0;
            rd7 = datosRaven.serie_d.Substring(6, 1) == "5" ? 1 : 0;
            rd8 = datosRaven.serie_d.Substring(7, 1) == "4" ? 1 : 0;
            rd9 = datosRaven.serie_d.Substring(8, 1) == "1" ? 1 : 0;
            rd10 = datosRaven.serie_d.Substring(9, 1) == "2" ? 1 : 0;
            rd11 = datosRaven.serie_d.Substring(10, 1) == "5" ? 1 : 0;
            rd12 = datosRaven.serie_d.Substring(11, 1) == "6" ? 1 : 0;

            re1 = datosRaven.serie_e.Substring(0, 1) == "7" ? 1 : 0;
            re2 = datosRaven.serie_e.Substring(1, 1) == "6" ? 1 : 0;
            re3 = datosRaven.serie_e.Substring(2, 1) == "8" ? 1 : 0;
            re4 = datosRaven.serie_e.Substring(3, 1) == "2" ? 1 : 0;
            re5 = datosRaven.serie_e.Substring(4, 1) == "1" ? 1 : 0;
            re6 = datosRaven.serie_e.Substring(5, 1) == "5" ? 1 : 0;
            re7 = datosRaven.serie_e.Substring(6, 1) == "1" ? 1 : 0;
            re8 = datosRaven.serie_e.Substring(7, 1) == "6" ? 1 : 0;
            re9 = datosRaven.serie_e.Substring(8, 1) == "3" ? 1 : 0;
            re10 = datosRaven.serie_e.Substring(9, 1) == "2" ? 1 : 0;
            re11 = datosRaven.serie_e.Substring(10, 1) == "4" ? 1 : 0;
            re12 = datosRaven.serie_e.Substring(11, 1) == "5" ? 1 : 0;

            //totales parciales
            totalA = ra1 + ra2 + ra3 + ra4 + ra5 + ra6 + ra7 + ra8 + ra9 + ra10 + ra11 + ra12;
            totalB = rb1 + rb2 + rb3 + rb4 + rb5 + rb6 + rb7 + rb8 + rb9 + rb10 + rb11 + rb12;
            totalC = rc1 + rc2 + rc3 + rc4 + rc5 + rc6 + rc7 + rc8 + rc9 + rc10 + rc11 + rc12;
            totalD = rd1 + rd2 + rd3 + rd4 + rd5 + rd6 + rd7 + rd8 + rd9 + rd10 + rd11 + rd12;
            totalE = re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12;
            totalGral = totalA + totalB + totalC + totalD + totalE;

            if(datosRaven.sexo == "MUJER")
            {
                if(datosRaven.edad == "18")
                {
                    if (totalGral < 32)
                        percentil = 5;
                    else if (totalGral >= 32 && totalGral <= 42)
                        percentil = 10;
                    else if (totalGral >= 43 && totalGral <= 45)
                        percentil = 25;
                    else if (totalGral >= 46 && totalGral <= 47)
                        percentil = 50;
                    else if (totalGral >= 48 && totalGral <= 49)
                        percentil = 75;
                    else if (totalGral >= 50 && totalGral <= 52)
                        percentil = 90;
                    else if (totalGral >= 53)
                        percentil = 95;
                }
                else if(datosRaven.edad == "19")
                {
                    if (totalGral < 32)
                        percentil = 5;
                    else if (totalGral >= 32 && totalGral <= 37)
                        percentil = 10;
                    else if (totalGral >= 38 && totalGral <= 45)
                        percentil = 25;
                    else if (totalGral >= 46 && totalGral <= 48)
                        percentil = 50;
                    else if (totalGral >= 49 && totalGral <= 50)
                        percentil = 75;
                    else if (totalGral >= 51)
                        percentil = 90;
                    else if (totalGral >= 52)
                        percentil = 95;
                }
                else if (datosRaven.edad == "20")
                {
                    if (totalGral < 42)
                        percentil = 5;
                    else if (totalGral >= 42 && totalGral <= 43)
                        percentil = 10;
                    else if (totalGral >= 44 && totalGral <= 45)
                        percentil = 25;
                    else if (totalGral >= 46 && totalGral <= 48)
                        percentil = 50;
                    else if (totalGral >= 49 && totalGral <= 52)
                        percentil = 75;
                    else if (totalGral >= 53)
                        percentil = 90;
                    else if (totalGral >= 54)
                        percentil = 95;
                }
                else if (datosRaven.edad == "21")
                {
                    if (totalGral < 43)
                        percentil = 5;
                    else if (totalGral == 43)
                        percentil = 10;
                    else if (totalGral >= 44 && totalGral <= 46)
                        percentil = 25;
                    else if (totalGral >= 47 && totalGral <= 51)
                        percentil = 50;
                    else if (totalGral == 52)
                        percentil = 75;
                    else if (totalGral == 53)
                        percentil = 90;
                    else if (totalGral >= 54)
                        percentil = 95;
                }
                else if (Convert.ToInt32(datosRaven.edad) >= 22)
                {
                    if (totalGral < 30)
                        percentil = 5;
                    else if (totalGral >= 30 && totalGral <= 37)
                        percentil = 10;
                    else if (totalGral >= 38 && totalGral <= 44)
                        percentil = 25;
                    else if (totalGral >= 45 && totalGral <= 48)
                        percentil = 50;
                    else if (totalGral == 49 && totalGral <= 51)
                        percentil = 75;
                    else if (totalGral >= 52 && totalGral <= 55)
                        percentil = 90;
                    else if (totalGral >= 56)
                        percentil = 95;
                }
            }
            else
            {
                if (datosRaven.edad == "18")
                {
                    if (totalGral < 41)
                        percentil = 5;
                    else if (totalGral >= 41 && totalGral <= 42)
                        percentil = 10;
                    else if (totalGral >= 43 && totalGral <= 44)
                        percentil = 25;
                    else if (totalGral >= 45 && totalGral <= 48)
                        percentil = 50;
                    else if (totalGral == 49)
                        percentil = 75;
                    else if (totalGral == 50)
                        percentil = 90;
                    else if (totalGral >= 51)
                        percentil = 95;
                }
                else if(datosRaven.edad == "19")
                {
                    if (totalGral < 41)
                        percentil = 5;
                    else if (totalGral >= 41 && totalGral <= 42)
                        percentil = 10;
                    else if (totalGral >= 43 && totalGral <= 44)
                        percentil = 25;
                    else if (totalGral >= 45 && totalGral <= 48)
                        percentil = 50;
                    else if (totalGral == 49)
                        percentil = 75;
                    else if (totalGral == 50)
                        percentil = 90;
                    else if (totalGral >= 51)
                        percentil = 95;
                }
                else if (datosRaven.edad == "20")
                {
                    if (totalGral < 37)
                        percentil = 5;
                    else if (totalGral >= 37 && totalGral <= 43)
                        percentil = 10;
                    else if (totalGral >= 44 && totalGral <= 47)
                        percentil = 25;
                    else if (totalGral >= 48 && totalGral <= 50)
                        percentil = 50;
                    else if (totalGral == 51)
                        percentil = 75;
                    else if (totalGral == 52)
                        percentil = 90;
                    else if (totalGral >= 53)
                        percentil = 95;
                }
                else if (datosRaven.edad == "21")
                {
                    if (totalGral < 40)
                        percentil = 5;
                    else if (totalGral >= 40 && totalGral <= 44)
                        percentil = 10;
                    else if (totalGral >= 45 && totalGral <= 47)
                        percentil = 25;
                    else if (totalGral >= 48 && totalGral <= 51)
                        percentil = 50;
                    else if (totalGral == 52)
                        percentil = 75;
                    else if (totalGral == 53)
                        percentil = 90;
                    else if (totalGral >= 54)
                        percentil = 95;
                }
                else if (Convert.ToInt32(datosRaven.edad) >= 22)
                {
                    if (totalGral < 32)
                        percentil = 5;
                    else if (totalGral >= 32 && totalGral <= 39)
                        percentil = 10;
                    else if (totalGral >= 40 && totalGral <= 43)
                        percentil = 25;
                    else if (totalGral >= 44 && totalGral <= 49)
                        percentil = 50;
                    else if (totalGral >= 50 && totalGral <= 52)
                        percentil = 75;
                    else if (totalGral >= 53 && totalGral <= 55)
                        percentil = 90;
                    else if (totalGral >= 56)
                        percentil = 95;
                }
            }

            if (percentil == 5)
            {
                rango = "V";
                diagnostico = "DEFICIENTE";
            }
            else if (percentil == 10)
            {
                rango = "IV";
                diagnostico = "INFERIOR AL TÉRMINO MEDIO";
            }
            else if (percentil == 25)
            {
                rango = "IV";
                diagnostico = "INFERIOR AL TÉRMINO MEDIO";
            }
            else if (percentil == 50)
            {
                rango = "III";
                diagnostico = "TÉRMINO MEDIO";
            }
            else if (percentil == 75)
            {
                rango = "II";
                diagnostico = "SUPERIOR AL TÉRMINO MEDIO";
            }
            else if (percentil == 90)
            {
                rango = "II";
                diagnostico = "SUPERIOR AL TÉRMINO MEDIO";
            }
            else if (percentil == 95)
            {
                rango = "I";
                diagnostico = "SUPERIOR";
            }



            #endregion

            //Todas las serie pregunta 1
            PdfPCell celSerA1 = new PdfPCell(new Phrase("   1              " + datosRaven.serie_a.Substring(0, 1) + "              " + ra1.ToString(), fontDato));
            celSerA1.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA1.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA1.UseAscender = true;
            celSerA1.BorderWidthBottom = 0;
            celSerA1.FixedHeight = 35f;
            tblRes.AddCell(celSerA1);

            PdfPCell celSerB1 = new PdfPCell(new Phrase("   1              " + datosRaven.serie_b.Substring(0, 1) + "              " + rb1.ToString(), fontDato));
            celSerB1.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB1.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB1.UseAscender = true;
            celSerB1.BorderWidthBottom = 0;
            celSerB1.FixedHeight = 35f;
            tblRes.AddCell(celSerB1);

            PdfPCell celSerC1 = new PdfPCell(new Phrase("   1              " + datosRaven.serie_c.Substring(0, 1) + "              " + rc1.ToString(), fontDato));
            celSerC1.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC1.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC1.UseAscender = true;
            celSerC1.BorderWidthBottom = 0;
            celSerC1.FixedHeight = 35f;
            tblRes.AddCell(celSerC1);

            PdfPCell celSerD1 = new PdfPCell(new Phrase("   1              " + datosRaven.serie_d.Substring(0, 1) + "              " + rd1.ToString(), fontDato));
            celSerD1.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD1.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD1.UseAscender = true;
            celSerD1.BorderWidthBottom = 0;
            celSerD1.FixedHeight = 35f;
            tblRes.AddCell(celSerD1);

            PdfPCell celSerE1 = new PdfPCell(new Phrase("   1              " + datosRaven.serie_e.Substring(0, 1) + "              " + re1.ToString(), fontDato));
            celSerE1.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE1.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE1.UseAscender = true;
            celSerE1.BorderWidthBottom = 0;
            celSerE1.FixedHeight = 35f;
            tblRes.AddCell(celSerE1);

            //Todas las serie pregunta 2
            PdfPCell celSerA2 = new PdfPCell(new Phrase("   2              " + datosRaven.serie_a.Substring(1, 1) + "              " + ra2.ToString(), fontDato));
            celSerA2.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA2.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA2.UseAscender = true;
            celSerA2.BorderWidthTop = 0;
            celSerA2.BorderWidthBottom = 0;
            celSerA2.FixedHeight = 35f;
            tblRes.AddCell(celSerA2);

            PdfPCell celSerB2 = new PdfPCell(new Phrase("   2              " + datosRaven.serie_b.Substring(1, 1) + "              " + rb2.ToString(), fontDato));
            celSerB2.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB2.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB2.UseAscender = true;
            celSerB2.BorderWidthTop = 0;
            celSerB2.BorderWidthBottom = 0;
            celSerB2.FixedHeight = 35f;
            tblRes.AddCell(celSerB2);

            PdfPCell celSerC2 = new PdfPCell(new Phrase("   2              " + datosRaven.serie_c.Substring(1, 1) + "              " + rc2.ToString(), fontDato));
            celSerC2.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC2.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC2.UseAscender = true;
            celSerC2.BorderWidthTop = 0;
            celSerC2.BorderWidthBottom = 0;
            celSerC2.FixedHeight = 35f;
            tblRes.AddCell(celSerC2);

            PdfPCell celSerD2 = new PdfPCell(new Phrase("   2              " + datosRaven.serie_d.Substring(1, 1) + "              " + rd2.ToString(), fontDato));
            celSerD2.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD2.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD2.UseAscender = true;
            celSerD2.BorderWidthTop = 0;
            celSerD2.BorderWidthBottom = 0;
            celSerD2.FixedHeight = 35f;
            tblRes.AddCell(celSerD2);

            PdfPCell celSerE2 = new PdfPCell(new Phrase("   2              " + datosRaven.serie_e.Substring(1, 1) + "              " + re3.ToString(), fontDato));
            celSerE2.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE2.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE2.UseAscender = true;
            celSerE2.BorderWidthTop = 0;
            celSerE2.BorderWidthBottom = 0;
            celSerE2.FixedHeight = 35f;
            tblRes.AddCell(celSerE2);

            //Todas las serie pregunta 3
            PdfPCell celSerA3 = new PdfPCell(new Phrase("   3              " + datosRaven.serie_a.Substring(2, 1) + "              " + ra3.ToString(), fontDato));
            celSerA3.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA3.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA3.UseAscender = true;
            celSerA3.BorderWidthTop = 0;
            celSerA3.BorderWidthBottom = 0;
            celSerA3.FixedHeight = 35f;
            tblRes.AddCell(celSerA3);

            PdfPCell celSerB3 = new PdfPCell(new Phrase("   3              " + datosRaven.serie_b.Substring(2, 1) + "              " + rb3.ToString(), fontDato));
            celSerB3.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB3.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB3.UseAscender = true;
            celSerB3.BorderWidthTop = 0;
            celSerB3.BorderWidthBottom = 0;
            celSerB3.FixedHeight = 35f;
            tblRes.AddCell(celSerB3);

            PdfPCell celSerC3 = new PdfPCell(new Phrase("   3              " + datosRaven.serie_c.Substring(2, 1) + "              " + rc3.ToString(), fontDato));
            celSerC3.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC3.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC3.UseAscender = true;
            celSerC3.BorderWidthTop = 0;
            celSerC3.BorderWidthBottom = 0;
            celSerC3.FixedHeight = 35f;
            tblRes.AddCell(celSerC3);

            PdfPCell celSerD3 = new PdfPCell(new Phrase("   3              " + datosRaven.serie_d.Substring(2, 1) + "              " + rd3.ToString(), fontDato));
            celSerD3.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD3.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD3.UseAscender = true;
            celSerD3.BorderWidthTop = 0;
            celSerD3.BorderWidthBottom = 0;
            celSerD3.FixedHeight = 35f;
            tblRes.AddCell(celSerD3);

            PdfPCell celSerE3 = new PdfPCell(new Phrase("   3              " + datosRaven.serie_e.Substring(2, 1) + "              " + re3.ToString(), fontDato));
            celSerE3.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE3.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE3.UseAscender = true;
            celSerE3.BorderWidthTop = 0;
            celSerE3.BorderWidthBottom = 0;
            celSerE3.FixedHeight = 35f;
            tblRes.AddCell(celSerE3);

            //Todas las serie pregunta 4
            PdfPCell celSerA4 = new PdfPCell(new Phrase("   4              " + datosRaven.serie_a.Substring(3, 1) + "              " + ra4.ToString(), fontDato));
            celSerA4.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA4.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA4.UseAscender = true;
            celSerA4.BorderWidthTop = 0;
            celSerA4.BorderWidthBottom = 0;
            celSerA4.FixedHeight = 35f;
            tblRes.AddCell(celSerA4);

            PdfPCell celSerB4 = new PdfPCell(new Phrase("   4              " + datosRaven.serie_b.Substring(3, 1) + "              " + rb4.ToString(), fontDato));
            celSerB4.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB4.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB4.UseAscender = true;
            celSerB4.BorderWidthTop = 0;
            celSerB4.BorderWidthBottom = 0;
            celSerB4.FixedHeight = 35f;
            tblRes.AddCell(celSerB4);

            PdfPCell celSerC4 = new PdfPCell(new Phrase("   4              " + datosRaven.serie_c.Substring(3, 1) + "              " + rc4.ToString(), fontDato));
            celSerC4.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC4.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC4.UseAscender = true;
            celSerC4.BorderWidthTop = 0;
            celSerC4.BorderWidthBottom = 0;
            celSerC4.FixedHeight = 35f;
            tblRes.AddCell(celSerC4);

            PdfPCell celSerD4 = new PdfPCell(new Phrase("   4              " + datosRaven.serie_d.Substring(3, 1) + "              " + rd4.ToString(), fontDato));
            celSerD4.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD4.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD4.UseAscender = true;
            celSerD4.BorderWidthTop = 0;
            celSerD4.BorderWidthBottom = 0;
            celSerD4.FixedHeight = 35f;
            tblRes.AddCell(celSerD4);

            PdfPCell celSerE4 = new PdfPCell(new Phrase("   4              " + datosRaven.serie_e.Substring(3, 1) + "              " + re4.ToString(), fontDato));
            celSerE4.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE4.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE4.UseAscender = true;
            celSerE4.BorderWidthTop = 0;
            celSerE4.BorderWidthBottom = 0;
            celSerE4.FixedHeight = 35f;
            tblRes.AddCell(celSerE4);

            //Todas las serie pregunta 5
            PdfPCell celSerA5 = new PdfPCell(new Phrase("   5              " + datosRaven.serie_a.Substring(4, 1) + "              " + ra5.ToString(), fontDato));
            celSerA5.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA5.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA5.UseAscender = true;
            celSerA5.BorderWidthTop = 0;
            celSerA5.BorderWidthBottom = 0;
            celSerA5.FixedHeight = 35f;
            tblRes.AddCell(celSerA5);

            PdfPCell celSerB5 = new PdfPCell(new Phrase("   5              " + datosRaven.serie_b.Substring(4, 1) + "              " + rb5.ToString(), fontDato));
            celSerB5.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB5.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB5.UseAscender = true;
            celSerB5.BorderWidthTop = 0;
            celSerB5.BorderWidthBottom = 0;
            celSerB5.FixedHeight = 35f;
            tblRes.AddCell(celSerB5);

            PdfPCell celSerC5 = new PdfPCell(new Phrase("   5              " + datosRaven.serie_c.Substring(4, 1) + "              " + rc5.ToString(), fontDato));
            celSerC5.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC5.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC5.UseAscender = true;
            celSerC5.BorderWidthTop = 0;
            celSerC5.BorderWidthBottom = 0;
            celSerC5.FixedHeight = 35f;
            tblRes.AddCell(celSerC5);

            PdfPCell celSerD5 = new PdfPCell(new Phrase("   5              " + datosRaven.serie_d.Substring(4, 1) + "              " + rd5.ToString(), fontDato));
            celSerD5.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD5.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD5.UseAscender = true;
            celSerD5.BorderWidthTop = 0;
            celSerD5.BorderWidthBottom = 0;
            celSerD5.FixedHeight = 35f;
            tblRes.AddCell(celSerD5);

            PdfPCell celSerE5 = new PdfPCell(new Phrase("   5              " + datosRaven.serie_e.Substring(4, 1) + "              " + re5.ToString(), fontDato));
            celSerE5.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE5.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE5.UseAscender = true;
            celSerE5.BorderWidthTop = 0;
            celSerE5.BorderWidthBottom = 0;
            celSerE5.FixedHeight = 35f;
            tblRes.AddCell(celSerE5);

            //Todas las serie pregunta 6
            PdfPCell celSerA6 = new PdfPCell(new Phrase("   6              " + datosRaven.serie_a.Substring(5, 1) + "              " + ra6.ToString(), fontDato));
            celSerA6.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA6.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA6.UseAscender = true;
            celSerA6.BorderWidthTop = 0;
            celSerA6.BorderWidthBottom = 0;
            celSerA6.FixedHeight = 35f;
            tblRes.AddCell(celSerA6);

            PdfPCell celSerB6 = new PdfPCell(new Phrase("   6              " + datosRaven.serie_b.Substring(5, 1) + "              " + rb6.ToString(), fontDato));
            celSerB6.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB6.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB6.UseAscender = true;
            celSerB6.BorderWidthTop = 0;
            celSerB6.BorderWidthBottom = 0;
            celSerB6.FixedHeight = 35f;
            tblRes.AddCell(celSerB6);

            PdfPCell celSerC6 = new PdfPCell(new Phrase("   6              " + datosRaven.serie_c.Substring(5, 1) + "              " + rc6.ToString(), fontDato));
            celSerC6.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC6.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC6.UseAscender = true;
            celSerC6.BorderWidthTop = 0;
            celSerC6.BorderWidthBottom = 0;
            celSerC6.FixedHeight = 35f;
            tblRes.AddCell(celSerC6);

            PdfPCell celSerD6 = new PdfPCell(new Phrase("   6              " + datosRaven.serie_d.Substring(5, 1) + "              " + rd6.ToString(), fontDato));
            celSerD6.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD6.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD6.UseAscender = true;
            celSerD6.BorderWidthTop = 0;
            celSerD6.BorderWidthBottom = 0;
            celSerD6.FixedHeight = 35f;
            tblRes.AddCell(celSerD6);

            PdfPCell celSerE6 = new PdfPCell(new Phrase("   6              " + datosRaven.serie_e.Substring(5, 1) + "              " + re6.ToString(), fontDato));
            celSerE6.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE6.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE6.UseAscender = true;
            celSerE6.BorderWidthTop = 0;
            celSerE6.BorderWidthBottom = 0;
            celSerE6.FixedHeight = 35f;
            tblRes.AddCell(celSerE6);

            //Todas las serie pregunta 7
            PdfPCell celSerA7 = new PdfPCell(new Phrase("   7              " + datosRaven.serie_a.Substring(6, 1) + "              " + ra7.ToString(), fontDato));
            celSerA7.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA7.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA7.UseAscender = true;
            celSerA7.BorderWidthTop = 0;
            celSerA7.BorderWidthBottom = 0;
            celSerA7.FixedHeight = 35f;
            tblRes.AddCell(celSerA7);

            PdfPCell celSerB7 = new PdfPCell(new Phrase("   7              " + datosRaven.serie_b.Substring(6, 1) + "              " + rb7.ToString(), fontDato));
            celSerB7.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB7.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB7.UseAscender = true;
            celSerB7.BorderWidthTop = 0;
            celSerB7.BorderWidthBottom = 0;
            celSerB7.FixedHeight = 35f;
            tblRes.AddCell(celSerB7);

            PdfPCell celSerC7 = new PdfPCell(new Phrase("   7              " + datosRaven.serie_c.Substring(6, 1) + "              " + rc7.ToString(), fontDato));
            celSerC7.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC7.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC7.UseAscender = true;
            celSerC7.BorderWidthTop = 0;
            celSerC7.BorderWidthBottom = 0;
            celSerC7.FixedHeight = 35f;
            tblRes.AddCell(celSerC7);

            PdfPCell celSerD7 = new PdfPCell(new Phrase("   7              " + datosRaven.serie_d.Substring(6, 1) + "              " + rd7.ToString(), fontDato));
            celSerD7.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD7.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD7.UseAscender = true;
            celSerD7.BorderWidthTop = 0;
            celSerD7.BorderWidthBottom = 0;
            celSerD7.FixedHeight = 35f;
            tblRes.AddCell(celSerD7);

            PdfPCell celSerE7 = new PdfPCell(new Phrase("   7              " + datosRaven.serie_e.Substring(6, 1) + "              " + re7.ToString(), fontDato));
            celSerE7.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE7.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE7.UseAscender = true;
            celSerE7.BorderWidthTop = 0;
            celSerE7.BorderWidthBottom = 0;
            celSerE7.FixedHeight = 35f;
            tblRes.AddCell(celSerE7);

            //Todas las serie pregunta 8
            PdfPCell celSerA8 = new PdfPCell(new Phrase("   8              " + datosRaven.serie_a.Substring(7, 1) + "              " + ra8.ToString(), fontDato));
            celSerA8.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA8.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA8.UseAscender = true;
            celSerA8.BorderWidthTop = 0;
            celSerA8.BorderWidthBottom = 0;
            celSerA8.FixedHeight = 35f;
            tblRes.AddCell(celSerA8);

            PdfPCell celSerB8 = new PdfPCell(new Phrase("   8              " + datosRaven.serie_b.Substring(7, 1) + "              " + rb8.ToString(), fontDato));
            celSerB8.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB8.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB8.UseAscender = true;
            celSerB8.BorderWidthTop = 0;
            celSerB8.BorderWidthBottom = 0;
            celSerB8.FixedHeight = 35f;
            tblRes.AddCell(celSerB8);

            PdfPCell celSerC8 = new PdfPCell(new Phrase("   8              " + datosRaven.serie_c.Substring(7, 1) + "              " + rc8.ToString(), fontDato));
            celSerC8.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC8.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC8.UseAscender = true;
            celSerC8.BorderWidthTop = 0;
            celSerC8.BorderWidthBottom = 0;
            celSerC8.FixedHeight = 35f;
            tblRes.AddCell(celSerC8);

            PdfPCell celSerD8 = new PdfPCell(new Phrase("   8              " + datosRaven.serie_d.Substring(7, 1) + "              " + rd8.ToString(), fontDato));
            celSerD8.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD8.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD8.UseAscender = true;
            celSerD8.BorderWidthTop = 0;
            celSerD8.BorderWidthBottom = 0;
            celSerD8.FixedHeight = 35f;
            tblRes.AddCell(celSerD8);

            PdfPCell celSerE8 = new PdfPCell(new Phrase("   8              " + datosRaven.serie_e.Substring(7, 1) + "              " + re8.ToString(), fontDato));
            celSerE8.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE8.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE8.UseAscender = true;
            celSerE8.BorderWidthTop = 0;
            celSerE8.BorderWidthBottom = 0;
            celSerE8.FixedHeight = 35f;
            tblRes.AddCell(celSerE8);

            //Todas las serie pregunta 9
            PdfPCell celSerA9 = new PdfPCell(new Phrase("   9              " + datosRaven.serie_a.Substring(8, 1) + "              " + ra9.ToString(), fontDato));
            celSerA9.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA9.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA9.UseAscender = true;
            celSerA9.BorderWidthTop = 0;
            celSerA9.BorderWidthBottom = 0;
            celSerA9.FixedHeight = 35f;
            tblRes.AddCell(celSerA9);

            PdfPCell celSerB9 = new PdfPCell(new Phrase("   9              " + datosRaven.serie_b.Substring(8, 1) + "              " + rb9.ToString(), fontDato));
            celSerB9.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB9.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB9.UseAscender = true;
            celSerB9.BorderWidthTop = 0;
            celSerB9.BorderWidthBottom = 0;
            celSerB9.FixedHeight = 35f;
            tblRes.AddCell(celSerB9);

            PdfPCell celSerC9 = new PdfPCell(new Phrase("   9              " + datosRaven.serie_c.Substring(8, 1) + "              " + rc9.ToString(), fontDato));
            celSerC9.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC9.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC9.UseAscender = true;
            celSerC9.BorderWidthTop = 0;
            celSerC9.BorderWidthBottom = 0;
            celSerC9.FixedHeight = 35f;
            tblRes.AddCell(celSerC9);

            PdfPCell celSerD9 = new PdfPCell(new Phrase("   9              " + datosRaven.serie_d.Substring(8, 1) + "              " + rd9.ToString(), fontDato));
            celSerD9.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD9.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD9.UseAscender = true;
            celSerD9.BorderWidthTop = 0;
            celSerD9.BorderWidthBottom = 0;
            celSerD9.FixedHeight = 35f;
            tblRes.AddCell(celSerD9);

            PdfPCell celSerE9 = new PdfPCell(new Phrase("   9              " + datosRaven.serie_e.Substring(8, 1) + "              " + re9.ToString(), fontDato));
            celSerE9.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE9.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE9.UseAscender = true;
            celSerE9.BorderWidthTop = 0;
            celSerE9.BorderWidthBottom = 0;
            celSerE9.FixedHeight = 35f;
            tblRes.AddCell(celSerE9);

            //Todas las serie pregunta 10
            PdfPCell celSerA10 = new PdfPCell(new Phrase("  10              " + datosRaven.serie_a.Substring(9, 1) + "              " + ra10.ToString(), fontDato));
            celSerA10.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA10.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA10.UseAscender = true;
            celSerA10.BorderWidthTop = 0;
            celSerA10.BorderWidthBottom = 0;
            celSerA10.FixedHeight = 35f;
            tblRes.AddCell(celSerA10);

            PdfPCell celSerB10 = new PdfPCell(new Phrase("  10              " + datosRaven.serie_b.Substring(9, 1) + "              " + rb10.ToString(), fontDato));
            celSerB10.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB10.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB10.UseAscender = true;
            celSerB10.BorderWidthTop = 0;
            celSerB10.BorderWidthBottom = 0;
            celSerB10.FixedHeight = 35f;
            tblRes.AddCell(celSerB10);

            PdfPCell celSerC10 = new PdfPCell(new Phrase("  10              " + datosRaven.serie_c.Substring(9, 1) + "              " + rc10.ToString(), fontDato));
            celSerC10.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC10.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC10.UseAscender = true;
            celSerC10.BorderWidthTop = 0;
            celSerC10.BorderWidthBottom = 0;
            celSerC10.FixedHeight = 35f;
            tblRes.AddCell(celSerC10);

            PdfPCell celSerD10 = new PdfPCell(new Phrase("  10              " + datosRaven.serie_d.Substring(9, 1) + "              " + rd10.ToString(), fontDato));
            celSerD10.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD10.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD10.UseAscender = true;
            celSerD10.BorderWidthTop = 0;
            celSerD10.BorderWidthBottom = 0;
            celSerD10.FixedHeight = 35f;
            tblRes.AddCell(celSerD10);

            PdfPCell celSerE10 = new PdfPCell(new Phrase("  10              " + datosRaven.serie_e.Substring(9, 1) + "              " + re10.ToString(), fontDato));
            celSerE10.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE10.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE10.UseAscender = true;
            celSerE10.BorderWidthTop = 0;
            celSerE10.BorderWidthBottom = 0;
            celSerE10.FixedHeight = 35f;
            tblRes.AddCell(celSerE10);

            //Todas las serie pregunta 11
            PdfPCell celSerA11 = new PdfPCell(new Phrase("  11              " + datosRaven.serie_a.Substring(10, 1) + "              " + ra11.ToString(), fontDato));
            celSerA11.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA11.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA11.UseAscender = true;
            celSerA11.BorderWidthTop = 0;
            celSerA11.BorderWidthBottom = 0;
            celSerA11.FixedHeight = 35f;
            tblRes.AddCell(celSerA11);

            PdfPCell celSerB11 = new PdfPCell(new Phrase("  11              " + datosRaven.serie_b.Substring(10, 1) + "              " + rb11.ToString(), fontDato));
            celSerB11.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB11.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB11.UseAscender = true;
            celSerB11.BorderWidthTop = 0;
            celSerB11.BorderWidthBottom = 0;
            celSerB11.FixedHeight = 35f;
            tblRes.AddCell(celSerB11);

            PdfPCell celSerC11 = new PdfPCell(new Phrase("  11              " + datosRaven.serie_c.Substring(10, 1) + "              " + rc11.ToString(), fontDato));
            celSerC11.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC11.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC11.UseAscender = true;
            celSerC11.BorderWidthTop = 0;
            celSerC11.BorderWidthBottom = 0;
            celSerC11.FixedHeight = 35f;
            tblRes.AddCell(celSerC11);

            PdfPCell celSerD11 = new PdfPCell(new Phrase("  11              " + datosRaven.serie_d.Substring(10, 1) + "              " + rd11.ToString(), fontDato));
            celSerD11.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD11.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD11.UseAscender = true;
            celSerD11.BorderWidthTop = 0;
            celSerD11.BorderWidthBottom = 0;
            celSerD11.FixedHeight = 35f;
            tblRes.AddCell(celSerD11);

            PdfPCell celSerE11 = new PdfPCell(new Phrase("  11              " + datosRaven.serie_e.Substring(10, 1) + "              " + re11.ToString(), fontDato));
            celSerE11.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE11.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE11.UseAscender = true;
            celSerE11.BorderWidthTop = 0;
            celSerE11.BorderWidthBottom = 0;
            celSerE11.FixedHeight = 35f;
            tblRes.AddCell(celSerE11);

            //Todas las serie pregunta 12
            PdfPCell celSerA12 = new PdfPCell(new Phrase("  12              " + datosRaven.serie_a.Substring(11, 1) + "              " + ra12.ToString(), fontDato));
            celSerA12.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA12.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA12.UseAscender = true;
            celSerA12.BorderWidthTop = 0;
            celSerA12.FixedHeight = 35f;
            tblRes.AddCell(celSerA12);

            PdfPCell celSerB12 = new PdfPCell(new Phrase("  12              " + datosRaven.serie_b.Substring(11, 1) + "              " + rb12.ToString(), fontDato));
            celSerB12.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB12.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB12.UseAscender = true;
            celSerB12.BorderWidthTop = 0;
            celSerB12.FixedHeight = 35f;
            tblRes.AddCell(celSerB12);

            PdfPCell celSerC12 = new PdfPCell(new Phrase("  12              " + datosRaven.serie_c.Substring(11, 1) + "              " + rc12.ToString(), fontDato));
            celSerC12.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC12.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC12.UseAscender = true;
            celSerC12.BorderWidthTop = 0;
            celSerC12.FixedHeight = 35f;
            tblRes.AddCell(celSerC12);

            PdfPCell celSerD12 = new PdfPCell(new Phrase("  12              " + datosRaven.serie_d.Substring(11, 1) + "              " + rd12.ToString(), fontDato));
            celSerD12.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD12.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD12.UseAscender = true;
            celSerD12.BorderWidthTop = 0;
            celSerD12.FixedHeight = 35f;
            tblRes.AddCell(celSerD12);

            PdfPCell celSerE12 = new PdfPCell(new Phrase("  12              " + datosRaven.serie_e.Substring(11, 1) + "              " + re12.ToString(), fontDato));
            celSerE12.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE12.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE12.UseAscender = true;
            celSerE12.BorderWidthTop = 0;
            celSerE12.FixedHeight = 35f;
            tblRes.AddCell(celSerE12);

            //Puntajes parciales
            PdfPCell celSerA13 = new PdfPCell(new Phrase(" Puntaje Parcial         " + totalA.ToString(), fontDato));
            celSerA13.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerA13.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerA13.UseAscender = true;
            celSerA13.FixedHeight = 35f;
            tblRes.AddCell(celSerA13);

            PdfPCell celSerB13 = new PdfPCell(new Phrase(" Puntaje Parcial         " + totalB.ToString(), fontDato));
            celSerB13.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerB13.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerB13.UseAscender = true;
            celSerB13.FixedHeight = 35f;
            tblRes.AddCell(celSerB13);

            PdfPCell celSerC13 = new PdfPCell(new Phrase(" Puntaje Parcial         " + totalC.ToString(), fontDato));
            celSerC13.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerC13.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerC13.UseAscender = true;
            celSerC13.FixedHeight = 35f;
            tblRes.AddCell(celSerC13);

            PdfPCell celSerD13 = new PdfPCell(new Phrase(" Puntaje Parcial         " + totalD.ToString(), fontDato));
            celSerD13.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerD13.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerD13.UseAscender = true;
            celSerD13.FixedHeight = 35f;
            tblRes.AddCell(celSerD13);

            PdfPCell celSerE13 = new PdfPCell(new Phrase(" Puntaje Parcial         " + totalE.ToString(), fontDato));
            celSerE13.HorizontalAlignment = Element.ALIGN_LEFT;
            celSerE13.VerticalAlignment = Element.ALIGN_MIDDLE;
            celSerE13.UseAscender = true;
            celSerE13.FixedHeight = 35f;
            tblRes.AddCell(celSerE13);

            elRaven.Add(tblRes);

            #endregion

            Paragraph califica = new Paragraph();
            califica.Add(new Phrase("PUNTAJE TOTAL", fontDato)); 
            califica.Add(Chunk.TABBING); 
            califica.Add(new Phrase(totalGral.ToString(), fonEiqueta));
            califica.Add(Chunk.NEWLINE);
            califica.Add(new Phrase("PERCENTIL", fontDato));
            califica.Add(Chunk.TABBING); califica.Add(Chunk.TABBING);
            califica.Add(new Phrase(percentil.ToString(), fonEiqueta));
            califica.Add(Chunk.NEWLINE);
            califica.Add(new Phrase("RANGO", fontDato));
            califica.Add(Chunk.TABBING); califica.Add(Chunk.TABBING); califica.Add(Chunk.TABBING);
            califica.Add(new Phrase(rango, fonEiqueta));
            califica.Add(Chunk.NEWLINE);
            califica.Add(new Phrase("DIAGNOSTICO", fontDato));
            califica.Add(Chunk.TABBING); califica.Add(Chunk.TABBING);
            califica.Add(new Phrase(diagnostico, fonEiqueta));
            califica.Add(Chunk.NEWLINE);

            elRaven.Add(califica);

            elRaven.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }

    class HeaderTerman : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Psicologia", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell2 = new PdfPCell(new Paragraph("Gráfica de rendimiento intelectual de Terman", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));

            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell2.HorizontalAlignment = Element.ALIGN_CENTER;

            _cell.Border = 0;
            _cell2.Border = 0;

            tbHeader.AddCell(_cell);
            tbHeader.AddCell(_cell2);

            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
        }
    }

    class HeaderRaven : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Psicologia", FontFactory.GetFont("ARIAL", 11, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell2 = new PdfPCell(new Paragraph("Resultados Raven", FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD)));

            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell2.HorizontalAlignment = Element.ALIGN_CENTER;

            _cell.Border = 0;
            _cell2.Border = 0;

            tbHeader.AddCell(_cell);
            tbHeader.AddCell(_cell2);

            tbHeader.AddCell(new Paragraph());

            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
        }
    }
}