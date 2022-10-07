using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class ControlController : Controller //Cat_Controlador: 39
    {
        // GET: Control
        [FiltroPermisos]
        public ActionResult Index(string idsupervisor = "", string f1 = "", string f2 = "") //Cat_Contolador_Acciones : 97
        {
            if (Session["Id_Usuario"] != null)
            {
                var losSup = Consultas.getSupervisores(3).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                if (idsupervisor == "" || f1 == "" || f2 == "" )
                {
                    return View();
                }
                else
                {
                    return View(Controlsupervision.getConcentradoSuper(idsupervisor, f1, f2).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult js_agregar_actualizar_control(int p_idHistorico, string p_observacion, string p_fCorreccion, string p_fDevolucion, string p_dx2, string p_fEntrega, int p_accion)
        {
            if (Session["Id_Usuario"] != null)
            {
                string result = Controlsupervision.agregarActualizarControlSupervision(p_idHistorico, p_observacion, p_fCorreccion, p_fDevolucion, p_dx2, p_fEntrega, p_accion);
                return Json(result);
            }
            else 
            { return Json("SESSION EXPIRED"); }
        }

        //public JsonResult GetDatosControl(int p_idHistorico)
        //{
        //    return Json(Controlsupervision.obtenerDatosControl(p_idHistorico).FirstOrDefault<Controlsupervision>(), JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetDatosControl(Controlsupervision controlsupervision)
        {
            return Json(Controlsupervision.obtenerDatosControl(controlsupervision.idHistorico).FirstOrDefault<Controlsupervision>(), JsonRequestBehavior.AllowGet);
        }
    }
}