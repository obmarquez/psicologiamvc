using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Entrega
    {
        public int idhistorico { get; set; }
        public string curp { get; set; }
        public int idevaluacion { get; set; }
        public DateTime fecha { get; set; }
        public string cmemo { get; set; }
        public string cPara { get; set; }
        public string cDe { get; set; }
        public int nDireccion { get; set; }
        public int idevaluacion_poligrafica { get; set; }
        public string evaluacion { get; set; }
        public int estatus { get; set; }
        public string usuario { get; set; }
        public string evaluado { get; set; }
        public string serie { get; set; }
        public string codigo { get; set; }
        public string dependencia { get; set; }


        //obteniendo el siguiente numero
        public static List<Entrega> getSiguienteMemo(int area)
        {
            List<Entrega> listaE = new List<Entrega>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Entrega>(cnn, "sp_general_obtner_numero_memo", new { @area = area }, commandType: CommandType.StoredProcedure);
            
            BDConn.CierraConexion(cnn);

            listaE = (List<Entrega>)returnDapper;

            return listaE;
        }

        //obteniendo los evaluados para su entrega a Custodia
        public static List<Entrega> getEvaluadosACustodia(int area)
        {
            List<Entrega> listaAC = new List<Entrega>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Entrega>(cnn, "sp_general_obtener_evaluados_a_entregar", new { @area = area }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaAC = (List<Entrega>)returnDapper;

            return listaAC;
        }

        //insertando en la tabla de pEntrega
        public static string agregaEntrega(int p_idhistorico, int p_direccion, string p_cmemo, int p_idevalpol)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = p_idhistorico });
                lparametros.Add(new parametros { parametro = "@direccion", tipodato = "int", valorEntero = p_direccion });
                lparametros.Add(new parametros { parametro = "@cmemo", tipodato = "string", valorCadena = p_cmemo });
                lparametros.Add(new parametros { parametro = "@idevalpol", tipodato = "int", valorEntero = p_idevalpol });

                result = BDConn.EjecutarStoreProc_string("sp_general_entregaCustodia", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        //OBTENIENDO el concentrado de memos
        public static List<Entrega> getConcentadoMemos(int direccion)
        {
            List<Entrega> listaConcentradoMemo = new List<Entrega>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returDapper = Dapper.SqlMapper.Query<Entrega>(cnn, "sp_general_concentrado_memos_direccion", new { @direccion = direccion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaConcentradoMemo = (List<Entrega>)returDapper;

            return listaConcentradoMemo;
        }

        public static List<Entrega> getDetalladoMemoCuerpo(string cmemo)
        {
            List<Entrega> listaDetalladaMemo = new List<Entrega>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Entrega>(cnn, "sp_general_memo_detalle_cuerpo", new { @memorandum = cmemo }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaDetalladaMemo = (List<Entrega>)returnDapper;
            return listaDetalladaMemo;
        }

        public static List<Entrega> getDetalladaoMaestroCabecera(string cmemo)
        {
            List<Entrega> listaDetalladaMemo = new List<Entrega>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Entrega>(cnn, "sp_general_memo_detalle_cabecera", new { @cmemo = cmemo }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaDetalladaMemo = (List<Entrega>)returnDapper;
            return listaDetalladaMemo;
        }
    }
}