using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class RedController : Controller
    {
        // GET: Red
        public ActionResult IndexRed(string cadena = "")
        {
            if (Session["Id_Usuario"] != null)
            {
                if (cadena.Trim() != "")
                    return View(Consultas.getListaReferenciasRed(cadena).ToList());
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult ConsultasRed_Municipio(string cadena = "")
        {
            if (Session["Id_Usuario"] != null)
            {
                //Obteniendo los municipios para el modal del red de vinculos
                var losMuni = Consultas.getMunicipiosEstado().ToList();
                var losMunic = new SelectList(losMuni, "cve_mun", "mun_desc");
                ViewData["municipios"] = losMunic;

                if (cadena.Trim() != "")
                {
                    return View(Consultas.get_sp_general_consultas_redVinculos(1, "-", cadena, 0).ToList());
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult ConsultasRed_Dependencias(int dependencia = 0)
        {
            if (Session["Id_Usuario"] != null)
            {
                //Obteniendo las dependencias para el modal del red de vinculos
                var lasDep = Consultas.getDependencias().ToList();
                var lasDepen = new SelectList(lasDep, "clave", "dependencia");
                ViewData["dependencias"] = lasDepen;

                if (dependencia != 0)
                {
                    return View(Consultas.get_sp_general_consultas_redVinculos(2, "-", "-", dependencia).ToList());
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult ConsultasRed_Alias(string cadena = "")
        {
            if (Session["Id_Usuario"] != null)
            {
                if (cadena.Trim() != "")
                {
                    return View(Consultas.get_sp_general_consultas_redVinculos(3, cadena, "-", 0).ToList());
                }
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}