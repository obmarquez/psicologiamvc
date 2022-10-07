using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using psicologiamvc.Models;
using System.Data;

namespace psicologiamvc.Controllers
{
    public class EvaluacionPsiController : Controller // Cat_controlador: 23
    {
        // GET: Evaluacion
        [FiltroPermisos]
        public ActionResult IndexEvaluacionPsi() //Cat_Controlador_Acciones : 54
        {
            if (Session["Id_Usuario"] != null)
            {
                //Obteniendo los municipios para el modal del red de vinculos
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
        public ActionResult IndexEvaluacionSupervisorPsi(string cadena = "") //Cat_Controlador_Acciones : 55
        {
            //string valor = Session["Id_Usuario"].ToString();

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

        public ActionResult NuevoPsi(int sIdH, string elEvaluado)
        {
            if (Session["Id_Usuario"] != null)
            {
                var lasAreas = Consultas.getCriterios01(3).ToList();
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
                List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(sIdH, 3);
                ViewData["obser"] = observacionesEval;

                //cantidad de observaciones por resolver
                var obsPen = Observaciones.getPendientesContestar(sIdH, 3);
                ViewData["obserPendiente"] = obsPen;

                //Lista de observaciones del área de Custodia
                List<Consultas> obsCus = Consultas.getListaObservacionCustodia(sIdH);
                ViewData["observaCust"] = obsCus;

                //Lista de referencias (3 ultimas)
                List<Evaluacion> referenciasEvaluado = Evaluacion.getReferencias(sIdH);
                ViewData["ultmasReferencia"] = referenciasEvaluado;

                ViewBag.sIdH = sIdH;

                ViewBag.evaluadillo = elEvaluado;

                return View();
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public JsonResult AddUpdInvestigacionPsicologica(Evaluacion Eval)
        {
            string resultado = "Error";

            string result = Evaluacion.agregaActualizaInvestigacionPsicologica(Eval.idhistorico, Eval.cDiagnostico, Eval.bMachover, Eval.bFigura, Eval.bMerril, Eval.bMmpi, Eval.bAutobiografia, Eval.cCi, Eval.cLaboralpsi, Eval.cRecomendaciones, Eval.cResultado, Eval.bConcluido, Session["UsuarioSise"].ToString(), Eval.cLugarnac, Eval.cGrado, Eval.cEscolaridad, Eval.bCleaver, Eval.bTest, Eval.bRaven, Eval.fEvalpsi, Eval.idSupPsi, Eval.bHtp, Eval.bSacks, Eval.bBetaiiifr, Eval.cCompetencia, Eval.bLiderazgo, Eval.bPlaneacion, Eval.bDecisiones, Eval.bconflictos, Eval.bAtencion, Eval.bAdaptabilidad, Eval.bVocacion, Eval.fortaleza_riesgo, Eval.notas, Eval.accion, Eval.bMoca, Eval.bMmpi2rf);
            //string result = Evaluacion.agregaActualizaInvestigacionPsicologica(Eval.idhistorico, Eval.cDiagnostico, Eval.bMachover, Eval.bFigura, Eval.bMerril, Eval.bMmpi, Eval.bAutobiografia, Eval.cCi, Eval.cLaboralpsi, Eval.cRecomendaciones, Eval.cResultado, Eval.bConcluido, Session["UsuarioSise"].ToString(), Eval.cLugarnac, Eval.cGrado, Eval.cEscolaridad, Eval.bCleaver, Eval.bTest, Eval.bRaven, Eval.bBeta, Eval.fEvalpsi, Eval.bLuscher, Eval.idSupPsi, Eval.bHtp, Eval.bSacks, Eval.b16fp, Eval.bBetaiiifr, Eval.cCompetencia, Eval.bLiderazgo, Eval.bPlaneacion, Eval.bDecisiones, Eval.bconflictos, Eval.bAtencion, Eval.bAdaptabilidad, Eval.bVocacion, Eval.fortaleza_riesgo, Eval.notas, Eval.accion, Eval.bMoca, Eval.bMmpi2rf);

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

        public ActionResult EditarPsi(int sIdH, string elEvaluado)
        {
            if (Session["Id_Usuario"] != null)
            {
                var lasAreas = Consultas.getCriterios01(3).ToList();
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
                List<Consultas> observacionesEval = Consultas.getListaObservacionDiariaxEvaluado(sIdH, 3);
                ViewData["obser"] = observacionesEval;

                //cantidad de observaciones por resolver
                var obsPen = Observaciones.getPendientesContestar(sIdH, 3);
                ViewData["obserPendiente"] = obsPen;

                //Lista de observaciones del área de Custodia
                List<Consultas> obsCus = Consultas.getListaObservacionCustodia(sIdH);
                ViewData["observaCust"] = obsCus;

                //Lista de referencias (3 ultimas)
                List<Evaluacion> referenciasEvaluado = Evaluacion.getReferencias(sIdH);
                ViewData["ultmasReferencia"] = referenciasEvaluado;

                ViewBag.sIdH = sIdH;
                ViewBag.evaluadillo = elEvaluado;

                return View(Evaluacion.getEvaluacion(sIdH).FirstOrDefault());
            }
            else
            {
                return RedirectToAction("Index");
            }
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

        public ActionResult js_red_vinculos(Int32 p_idHistoricoIndex, String p_nombreReferido_Modal, String p_paternoReferido_Modal, String p_maternoReferido_Modal, String p_genero_Modal, String p_relacion_Modal, String p_alias_Modal, String p_coorporacion_Modal, String p_municipio_Modal, String p_referencia_Modal)
        {
            if (Session["Id_Usuario"] != null)
            {
                //List<Permisos> list_permisos = new List<Permisos>();
                //list_permisos = (List<Permisos>)HttpContext.Session["ListaPermisos"];
                //int pid_controlador_accion = Permisos.getId_Accion(Request.RequestContext.RouteData.Values["action"].ToString(), list_permisos);
                string result = Red.agregarRed(0, p_idHistoricoIndex, p_nombreReferido_Modal, p_alias_Modal, p_relacion_Modal, p_genero_Modal, p_coorporacion_Modal, p_municipio_Modal, p_referencia_Modal, Session["UsuarioSise"].ToString(), 1, p_paternoReferido_Modal, p_maternoReferido_Modal);
                return Json(result);
            }
            else { return Json("SESSION EXPIRED"); }
        }

        [FiltroPermisos]
        public ActionResult EntradaDiariaPsi(string fecha = "") //Cat_Controlador_Accion : 69
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

        public ActionResult convertirImagen(int _IdH)
        {
            var imagenEvaluado = Evaluacion.getImgEva(_IdH).FirstOrDefault<Evaluacion>();
            if (imagenEvaluado != null)
                return File(imagenEvaluado.Picture, "image/jpeg");
            else
                return Json("Nada");
        }

        public ActionResult getTatuajeEspecifico(int idh, int idt, int opcion)
        {
            var imagent = Evaluacion.getImgTatuaje(idh, idt, opcion).FirstOrDefault<Evaluacion>();
            if (imagent != null)
                return File(imagent.tatuaje, "image/jpeg");
            else
                return Json("Nada");
        }

        //ver lista de tatuajes
        public PartialViewResult seccionDetalle(int IdH, int idt, int opcion)
        {
            return PartialView("_evaluacionpsi", Evaluacion.getImgTatuaje(IdH, idt, opcion).ToList());
        }

        public ActionResult IndiceEvaluacionPsi(int sIdH)  //Cat_Controlador_Accion : 54
        {
            if (Session["Id_Usuario"] != null)
            {
                return RedirectToAction("NuevoIndice", "IndicePsi", new { sIdH = sIdH });
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult IndiceEvaluacionEditarPsi(int sIdH)
        {
            if (Session["Id_Usuario"] != null)
            {
                return RedirectToAction("EditarIndice", "IndicePsi", new { sIdH = sIdH });
            }
            else
            {
                return RedirectToAction("acceso", "Home");
            }
        }

        public ActionResult js_ActualizaEstatusPsi(int pidhistorico, int popcion)
        {
            if (Session["Id_Usuario"] != null)
            {
                string result = Evaluacion.actualizaStatusPsi(pidhistorico, popcion);

                return Json(result);
            }
            else
            {
                return Json("Sesion Expirada");
            }
        }
    }
}