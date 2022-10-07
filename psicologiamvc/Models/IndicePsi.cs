using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace psicologiamvc.Models
{
    public class IndicePsi
    {
        public int idh { get; set; }
        public string clave_psicologo { get; set; }
        public int nIntegracion { get; set; }   //Ok
        public int nSolicitud { get; set; } //Ok
        public int nFicha { get; set; }//Ok
        public int nFichapermanencia { get; set; } //Ok
        public int nFichaloc { get; set; } //Ok
        public int nAutorizacion { get; set; } //Ok
        public int nComentarios { get; set; }//Ok
        public int nNoconcluyo { get; set; } //Ok
        public int nExamen { get; set; } //Ok
        public int nEntrevista { get; set; } //Ok
        public int nPruebas { get; set; } //Ok
        public int nCambio { get; set; } //Ok
        public int nWais { get; set; } //Ok
        public int nTerman { get; set; } //Ok
        public int nBeta { get; set; } //Ok
        public int nRaven { get; set; } //Ok
        public int nMmpii { get; set; } //Ok
        public int n16pf { get; set; } //Ok
        public int nAutobiografia { get; set; } //Ok
        public int nMachover { get; set; } //Ok
        public int nLluvia { get; set; } //Ok
        public int nTat { get; set; } //Ok
        public int nCleaver { get; set; } //Ok
        public int nBender { get; set; } //Ok
        public int nGrupo { get; set; } //Ok
        public int nLusher { get; set; } //Ok
        public string cOtro { get; set; } //Ok
        public int nOtro { get; set; } //Ok
        public int nBarsit { get; set; } //Ok
        //@clave_psicologorevisor as char(10)
        public int nBeta3 { get; set; } //Ok
        public int nHtp { get; set; } //Ok
        public int nSacks { get; set; } //Ok
        public int accion { get; set; }
        public int nMoca { get; set; }
        public int nMmp2rf { get; set; } //Ok

        public static string agregaActualizaIndicePsi(int p_idh , string p_clave_psicologo, int p_nIntegracion, int p_nSolicitud, int p_nFicha, int p_nFichapermanencia, int p_nFichaloc, int p_nAutorizacion, int p_nComentarios, int p_nNoconcluyo, int p_nExamen, int p_nEntrevista, int p_nPruebas, int p_nCambio, int p_nWais, int p_nTerman, int p_nBeta, int p_nRaven, int p_nMmpii, int p_n16pf, int p_nAutobiografia, int p_nMachover, int p_nLluvia, int p_nTat, int p_nCleaver, int p_nBender, int p_nGrupo, int p_nLusher, string p_cOtro, int p_nOtro, int p_nBarsit, int p_nBeta3, int p_nHtp, int p_nSacks, int p_accion, int p_nMoca, int p_nMmp2rf)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idh", tipodato = "int", valorEntero = p_idh });
                lparametros.Add(new parametros { parametro = "@clave_psicologo", tipodato = "string", valorCadena = p_clave_psicologo });
                lparametros.Add(new parametros { parametro = "@nIntegracion", tipodato = "int", valorEntero = p_nIntegracion });
                lparametros.Add(new parametros { parametro = "@nSolicitud", tipodato = "int", valorEntero = p_nSolicitud });
                lparametros.Add(new parametros { parametro = "@nFicha", tipodato = "int", valorEntero = p_nFicha });
                lparametros.Add(new parametros { parametro = "@nFichapermanencia", tipodato = "int", valorEntero = p_nFichapermanencia });
                lparametros.Add(new parametros { parametro = "@nFichaloc", tipodato = "int", valorEntero = p_nFichaloc });
                lparametros.Add(new parametros { parametro = "@nAutorizacion", tipodato = "int", valorEntero = p_nAutorizacion });
                lparametros.Add(new parametros { parametro = "@nComentarios", tipodato = "int", valorEntero = p_nComentarios });
                lparametros.Add(new parametros { parametro = "@nNoconcluyo", tipodato = "int", valorEntero = p_nNoconcluyo });
                lparametros.Add(new parametros { parametro = "@nExamen", tipodato = "int", valorEntero = p_nExamen });
                lparametros.Add(new parametros { parametro = "@nEntrevista", tipodato = "int", valorEntero = p_nEntrevista });
                lparametros.Add(new parametros { parametro = "@nPruebas", tipodato = "int", valorEntero = p_nPruebas });
                lparametros.Add(new parametros { parametro = "@nCambio", tipodato = "int", valorEntero = p_nCambio });
                lparametros.Add(new parametros { parametro = "@nWais", tipodato = "int", valorEntero = p_nWais });
                lparametros.Add(new parametros { parametro = "@nTerman", tipodato = "int", valorEntero = p_nTerman });
                lparametros.Add(new parametros { parametro = "@nBeta", tipodato = "int", valorEntero = p_nBeta });
                lparametros.Add(new parametros { parametro = "@nRaven", tipodato = "int", valorEntero = p_nRaven });
                lparametros.Add(new parametros { parametro = "@nMmpii", tipodato = "int", valorEntero = p_nMmpii });
                lparametros.Add(new parametros { parametro = "@n16pf", tipodato = "int", valorEntero = p_n16pf });
                lparametros.Add(new parametros { parametro = "@nAutobiografia", tipodato = "int", valorEntero = p_nAutobiografia });
                lparametros.Add(new parametros { parametro = "@nMachover", tipodato = "int", valorEntero = p_nMachover });
                lparametros.Add(new parametros { parametro = "@nLluvia", tipodato = "int", valorEntero = p_nLluvia });
                lparametros.Add(new parametros { parametro = "@nTat", tipodato = "int", valorEntero = p_nTat });
                lparametros.Add(new parametros { parametro = "@nCleaver", tipodato = "int", valorEntero = p_nCleaver });
                lparametros.Add(new parametros { parametro = "@nBender", tipodato = "int", valorEntero = p_nBender });
                lparametros.Add(new parametros { parametro = "@nGrupo", tipodato = "int", valorEntero = p_nGrupo });
                lparametros.Add(new parametros { parametro = "@nLusher", tipodato = "int", valorEntero = p_nLusher });
                lparametros.Add(new parametros { parametro = "@cOtro", tipodato = "string", valorCadena = p_cOtro });
                lparametros.Add(new parametros { parametro = "@nOtro", tipodato = "int", valorEntero = p_nOtro });
                lparametros.Add(new parametros { parametro = "@nBarsit", tipodato = "int", valorEntero = p_nBarsit });
                lparametros.Add(new parametros { parametro = "@nBeta3", tipodato = "int", valorEntero = p_nBeta3 });
                lparametros.Add(new parametros { parametro = "@nHtp", tipodato = "int", valorEntero = p_nHtp });
                lparametros.Add(new parametros { parametro = "@nSacks", tipodato = "int", valorEntero = p_nSacks });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = p_accion });
                lparametros.Add(new parametros { parametro = "@nMoca", tipodato = "int", valorEntero = p_nMoca });
                lparametros.Add(new parametros { parametro = "@nMmp2rf", tipodato = "int", valorEntero = p_nMmp2rf });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_agrega_actualiza_indice", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        public static List<IndicePsi> getIndice(int sIdH)
        {
            List<IndicePsi> listaIndice = new List<IndicePsi>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<IndicePsi>(cnn, "sp_psicologia_obtiene_indice", new { @idhistorico = sIdH }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaIndice = (List<IndicePsi>)returnDapper;

            return listaIndice;
        }
    }
}