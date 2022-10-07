using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class ObservacionController : Controller //Cat_Controlador : 32
    {
        // GET: Observacion
        [FiltroPermisos]
        public ActionResult Index(string fecha) //Cat_Controlador_Acciones : 70
        {
            if (Session["Id_Usuario"] != null)
            {
                if (fecha == null)
                {
                    return View();
                }
                else
                {
                    return View(Consultas.getListaObservacionDiaria(1, fecha, 0).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public JsonResult GetPendRes(Observaciones observaciones)
        {
            return Json(Observaciones.getPendientesContestar(observaciones.sIdH, 3).FirstOrDefault<Observaciones>(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDataObservacionPendiente(Observaciones observaciones)
        {
            return Json(Consultas.getListaObservacionDiaria(2, "", observaciones.id).FirstOrDefault<Consultas>(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult js_agrega_observacion(int p_idHistorico, String p_observacionpublica)
        {
            if(Session["Id_Usuario"] != null)
            {
                List<Permisos> list_permisos = new List<Permisos>();
                list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Observaciones.agregarObservacion(Session["UsuarioSise"].ToString(), p_idHistorico, p_observacionpublica);
                return Json(result);
            }
            else { return Json("SESSION EXPIRED"); }
        }

        public ActionResult js_responder_observacion(int p_idObs, string p_respuesta)
        {
            if (Session["Id_Usuario"] != null)
            {
                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Observaciones.responderObservacion(p_idObs, Session["UsuarioSise"].ToString(), p_respuesta, 3);
                return Json(result);
            }
            else { return Json("SESSION EXPIRED"); }
        }
    }
}