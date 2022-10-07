using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class IndiceController : Controller
    {
        // GET: Indice
        public ActionResult IndexIndice(string curp, int ide)
        {
            if (Session["Id_Usuario"] != null)
            {
                ViewBag._curp = curp;
                ViewBag._ide = ide;

                return View();
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult IndexIndice(Indice i )
        {
            if (Session["Id_Usuario"] != null)
            {
                string result = Indice.agregarIndice(i.idhistorico, i.curp_evaluado, Session["UsuarioSise"].ToString(), i.nIntegracion, i.nSolicitud, i.nFicha, i.nFichapermanencia, i.nFichaloc, i.nAutorizacion,
                    i.nComentarios, i.nNoconcluyo, i.nExamen, i.nEntrevista, i.nPruebas, i.nCambio, i.nWais, i.nTerman, i.nBeta, i.nRaven, i.nMmpii, i.n16pf, i.nAutobiografia, i.nMachover, i.nLluvia, i.nTat, i.nCleaver,
                    i.nBender, i.nGrupo, i.nLusher, i.cOtro, i.nOtro, i.nBarsit, i.nBeta3, i.nHtp, i.nSacks);

                if (result == "Ok")
                {
                    return RedirectToAction("IndexEvaluacion", "Evaluacion");
                }
                else
                    return View();
            }
            else { return Json("SESSION EXPIRED"); }
        }

        public ActionResult EditarIndice(string curp, int ide)
        {
            if (Session["Id_Usuario"] != null)
            {
                ViewBag._editarCurp = curp;
                ViewBag._editarIde = ide;

                return View(Indice.getIndice(curp, ide).FirstOrDefault<Indice>());
            }

            return RedirectToAction("IndexEvaluacion", "Evaluacion");
        }

        [HttpPost]
        public ActionResult EditarIndice(Indice i)
        {
            if(Session["Id_Usuario"] != null)
            {
                string result = Indice.actualizarIndice(i.idhistorico, i.curp_evaluado, Session["UsuarioSise"].ToString(), i.nIntegracion, i.nSolicitud, i.nFicha, i.nFichapermanencia, i.nFichaloc, i.nAutorizacion, 
                    i.nComentarios, i.nNoconcluyo, i.nExamen, i.nEntrevista, i.nPruebas, i.nCambio, i.nWais, i.nTerman, i.nBeta, i.nRaven, i.nMmpii, i.n16pf, i.nAutobiografia, i.nMachover, i.nLluvia, i.nTat, i.nCleaver, 
                    i.nBender, i.nGrupo, i.nLusher, i.cOtro, i.nOtro, i.nBarsit, i.nBeta3, i.nHtp, i.nSacks);

                if (result == "Ok")
                    return RedirectToAction("IndexEvaluacion", "Evaluacion");
                else
                    return View();
            }
            else { return Json("SESSION EXPIRED"); }
        }
    }
}