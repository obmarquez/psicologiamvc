using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace psicologiamvc.Controllers
{
    public class ImpresionesController : Controller
    {
        float[] widthsTitulosGenerales = new float[] { 1f };

        // GET: Impresiones
        public ActionResult Index()
        {
            return View();
        }

        //[FiltroPermisos]
        public ActionResult printInvestigacionPsicologica(int sIdH) //Cat_controlador_Acciones: 
        {
            var datosInvPsi = RepInvPsi.getRepInvPsi(sIdH).FirstOrDefault();

            MemoryStream ms = new MemoryStream();

            //Document documentInvPsi = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            Document documentInvPsi = new Document(PageSize.LETTER, 30f, 20f, 80f, 50f);

            PdfWriter pwInvPsi = PdfWriter.GetInstance(documentInvPsi, ms);

            string elCodigoEvaluado = datosInvPsi.codigoevaluado;

            //pwInvPsi.PageEvent = new HeaderFooter.getMultilineFooter(elCodigoEvaluado);
            pwInvPsi.PageEvent = HeaderFooter.getMultilineFooter(elCodigoEvaluado);

            documentInvPsi.Open();

            //------------------------------------------------------------------------------------para verlo en mi el proyecto
            //string imagenPath = @"C:\Net 2012\psicologiamvc\psicologiamvc\Content\img\gobedohor.png";
            //Image imagen = Image.GetInstance(imagenPath);
            //imagen.ScalePercent(35f);
            //imagen.SetAbsolutePosition(30f, 725f);
            //documentInvPsi.Add(imagen);          

            //------------------------------------------------------------------------------------para verlo en SERVIDOR
            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            documentInvPsi.Add(imagen);
            //------------------------------------------------------------------------------------para verlo en SERVIDOR

            #region Datos Generales
            Chunk chunk = new Chunk();
            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;

            //------------------------------------ ver imagen desde base de datos
            Byte[] bytes = (Byte[])datosInvPsi.Picture;
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(bytes);
            img.ScalePercent(50f);
            //img.SetAbsolutePosition(500f, 600f); //ORIGINAL
            img.SetAbsolutePosition(500f, 580f);
            derecha.Add(img);

            //derecha.Add(Chunk.NEWLINE);
            //derecha.Add(Chunk.NEWLINE);
            //derecha.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            //derecha.Add("Codigo Evaluado: ");
            //derecha.Add(Chunk.TABBING);
            //derecha.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            //derecha.Add(DateTime.Now.ToShortDateString());
            //derecha.Add(datosInvPsi.codigoevaluado);

            documentInvPsi.Add(derecha);
            #endregion

            #region Titulo Datos Generales
            PdfPTable tableTituloDatoGeneral = new PdfPTable(1);
            tableTituloDatoGeneral.TotalWidth = 560f;
            tableTituloDatoGeneral.LockedWidth = true;

            tableTituloDatoGeneral.SetWidths(widthsTitulosGenerales);
            tableTituloDatoGeneral.HorizontalAlignment = 0;
            tableTituloDatoGeneral.SpacingBefore = 10f;
            tableTituloDatoGeneral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloDato = new PdfPCell(new Phrase("DATOS GENERALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloDato.HorizontalAlignment = 1;
            cellTituloTituloDato.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloDato.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloDato.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloDatoGeneral.AddCell(cellTituloTituloDato);

            documentInvPsi.Add(tableTituloDatoGeneral);
            #endregion

            #region Contenido Datos Generales 2
            Paragraph datosGenerales = new Paragraph();
            datosGenerales.Alignment = Element.ALIGN_LEFT;
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Nombre: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.evaluado);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Edad: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.edad);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Genero: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.sexo);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Escolaridad: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.cEscolaridad);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Grado: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.cGrado);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("RFC: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.rfc);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("CURP: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.curp);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Domicilio: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.domicilio);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Lugar de Nacimiento: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.cLugarnac);
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Telefonos: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.telefonos);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Motivo de Evaluacion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.cevaluacion);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Dependencia: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.desc_dependencia);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Puesto: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.puesto);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Categoria del puesto: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.puestoTabular);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Funcion declarada: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.funcion);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Funcion institucional: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.funcionInstitucional);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Area de adscripcion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.cAdscripcion);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Comisionado: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.comision);
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosGenerales.Add("Fecha de evaluacion: ");
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            datosGenerales.Add(datosInvPsi.fEvalpsi);
            datosGenerales.Add(Chunk.NEWLINE);

            documentInvPsi.Add(datosGenerales);
            #endregion

            #region Trayectoria laboral
            PdfPTable tableTrayectoria = new PdfPTable(1);
            tableTrayectoria.TotalWidth = 560f;
            tableTrayectoria.LockedWidth = true;

            tableTrayectoria.SetWidths(widthsTitulosGenerales);
            tableTrayectoria.HorizontalAlignment = 0;
            tableTrayectoria.SpacingBefore = 20f;
            tableTrayectoria.SpacingAfter = 10f;

            PdfPCell cellTrayectoria = new PdfPCell(new Phrase("TRAYECTORIA LABORAL", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTrayectoria.HorizontalAlignment = 1;
            cellTrayectoria.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTrayectoria.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTrayectoria.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTrayectoria.AddCell(cellTrayectoria);

            documentInvPsi.Add(tableTrayectoria);
            #endregion

            #region Contenido Trayectoria
            Paragraph trayectoria = new Paragraph();
            trayectoria.Alignment = Element.ALIGN_JUSTIFIED;

            trayectoria.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            trayectoria.Add(datosInvPsi.cLaboralpsi);

            documentInvPsi.Add(trayectoria);
            #endregion

            #region Perfil psicologica
            PdfPTable tablePerfil = new PdfPTable(1);
            tablePerfil.TotalWidth = 560f;
            tablePerfil.LockedWidth = true;

            tablePerfil.SetWidths(widthsTitulosGenerales);
            tablePerfil.HorizontalAlignment = 0;
            tablePerfil.SpacingBefore = 20f;
            tablePerfil.SpacingAfter = 10f;

            PdfPCell cellPerfil = new PdfPCell(new Phrase("PERFIL PSICOLOGICO FORTALEZAS - FACTORES DE RIESGO", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellPerfil.HorizontalAlignment = 1;
            cellPerfil.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellPerfil.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellPerfil.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tablePerfil.AddCell(cellPerfil);

            documentInvPsi.Add(tablePerfil);
            #endregion

            #region Contenido Perfil
            Paragraph perfil = new Paragraph();
            perfil.Alignment = Element.ALIGN_JUSTIFIED;

            perfil.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            perfil.Add(datosInvPsi.fortaleza_riesgo);

            documentInvPsi.Add(perfil);
            #endregion

            #region Notas
            PdfPTable tableNotas = new PdfPTable(1);
            tableNotas.TotalWidth = 560f;
            tableNotas.LockedWidth = true;

            tableNotas.SetWidths(widthsTitulosGenerales);
            tableNotas.HorizontalAlignment = 0;
            tableNotas.SpacingBefore = 20f;
            tableNotas.SpacingAfter = 10f;

            PdfPCell cellNotas = new PdfPCell(new Phrase("NOTAS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellNotas.HorizontalAlignment = 1;
            cellNotas.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellNotas.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellNotas.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableNotas.AddCell(cellNotas);

            documentInvPsi.Add(tableNotas);
            #endregion

            #region Contenido Notas
            Paragraph notas = new Paragraph();
            notas.Alignment = Element.ALIGN_JUSTIFIED;

            notas.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            notas.Add(datosInvPsi.notas);

            documentInvPsi.Add(notas);

            documentInvPsi.Add(Chunk.NEWLINE);
            documentInvPsi.Add(Chunk.NEWLINE);
            #endregion

            #region Competencias            
            Paragraph competencia = new Paragraph();
            competencia.Alignment = Element.ALIGN_LEFT;

            competencia.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            competencia.Add("Competencia para el cargo: ");
            competencia.Add(Chunk.TABBING);
            competencia.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            competencia.Add(datosInvPsi.cCompetencia);
            competencia.Add(Chunk.NEWLINE);
            competencia.Add(Chunk.NEWLINE);

            documentInvPsi.Add(competencia);
            #endregion

            #region Titulo Criterios Mayores
            PdfPTable tableCriterios = new PdfPTable(1);
            tableCriterios.TotalWidth = 560f;
            tableCriterios.LockedWidth = true;

            tableCriterios.SetWidths(widthsTitulosGenerales);
            tableCriterios.HorizontalAlignment = 0;
            tableCriterios.SpacingBefore = 20f;
            tableCriterios.SpacingAfter = 10f;

            PdfPCell cellCriterio = new PdfPCell(new Phrase("CRITERIOS MAYORES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellCriterio.HorizontalAlignment = 1;
            cellCriterio.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellCriterio.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellCriterio.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableCriterios.AddCell(cellCriterio);

            documentInvPsi.Add(tableCriterios);
            #endregion

            #region Contenido criterios mayores

            Paragraph datosCriteriosMayores = new Paragraph();
            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMayores.Add("C1: ");
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if(datosInvPsi.bJuicio == true)
                datosCriteriosMayores.Add(" X ");
            else
                datosCriteriosMayores.Add("   ");

            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);

            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMayores.Add("C2: ");
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bAutoestima == true)
                datosCriteriosMayores.Add(" X ");
            else
                datosCriteriosMayores.Add("   ");

            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);

            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMayores.Add("C3: ");
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bManejo == true)
                datosCriteriosMayores.Add(" X ");
            else
                datosCriteriosMayores.Add("   ");

            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Add(Chunk.TABBING);

            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMayores.Add("C4: ");
            datosCriteriosMayores.Add(Chunk.TABBING);
            datosCriteriosMayores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bAlteraciones == true)
                datosCriteriosMayores.Add(" X ");
            else
                datosCriteriosMayores.Add("   ");

            datosCriteriosMayores.Add(Chunk.NEWLINE);


            documentInvPsi.Add(datosCriteriosMayores);

            #endregion

            #region Titulo Criterios Menores
            PdfPTable tableCriteriosMenores = new PdfPTable(1);
            tableCriteriosMenores.TotalWidth = 560f;
            tableCriteriosMenores.LockedWidth = true;

            tableCriteriosMenores.SetWidths(widthsTitulosGenerales);
            tableCriteriosMenores.HorizontalAlignment = 0;
            tableCriteriosMenores.SpacingBefore = 20f;
            tableCriteriosMenores.SpacingAfter = 10f;

            PdfPCell cellCriterioMenores = new PdfPCell(new Phrase("CRITERIOS MENORES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellCriterioMenores.HorizontalAlignment = 1;
            cellCriterioMenores.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellCriterioMenores.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellCriterioMenores.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableCriteriosMenores.AddCell(cellCriterioMenores);

            documentInvPsi.Add(tableCriteriosMenores);
            #endregion

            #region Contenido Criterios Menores

            Paragraph datosCriteriosMenores = new Paragraph();
            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMenores.Add("CA: ");
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bApego == true)
                datosCriteriosMenores.Add(" X ");
            else
                datosCriteriosMenores.Add("   ");

            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);

            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMenores.Add("CB: ");
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bTolerancia == true)
                datosCriteriosMenores.Add(" X ");
            else
                datosCriteriosMenores.Add("   ");

            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Add(Chunk.TABBING);

            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            datosCriteriosMenores.Add("CC: ");
            datosCriteriosMenores.Add(Chunk.TABBING);
            datosCriteriosMenores.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL + Font.UNDERLINE);
            if (datosInvPsi.bRelaciones == true)
                datosCriteriosMenores.Add(" X ");
            else
                datosCriteriosMenores.Add("   ");

            datosCriteriosMenores.Add(Chunk.NEWLINE);

            documentInvPsi.Add(datosCriteriosMenores);

            #endregion

            #region Titulo Sintesis Tecnica
            PdfPTable tableSintesis = new PdfPTable(1);
            tableSintesis.TotalWidth = 560f;
            tableSintesis.LockedWidth = true;

            tableSintesis.SetWidths(widthsTitulosGenerales);
            tableSintesis.HorizontalAlignment = 0;
            tableSintesis.SpacingBefore = 20f;
            tableSintesis.SpacingAfter = 10f;

            PdfPCell cellSintesis = new PdfPCell(new Phrase("SINTESIS TECNICA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellSintesis.HorizontalAlignment = 1;
            cellSintesis.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellSintesis.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellSintesis.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableSintesis.AddCell(cellSintesis);

            documentInvPsi.Add(tableSintesis);
            #endregion

            #region Contenido Sintesis
            Paragraph sintesis = new Paragraph();
            sintesis.Alignment = Element.ALIGN_JUSTIFIED;

            sintesis.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            sintesis.Add(datosInvPsi.cDiagnostico);
            sintesis.Add(Chunk.NEWLINE);
            sintesis.Add(Chunk.NEWLINE);

            documentInvPsi.Add(sintesis);
            #endregion

            #region Resultado

            Paragraph resultado = new Paragraph();
            resultado.Alignment = Element.ALIGN_LEFT;

            resultado.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            resultado.Add("Resultado: ");
            resultado.Add(Chunk.TABBING);
            resultado.Font = FontFactory.GetFont("Arial", 12, Font.NORMAL + Font.UNDERLINE);
            resultado.Add(datosInvPsi.cResultado);
            resultado.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            resultado.Add(Chunk.NEWLINE);
            resultado.Add(Chunk.NEWLINE);

            documentInvPsi.Add(resultado);

            #endregion

            #region Titulo Recomendacion
            PdfPTable tableRecomendacion = new PdfPTable(1);
            tableRecomendacion.TotalWidth = 560f;
            tableRecomendacion.LockedWidth = true;

            tableRecomendacion.SetWidths(widthsTitulosGenerales);
            tableRecomendacion.HorizontalAlignment = 0;
            tableRecomendacion.SpacingBefore = 20f;
            tableRecomendacion.SpacingAfter = 10f;

            PdfPCell cellRecomendacion = new PdfPCell(new Phrase("RECOMENDACIONES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellRecomendacion.HorizontalAlignment = 1;
            cellRecomendacion.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellRecomendacion.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellRecomendacion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableRecomendacion.AddCell(cellRecomendacion);

            documentInvPsi.Add(tableRecomendacion);
            #endregion

            #region Contenido Recomendacion
            Paragraph recomendacion = new Paragraph();
            recomendacion.Alignment = Element.ALIGN_JUSTIFIED;

            recomendacion.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            recomendacion.Add(datosInvPsi.cRecomendaciones);
            recomendacion.Add(Chunk.NEWLINE);
            recomendacion.Add(Chunk.NEWLINE);
            recomendacion.Add(Chunk.NEWLINE);
            recomendacion.Add(Chunk.NEWLINE);
            recomendacion.Add(Chunk.NEWLINE);
            recomendacion.Add(Chunk.NEWLINE);

            documentInvPsi.Add(recomendacion);
            #endregion

            #region Firmas
            PdfPTable tablaFirmas = new PdfPTable(3);
            tablaFirmas.TotalWidth = 560;
            tablaFirmas.LockedWidth = true;

            float[] widths = new float[] { 1f, 1f, 1f };
            tablaFirmas.SetWidths(widths);
            tablaFirmas.HorizontalAlignment = 0;
            tablaFirmas.SpacingAfter = 20f;
            tablaFirmas.SpacingAfter = 30f;
            tablaFirmas.DefaultCell.Border = 0; //--------------------

            PdfPCell cela = new PdfPCell(new Phrase("______________________", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cela.HorizontalAlignment = Element.ALIGN_CENTER;
            cela.Border = 0;

            PdfPCell celb = new PdfPCell(new Phrase("______________________", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            celb.HorizontalAlignment = Element.ALIGN_CENTER;
            celb.Border = 0;

            PdfPCell celc = new PdfPCell(new Phrase("______________________", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            celc.HorizontalAlignment = Element.ALIGN_CENTER;
            celc.Border = 0;

            PdfPCell cel1a = new PdfPCell(new Phrase("No. Psicologo: " + datosInvPsi.clave_psicologo, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1a.Border = 0;

            PdfPCell cel1b = new PdfPCell(new Phrase(datosInvPsi.idSupPsi, new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1b.Border = 0;

            PdfPCell cel1c = new PdfPCell(new Phrase("503", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel1c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel1c.Border = 0;

            PdfPCell cel2a = new PdfPCell(new Phrase("Clave Investigador", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2a.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2a.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2b = new PdfPCell(new Phrase("Clave Supervisor", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2b.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2b.Border = 0;
            //cel2a.Border = 1;

            PdfPCell cel2c = new PdfPCell(new Phrase("Vo. Bo.", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)));
            cel2c.HorizontalAlignment = Element.ALIGN_CENTER;
            cel2c.Border = 0;
            //cel2c.Border = 1;

            tablaFirmas.AddCell(cela);
            tablaFirmas.AddCell(celb);
            tablaFirmas.AddCell(celc);

            tablaFirmas.AddCell(cel1a);
            tablaFirmas.AddCell(cel1b);
            tablaFirmas.AddCell(cel1c);

            tablaFirmas.AddCell(cel2a);
            tablaFirmas.AddCell(cel2b);
            tablaFirmas.AddCell(cel2c);

            documentInvPsi.Add(tablaFirmas);

            #endregion

            documentInvPsi.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }

        public ActionResult printIndice(int sIdH) //Cat_controlador_Acciones:
        {
            var datosIndice = RepIndice.getRepIndice(sIdH).FirstOrDefault();
            int totalFojas = 0;

            MemoryStream ms = new MemoryStream();

            Document documentIndice = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwIndice = PdfWriter.GetInstance(documentIndice, ms);

            pwIndice.PageEvent = new HeaderIndice();

            documentIndice.Open();

            //------------------------------------------------------------------------------------para verlo en mi el proyecto
            //string imagenPath = @"C:\Net 2012\psicologiamvc\psicologiamvc\Content\img\gobedohor.png";
            //Image imagen = Image.GetInstance(imagenPath);
            //imagen.ScalePercent(35f);
            //imagen.SetAbsolutePosition(30f, 725f);
            //documentIndice.Add(imagen);

            //------------------------------------------------------------------------------------para verlo en SERVIDOR
            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            documentIndice.Add(imagen);
            //------------------------------------------------------------------------------------para verlo en SERVIDOR

            #region Datos Generales
            Chunk chunk = new Chunk();

            Paragraph derecha = new Paragraph();
            derecha.Alignment = Element.ALIGN_RIGHT;
            derecha.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            derecha.Add(datosIndice.SerialElement.Substring(0, 6));
            derecha.Add(Chunk.NEWLINE);
            documentIndice.Add(derecha);

            Paragraph izquierda = new Paragraph();
            izquierda.Alignment = Element.ALIGN_LEFT;

            izquierda.Add(Chunk.NEWLINE);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            izquierda.Add("Codigo Evaluado: ");
            izquierda.Add(Chunk.TABBING);
            //derecha.Add(DateTime.Now.ToShortDateString());
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            izquierda.Add(datosIndice.codigoevaluado);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            //izquierda.Add(datosIndice.SerialElement.Substring(0,6));
            izquierda.Add(Chunk.NEWLINE);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            izquierda.Add("Nombre: ");
            izquierda.Add(Chunk.TABBING);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            izquierda.Add(datosIndice.evaluado);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Add(Chunk.TABBING);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            izquierda.Add("Fecha Evaluacion: ");
            izquierda.Add(Chunk.TABBING);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            izquierda.Add(datosIndice.fEvalpsi);
            izquierda.Add(Chunk.NEWLINE);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            izquierda.Add("Puesto: ");
            izquierda.Add(Chunk.TABBING);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            izquierda.Add(datosIndice.puesto);
            izquierda.Add(Chunk.NEWLINE);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            izquierda.Add("Fuente: ");
            izquierda.Add(Chunk.TABBING);
            izquierda.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            izquierda.Add(datosIndice.desc_dependencia);
            izquierda.Add(Chunk.NEWLINE);

            documentIndice.Add(izquierda);
            #endregion

            #region Tabla Indice
            PdfPTable tableCriteriosContenido= new PdfPTable(1);
            tableCriteriosContenido.TotalWidth = 560f;
            tableCriteriosContenido.LockedWidth = true;

            tableCriteriosContenido.SetWidths(widthsTitulosGenerales);
            tableCriteriosContenido.HorizontalAlignment = 0;
            tableCriteriosContenido.SpacingBefore = 20f;
            tableCriteriosContenido.SpacingAfter = 10f;

            PdfPCell cellCriterioConteido = new PdfPCell(new Phrase("CONTENIDO DEL INDICE", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellCriterioConteido.HorizontalAlignment = 1;
            cellCriterioConteido.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellCriterioConteido.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellCriterioConteido.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableCriteriosContenido.AddCell(cellCriterioConteido);

            documentIndice.Add(tableCriteriosContenido);
            #endregion

            #region Contenido Indice
            Paragraph contenido = new Paragraph();
            contenido.Alignment = Element.ALIGN_LEFT;
            //contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 12, Font.BOLD);
            contenido.Add("Formatos de evaluacion");
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Add("No. Hojas");
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Add("Pruebas Psicologicas");
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Add("No. Hojas");
            contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Reporte de Integracion Psicologica: ");
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nIntegracion.ToString());
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD+Font.UNDERLINE);
            contenido.Add("TEST DE PERSONALIDAD");
            contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Ficha de datos personales: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nFicha.ToString());
            contenido.Add(Chunk.TABBING);   contenido.Add(Chunk.TABBING);   contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("MMPI-II:");
            contenido.Add(Chunk.TABBING);   contenido.Add(Chunk.TABBING);   contenido.Add(Chunk.TABBING);   contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nMmpii.ToString());
            contenido.Add(Chunk.NEWLINE);//------------------------------
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Hoja de opinión y comentarios: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nComentarios.ToString());
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("MMPI-2RF PF:");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); 
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nMmp2rf.ToString());
            contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Hoja de No Concluyó: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nNoconcluyo.ToString());            
            contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Formato de entrevista: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nEntrevista.ToString());
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            contenido.Add("TEST PROYECTIVOS");
            contenido.Add(Chunk.NEWLINE);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Integracion de las pruebas psicologicas: ");
            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nPruebas.ToString());
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Autobiografía: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nAutobiografia.ToString());            
            contenido.Add(Chunk.NEWLINE);   //------------------------------------------------------------------------
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Machover: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nMachover.ToString());
            contenido.Add(Chunk.NEWLINE);   //------------------------------------------------------------------------
            contenido.Font = FontFactory.GetFont("Arial", 12, Font.BOLD);
            contenido.Add("Pruebas Psicologicas");
            contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING);
            contenido.Add("No. Hojas");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Bajo la lluvia: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nLluvia.ToString());
            contenido.Add(Chunk.NEWLINE);   //------------------------------------------------------------------------
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            contenido.Add("TEST DE INTELIGENCIA");
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("HTP: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nHtp.ToString());

            contenido.Add(Chunk.NEWLINE);   //------------------------------------------------------------------------           
            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Terman Merril:");
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(datosIndice.nTerman.ToString());
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("SACKS: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nSacks.ToString());

            contenido.Add(Chunk.NEWLINE);

            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Raven:");
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(datosIndice.nRaven.ToString());

            contenido.Add(Chunk.NEWLINE);

            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Beta III-R:");
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(datosIndice.nBeta3.ToString());

            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            contenido.Add("ADAPTABILIDAD");
            contenido.Add(Chunk.NEWLINE);

            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Cleaver: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nCleaver.ToString());
            contenido.Add(Chunk.NEWLINE);

            contenido.Add(Chunk.NEWLINE);

            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            contenido.Add("EVALUACION COGNITIVA");
            contenido.Add(Chunk.NEWLINE);

            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.NORMAL);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Moca: ");
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nMoca.ToString());

            contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE);   //------------------------------------------------------------------------
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.UNDERLINE);
            contenido.Add("OTROS DOCUMENTOS");
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD + Font.NORMAL);
            contenido.Add(Chunk.NEWLINE);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Hoja de observaciones psicometría: ");
            contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nGrupo.ToString());
            contenido.Add(Chunk.NEWLINE);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add(datosIndice.cOtro);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); 
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(datosIndice.nOtro.ToString());
            contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            contenido.Add("Total fojas: ");  //------------------------------------------------------------------------------------------------------------ Sumatoria
            contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            contenido.Font = FontFactory.GetFont("Arial", 10, Font.UNDERLINE + Font.BOLD);

            totalFojas = datosIndice.nIntegracion + datosIndice.nSolicitud + datosIndice.nWais + datosIndice.nFicha + datosIndice.nTerman + datosIndice.nFichapermanencia + datosIndice.nBeta + datosIndice.nFichaloc + datosIndice.nRaven + datosIndice.nAutorizacion + datosIndice.nBeta3 + datosIndice.nComentarios + datosIndice.nNoconcluyo + datosIndice.nExamen + datosIndice.nMmpii + datosIndice.nEntrevista + datosIndice.n16pf + datosIndice.nPruebas + datosIndice.nLusher + datosIndice.nCambio + datosIndice.nBarsit + datosIndice.nAutobiografia + datosIndice.nMachover + datosIndice.nLluvia + datosIndice.nTat + datosIndice.nHtp + datosIndice.nSacks + datosIndice.nCleaver + datosIndice.nBender + datosIndice.nGrupo + datosIndice.nOtro + datosIndice.nMoca + datosIndice.nMmp2rf;

            contenido.Add(totalFojas.ToString());

            contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE);

            documentIndice.Add(contenido);

            #region Firmas
            PdfPTable tablaFirmas = new PdfPTable(3)
            {
                TotalWidth = 560,
                LockedWidth = true
            };

            float[] widths = new float[] { 1f, 1f, 1f };
            tablaFirmas.SetWidths(widths);
            tablaFirmas.HorizontalAlignment = 0;
            tablaFirmas.SpacingAfter = 20f;
            tablaFirmas.SpacingAfter = 30f;
            tablaFirmas.DefaultCell.Border = 0;

            PdfPCell cel1a = new PdfPCell(new Phrase(datosIndice.clave_psicologo+"\nElaboró", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                BorderWidthTop=1,
                BorderWidthBottom=0,
                BorderWidthLeft=0,
                BorderWidthRight=0
            };

            PdfPCell cel1b = new PdfPCell(new Phrase(" ", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = 0
            };

            PdfPCell cel1c = new PdfPCell(new Phrase(datosIndice.clave_psicologorevisor+"\nSupervisó", new Font(Font.FontFamily.HELVETICA, 10f, Font.NORMAL)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                BorderWidthTop = 1,
                BorderWidthBottom = 0,
                BorderWidthLeft = 0,
                BorderWidthRight = 0
            };

            tablaFirmas.AddCell(cel1a);
            tablaFirmas.AddCell(cel1b);
            tablaFirmas.AddCell(cel1c);

            documentIndice.Add(tablaFirmas);

            #endregion

            //contenido.Add(Chunk.NEWLINE); contenido.Add(Chunk.NEWLINE); //--------------------------------------------------------------------------------
            //contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            //contenido.Add("Elaboro: ");
            //contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); 
            //contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            //contenido.Add(datosIndice.clave_psicologo);
            //contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            //contenido.Font = FontFactory.GetFont("Arial", 10, Font.BOLD);
            //contenido.Add("Superviso: ");
            //contenido.Add(Chunk.TABBING); contenido.Add(Chunk.TABBING);
            //contenido.Font = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            //contenido.Add(datosIndice.clave_psicologorevisor);
            //documentIndice.Add(contenido);
            #endregion

            documentIndice.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");

        }

        public ActionResult printRepAntecedentes(int sIdH) //Cat_controlador_Acciones XX
        {
            var datosRepAnt = RepAnt.getRepAntPrimero(sIdH).FirstOrDefault();
            var datosRepAnt_2 = RepAnt.getRepAntSegundo(sIdH).ToList();
            var datosRepAnt_3 = RepAnt.getRepAntTercero(sIdH).ToList();
            var datosRepAnt_4 = RepAnt.getRepAntCuarto(sIdH).FirstOrDefault();
            var datosRepAnt_5 = RepAnt.getRepAntQuinto(sIdH).ToList();

            MemoryStream msAnt = new MemoryStream();
            Document docRepAnt = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwRepAnt = PdfWriter.GetInstance(docRepAnt, msAnt);

            docRepAnt.Open();

            Chunk chunkAnt = new Chunk();

            #region Titulo Reporte
            Paragraph tituloReporte = new Paragraph();
            tituloReporte.Alignment = Element.ALIGN_CENTER;
            tituloReporte.Font = FontFactory.GetFont("Arial", 10, Font.BOLDITALIC);
            tituloReporte.Add("Concentrado Reporte de Antecedentes");
            tituloReporte.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloReporte);
            #endregion

            #region Principales
            Paragraph principales = new Paragraph();
            principales.Alignment = Element.ALIGN_JUSTIFIED;
            principales.Add(Chunk.NEWLINE);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("CUIP");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.cuip);
            principales.Add(Chunk.TABBING); principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("Personal a Cargo:");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.personaCargo);
            principales.Add(Chunk.TABBING); principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            principales.Add("Cuantos:");
            principales.Add(Chunk.TABBING);
            principales.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            principales.Add(datosRepAnt.cantidadPersonal);
            principales.Add(Chunk.NEWLINE);
            docRepAnt.Add(principales);
            #endregion

            #region Titulo Antecedente registral
            PdfPTable tableTituloRegistral = new PdfPTable(1);
            tableTituloRegistral.TotalWidth = 560f;
            tableTituloRegistral.LockedWidth = true;

            float[] widthsEstudio = new float[] { 1f };
            tableTituloRegistral.SetWidths(widthsEstudio);
            tableTituloRegistral.HorizontalAlignment = 0;
            tableTituloRegistral.SpacingBefore = 10f;
            tableTituloRegistral.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRegistral = new PdfPCell(new Phrase("ANTECEDENTES REGISTRALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRegistral.HorizontalAlignment = 1;
            cellTituloTituloRegistral.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRegistral.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRegistral.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRegistral.AddCell(cellTituloTituloRegistral);

            docRepAnt.Add(tableTituloRegistral);
            #endregion

            #region Datos Antecedentes Registrales
            Paragraph datosAntReg = new Paragraph();
            //int antreg = datosRepAnt_2.Count();
            foreach (var item in datosRepAnt_2)
            {
                datosAntReg.Alignment = Element.ALIGN_JUSTIFIED;
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("Tipo Registro");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.tipoRegistro);
                datosAntReg.Add(Chunk.TABBING); datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("N.C.P.");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.ncp);
                datosAntReg.Add(Chunk.NEWLINE);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datosAntReg.Add("Procedencia");
                datosAntReg.Add(Chunk.TABBING);
                datosAntReg.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datosAntReg.Add(item.procedencia);
                datosAntReg.Add(Chunk.NEWLINE);
            }
            docRepAnt.Add(datosAntReg);
            #endregion

            #region observacione registral
            Paragraph observacionRegistral = new Paragraph();
            observacionRegistral.Alignment = Element.ALIGN_JUSTIFIED;
            observacionRegistral.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            observacionRegistral.Add("Observaciones:");
            observacionRegistral.Add(Chunk.TABBING);
            observacionRegistral.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            observacionRegistral.Add(datosRepAnt.observacionRegistral);
            observacionRegistral.Add(Chunk.NEWLINE);
            docRepAnt.Add(observacionRegistral);
            #endregion

            #region Titulo Plataforma Mexico SUIC
            PdfPTable tableTituloSUIC = new PdfPTable(1);
            tableTituloSUIC.TotalWidth = 560f;
            tableTituloSUIC.LockedWidth = true;

            float[] widthSUIC = new float[] { 1f };
            tableTituloSUIC.SetWidths(widthSUIC);
            tableTituloSUIC.HorizontalAlignment = 0;
            tableTituloSUIC.SpacingBefore = 10f;
            tableTituloSUIC.SpacingAfter = 10f;

            PdfPCell cellTituloTituloSuic = new PdfPCell(new Phrase("PLATAFORMA MEXICO (SUIC)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloSuic.HorizontalAlignment = 1;
            cellTituloTituloSuic.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloSuic.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloSuic.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloSUIC.AddCell(cellTituloTituloSuic);

            docRepAnt.Add(tableTituloSUIC);
            #endregion

            #region Datos SUIC
            Paragraph datoSuic = new Paragraph();
            datoSuic.Alignment = Element.ALIGN_JUSTIFIED;
            datoSuic.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoSuic.Add(datosRepAnt.cSuic);
            datoSuic.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoSuic);
            #endregion

            #region Titulo RNPSP
            PdfPTable tableTituloRNPSP = new PdfPTable(1);
            tableTituloRNPSP.TotalWidth = 560f;
            tableTituloRNPSP.LockedWidth = true;

            float[] widthRnpsp = new float[] { 1f };
            tableTituloRNPSP.SetWidths(widthRnpsp);
            tableTituloRNPSP.HorizontalAlignment = 0;
            tableTituloRNPSP.SpacingBefore = 10f;
            tableTituloRNPSP.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRnpsp = new PdfPCell(new Phrase("RNPSP", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRnpsp.HorizontalAlignment = 1;
            cellTituloTituloRnpsp.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRnpsp.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRnpsp.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRNPSP.AddCell(cellTituloTituloRnpsp);

            docRepAnt.Add(tableTituloRNPSP);
            #endregion

            #region Datos RNPSP
            Paragraph datoRnpsp = new Paragraph();
            datoRnpsp.Alignment = Element.ALIGN_JUSTIFIED;
            datoRnpsp.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoRnpsp.Add(datosRepAnt.cRnpsp);
            datoRnpsp.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoRnpsp);
            #endregion

            #region Titulo TELESCAN
            PdfPTable tableTituloTelescan = new PdfPTable(1);
            tableTituloTelescan.TotalWidth = 560f;
            tableTituloTelescan.LockedWidth = true;

            float[] widthTelescan = new float[] { 1f };
            tableTituloTelescan.SetWidths(widthTelescan);
            tableTituloTelescan.HorizontalAlignment = 0;
            tableTituloTelescan.SpacingBefore = 10f;
            tableTituloTelescan.SpacingAfter = 10f;

            PdfPCell cellTituloTituloTelescan = new PdfPCell(new Phrase("TELESCAN", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloTelescan.HorizontalAlignment = 1;
            cellTituloTituloTelescan.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloTelescan.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloTelescan.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloTelescan.AddCell(cellTituloTituloTelescan);

            docRepAnt.Add(tableTituloTelescan);
            #endregion

            #region Datos TELESCAN
            Paragraph datoTelescan = new Paragraph();
            datoTelescan.Alignment = Element.ALIGN_JUSTIFIED;
            datoTelescan.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoTelescan.Add(datosRepAnt.cTelscan);
            datoTelescan.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoTelescan);
            #endregion

            #region Titulo Antecedente Penal
            PdfPTable tableTituloPenal = new PdfPTable(1);
            tableTituloPenal.TotalWidth = 560f;
            tableTituloPenal.LockedWidth = true;

            float[] widthsPenal = new float[] { 1f };
            tableTituloPenal.SetWidths(widthsPenal);
            tableTituloPenal.HorizontalAlignment = 0;
            tableTituloPenal.SpacingBefore = 10f;
            tableTituloPenal.SpacingAfter = 10f;

            PdfPCell cellTituloTituloPenal = new PdfPCell(new Phrase("ANTECEDENTES PENALES", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloPenal.HorizontalAlignment = 1;
            cellTituloTituloPenal.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloPenal.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloPenal.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloPenal.AddCell(cellTituloTituloPenal);

            docRepAnt.Add(tableTituloPenal);
            #endregion

            #region Datos Antecedentes Penales
            PdfPTable tablaPenal = new PdfPTable(2);
            tablaPenal.TotalWidth = 560;
            tablaPenal.LockedWidth = true;

            float[] widths = new float[] { 1f, 3f };
            tablaPenal.SetWidths(widths);
            tablaPenal.HorizontalAlignment = 0;
            tablaPenal.SpacingAfter = 5f;
            tablaPenal.SpacingBefore = 10f;
            tablaPenal.DefaultCell.Border = 1;

            foreach (var itemPen in datosRepAnt_3)
            {
                PdfPCell cel5a = new PdfPCell(new Phrase("", new Font(Font.FontFamily.HELVETICA, 9F, Font.NORMAL)));
                cel5a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel5a.Border = 0;

                PdfPCell cel5b = new PdfPCell(new Phrase("Constancia de NO Antecedentes Penales de la Evaluación " + itemPen.idhistorico + " del " + itemPen.fechaEvalPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel5b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel5b.Border = 0;

                PdfPCell cel4a = new PdfPCell(new Phrase("Lugar - Fecha Exp:", new Font(Font.FontFamily.HELVETICA, 9F, Font.BOLD)));
                cel4a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel4a.Border = 0;

                PdfPCell cel4b = new PdfPCell(new Phrase(itemPen.lugarFechaPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel4b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel4b.Border = 0;

                PdfPCell cel2a = new PdfPCell(new Phrase("Num. Constancia:", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel2a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel2a.Border = 0;

                PdfPCell cel2b = new PdfPCell(new Phrase(itemPen.constanciaPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel2b.HorizontalAlignment = Element.ALIGN_LEFT;
                cel2b.Border = 0;

                PdfPCell cel1a = new PdfPCell(new Phrase("Observaciones:", new Font(Font.FontFamily.HELVETICA, 9f, Font.BOLD)));
                cel1a.HorizontalAlignment = Element.ALIGN_LEFT;
                cel1a.Border = 0;

                PdfPCell cel1b = new PdfPCell(new Phrase(itemPen.obsPenal, new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel1b.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel1b.Border = 0;

                PdfPCell cel3a = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel3a.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel3a.Border = 0;

                PdfPCell cel3b = new PdfPCell(new Phrase("\n", new Font(Font.FontFamily.HELVETICA, 9f, Font.NORMAL)));
                cel3b.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cel3b.Border = 0;

                tablaPenal.AddCell(cel5a);
                tablaPenal.AddCell(cel5b);

                tablaPenal.AddCell(cel2a);
                tablaPenal.AddCell(cel2b);

                tablaPenal.AddCell(cel4a);
                tablaPenal.AddCell(cel4b);

                tablaPenal.AddCell(cel1a);
                tablaPenal.AddCell(cel1b);

                tablaPenal.AddCell(cel3a);
                tablaPenal.AddCell(cel3b);
            }

            docRepAnt.Add(tablaPenal);
            #endregion

            #region Titulo Antecedente Admivos
            PdfPTable tableTituloAdministrativo = new PdfPTable(1);
            tableTituloAdministrativo.TotalWidth = 560f;
            tableTituloAdministrativo.LockedWidth = true;

            float[] widthsAdmivo = new float[] { 1f };
            tableTituloAdministrativo.SetWidths(widthsAdmivo);
            tableTituloAdministrativo.HorizontalAlignment = 0;
            tableTituloAdministrativo.SpacingBefore = 10f;
            tableTituloAdministrativo.SpacingAfter = 10f;

            PdfPCell cellTituloTituloAdmivo = new PdfPCell(new Phrase("ANTECEDENTES ADMINISTRATIVOS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloAdmivo.HorizontalAlignment = 1;
            cellTituloTituloAdmivo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloAdmivo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloAdmivo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloAdministrativo.AddCell(cellTituloTituloAdmivo);

            docRepAnt.Add(tableTituloAdministrativo);
            #endregion

            #region Titulos Antecedentes administrativos
            Paragraph tituloantAdm = new Paragraph();
            tituloantAdm.Alignment = Element.ALIGN_CENTER;
            //tituloantAdm.Add(Chunk.NEWLINE);
            //tituloantAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
            //tituloantAdm.Add("ANTECEDENTES ADMINISTRATIVOS");
            tituloantAdm.Add(Chunk.NEWLINE);
            tituloantAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            tituloantAdm.Add("Resultado de Inhabilitacion");
            tituloantAdm.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloantAdm);
            #endregion

            #region Antecedentes administrativos
            Paragraph antAdm = new Paragraph();
            antAdm.Alignment = Element.ALIGN_JUSTIFIED;
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add("Estatal");
            antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.inhabilitacionEstatal);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Add("No. Constancia");
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.constanciaInhabilitacionEstatal);
            antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING); antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdm.Add("Fecha");
            antAdm.Add(Chunk.TABBING);
            antAdm.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdm.Add(datosRepAnt.fechaConstanciaEstatal);
            antAdm.Add(Chunk.NEWLINE);

            docRepAnt.Add(antAdm);
            #endregion

            #region resultado antecedentes federal
            Paragraph tituloantFed = new Paragraph();
            tituloantFed.Alignment = Element.ALIGN_CENTER;
            tituloantFed.Add(Chunk.NEWLINE);
            tituloantFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            tituloantFed.Add("Resultado de Antecedente");
            tituloantFed.Add(Chunk.NEWLINE);
            docRepAnt.Add(tituloantFed);

            #endregion

            #region datos antecedente federal
            Paragraph antAdmFed = new Paragraph();
            antAdmFed.Alignment = Element.ALIGN_JUSTIFIED;
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add("Federal");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.antecedenteFederal);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Add("No. Constancia");
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.constanciaAntecedenteFederal);
            antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING); antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add("Fecha");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add(datosRepAnt.fechaAntecedenteFederal);
            antAdmFed.Add(Chunk.NEWLINE);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
            antAdmFed.Add("Obsevación:");
            antAdmFed.Add(Chunk.TABBING);
            antAdmFed.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            antAdmFed.Add(datosRepAnt.observacionAdministrativo);
            antAdmFed.Add(Chunk.NEWLINE);

            docRepAnt.Add(antAdmFed);
            #endregion

            if (datosRepAnt_4 != null)
            {
                #region Titulo Aplicativo
                Paragraph tituloAplicativo = new Paragraph();
                tituloAplicativo.Add(Chunk.NEWLINE);
                tituloAplicativo.Alignment = Element.ALIGN_CENTER;
                tituloAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
                tituloAplicativo.Add("REGISTRO NACIONAL DE PERSONAL DE SEGURIDAD PUBLICA");
                tituloAplicativo.Add(Chunk.NEWLINE);
                docRepAnt.Add(tituloAplicativo);
                #endregion

                #region Titulo Aplicativ
                PdfPTable tableTituloAplica = new PdfPTable(1);
                tableTituloAplica.TotalWidth = 560f;
                tableTituloAplica.LockedWidth = true;

                float[] widthsAplicativo = new float[] { 1f };
                tableTituloAplica.SetWidths(widthsAplicativo);
                tableTituloAplica.HorizontalAlignment = 0;
                tableTituloAplica.SpacingBefore = 10f;
                tableTituloAplica.SpacingAfter = 10f;

                PdfPCell cellTituloTituloAplicativo = new PdfPCell(new Phrase("REGISTRO NACIONAL DE PERSONAL DE SEGURIDAD PUBLICA", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
                cellTituloTituloAplicativo.HorizontalAlignment = 1;
                cellTituloTituloAplicativo.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
                cellTituloTituloAplicativo.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
                cellTituloTituloAplicativo.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
                tableTituloAplica.AddCell(cellTituloTituloAplicativo);

                docRepAnt.Add(tableTituloAplica);
                #endregion

                #region Dato Aplicativo
                Paragraph datoAplicativo = new Paragraph();
                datoAplicativo.Alignment = Element.ALIGN_LEFT;
                datoAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                if (datosRepAnt_4.aplicativoActual == true)
                    datoAplicativo.Add("Adscripcion Actual");
                else
                    datoAplicativo.Add("Adscripcion Anterior");
                datoAplicativo.Add(Chunk.NEWLINE);
                datoAplicativo.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoAplicativo.Add("Observaciones:");
                datoAplicativo.Add(Chunk.TABBING);
                datoAplicativo.Add(datosRepAnt_4.observaAplicativo);
                datoAplicativo.Add(Chunk.NEWLINE);
                docRepAnt.Add(datoAplicativo);
                #endregion
            }

            #region Titulo de Control Vehicular TABLE
            PdfPTable tableCtrVeh = new PdfPTable(1);
            tableCtrVeh.TotalWidth = 560f;
            tableCtrVeh.LockedWidth = true;

            float[] widthsVeh = new float[] { 1f };
            tableCtrVeh.SetWidths(widthsVeh);
            tableCtrVeh.HorizontalAlignment = 0;
            tableCtrVeh.SpacingBefore = 10f;
            tableCtrVeh.SpacingAfter = 10f;

            PdfPCell cellTituloTituloVeh = new PdfPCell(new Phrase("REPORTE DE CONTROL VEHICULAR", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloVeh.HorizontalAlignment = 1;
            cellTituloTituloVeh.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloVeh.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloVeh.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableCtrVeh.AddCell(cellTituloTituloVeh);

            docRepAnt.Add(tableCtrVeh);
            #endregion

            #region Dato Vehicular oficios
            Paragraph datoVehicular = new Paragraph();
            foreach (var itemV in datosRepAnt_5)
            {
                datoVehicular.Alignment = Element.ALIGN_JUSTIFIED;
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Num. Oficio Enviado");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.ofiEnvVeh);
                datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Fecha");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.fenviado);
                datoVehicular.Add(Chunk.NEWLINE);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Num. Oficio Recibido");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.ofiRecVeh);
                datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                datoVehicular.Add("Fecha");
                datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                datoVehicular.Add(Chunk.TABBING);
                datoVehicular.Add(itemV.frecibido);
                datoVehicular.Add(Chunk.NEWLINE);
                if (itemV.vehiculoencontrado == false)
                {
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Observaciones");
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Add(itemV.observaVehiculo);
                    datoVehicular.Add(Chunk.NEWLINE);
                }
                else
                {
                    datoVehicular.Alignment = Element.ALIGN_JUSTIFIED;
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLDITALIC);
                    datoVehicular.Add("RESULTADOS");
                    datoVehicular.Add(Chunk.NEWLINE);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Tipo");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.tipo);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Placa");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.placa);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Serie");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.serie);
                    datoVehicular.Add(Chunk.NEWLINE);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Marca");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.marca);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Clase");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.clase);
                    datoVehicular.Add(Chunk.TABBING); datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Modelo");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.modelo);
                    datoVehicular.Add(Chunk.NEWLINE);

                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.BOLD);
                    datoVehicular.Add("Fecha de Alta");
                    datoVehicular.Add(Chunk.TABBING);
                    datoVehicular.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
                    datoVehicular.Add(itemV.faltaVehicular);
                    datoVehicular.Add(Chunk.NEWLINE); datoVehicular.Add(Chunk.NEWLINE);
                }
                docRepAnt.Add(datoVehicular);
            }
            #endregion

            #region Titulo Publicaciones y redes sociales
            PdfPTable tableTituloPublicaciones = new PdfPTable(1);
            tableTituloPublicaciones.TotalWidth = 560f;
            tableTituloPublicaciones.LockedWidth = true;

            float[] widthPublicacion = new float[] { 1f };
            tableTituloPublicaciones.SetWidths(widthPublicacion);
            tableTituloPublicaciones.HorizontalAlignment = 0;
            tableTituloPublicaciones.SpacingBefore = 10f;
            tableTituloPublicaciones.SpacingAfter = 10f;

            PdfPCell cellTituloTituloPublicacion = new PdfPCell(new Phrase("PUBLICACIONES Y REDES SOCIALES)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloPublicacion.HorizontalAlignment = 1;
            cellTituloTituloPublicacion.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloPublicacion.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloPublicacion.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloPublicaciones.AddCell(cellTituloTituloPublicacion);

            docRepAnt.Add(tableTituloPublicaciones);
            #endregion

            #region Datos Publicacion y redes sociales
            Paragraph datoPublicacion = new Paragraph();
            datoPublicacion.Alignment = Element.ALIGN_JUSTIFIED;
            datoPublicacion.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoPublicacion.Add(datosRepAnt.publicacion);
            datoPublicacion.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoPublicacion);
            #endregion

            #region Titulo Riesgos detectados en evaluaciones anteriores
            PdfPTable tableTituloRiesgos = new PdfPTable(1);
            tableTituloRiesgos.TotalWidth = 560f;
            tableTituloRiesgos.LockedWidth = true;

            float[] widthRiesgos = new float[] { 1f };
            tableTituloRiesgos.SetWidths(widthRiesgos);
            tableTituloRiesgos.HorizontalAlignment = 0;
            tableTituloRiesgos.SpacingBefore = 10f;
            tableTituloRiesgos.SpacingAfter = 10f;

            PdfPCell cellTituloTituloRiesgos = new PdfPCell(new Phrase("RIESGOS DETECTADOS EN EVALUACIONES ANTERIORES)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloRiesgos.HorizontalAlignment = 1;
            cellTituloTituloRiesgos.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloRiesgos.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloRiesgos.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloRiesgos.AddCell(cellTituloTituloRiesgos);

            docRepAnt.Add(tableTituloRiesgos);
            #endregion

            #region Datos Riesgos detectados en evaluaciones anteriores
            Paragraph datoRiesgos = new Paragraph();
            datoRiesgos.Alignment = Element.ALIGN_JUSTIFIED;
            datoRiesgos.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoRiesgos.Add(datosRepAnt.cEvaluacionAnterior);
            datoRiesgos.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoRiesgos);
            #endregion

            #region Titulo y Datos sociodemograficos
            PdfPTable tableTituloContexto = new PdfPTable(1);
            tableTituloContexto.TotalWidth = 560f;
            tableTituloContexto.LockedWidth = true;

            float[] widthscontexto = new float[] { 1f };
            tableTituloContexto.SetWidths(widthscontexto);
            tableTituloContexto.HorizontalAlignment = 0;
            tableTituloContexto.SpacingBefore = 10f;
            tableTituloContexto.SpacingAfter = 10f;

            PdfPCell cellTituloTituloContexto = new PdfPCell(new Phrase("DATOS SOCIODEMOGRAFICOS", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloContexto.HorizontalAlignment = 1;
            cellTituloTituloContexto.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloContexto.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloContexto.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloContexto.AddCell(cellTituloTituloContexto);

            docRepAnt.Add(tableTituloContexto);
            #endregion

            #region Dato Contexto
            Paragraph datoContexto = new Paragraph();
            datoContexto.Alignment = Element.ALIGN_JUSTIFIED;
            datoContexto.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoContexto.Add(datosRepAnt.contexto);
            datoContexto.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoContexto);
            #endregion

            #region Titulo Analisis - Riesgo detectado
            PdfPTable tableTituloAnalisis = new PdfPTable(1);
            tableTituloAnalisis.TotalWidth = 560f;
            tableTituloAnalisis.LockedWidth = true;

            float[] widthAnalisis = new float[] { 1f };
            tableTituloAnalisis.SetWidths(widthAnalisis);
            tableTituloAnalisis.HorizontalAlignment = 0;
            tableTituloAnalisis.SpacingBefore = 10f;
            tableTituloAnalisis.SpacingAfter = 10f;

            PdfPCell cellTituloTituloAnalisis = new PdfPCell(new Phrase("ANALISIS DE INFORMACION)", new Font(Font.FontFamily.HELVETICA, 10f, Font.BOLDITALIC)));
            cellTituloTituloAnalisis.HorizontalAlignment = 1;
            cellTituloTituloAnalisis.BackgroundColor = new iTextSharp.text.BaseColor(234, 236, 238);
            cellTituloTituloAnalisis.BorderColor = new iTextSharp.text.BaseColor(0, 0, 0);
            cellTituloTituloAnalisis.Border = Rectangle.BOTTOM_BORDER | Rectangle.TOP_BORDER;
            tableTituloAnalisis.AddCell(cellTituloTituloAnalisis);

            docRepAnt.Add(tableTituloAnalisis);
            #endregion

            #region Datos Analisis
            Paragraph datoAnalisis = new Paragraph();
            datoAnalisis.Alignment = Element.ALIGN_JUSTIFIED;
            datoAnalisis.Font = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            datoAnalisis.Add(datosRepAnt.cAnalisis);
            datoAnalisis.Add(Chunk.NEWLINE);
            docRepAnt.Add(datoAnalisis);
            #endregion

            docRepAnt.Close();
            byte[] bytesStreamAnt = msAnt.ToArray();
            msAnt = new MemoryStream();
            msAnt.Write(bytesStreamAnt, 0, bytesStreamAnt.Length);
            msAnt.Position = 0;
            return new FileStreamResult(msAnt, "application/pdf");
        }

        public ActionResult printEntrega(string cmemo)
        {
            Font tituloTabla = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Font datoTabla = FontFactory.GetFont("Arial", 9, Font.NORMAL);

            var datosEntrega = Entrega.getDetalladaoMaestroCabecera(cmemo).FirstOrDefault();
            var datosEntregaBody = Entrega.getDetalladoMemoCuerpo(cmemo).ToList();

            MemoryStream ms = new MemoryStream();

            Document documentEntrega = new Document(PageSize.LETTER, 30f, 20f, 50f, 40f);
            PdfWriter pwEntrega = PdfWriter.GetInstance(documentEntrega, ms);

            pwEntrega.PageEvent = new HeaderEntrega();

            documentEntrega.Open();

            string imagenPath = @"C:/inetpub/wwwroot/fotoUser/gobedohor.png";
            Image imagen = Image.GetInstance(imagenPath);
            imagen.ScalePercent(35f);
            imagen.SetAbsolutePosition(30f, 725f);
            documentEntrega.Add(imagen);

            Paragraph datosGenerales = new Paragraph();
            datosGenerales.Alignment = Element.ALIGN_RIGHT;
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add(new Phrase("Memorándum: ", datoTabla));
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(new Phrase(datosEntrega.cmemo.ToString(), tituloTabla));
            datosGenerales.Add(Chunk.NEWLINE);
            datosGenerales.Add(new Phrase("Fecha Impresion: ", datoTabla));
            datosGenerales.Add(Chunk.TABBING);
            datosGenerales.Add(new Phrase(DateTime.Now.ToShortDateString(), tituloTabla));
            datosGenerales.Add(Chunk.NEWLINE); datosGenerales.Add(Chunk.NEWLINE);
            documentEntrega.Add(datosGenerales);

            Paragraph parayde = new Paragraph();
            parayde.Alignment = Element.ALIGN_LEFT;
            parayde.Add(new Phrase("Para: ", datoTabla));
            parayde.Add(Chunk.TABBING);
            parayde.Add(new Phrase(datosEntrega.cPara, tituloTabla));
            parayde.Add(Chunk.NEWLINE); parayde.Add(Chunk.TABBING);
            parayde.Add(new Phrase("Director de Información, Registro y Cadena de Custodia", datoTabla));
            parayde.Add(Chunk.NEWLINE);
            parayde.Add(new Phrase("De: ", datoTabla));
            parayde.Add(Chunk.TABBING);
            parayde.Add(new Phrase(datosEntrega.cDe, tituloTabla));
            parayde.Add(Chunk.NEWLINE); parayde.Add(Chunk.TABBING);
            parayde.Add(new Phrase("Director(a) de Psicología", datoTabla));
            parayde.Add(Chunk.NEWLINE);
            documentEntrega.Add(parayde);

            PdfPTable lista = new PdfPTable(4);
            lista.TotalWidth = 560f;
            lista.LockedWidth = true;


            float[] widthsLista = new float[] { 70f, 90f, 200f, 300f };
            lista.SetWidths(widthsLista);
            lista.HorizontalAlignment = 0;
            lista.SpacingBefore = 15f;
            lista.SpacingAfter = 10f;

            PdfPCell cellSerie = new PdfPCell(new Phrase("Serie", tituloTabla));
            cellSerie.HorizontalAlignment = 1;
            lista.AddCell(cellSerie);

            PdfPCell cellCodigo = new PdfPCell(new Phrase("Codigo", tituloTabla));
            cellCodigo.HorizontalAlignment = 1;
            lista.AddCell(cellCodigo);

            PdfPCell cellEvaluado = new PdfPCell(new Phrase("Evaluado", tituloTabla));
            cellEvaluado.HorizontalAlignment = 1;
            lista.AddCell(cellEvaluado);

            PdfPCell cellDependencia = new PdfPCell(new Phrase("Evaluado", tituloTabla));
            cellDependencia.HorizontalAlignment = 1;
            lista.AddCell(cellDependencia);

            foreach (var item in datosEntregaBody)
            {
                PdfPCell cellContendioSerie = new PdfPCell(new Phrase(item.serie, datoTabla));
                cellContendioSerie.BorderWidth = 0;
                lista.AddCell(cellContendioSerie);

                PdfPCell cellContendioCodigo = new PdfPCell(new Phrase(item.codigo, datoTabla));
                cellContendioCodigo.BorderWidth = 0;
                lista.AddCell(cellContendioCodigo);

                PdfPCell cellContendioEvaluado = new PdfPCell(new Phrase(item.evaluado, datoTabla));
                cellContendioEvaluado.BorderWidth = 0;
                lista.AddCell(cellContendioEvaluado);

                PdfPCell cellContendioDependencia = new PdfPCell(new Phrase(item.dependencia, datoTabla));
                cellContendioDependencia.BorderWidth = 0;
                lista.AddCell(cellContendioDependencia);
            }

            documentEntrega.Add(lista);

            Paragraph totalExp = new Paragraph();
            totalExp.Alignment = Element.ALIGN_LEFT;
            totalExp.Add(Chunk.NEWLINE); totalExp.Add(Chunk.NEWLINE);
            totalExp.Add(new Phrase("Total de expedientes: " + datosEntregaBody.Count.ToString(), tituloTabla));
            documentEntrega.Add(totalExp);

            Paragraph firma = new Paragraph();
            firma.Alignment = Element.ALIGN_CENTER;
            firma.Add(Chunk.NEWLINE); firma.Add(Chunk.NEWLINE); firma.Add(Chunk.NEWLINE);
            firma.Add(new Phrase(datosEntrega.cDe, datoTabla));
            firma.Add(Chunk.NEWLINE);
            firma.Add(new Phrase("_________________________________________", datoTabla));
            firma.Add(Chunk.NEWLINE);
            firma.Add(new Phrase("Directora de Psicología", datoTabla));
            documentEntrega.Add(firma);

            documentEntrega.Close();

            byte[] bytesStream = ms.ToArray();

            ms = new MemoryStream();
            ms.Write(bytesStream, 0, bytesStream.Length);

            ms.Position = 0;

            return new FileStreamResult(ms, "application/pdf");
        }
    }

    public class HeaderFooter : PdfPageEventHelper
    {
        private string _elCodigoEvaluado;

        public string CodEva
        {
            get { return _elCodigoEvaluado; }
            set { _elCodigoEvaluado = value; }
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Psicologia", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell2 = new PdfPCell(new Paragraph("Reporte de Evaluacion Psicologica", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell3 = new PdfPCell(new Paragraph("CECCC - DPS - F - 09", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell4 = new PdfPCell(new Paragraph("Código Evaluado: "+ _elCodigoEvaluado, FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL)));

            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell4.HorizontalAlignment = Element.ALIGN_RIGHT;

            _cell.Border = 0;
            _cell2.Border = 0;
            _cell3.Border = 0;
            _cell4.Border = 0;

            tbHeader.AddCell(_cell);
            tbHeader.AddCell(_cell2);
            tbHeader.AddCell(_cell3);
            tbHeader.AddCell(_cell4);

            tbHeader.AddCell(new Paragraph());

            //tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40, writer.DirectContent);
            tbHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 50, writer.DirectContent);

            //Footer
            PdfPTable tbFooter = new PdfPTable(1); //1 columna
            tbFooter.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbFooter.DefaultCell.Border = 0;

            tbFooter.AddCell(new Paragraph());  //celda 1

            _cell = new PdfPCell(new Paragraph("___________________________________________________________________________________", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;
            tbFooter.AddCell(_cell);

            _cell = new PdfPCell(new Paragraph("Toda Información contenida en este formato está clasificada como Confidencial, de conformidad con lo dispuesto en los artículos 133, 136 fracción I y 139 de la Ley de Transparencia y Acceso a la Información Pública del Estado de Chiapas.", FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.ITALIC)));
            _cell.HorizontalAlignment = Element.ALIGN_CENTER;
            _cell.Border = 0;

            tbFooter.AddCell(_cell);    //celda 2

            _cell = new PdfPCell(new Paragraph("Página " + writer.PageNumber, FontFactory.GetFont("ARIAL", 8, Font.BOLD)));
            _cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            _cell.Border = 0;

            tbFooter.AddCell(_cell); //celda 3

            tbFooter.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 10, writer.DirectContent);
        }
    
        public static HeaderFooter getMultilineFooter(string CodigoEvaluado)
        {
            HeaderFooter result = new HeaderFooter();

            result.CodEva = CodigoEvaluado;

            return result;
        }
    }

    class HeaderIndice : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Psicologia", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell2 = new PdfPCell(new Paragraph("Indice - CECCC - DPS - F - 10", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

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

    class HeaderEntrega : PdfPageEventHelper
    {
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //Header
            PdfPTable tbHeader = new PdfPTable(1); //entre parentesis es el número de columnas
            tbHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            tbHeader.DefaultCell.Border = 0;

            PdfPCell _cell = new PdfPCell(new Paragraph("Dirección de Psicologia", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));
            PdfPCell _cell2 = new PdfPCell(new Paragraph("Relación de Expediente entregados a Custodia", FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD)));

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