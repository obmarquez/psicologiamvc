using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace psicologiamvc.Models
{
    public class RepIndice
    {
        public int idhistorico { get; set; }
        public string codigoevaluado {get; set; }
        public string evaluado { get; set; }
        public string puesto { get; set; }
        public string fEvalpsi { get; set; }
        public string desc_dependencia { get; set; }
		public int nIntegracion { get; set; }
        public int nSolicitud { get; set; }
        public int nFicha { get; set; }
        public int nFichapermanencia { get; set; }
        public int nFichaloc { get; set; }
        public int nAutorizacion { get; set; }
        public int nComentarios { get; set; }
        public int nNoconcluyo { get; set; }
        public int nExamen { get; set; }
        public int nEntrevista { get; set; }
        public int nPruebas { get; set; }
        public int nCambio { get; set; }
        public int nWais { get; set; }
        public int nTerman { get; set; }
        public int nBeta { get; set; }
        public int nRaven { get; set; }
        public int nBeta3 { get; set; }
        public int nMmpii { get; set; }
        public int n16pf { get; set; }
        public int nLusher { get; set; }
        public int nBarsit { get; set; }
        public int nAutobiografia { get; set; }
        public int nMachover { get; set; }
        public int nLluvia { get; set; }
        public int nTat { get; set; }
        public int nHtp { get; set; }
        public int nSacks { get; set; }
        public int nCleaver { get; set; }
        public int nBender { get; set; }
        public int nGrupo { get; set; }
        public int nOtro { get; set; }
        public string clave_psicologo { get; set; }
        public string clave_psicologorevisor { get; set; }
        public string SerialElement { get; set; }
        public int nMoca { get; set; }
        public int nMmp2rf { get; set; }
        public string cOtro { get; set; }

        public static List<RepIndice> getRepIndice(int idhistorico)
        {
            List<RepIndice> repElIndice = new List<RepIndice>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<RepIndice>(cnn, "sp_psicologia_get_rep_indice", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            repElIndice = (List<RepIndice>)returnDapper;

            return repElIndice;
        }
    }
}