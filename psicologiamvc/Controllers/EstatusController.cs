using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    
    [FiltroPermisos]
    public class EstatusController : Controller //ID CAT_CONTROLADOR : 26
    {
        // GET: Estatus
        public ActionResult IndexEstatus()  //ID CAT_CONTROLADOR_ACCIONES : 58
        {
            if (Session["Id_Usuario"] != null)
            {
                return View(Estatus.getListaEstatusDifCincoPsicologia(0).ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [HttpPost]
        public JsonResult EditarEstatus(Estatus est)  //ID CAT_CONTROLADOR_ACCIONES : 59
        {
            var resultado = new BaseResultado();

            string result = Estatus.updateEstatusSoc(est.idhistorico, est.direccion);

            if (result == "OK")
            {
                resultado.mensaje = "Estatus actualizado satisfactoriamente";
                resultado.Ok = true;
            }
            else
            {
                resultado.mensaje = "No se pudo, verificarlo";
                resultado.Ok = false;
            }
            return Json(resultado);
        }
    }
}

public class BaseResultado
{
    public bool Ok { get; set; }
    public string mensaje { get; set; }
}