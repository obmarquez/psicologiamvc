using psicologiamvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace psicologiamvc.Controllers
{
    public class EvaluacionController : Controller 
    {  
        // GET: Evaluacion
        [FiltroPermisos]
        public ActionResult IndexEvaluacion()
        {

            //if (Convert.ToInt32(Session["Id_Rol"]) == 1)
            //{
            //    return View(Consultas.getListaConsultaEvaluado(Session["UsuarioSise"].ToString()).ToList());
            //}
            //else
            //{
            //    return View(Consultas.getListaConsultaEvaluado(Session["UsuarioSise"].ToString()).ToList());
            //}

            //Obteniendo los municipios para el modal del red de vinculos
            if (Session["Id_Usuario"] != null)
            {
                var losMuni = Consultas.getMunicipiosEstado().ToList();
                var losMunic = new SelectList(losMuni, "cve_mun", "mun_desc");
                ViewData["municipios"] = losMunic;

                return View(Consultas.getListaConsultaEvaluado(Session["UsuarioSise"].ToString()).ToList());
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        [FiltroPermisos]
        public ActionResult IndexEvaluacionSupervisor(string cadena = "")
        {
            if (Session["Id_Usuario"] != null)
            {
                if (cadena.Trim() != "")
                    return View(Consultas.getListaParaSupervisor(cadena).ToList());
                else
                    return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        //public ActionResult Ver(string curp, int ide)
        //{
        //    return View(Evaluacion.getEval(curp, ide));
        //}

        public ActionResult Nuevo(string curp, int ide)
        {
            if (Session["Id_Usuario"] != null)
            {
                var lasAreas = Consultas.getCriterios01(1).ToList();
                var listaAreas = new SelectList(lasAreas, "criterio", "criterio");
                ViewData["criter"] = listaAreas;

                var lasAreas2 = Consultas.getCriterios01(2).ToList();
                var listaAreas2 = new SelectList(lasAreas2, "criterio", "criterio");
                ViewData["criter02"] = listaAreas2;

                var lasAreas3 = Consultas.getCriterios01(3).ToList();
                var listaAreas3 = new SelectList(lasAreas3, "criterio", "criterio");
                ViewData["criter03"] = listaAreas3;

                var losSup = Consultas.getSupervisores(3).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                ////lista de las observaciones publicas del evaluado
                //List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(curp).ToList();
                //ViewData["obser"] = observacionesEval;

                //cantidad de observaciones por resolver
                //var obsPen = Observaciones.getPendientesContestar(curp).ToList();
                //ViewData["obserPendiente"] = obsPen;

                ViewBag.curp = curp;
                ViewBag.ide = ide;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        //[HttpPost]
        //public ActionResult Nuevo(Evaluacion e)
        //{
        //    if (Session["Id_Usuario"] != null)
        //    {
        //        string result = Evaluacion.insertaEvaluacion(e.idhistorico, e.curp_evaluado, Session["UsuarioSise"].ToString(), e.cmbJuicio2, e.cmbControlimpulsos, e.cmbApegonormas, e.cmbToleranciapresion, e.cmbEstabilidademocional,
        //            e.cmbAlteraciones, e.cAutoestima, e.cEmociones, e.cRelaciones, e.cmbCompetencias, e.cCompetencia, e.bLiderazgo, e.bPlaneacion, e.bDecisiones, e.bconflictos, e.bAtencion, e.bAdaptabilidad, e.bVocacion, e.bCapacidad,
        //            e.bObservaciones, e.bAnalisis, e.bHabilidades, e.bTrabajo, e.bOrientacion, e.bIntegridad, e.fEvalpsi, e.cLugarnac, e.cEscolaridad, e.cGrado, e.cBender, e.cDescdano, e.cLaboralpsi, e.bMachover, e.bFigura, e.bMerril,
        //            e.bMmpi, e.bAutobiografia, e.bCleaver, e.bLuscher, e.bTest, e.bRaven, e.bBeta, e.bHtp, e.bSacks, e.b16fp, e.bBetaiiifr, e.cCi, e.cFortalezas, e.cRiesgos, e.cDiagnostico, e.cRecomendaciones, e.cResultado,
        //            e.idSupPsi, e.bConcluido);

        //        if (result == "Ok")
        //            return RedirectToAction("IndexEvaluacion");

        //        else
        //            return View();
        //    }
        //    else { return Json("SESION_EXPIRED"); }

        //}

        //con Json
        //public ActionResult js_inserta_evaluacion(string pcurp, int pid)
        //{
        //    if (Session["Id_Usuario"] != null)
        //    {
        //        string result = Evaluacion.insertaEvaluacion(pid, pcurp, Session["UsuarioSise"].ToString());
        //        return Json(result);
        //    }
        //    else { return Json("SESION_EXPIRED"); }
        //}

        public ActionResult Editar(string curp, int ide)
        {
            if(Session["Id_Usuario"] != null)
            {
                var lasAreas = Consultas.getCriterios01(1).ToList();
                var listaAreas = new SelectList(lasAreas, "criterio", "criterio");
                ViewData["criter"] = listaAreas;

                var lasAreas2 = Consultas.getCriterios01(2).ToList();
                var listaAreas2 = new SelectList(lasAreas2, "criterio", "criterio");
                ViewData["criter02"] = listaAreas2;

                var lasAreas3 = Consultas.getCriterios01(3).ToList();
                var listaAreas3 = new SelectList(lasAreas3, "criterio", "criterio");
                ViewData["criter03"] = listaAreas3;

                var losSup = Consultas.getSupervisores(3).ToList();
                var losSuper = new SelectList(losSup, "UsuarioSise", "Nombre");
                ViewData["super"] = losSuper;

                //lista de las observaciones publicas del evaluado
                //List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(curp).ToList();
                //ViewData["obser"] = observacionesEval;

                //cantidad de observaciones por resolver
                //var obsPen = Observaciones.getPendientesContestar(curp).ToList();
                //ViewData["obserPendiente"] = obsPen;

                ViewBag.curp = curp;
                ViewBag.ide = ide;

                //return View(Evaluacion.getEvaluacion(curp, ide).FirstOrDefault<Evaluacion>());
                return View();
            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult Editar(Evaluacion e)
        //{
        //    if (Session["Id_Usuario"] != null)
        //    {
        //        string result = Evaluacion.actualizaPsicologia(e.idhistorico, e.curp_evaluado, e.cBender, e.cDescdano, e.cDiagnostico, e.bMachover, e.bFigura, e.bMerril, e.bMmpi, e.bAutobiografia, e.cCi, Session["UsuarioSise"].ToString(), 
        //            e.cLaboralpsi, e.cFortalezas, e.cRiesgos, e.cRecomendaciones, e.cResultado, e.bConcluido, e.cLugarnac, e.cGrado, e.cEscolaridad, e.bCleaver, e.bTest, e.bRaven, e.bBeta, e.fEvalpsi, e.bLuscher, e.idSupPsi, e.cmbJuicio2, 
        //            e.cmbControlimpulsos, e.cmbApegonormas, e.cmbToleranciapresion, e.cmbEstabilidademocional, e.cmbAlteraciones, e.cmbCompetencias, e.bHtp, e.bSacks, e.b16fp, e.bBetaiiifr, e.cAutoestima, e.cEmociones, e.cRelaciones, 
        //            e.cCompetencia, e.bLiderazgo, e.bPlaneacion, e.bDecisiones, e.bconflictos, e.bAtencion, e.bAdaptabilidad, e.bVocacion, e.bCapacidad, e.bObservaciones, e.bAnalisis, e.bHabilidades, e.bTrabajo, e.bOrientacion, e.bIntegridad);

        //        if (result == "Ok")
        //        {
        //            if(Convert.ToInt32(Session["Id_Rol"]) == 4)
        //                return RedirectToAction("IndexEvaluacion");
        //            else
        //                return RedirectToAction("IndexEvaluacionSupervisor");
        //        }
                    
        //        else
        //            return View();
        //    }
        //    else { return Json("Session_Expired"); }
        //}

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

        //public ActionResult js_red_vinculos(Int32 p_idHistoricoIndex, String p_nombreReferido_Modal, String p_paternoReferido_Modal, String p_maternoReferido_Modal, String p_genero_Modal, String p_relacion_Modal, String p_alias_Modal, 
        //    String p_coorporacion_Modal, String p_municipio_Modal, String p_referencia_Modal)
        //{
        //    if(Session["Id_Usuario"] != null)
        //    {
        //        List<Permisos> list_permisos = new List<Permisos>();
        //        list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
        //        int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
        //        string result = Red.agregarRed(0, p_idHistoricoIndex, p_nombreReferido_Modal, p_alias_Modal, p_relacion_Modal, p_genero_Modal, p_coorporacion_Modal, p_municipio_Modal, p_referencia_Modal, Session["UsuarioSise"].ToString(),
        //            1, "curp", 1, p_paternoReferido_Modal, p_maternoReferido_Modal);
        //        return Json(result);
        //    }
        //    else { return Json("SESSION EXPIRED"); }
        //}

        [FiltroPermisos]
        public ActionResult EntradaDiaria(string fecha = "")
        {
            if (Session["Id_Usuario"] != null)
            {
                if (fecha == "")
                {
                    return View();
                }
                else
                {
                    return View(Consultas.getEntradaDiaria(fecha).ToList());
                }
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult IndiceEvaluacion(string curp, int ide)
        {
            if (Session["Id_Usuario"] != null)
            {
                return RedirectToAction("IndexIndice", "Indice", new { curp = curp, ide = ide });
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult EditarIndiceEvaluacion(string curp, int ide)
        {
            if (Session["Id_Usuario"] != null)
            {
                return RedirectToAction("EditarIndice", "Indice", new { curp = curp, ide = ide });
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }
    }
}