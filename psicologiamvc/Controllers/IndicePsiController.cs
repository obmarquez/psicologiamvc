using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class IndicePsiController : Controller
    {
        // GET: IndicePsi
        public ActionResult NuevoIndice(int sIdH)
        {
            if (Session["Id_Usuario"] != null)
            {
                ViewBag.sIdH = sIdH;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public JsonResult AddUpdIndice(IndicePsi Ind)
        {
            string resultado = "Error";

            string result = IndicePsi.agregaActualizaIndicePsi(Ind.idh, Session["UsuarioSise"].ToString(), Ind.nIntegracion, Ind.nSolicitud, Ind.nFicha, Ind.nFichapermanencia, Ind.nFichaloc, Ind.nAutorizacion, Ind.nComentarios, Ind.nNoconcluyo, Ind.nExamen, Ind.nEntrevista, Ind.nPruebas, Ind.nCambio, Ind.nWais, Ind.nTerman, Ind.nBeta, Ind.nRaven, Ind.nMmpii, Ind.n16pf, Ind.nAutobiografia, Ind.nMachover, Ind.nLluvia, Ind.nTat, Ind.nCleaver, Ind.nBender, Ind.nGrupo, Ind.nLusher, Ind.cOtro, Ind.nOtro, Ind.nBarsit, Ind.nBeta3, Ind.nHtp, Ind.nSacks, Ind.accion, Ind.nMoca, Ind.nMmp2rf);

            if (result == "Ok")
            {
                resultado = "Ok";
            }
            else
            {
                resultado = "Error";
            }
            return Json(resultado);
        }

        public ActionResult EditarIndice(int sIdH)
        {
            if (Session["Id_Usuario"] != null)
            {
                ViewBag.sIdH = sIdH;

                return View(IndicePsi.getIndice(sIdH).FirstOrDefault());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}