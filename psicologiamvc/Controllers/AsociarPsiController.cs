using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class AsociarPsiController : Controller // Cat_controlador: 24
    {
        // GET: AsociarPsi
        [FiltroPermisos]
        public ActionResult IndexAsoPsi(string fecha = "") //Cat_Controlador_Acciones : 56
        {
            if (Session["Id_Usuario"] != null)
            {
                if (fecha == "")
                    return View();
                else
                {                   
                    var losPsi = Consultas.get_investigador_area(7).ToList();   //llama a psicologos y supervisores
                    var losPsicos = new SelectList(losPsi, "UsuarioSise", "Nombre");
                    ViewData["psicologos"] = losPsicos;

                    return View(Consultas.getListaAsociarFecha(fecha, 3).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult js_asociar_curp(int p_idHistorico, string p_idEvaluador, string p_idSupervisor)
        {
            if (Session["Id_Usuario"] != null)
            {
                string result = Asociar.asociarPsi(p_idHistorico, p_idEvaluador, 3, true, 0, p_idSupervisor); //devolver OK

                return Json(result);
            }
            else { return Json("SESION_EXPIRED"); }
        }

        public JsonResult GetHistoricoEvaluadores(int sIdH)
        {
            return Json(Asociar.getHistoricoEvaluadoresxIdhistorico(sIdH));
        }

        public ActionResult js_desasociar(int p_idHistorico)
        {
            if(Session["Id_Usuario"] != null)
            {
                string result = Asociar.desasociar(p_idHistorico);
                return Json(result);
            }
            else { return Json("SESION_EXPIRED"); }
        }
    }
}