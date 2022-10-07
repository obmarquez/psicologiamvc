using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class AsistenciaPsiController : Controller //Cat_controlador : 31
    {
        // GET: AsistenciaPsi
        [FiltroPermisos]
        public ActionResult Index(string fecha = "")
        {
            if(Session["Id_Usuario"] != null)
            {
                if (fecha == "")
                {
                    return View();
                }
                else
                {
                    return View(Asistencia.getListaAsistencia(fecha).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
            
        }

        public JsonResult GetData(Asistencia asistencia)
        {
            //return Json(asistencia.idHistorico, JsonRequestBehavior.AllowGet);
            return Json(Asistencia.getAsistncia(asistencia.idHistorico).FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult js_asistencia_tomar(Asistencia asis)
        {
            if (Session["Id_Usuario"] != null)
            {
                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                //string result = Asistencia.actualizaAsistencia(p_bToxIn, p_fToxIn, p_bToxOut, p_fTox, p_bMedIn, p_fMedIn, p_bMedOut, p_fMed, 3, Session["UsuarioSise"].ToString(), false, p_idHistorico);
                string result = Asistencia.actualizaAsistencia(asis.bPsi1in, asis.fPsico1in, asis.bPsi1out, asis.fPsico1out, asis.bPsi2in, asis.fPsico2in, asis.bPsi2out, asis.fPsico2out, 3, Session["UsuarioSise"].ToString(), false, asis.idHistorico);

                return Json(result);
            }
            else
            {
                return Json("SESSION EXPIRED");
            }
        }
    }
}