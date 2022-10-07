using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Observaciones
    {
        public Int32 id { get; set; }
        public String curp { get; set; }
        public String idusuario { get; set; }
        public DateTime fecha { get; set; }
        public String observacionpublica { get; set; }
        public String user_responde { get; set; }
        public String recomendacion { get; set; }
        [NotMapped]
        public Int32 tt { get; set; }
        [NotMapped]
        public Int32 sIdH { get; set; }

        //frespuesta smalldatetime
        //user_responde_pol nchar(10)
        //recomienda_pol varchar(200)
        //frespuesta_pol smalldatetime
        //user_responde_soc nchar(10)
        //recomienda_soc varchar(200)
        //frespuesta_soc smalldatetime
        //user_responde_med nchar(10)
        //recomienda_med varchar(200)
        //frespuesta_med smalldatetime
        //idElemento int

        public static List<Observaciones> getPendientesContestar(int sIdH, int area)
        {
            List<Observaciones> lista = new List<Observaciones>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Observaciones>(cnn, "sp_general_observaciones_x_contestar", new { @idHistorico = sIdH, @area = area }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Observaciones>)returnDapper;
            return lista;
        }

        public static string agregarObservacion(String idusuario, Int32 idHistorico, String observacionpublica)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idusuario", tipodato = "string", valorCadena = idusuario });
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idHistorico });
                lparametros.Add(new parametros { parametro = "@observacionpublica", tipodato = "string", valorCadena = observacionpublica });

                result = BDConn.EjecutarStoreProc_string("sp_general_inserta_observacion", lparametros, true, "@Result");  //Si lo hizo bien regresará un "Ok"
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        //public static string agregarObservacion(String idusuario, String curp, String observacionpublica)
        //{
        //    string result = "ERROR";

        //    try
        //    {
        //        List<parametros> lparametros = new List<parametros>();
        //        lparametros.Add(new parametros { parametro = "@idusuario", tipodato = "string", valorCadena = idusuario });
        //        lparametros.Add(new parametros { parametro = "@curp", tipodato = "string", valorCadena = curp });
        //        lparametros.Add(new parametros { parametro = "@observacionpublica", tipodato = "string", valorCadena = observacionpublica });

        //        result = BDConn.EjecutarStoreProc_string("InsertaObserva", lparametros, true, "@Result"); 
        //    }
        //    catch
        //    {
        //        result = "ERROR";
        //    }

        //    return result;
        //}

        public static string responderObservacion(Int32 id, String user_responde, String recomendacion, Int32 area)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@id", tipodato = "int", valorEntero = id });
                lparametros.Add(new parametros { parametro = "@user_responde", tipodato = "string", valorCadena = user_responde });
                lparametros.Add(new parametros { parametro = "@recomendacion", tipodato = "string", valorCadena = recomendacion });
                lparametros.Add(new parametros { parametro = "@area", tipodato = "int", valorEntero = area });

                result = BDConn.EjecutarStoreProc_string("sp_general_actualiza_respuesta_obsevacion", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

    }
}