using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult acceso()
        {
            return View();
        }

        public ActionResult inicio()
        {
            ViewBag.anual = Consultas.getDatosHomeIndex(1).ToList();
            ViewBag.test = Consultas.obtenerDatosTest().FirstOrDefault();
            ViewBag.anualEvaluado = Consultas.obtenerEvaluacionesMensuales().ToList();
            
            ViewBag.anual_b = Consultas.getEvolucionEvaluacionPsicologo(1, Session["UsuarioSise"].ToString()).ToList();
            ViewBag.anualDx_b = Consultas.getEvolucionEvaluacionPsicologo(2, Session["UsuarioSise"].ToString()).ToList();

            if (Session["Id_Usuario"] != null)
            {
                if (Convert.ToInt32(Session["Id_Rol"]) == 19)
                {
                    var nin = Consultas.getTotalEvaluadoTipoPsicologia(1, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.nuevoIngreso = nin.TotalE;

                    var per = Consultas.getTotalEvaluadoTipoPsicologia(2, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.permanencia = per.TotalE;

                    var pro = Consultas.getTotalEvaluadoTipoPsicologia(3, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.promocion = pro.TotalE;

                    //ViewBag.anual = Consultas.getDatosHomeIndex(1).ToList();
                }

                if (Convert.ToInt32(Session["Id_Rol"]) == 10)
                {
                    var nin = Consultas.getTotalEvaluadoTipoPsicologia(4, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.nuevoIngreso = nin.TotalE;

                    var per = Consultas.getTotalEvaluadoTipoPsicologia(5, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.permanencia = per.TotalE;

                    var pro = Consultas.getTotalEvaluadoTipoPsicologia(6, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.promocion = pro.TotalE;

                    ViewBag.totHomMuj = Consultas.getDatosHomeIndex(2).FirstOrDefault();

                    //ViewBag.anual = Consultas.getDatosHomeIndex(1).ToList();
                }

                if (Convert.ToInt32(Session["Id_Rol"]) == 5)
                {
                    var nin = Consultas.getTotalEvaluadoTipoPsicologia(7, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.nuevoIngreso = nin.TotalE;

                    var per = Consultas.getTotalEvaluadoTipoPsicologia(8, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.permanencia = per.TotalE;

                    var pro = Consultas.getTotalEvaluadoTipoPsicologia(9, Session["UsuarioSise"].ToString()).FirstOrDefault();
                    ViewBag.promocion = pro.TotalE;

                    ViewBag.totHomMuj = Consultas.getDatosHomeIndex(2).FirstOrDefault();

                    //ViewBag.anual = Consultas.getDatosHomeIndex(1).ToList();
                }

                return View(Consultas.ListStatus(Session["UsuarioSise"].ToString(), Convert.ToInt32(Session["Id_Rol"])).ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }

        }

        [HttpPost]
        public ActionResult acceso(Logeo model)
        {
            if (ModelState.IsValid)
            {
                Usuarios Usuario = Logeo.IsValid(model.UserName, model.Password);
                if (Usuario.Activo == 1)
                {
                    Session["Id_Usuario"] = Usuario.Id_Usuario;
                    Session["Id_Rol"] = Usuario.Id_Rol;
                    Session["Nombre"] = Usuario.NombreCompleto;
                    Session["Correo"] = Usuario.Correo;
                    Session["Foto"] = Usuario.Foto;
                    Session["usuarioX"] = Usuario;
                    Session["UsuarioSise"] = Usuario.UsuarioSise;

                    List<Permisos> permisos = Permisos.getLista(Usuario.Id_Rol);
                    Session["ListaPermisos"] = permisos;

                    return RedirectToAction("inicio", "Home");

                }
                else if (Usuario.Activo == 2)
                {
                    ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                }
            }
            else { ModelState.AddModelError("", "El usuario o la contraseña son incorrectos."); }
            return View(model);
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("acceso", "Home");
        }
    }
}