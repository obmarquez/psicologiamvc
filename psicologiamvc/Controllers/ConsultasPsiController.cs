using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;

namespace psicologiamvc.Controllers
{
    public class ConsultasPsiController : Controller // Cat_controlador: 27
    {
        [FiltroPermisos]
        // GET: ConsultasPsi
        public ActionResult IndexGralPsi()  // Cat_controlador_Acciones: 60
        {
            if(Session["Id_Usuario"] != null)
            {
                return View(Consultas.getGeneralPsicologo().ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexConcentradoPsi() // Cat_Controlador_Acciones : 71
        {
            if (Session["Id_Usuario"] != null)
            {
                return View(Consultas.getConcentradoPsicologo().ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexConcentradoSupervisor() // Cat_Controlador_Acciones : 72
        {
            if (Session["Id_Usuario"] != null)
            {
                return View(Consultas.getConcentradoSupervisor().ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexGraPsiRes(string psicologo = "", string fecha01 = "", string fecha02 = "") // Cat_Controlador_Acciones : 73
        {
            ViewBag.anual = Consultas.getEvolucionEvaluacionPsicologo(1, psicologo).ToList();
            ViewBag.anualDx = Consultas.getEvolucionEvaluacionPsicologo(2, psicologo).ToList();

            if (Session["Id_Usuario"] != null)
            {
                var losEval = Consultas.get_investigador_area(3).ToList();
                var losPsi = new SelectList(losEval, "UsuarioSise", "Nombre");
                ViewData["psicologos"] = losPsi;

                if (psicologo != "")
                {
                    return View(Consultas.getConcentradoResultado(psicologo, fecha01, fecha02).ToList());
                }
                else
                {
                    return View(Consultas.getConcentradoResultado("999", "01/01/1900", "01/01/1900").ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexCriteriosMyM(string supervisor = "", string fecha01 = "", string fecha02 = "") //Cat_Controlador_Acciones 98
        {
            if (Session["Id_Usuario"] != null)
            {
                var losSup = Consultas.getSupervisores(3).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                if (supervisor != "" || fecha01 != "" || fecha02 != "")
                {
                    return View(Consultas.obtenerListaCritreriosMayoresMenores(supervisor, fecha01, fecha02).ToList());
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}