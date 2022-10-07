using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace psicologiamvc.Models
{
    public class Estatus
    {
        public int idhistorico { get; set; }
        public string evaluado { get; set; }
        public string curp { get; set; }
        public int idevaluacion { get; set; }
        public string fecha_alta { get; set; }
        public int estatuspsi { get; set; }
        public string idpsi { get; set; }
        public string fentregaPsi { get; set; }
        public int direccion { get; set; }

        public static List<Estatus> getListaEstatusDifCincoPsicologia(int p_IdH)
        {
            List<Estatus> lista = new List<Estatus>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Estatus>(cnn, "sp_psicologia_estatus_regresados", new { @Id_His = p_IdH }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Estatus>)returnDapper;
            return lista;
        }

        public static string updateEstatusSoc(int p_idhistorico, int p_direccion)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = p_idhistorico });
                lparametros.Add(new parametros { parametro = "@direccion", tipodato = "int", valorEntero = p_direccion });

                result = BDConn.EjecutarStoreProc_string("sp_admin_upd_estatus", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}