using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class EntregaPsiController : Controller //Cat_Controller: 37
    {
        // GET: Entrega
        [FiltroPermisos]
        public ActionResult Index()//Cat_Controlador_Accion: 93
        {
            //return View(Usuarios.getLista(0).ToList());
            ViewBag.memo = Entrega.getSiguienteMemo(3).FirstOrDefault();

            var losACustodia = Entrega.getEvaluadosACustodia(3).ToList();
            var losAC = new SelectList(losACustodia, "idhistorico", "evaluado");
            ViewData["aCustodia"] = losAC;

            //List<Entrega> evalCus = Entrega.getEvaluadosACustodia(3).ToList();
            //ViewData["aCustodia"] = evalCus;

            return View();
        }

        public JsonResult AgregaEntrega(Entrega ent)
        {
            string resultado = "Error";

            string result = Entrega.agregaEntrega(ent.idhistorico, ent.nDireccion, ent.cmemo, ent.idevaluacion_poligrafica);

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

        [FiltroPermisos]
        public ActionResult ConcentradoMemos() //Cat_Controlador_Accion: 94
        {
            return View(Entrega.getConcentadoMemos(3).ToList());
        }
    }
}
