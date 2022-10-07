using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace psicologiamvc.Models
{
    public class Evaluacion
    {
        public Int32 idhistorico { get; set; }
        public string curp_evaluado { get; set; }
        public string cBender { get; set; }
        public string cDescdano { get; set; }
        public string cDiagnostico { get; set; }
        public bool bMachover { get; set; }
        public bool bFigura { get; set; }
        public bool bMerril { get; set; }
        public bool bMmpi { get; set; }
        public bool bAutobiografia { get; set; }
        public string cCi { get; set; }
        public string cLaboralpsi { get; set; }
        //public string cFortalezas { get; set; }
        //public string cRiesgos { get; set; }
        public string cRecomendaciones { get; set; }
        public string cResultado { get; set; }
        public bool bConcluido { get; set; }
        public DateTime fecha_evaluacion { get; set; }
        public string clave_psicologo { get; set; }
        public DateTime fecha_modifica { get; set; }
        public string clave_psicologo_mod { get; set; }
        public string cLugarnac { get; set; }
        public string cGrado { get; set; }
        public string cEscolaridad { get; set; }
        public bool bCleaver { get; set; }
        public bool bTest { get; set; }
        public bool bRaven { get; set; }
        //public bool bBeta { get; set; }
        public DateTime fEvalpsi { get; set; }
        //public bool bLuscher { get; set; }
        public string idSupPsi { get; set; }
        public string cmbJuicio2 { get; set; }
        //public string cmbControlimpulsos { get; set; }
        //public string cmbApegonormas { get; set; }
        //public string cmbToleranciapresion { get; set; }
        //public string cmbEstabilidademocional { get; set; }
        //public string cmbAlteraciones { get; set; }
        //public string cmbCompetencias { get; set; }
        public bool bHtp { get; set; }
        public bool bSacks { get; set; }
        //public bool b16fp { get; set; }
        public bool bBetaiiifr { get; set; }
        //public string cAutoestima { get; set; }
        //public string cEmociones { get; set; }
        //public string cRelaciones { get; set; }
        public string cCompetencia { get; set; }
        public bool bLiderazgo { get; set; }        // Juiicio                                          Criterio 1
        public bool bPlaneacion { get; set; }       // Autoestima                                       Criterio 2
        public bool bDecisiones { get; set; }       // Manejo de emociones                              Criterio 3
        public bool bconflictos { get; set; }       // Alteraciones en la percepcion y el pensamiento   Criterio 4
        public bool bAtencion { get; set; }         // Apego a normal                                   Criterio A
        public bool bAdaptabilidad { get; set; }    // Tolerancia al estres                             Criterio B
        public bool bVocacion { get; set; }         // Relaciones interpersonales                       Criterio C
        //public bool bCapacidad { get; set; }
        //public bool bObservaciones { get; set; }
        //public bool bAnalisis { get; set; }
        //public bool bHabilidades { get; set; }
        //public bool bTrabajo { get; set; }
        //public bool bOrientacion { get; set; }
        //public bool bIntegridad { get; set; }
        public string fortaleza_riesgo { get; set; }
        public string notas { get; set; }
        public bool bJuicio { get; set; }
        public bool bAutoestima { get; set; }
        public bool bManejo { get; set; }
        public bool bAlteraciones { get; set; }
        public bool bApego { get; set; }
        public bool bTolerancia { get; set; }
        public bool bRelaciones { get; set; }
        public bool bMoca { get; set; }
        public bool bMmpi2rf { get; set; }

        public byte[] Picture { get; set; }
        public byte[] tatuaje { get; set; }
        public int id { get; set; }
        public string fecha { get; set; }
        public string cDescripcion { get; set; }

        [NotMapped]
        public int accion { get; set; }

        public static string agregaActualizaInvestigacionPsicologica(int p_idh, string p_cDiagnostico, bool p_bMachover, bool p_bFigura, bool p_bMerril, bool p_bMmpi, bool p_bAutobiografia, string p_cCi, string p_cLaboralpsi, string p_cRecomendaciones, string p_cResultado, bool p_bConcluido, string p_clave_psicologo, string p_cLugarnac, string p_cGrado, string p_cEscolaridad, bool p_bCleaver, bool p_bTest, bool p_bRaven, DateTime p_fEvalpsi, string p_idSupPsi, bool p_bHtp, bool p_bSacks, bool p_bBetaiiifr, string p_cCompetencia, bool p_bJuicio, bool p_bAutoestima, bool p_bManejo, bool p_bAlteraciones, bool p_bApego, bool p_bTolerancia, bool p_bRelaciones, string p_fortaleza_riesgo, string p_notas, int p_accion, bool p_bMoca, bool p_bMmpi2rf)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idh", tipodato = "int", valorEntero = p_idh });
                //lparametros.Add(new parametros { parametro = "@cBender", tipodato = "string", valorCadena = p_cBender });
                //lparametros.Add(new parametros { parametro = "@cDescdano", tipodato = "string", valorCadena = p_cDescdano });
                lparametros.Add(new parametros { parametro = "@cDiagnostico", tipodato = "string", valorCadena = p_cDiagnostico });
                lparametros.Add(new parametros { parametro = "@bMachover", tipodato = "boleano", valorBoleano = p_bMachover });
                lparametros.Add(new parametros { parametro = "@bFigura", tipodato = "boleano", valorBoleano = p_bFigura });
                lparametros.Add(new parametros { parametro = "@bMerril", tipodato = "boleano", valorBoleano = p_bMerril });
                lparametros.Add(new parametros { parametro = "@bMmpi", tipodato = "boleano", valorBoleano = p_bMmpi });
                lparametros.Add(new parametros { parametro = "@bAutobiografia", tipodato = "boleano", valorBoleano = p_bAutobiografia });
                lparametros.Add(new parametros { parametro = "@cCi", tipodato = "string", valorCadena = p_cCi });
                lparametros.Add(new parametros { parametro = "@cLaboralpsi", tipodato = "string", valorCadena = p_cLaboralpsi });
                lparametros.Add(new parametros { parametro = "@cRecomendaciones", tipodato = "string", valorCadena = p_cRecomendaciones });
                lparametros.Add(new parametros { parametro = "@cResultado", tipodato = "string", valorCadena = p_cResultado });
                lparametros.Add(new parametros { parametro = "@bConcluido", tipodato = "boleano", valorBoleano = p_bConcluido });
                lparametros.Add(new parametros { parametro = "@clave_psicologo", tipodato = "string", valorCadena = p_clave_psicologo });
                lparametros.Add(new parametros { parametro = "@cLugarnac", tipodato = "string", valorCadena = p_cLugarnac });
                lparametros.Add(new parametros { parametro = "@cGrado", tipodato = "string", valorCadena = p_cGrado });
                lparametros.Add(new parametros { parametro = "@cEscolaridad", tipodato = "string", valorCadena = p_cEscolaridad });
                lparametros.Add(new parametros { parametro = "@bCleaver", tipodato = "boleano", valorBoleano = p_bCleaver });
                lparametros.Add(new parametros { parametro = "@bTest", tipodato = "boleano", valorBoleano = p_bTest });
                lparametros.Add(new parametros { parametro = "@bRaven", tipodato = "boleano", valorBoleano = p_bRaven });
                //lparametros.Add(new parametros { parametro = "@bBeta", tipodato = "boleano", valorBoleano = p_bBeta });
                lparametros.Add(new parametros { parametro = "@fEvalpsi", tipodato = "date", valorFecha = p_fEvalpsi });
                //lparametros.Add(new parametros { parametro = "@bLuscher", tipodato = "boleano", valorBoleano = p_bLuscher });
                lparametros.Add(new parametros { parametro = "@idSupPsi", tipodato = "string", valorCadena = p_idSupPsi });
                lparametros.Add(new parametros { parametro = "@bHtp", tipodato = "boleano", valorBoleano = p_bHtp });
                lparametros.Add(new parametros { parametro = "@bSacks", tipodato = "boleano", valorBoleano = p_bSacks });
                //lparametros.Add(new parametros { parametro = "@b16fp", tipodato = "boleano", valorBoleano = p_b16fp });
                lparametros.Add(new parametros { parametro = "@bBetaiiifr", tipodato = "boleano", valorBoleano = p_bBetaiiifr });
                lparametros.Add(new parametros { parametro = "@cCompetencia", tipodato = "string", valorCadena = p_cCompetencia });
                lparametros.Add(new parametros { parametro = "@bJuicio", tipodato = "boleano", valorBoleano = p_bJuicio });
                lparametros.Add(new parametros { parametro = "@bAutoestima", tipodato = "boleano", valorBoleano = p_bAutoestima });
                lparametros.Add(new parametros { parametro = "@bManejo", tipodato = "boleano", valorBoleano = p_bManejo });
                lparametros.Add(new parametros { parametro = "@bAlteraciones", tipodato = "boleano", valorBoleano = p_bAlteraciones });
                lparametros.Add(new parametros { parametro = "@bApego", tipodato = "boleano", valorBoleano = p_bApego });
                lparametros.Add(new parametros { parametro = "@bTolerancia", tipodato = "boleano", valorBoleano = p_bTolerancia });
                lparametros.Add(new parametros { parametro = "@bRelaciones", tipodato = "boleano", valorBoleano = p_bRelaciones });
                lparametros.Add(new parametros { parametro = "@fortaleza_riesgo", tipodato = "string", valorCadena = p_fortaleza_riesgo });
                lparametros.Add(new parametros { parametro = "@notas", tipodato = "string", valorCadena = p_notas });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = p_accion });
                lparametros.Add(new parametros { parametro = "@bMoca", tipodato = "boleano", valorBoleano = p_bMoca });
                lparametros.Add(new parametros { parametro = "@bMmpi2rf", tipodato = "boleano", valorBoleano = p_bMmpi2rf });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_agrega_actualiza_investigacion", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        public static string actualizaPsicologia(int pidhistorico, string pcurp_evaluado, string pcDiagnostico, bool pbMachover, bool pbFigura, bool pbMerril, bool pbMmpi, bool pbAutobiografia, string pcCi, string pclave_psicologo_mod, string pcLaboralpsi, string pcFortalezas, string pcRiesgos, string pcRecomendaciones, string pcResultado, bool pbConcluido, string pcLugarnac, string pcGrado, string pcEscolaridad, bool pbCleaver, bool pbTest, bool pbRaven, DateTime pfEvalpsi, string pcSupervisor, string pcmbJuicio2, string pcmbControlimpulsos, string pcmbApegonormas, string pcmbToleranciapresion, string pcmbEstabilidademocional, string pcmbAlteraciones, string pcmbCompetencias, bool pbHtp, bool pbSacks, bool pbBetaiiifr, string pcAutoestima, string pcEmociones, string pcRelaciones, string pcCompetencia, bool pbLiderazgo, bool pbPlaneacion, bool pbDecisiones, bool pbconflictos, bool pbAtencion, bool pbAdaptabilidad, bool pbVocacion, bool pbCapacidad, bool pbObservaciones, bool pbAnalisis, bool pbHabilidades, bool pbTrabajo, bool pbOrientacion, bool pbIntegridad)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = pidhistorico });
                lparametros.Add(new parametros { parametro = "@curp_evaluado", tipodato = "string", valorCadena = pcurp_evaluado });
                //lparametros.Add(new parametros { parametro = "@cBender", tipodato = "string", valorCadena = pcBender });
                //lparametros.Add(new parametros { parametro = "@cDescdano", tipodato = "string", valorCadena = pcDescdano });
                lparametros.Add(new parametros { parametro = "@cDiagnostico", tipodato = "string", valorCadena = pcDiagnostico });
                lparametros.Add(new parametros { parametro = "@bMachover", tipodato = "boleano", valorBoleano = pbMachover });
                lparametros.Add(new parametros { parametro = "@bFigura", tipodato = "boleano", valorBoleano = pbFigura });
                lparametros.Add(new parametros { parametro = "@bMerril", tipodato = "boleano", valorBoleano = pbMerril });
                lparametros.Add(new parametros { parametro = "@bMmpi", tipodato = "boleano", valorBoleano = pbMmpi });
                lparametros.Add(new parametros { parametro = "@bAutobiografia", tipodato = "boleano", valorBoleano = pbAutobiografia });
                lparametros.Add(new parametros { parametro = "@cCi", tipodato = "string", valorCadena = pcCi });
                lparametros.Add(new parametros { parametro = "@clave_psicologo_mod", tipodato = "string", valorCadena = pclave_psicologo_mod });//----psicologo que modifica verificar
                lparametros.Add(new parametros { parametro = "@cLaboralpsi", tipodato = "string", valorCadena = pcLaboralpsi });
                lparametros.Add(new parametros { parametro = "@cFortalezas", tipodato = "string", valorCadena = pcFortalezas });
                lparametros.Add(new parametros { parametro = "@cRiesgos", tipodato = "string", valorCadena = pcRiesgos });
                lparametros.Add(new parametros { parametro = "@cRecomendaciones", tipodato = "string", valorCadena = pcRecomendaciones });
                lparametros.Add(new parametros { parametro = "@cResultado", tipodato = "string", valorCadena = pcResultado });
                lparametros.Add(new parametros { parametro = "@bConcluido", tipodato = "boleano", valorBoleano = pbConcluido });
                lparametros.Add(new parametros { parametro = "@cLugarnac", tipodato = "string", valorCadena = pcLugarnac });
                lparametros.Add(new parametros { parametro = "@cGrado", tipodato = "string", valorCadena = pcGrado });
                lparametros.Add(new parametros { parametro = "@cEscolaridad", tipodato = "string", valorCadena = pcEscolaridad });
                lparametros.Add(new parametros { parametro = "@bCleaver", tipodato = "boleano", valorBoleano = pbCleaver });
                lparametros.Add(new parametros { parametro = "@bTest", tipodato = "boleano", valorBoleano = pbTest });
                lparametros.Add(new parametros { parametro = "@bRaven", tipodato = "boleano", valorBoleano = pbRaven });
                //lparametros.Add(new parametros { parametro = "@bBeta", tipodato = "boleano", valorBoleano = pbBeta });
                lparametros.Add(new parametros { parametro = "fEvalpsi", tipodato = "date", valorFecha = pfEvalpsi });
                //lparametros.Add(new parametros { parametro = "@bLuscher", tipodato = "boleano", valorBoleano = pbLuscher });
                lparametros.Add(new parametros { parametro = "@cSupervisor", tipodato = "string", valorCadena = pcSupervisor });//----------------clave del supervisor
                lparametros.Add(new parametros { parametro = "@cmbJuicio2", tipodato = "string", valorCadena = pcmbJuicio2 });
                lparametros.Add(new parametros { parametro = "@cmbControlimpulsos", tipodato = "string", valorCadena = pcmbControlimpulsos });
                lparametros.Add(new parametros { parametro = "@cmbApegonormas", tipodato = "string", valorCadena = pcmbApegonormas });
                lparametros.Add(new parametros { parametro = "@cmbToleranciapresion", tipodato = "string", valorCadena = pcmbToleranciapresion });
                lparametros.Add(new parametros { parametro = "@cmbEstabilidademocional", tipodato = "string", valorCadena = pcmbEstabilidademocional });
                lparametros.Add(new parametros { parametro = "@cmbAlteraciones", tipodato = "string", valorCadena = pcmbAlteraciones });
                lparametros.Add(new parametros { parametro = "@cmbCompetencias", tipodato = "string", valorCadena = pcmbCompetencias });
                lparametros.Add(new parametros { parametro = "@bHtp", tipodato = "boleano", valorBoleano = pbHtp });
                lparametros.Add(new parametros { parametro = "@bSacks", tipodato = "boleano", valorBoleano = pbSacks });
                //lparametros.Add(new parametros { parametro = "@b16fp", tipodato = "boleano", valorBoleano = pb16fp });
                lparametros.Add(new parametros { parametro = "@bBetaiiifr", tipodato = "boleano", valorBoleano = pbBetaiiifr });
                lparametros.Add(new parametros { parametro = "@cAutoestima", tipodato = "string", valorCadena = pcAutoestima });
                lparametros.Add(new parametros { parametro = "@cEmociones", tipodato = "string", valorCadena = pcEmociones });
                lparametros.Add(new parametros { parametro = "@cRelaciones", tipodato = "string", valorCadena = pcRelaciones });
                lparametros.Add(new parametros { parametro = "@cCompetencia", tipodato = "string", valorCadena = pcCompetencia });
                lparametros.Add(new parametros { parametro = "@bLiderazgo", tipodato = "boleano", valorBoleano = pbLiderazgo });
                lparametros.Add(new parametros { parametro = "@bPlaneacion", tipodato = "boleano", valorBoleano = pbPlaneacion });
                lparametros.Add(new parametros { parametro = "@bDecisiones", tipodato = "boleano", valorBoleano = pbDecisiones });
                lparametros.Add(new parametros { parametro = "@bconflictos", tipodato = "boleano", valorBoleano = pbconflictos });
                lparametros.Add(new parametros { parametro = "@bAtencion", tipodato = "boleano", valorBoleano = pbAtencion });
                lparametros.Add(new parametros { parametro = "@bAdaptabilidad", tipodato = "boleano", valorBoleano = pbAdaptabilidad });
                lparametros.Add(new parametros { parametro = "@bVocacion", tipodato = "boleano", valorBoleano = pbVocacion });
                lparametros.Add(new parametros { parametro = "@bCapacidad", tipodato = "boleano", valorBoleano = pbCapacidad });
                lparametros.Add(new parametros { parametro = "@bObservaciones", tipodato = "boleano", valorBoleano = pbObservaciones });
                lparametros.Add(new parametros { parametro = "@bAnalisis", tipodato = "boleano", valorBoleano = pbAnalisis });
                lparametros.Add(new parametros { parametro = "@bHabilidades", tipodato = "boleano", valorBoleano = pbHabilidades });
                lparametros.Add(new parametros { parametro = "@bTrabajo", tipodato = "boleano", valorBoleano = pbTrabajo });
                lparametros.Add(new parametros { parametro = "@bOrientacion", tipodato = "boleano", valorBoleano = pbOrientacion });
                lparametros.Add(new parametros { parametro = "@bIntegridad", tipodato = "boleano", valorBoleano = pbIntegridad });

                result = BDConn.EjecutarStoreProc_string("Actualiza_Psico01", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Evaluacion> getEvaluacion(int sIdH)
        {
            List<Evaluacion> lista = new List<Evaluacion>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Evaluacion>(cnn, "sp_psicologia_seleccionar_evaluacion", new { @idhistorico = sIdH }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Evaluacion>)returnDapper;

            return lista;
        }

        public static List<Evaluacion> getImgEva(int pIdH)
        {
            List<Evaluacion> img = new List<Evaluacion>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Evaluacion>(cnn, "sp_general_obtieneImagenEvaluado", new { @idhistorico = pIdH }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            img = (List<Evaluacion>)returnDapper;
            return img;
        }

        public static List<Evaluacion> getImgTatuaje(int IdH, int id, int opcion)
        {
            List<Evaluacion> imgTatuaje = new List<Evaluacion>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDappe = Dapper.SqlMapper.Query<Evaluacion>(cnn, "sp_general_obtiene_imagen_tatuaje", new { @idh = IdH, @id = id, @opcion = opcion }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            imgTatuaje = (List<Evaluacion>)returnDappe;
            return imgTatuaje;
        }

        public static List<Evaluacion> getReferencias(int IdH)
        {
            List<Evaluacion> referencias = new List<Evaluacion>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDappe = Dapper.SqlMapper.Query<Evaluacion>(cnn, "sp_psicologia_referencias_obtener", new { @idHistorio = IdH }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            referencias = (List<Evaluacion>)returnDappe;
            return referencias;
        }

        //public static Evaluacion getEval(string curp, int ide)
        //{
        //    Evaluacion _evaluacion = new Evaluacion();

        //    IDbConnection cnn = BDConn.AbreConexion();

        //    _evaluacion = Dapper.SqlMapper.Query<Evaluacion>(cnn, "sp_psicologia_seleccionar_evaluacion", new { @curp_evaluado = curp, @idhistorico = ide }, commandType: CommandType.StoredProcedure).SingleOrDefault();

        //    BDConn.CierraConexion(cnn);

        //    return _evaluacion;
        //}

        //actualizacion de los estatus a 3 o 4
        public static string actualizaStatusPsi(int pidhistorico, int popcion)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = pidhistorico });
                lparametros.Add(new parametros { parametro = "@opcion", tipodato = "int", valorEntero = popcion });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_actualiza_estatus", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}