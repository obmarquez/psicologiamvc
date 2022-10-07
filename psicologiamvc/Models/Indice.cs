using System;
using System.Collections.Generic;
using System.Data;
using psicologiamvc.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace psicologiamvc.Models
{
    public class Indice
    {
        public Int32 idhistorico { get; set; }
        public String curp_evaluado { get; set; }
        public String clave_psicologo { get; set; }
        public Int32 nIntegracion { get; set; }
        public Int32 nSolicitud { get; set; }
        public Int32 nFicha { get; set; }
        public Int32 nFichapermanencia { get; set; }
        public Int32 nFichaloc { get; set; }
        public Int32 nAutorizacion { get; set; }
        public Int32 nComentarios { get; set; }
        public Int32 nNoconcluyo { get; set; }
        public Int32 nExamen { get; set; }
        public Int32 nEntrevista { get; set; }
        public Int32 nPruebas { get; set; }
        public Int32 nCambio { get; set; }
        public Int32 nWais { get; set; }
        public Int32 nTerman { get; set; }
        public Int32 nBeta { get; set; }
        public Int32 nRaven { get; set; }
        public Int32 nMmpii { get; set; }
        public Int32 n16pf { get; set; }
        public Int32 nAutobiografia { get; set; }
        public Int32 nMachover { get; set; }
        public Int32 nLluvia { get; set; }
        public Int32 nTat { get; set; }
        public Int32 nCleaver { get; set; }
        public Int32 nBender { get; set; }
        public Int32 nGrupo { get; set; }
        public Int32 nLusher { get; set; }
        public String cOtro { get; set; }
        public Int32 nOtro { get; set; }
        public Int32 nBarsit { get; set; }
        public Int32 nBeta3 { get; set; }
        public Int32 nHtp { get; set; }
        public Int32 nSacks { get; set; }

        public static string agregarIndice(int idhistorico, string curp_evaluado, string clave_psicologo, int nIntegracion, int nSolicitud, int nFicha, int nFichapermanencia, int nFichaloc, int nAutorizacion, int nComentarios, int nNoconcluyo, int nExamen, int nEntrevista, int nPruebas, int nCambio, int nWais, int nTerman, int nBeta, int nRaven, int nMmpii, int n16pf, int nAutobiografia, int nMachover, int nLluvia, int nTat, int nCleaver, int nBender, int nGrupo, int nLusher, String cOtro, int nOtro, int nBarsit, int nBeta3, int nHtp, int nSacks)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = idhistorico });
                lparametros.Add(new parametros { parametro = "@curp_evaluado", tipodato = "string", valorCadena = curp_evaluado });
                lparametros.Add(new parametros { parametro = "@clave_psicologo", tipodato = "string", valorCadena = clave_psicologo });
                lparametros.Add(new parametros { parametro = "@nIntegracion", tipodato = "int", valorEntero = nIntegracion });
                lparametros.Add(new parametros { parametro = "@nSolicitud", tipodato = "int", valorEntero = nSolicitud });
                lparametros.Add(new parametros { parametro = "@nFicha", tipodato = "int", valorEntero = nFicha });
                lparametros.Add(new parametros { parametro = "@nFichapermanencia", tipodato = "int", valorEntero = nFichapermanencia });
                lparametros.Add(new parametros { parametro = "@nFichaloc", tipodato = "int", valorEntero = nFichaloc });
                lparametros.Add(new parametros { parametro = "@nAutorizacion", tipodato = "int", valorEntero = nAutorizacion });
                lparametros.Add(new parametros { parametro = "@nComentarios", tipodato = "int", valorEntero = nComentarios });
                lparametros.Add(new parametros { parametro = "@nNoconcluyo", tipodato = "int", valorEntero = nNoconcluyo });
                lparametros.Add(new parametros { parametro = "@nExamen", tipodato = "int", valorEntero = nExamen });
                lparametros.Add(new parametros { parametro = "@nEntrevista", tipodato = "int", valorEntero = nEntrevista });
                lparametros.Add(new parametros { parametro = "@nPruebas", tipodato = "int", valorEntero = nPruebas });
                lparametros.Add(new parametros { parametro = "@nCambio", tipodato = "int", valorEntero = nCambio });
                lparametros.Add(new parametros { parametro = "@nWais", tipodato = "int", valorEntero = nWais });
                lparametros.Add(new parametros { parametro = "@nTerman", tipodato = "int", valorEntero = nTerman });
                lparametros.Add(new parametros { parametro = "@nBeta", tipodato = "int", valorEntero = nBeta });
                lparametros.Add(new parametros { parametro = "@nRaven", tipodato = "int", valorEntero = nRaven });
                lparametros.Add(new parametros { parametro = "@nMmpii", tipodato = "int", valorEntero = nMmpii });
                lparametros.Add(new parametros { parametro = "@n16pf", tipodato = "int", valorEntero = n16pf });
                lparametros.Add(new parametros { parametro = "@nAutobiografia", tipodato = "int", valorEntero = nAutobiografia });
                lparametros.Add(new parametros { parametro = "@nMachover", tipodato = "int", valorEntero = nMachover });
                lparametros.Add(new parametros { parametro = "@nLluvia", tipodato = "int", valorEntero = nLluvia });
                lparametros.Add(new parametros { parametro = "@nTat", tipodato = "int", valorEntero = nTat });
                lparametros.Add(new parametros { parametro = "@nCleaver", tipodato = "int", valorEntero = nCleaver });
                lparametros.Add(new parametros { parametro = "@nBender", tipodato = "int", valorEntero = nBender });
                lparametros.Add(new parametros { parametro = "@nGrupo", tipodato = "int", valorEntero = nGrupo });
                lparametros.Add(new parametros { parametro = "@nLusher", tipodato = "int", valorEntero = nLusher });
                lparametros.Add(new parametros { parametro = "@cOtro", tipodato = "string", valorCadena = cOtro });
                lparametros.Add(new parametros { parametro = "@nOtro", tipodato = "int", valorEntero = nOtro });
                lparametros.Add(new parametros { parametro = "@nBarsit", tipodato = "int", valorEntero = nBarsit });
                lparametros.Add(new parametros { parametro = "@nBeta3", tipodato = "int", valorEntero = nBeta3 });
                lparametros.Add(new parametros { parametro = "@nHtp", tipodato = "int", valorEntero = nHtp });
                lparametros.Add(new parametros { parametro = "@nSacks", tipodato = "int", valorEntero = nSacks });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_InsertatPsicologia03", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static string actualizarIndice(int idhistorico, String curp_evaluado, String clave_psicologo, int nIntegracion, int nSolicitud, int nFicha, int	nFichapermanencia, int nFichaloc, int nAutorizacion, int nComentarios, int nNoconcluyo, int nExamen, int nEntrevista, int nPruebas, int nCambio, int nWais, int nTerman, int nBeta, int nRaven, int nMmpii, int n16pf, int nAutobiografia, int nMachover, int nLluvia, int nTat, int nCleaver, int nBender, int nGrupo, int nLusher, String cOtro, int nOtro, int nBarsit, int nBeta3, int nHtp, int nSacks)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = idhistorico });
                lparametros.Add(new parametros { parametro = "@curp_evaluado", tipodato = "string", valorCadena = curp_evaluado });
                lparametros.Add(new parametros { parametro = "@clave_psicologo", tipodato = "string", valorCadena = clave_psicologo });
                lparametros.Add(new parametros { parametro = "@nIntegracion", tipodato = "int", valorEntero = nIntegracion });
                lparametros.Add(new parametros { parametro = "@nSolicitud", tipodato = "int", valorEntero = nSolicitud });
                lparametros.Add(new parametros { parametro = "@nFicha", tipodato = "int", valorEntero = nFicha });
                lparametros.Add(new parametros { parametro = "@nFichapermanencia", tipodato = "int", valorEntero = nFichapermanencia });
                lparametros.Add(new parametros { parametro = "@nFichaloc", tipodato = "int", valorEntero = nFichaloc });
                lparametros.Add(new parametros { parametro = "@nAutorizacion", tipodato = "int", valorEntero = nAutorizacion });
                lparametros.Add(new parametros { parametro = "@nComentarios", tipodato = "int", valorEntero = nComentarios });
                lparametros.Add(new parametros { parametro = "@nNoconcluyo", tipodato = "int", valorEntero = nNoconcluyo });
                lparametros.Add(new parametros { parametro = "@nExamen", tipodato = "int", valorEntero = nExamen });
                lparametros.Add(new parametros { parametro = "@nEntrevista", tipodato = "int", valorEntero = nEntrevista });
                lparametros.Add(new parametros { parametro = "@nPruebas", tipodato = "int", valorEntero = nPruebas });
                lparametros.Add(new parametros { parametro = "@nCambio", tipodato = "int", valorEntero = nCambio });
                lparametros.Add(new parametros { parametro = "@nWais", tipodato = "int", valorEntero = nWais });
                lparametros.Add(new parametros { parametro = "@nTerman", tipodato = "int", valorEntero = nTerman });
                lparametros.Add(new parametros { parametro = "@nBeta", tipodato = "int", valorEntero = nBeta });
                lparametros.Add(new parametros { parametro = "@nRaven", tipodato = "int", valorEntero = nRaven });
                lparametros.Add(new parametros { parametro = "@nMmpii", tipodato = "int", valorEntero = nMmpii });
                lparametros.Add(new parametros { parametro = "@n16pf", tipodato = "int", valorEntero = n16pf });
                lparametros.Add(new parametros { parametro = "@nAutobiografia", tipodato = "int", valorEntero = nAutobiografia });
                lparametros.Add(new parametros { parametro = "@nMachover", tipodato = "int", valorEntero = nMachover });
                lparametros.Add(new parametros { parametro = "@nLluvia", tipodato = "int", valorEntero = nLluvia });
                lparametros.Add(new parametros { parametro = "@nTat", tipodato = "int", valorEntero = nTat });
                lparametros.Add(new parametros { parametro = "@nCleaver", tipodato = "int", valorEntero = nCleaver });
                lparametros.Add(new parametros { parametro = "@nBender", tipodato = "int", valorEntero = nBender });
                lparametros.Add(new parametros { parametro = "@nGrupo", tipodato = "int", valorEntero = nGrupo });
                lparametros.Add(new parametros { parametro = "@nLusher", tipodato = "int", valorEntero = nLusher });
                lparametros.Add(new parametros { parametro = "@cOtro", tipodato = "string", valorCadena = cOtro });
                lparametros.Add(new parametros { parametro = "@nOtro", tipodato = "int", valorEntero = nOtro });
                lparametros.Add(new parametros { parametro = "@nBarsit", tipodato = "int", valorEntero = nBarsit });
                lparametros.Add(new parametros { parametro = "@nBeta3", tipodato = "int", valorEntero = nBeta3 });
                lparametros.Add(new parametros { parametro = "@nHtp", tipodato = "int", valorEntero = nHtp });
                lparametros.Add(new parametros { parametro = "@nSacks", tipodato = "int", valorEntero = nSacks });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_ActualizatPsicologia03", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Indice> getIndice(string curp, int idevaluacion)
        {
            List<Indice> listaIndice = new List<Indice>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Indice>(cnn, "sp_psicologia_obtiene_indice", new { @curp = curp, @idevaluacion = idevaluacion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaIndice = (List<Indice>)returnDapper;

            return listaIndice;
        }

        public static string add_upd_protocolos(int idhistorico, string curp_evaluado, string clave_psicologo, bool bAbordaje, bool bRegistroentrevista, bool bAclaracionhipotesis, bool bCongruenciadiagnostico, 
            bool bRedaccionclara, bool bRespondemotivo, bool bPresenciaobservado, string cObservacion)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = idhistorico });
                lparametros.Add(new parametros { parametro = "@curp_evaluado", tipodato = "string", valorCadena = curp_evaluado });
                lparametros.Add(new parametros { parametro = "@bAbordaje", tipodato = "boleano", valorBoleano = bAbordaje });
                lparametros.Add(new parametros { parametro = "@bRegistroentrevista", tipodato = "boleano", valorBoleano = bRegistroentrevista });
                lparametros.Add(new parametros { parametro = "@bAclaracionhipotesis", tipodato = "boleano", valorBoleano = bAclaracionhipotesis });
                lparametros.Add(new parametros { parametro = "@bCongruenciadiagnostico", tipodato = "boleano", valorBoleano = bCongruenciadiagnostico });
                lparametros.Add(new parametros { parametro = "@bRedaccionclara", tipodato = "boleano", valorBoleano = bRedaccionclara });
                lparametros.Add(new parametros { parametro = "@bRespondemotivo", tipodato = "boleano", valorBoleano = bRespondemotivo });
                lparametros.Add(new parametros { parametro = "@bPresenciaobservado", tipodato = "boleano", valorBoleano = bPresenciaobservado });
                lparametros.Add(new parametros { parametro = "@cObservacion", tipodato = "string", valorCadena = cObservacion });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_agrega_actualiza_protocolos", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
                
    }
}