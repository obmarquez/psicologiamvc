using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace psicologiamvc.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        [FiltroPermisos]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult js_p_usuario_select()
        {
            var data = new { data = Usuarios.getLista(0).ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
            //return View(Usuarios.getLista(1).ToList());
        }

        public ActionResult Create()
        {
            //ViewBag.rolesCombo = Usuarios.getRolesCombo().ToList();
            //ViewData["rolesCombo"] = Usuarios.getRolesCombo().ToList();

            string constr = BDConn.getCadenaConexion();
            SqlConnection _con = new SqlConnection(constr);
            SqlDataAdapter _da = new SqlDataAdapter("select Id_Rol, Desc_Rol from Cat_Usuarios_Roles order by Desc_Rol", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            ViewBag.Roles = ToSelectList(_dt, "Id_Rol", "Desc_Rol");

            return View();
        }

        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }


        [HttpPost]
        [FiltroPermisos]
        public ActionResult Create(Usuarios u)
        {
            if (Session["Id_Usuario"] != null)
            {
                List<Permisos> list_permisos = new List<Permisos>();
                list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Usuarios.insert(Convert.ToInt32(Session["Id_Usuario"]), u.Id_Rol, pid_controlador_accion, u.Nombre, u.Ap_Paterno, u.Ap_Materno, u.Correo,u.Contrasena, u.UsuarioSise);
                //return Json(result);
                return RedirectToAction("Index");

            }
            else { return RedirectToAction("acceso", "Home"); }
          //  Usuarios.insert(1, u.Id_Rol, u.Id_Rol, u.Nombre, u.Ap_Paterno, u.Ap_Materno, u.Correo, u.Id_Rol);

           
        }

        [FiltroPermisos]
        public ActionResult EditarUsuario(int id)
        {
            if (Session["Id_Usuario"] != null)
            {
                string constr = BDConn.getCadenaConexion();
                SqlConnection _con = new SqlConnection(constr);
                SqlDataAdapter _da = new SqlDataAdapter("select Id_Rol, Desc_Rol from Cat_Usuarios_Roles order by Desc_Rol", constr);
                DataTable _dt = new DataTable();
                _da.Fill(_dt);
                ViewBag.Roles = ToSelectList(_dt, "Id_Rol", "Desc_Rol");

                List<Permisos> list_permisos = new List<Permisos>();
                list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);

                return View(Usuarios.getLista(id).FirstOrDefault<Usuarios>());
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [FiltroPermisos]
        public ActionResult EditarUsuario(Usuarios user)
        {
            if(Session["Id_Usuario"] != null)
            {
                List<Permisos> list_permisos = new List<Permisos>();
                list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Usuarios.updateContrasena(user.Id_Usuario, user.Id_Rol, user.Nombre, user.Ap_Paterno, user.Ap_Materno, user.Contrasena, user.Activo2, user.UsuarioSise);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    
        public ActionResult ObtenerUsuario()
        {
            return View(Usuarios.getListaUsuariosArea(3).ToList());
        }
    }
}