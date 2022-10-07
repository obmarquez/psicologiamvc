using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class ProtocoloController : Controller
    {
        // GET: Protocolo
        public ActionResult IndexProtocolo(int sIdH /*string curp, int ide*/)
        {
            if (Session["Id_Usuario"] != null)
            {
                //ViewBag.curp = curp;
                //ViewBag.ide = ide;
                ViewBag.sIdH = sIdH;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [HttpPost]
        public ActionResult IndexProtocolo(Protocolo p)
        {
            if (Session["Id_Usuario"] != null)
            {
                var accion_ = p.accion;

                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Protocolo.agregaActualizaProtocolo(p.idhistorico, Session["UsuarioSise"].ToString(), p.bAbordaje, p.bRegistroentrevista, p.bAclaracionhipotesis, p.bCongruenciadiagnostico, p.bRedaccionclara, p.bRespondemotivo, p.bPresenciaobservado, p.cObservacion, p.accion);

                if (result == "Ok")
                    return RedirectToAction("IndexEvaluacionSupervisorPsi", "EvaluacionPsi");
                else
                    return View();
            }
            else { return Json("SESION_EXPIRED"); }

            //return RedirectToAction("IndexProtocolo", new { curp = p.curp_evaluado, ide = p.idhistorico });

            //}
            //return RedirectToAction("IndexEvaluacionSupervisor", "Evaluacion");
        }

        public ActionResult EditarProtocolo(int sIdH /*string curp, int ide*/)
        {
            if (Session["Id_Usuario"] != null)
            {
                //ViewBag.curp = curp;
                //ViewBag.ide = ide;
                ViewBag.sIdH = sIdH;

                return View(Protocolo.getProtocolo(sIdH).FirstOrDefault<Protocolo>());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditarProtocolo(Protocolo p)
        {
            if (Session["Id_Usuario"] != null)
            {
                var accion_ = p.accion;

                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);

                string result = Protocolo.agregaActualizaProtocolo(p.idhistorico, Session["UsuarioSise"].ToString(), p.bAbordaje, p.bRegistroentrevista, p.bAclaracionhipotesis, p.bCongruenciadiagnostico, p.bRedaccionclara, p.bRespondemotivo, p.bPresenciaobservado, p.cObservacion, p.accion);

                if (result == "Ok")
                    //return RedirectToAction("IndexEvaluacionSupervisorPsi", "EvaluacionPsi");
                    return RedirectToAction("PsiProtocoloEditar", "Message");
                else
                    return View();
            }
            else { return Json("SESION_EXPIRED"); }
        }
    }
}