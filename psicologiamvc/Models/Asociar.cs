using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace psicologiamvc.Models
{
    public class Asociar
    {
        public int idh { get; set; }
        public string idevaluador { get; set; }
        public int depa { get; set; }

        [NotMapped]
        public bool reex { get; set; }
        public string alta { get; set; }

        public static string asociarPsi(int idh, string idEvaluador, int depa, bool reex, int idevalpol, string idSupervisor)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idh });
                lparametros.Add(new parametros { parametro = "@idEvaluador", tipodato = "string", valorCadena = idEvaluador });
                lparametros.Add(new parametros { parametro = "@depa", tipodato = "int", valorEntero = depa });
                lparametros.Add(new parametros { parametro = "@reex", tipodato = "boleano", valorBoleano = reex });
                lparametros.Add(new parametros { parametro = "@idevalpol", tipodato = "int", valorEntero = idevalpol });
                lparametros.Add(new parametros { parametro = "@idSupervisor", tipodato = "string", valorCadena = idSupervisor });

                result = BDConn.EjecutarStoreProc_string("sp_general_actualiza_asocia", lparametros, true, "@Result");
                //result = BDConn.EjecutarStoreProc_string("Actualiza_asocia", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Asociar> getHistoricoEvaluadoresxIdhistorico(int sIdH)
        {
            List<Asociar> lista = new List<Asociar>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Asociar>(cnn, "sp_psicologia_historico_asociacion", new { @idhistorico = sIdH }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Asociar>)returnDapper;
            return lista;
        }

        public static string desasociar(int idh)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = idh });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_desasociar_evaluado", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
    }
}