using System;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Protocolo
    {
        public int idhistorico { get; set; }
        public string curp_evaluado { get; set; }
        public string clave_psicologo { get; set; }
        public bool bAbordaje { get; set; }
        public bool bRegistroentrevista { get; set; }
        public bool bAclaracionhipotesis { get; set; }
        public bool bCongruenciadiagnostico { get; set; }
        public bool bRedaccionclara { get; set; }
        public bool bRespondemotivo { get; set; }
        public bool bPresenciaobservado { get; set; }
        public string cObservacion { get; set; }

        [NotMapped]
        public int accion { get; set; }

        public static string agregaActualizaProtocolo(int idhistorico, string clave_psicologo, bool bAbordaje, bool bRegistroentrevista, bool bAclaracionhipotesis, bool bCongruenciadiagnostico, bool bRedaccionclara, bool bRespondemotivo, bool bPresenciaobservado, string cObservacion, int accion)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idhistorico", tipodato = "int", valorEntero = idhistorico });
                lparametros.Add(new parametros { parametro = "@clave_psicologo", tipodato = "string", valorCadena = clave_psicologo });
                lparametros.Add(new parametros { parametro = "@bAbordaje", tipodato = "boleano", valorBoleano = bAbordaje });
                lparametros.Add(new parametros { parametro = "@bRegistroentrevista", tipodato = "boleano", valorBoleano = bRegistroentrevista });
                lparametros.Add(new parametros { parametro = "@bAclaracionhipotesis", tipodato = "boleano", valorBoleano = bAclaracionhipotesis });
                lparametros.Add(new parametros { parametro = "@bCongruenciadiagnostico", tipodato = "boleano", valorBoleano = bCongruenciadiagnostico });
                lparametros.Add(new parametros { parametro = "@bRedaccionclara", tipodato = "boleano", valorBoleano = bRedaccionclara });
                lparametros.Add(new parametros { parametro = "@bRespondemotivo", tipodato = "boleano", valorBoleano = bRespondemotivo });
                lparametros.Add(new parametros { parametro = "@bPresenciaobservado", tipodato = "boleano", valorBoleano = bPresenciaobservado });
                lparametros.Add(new parametros { parametro = "@cObservacion", tipodato = "string", valorCadena = cObservacion });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = accion });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_agrega_actualiza_protocolos", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Protocolo> getProtocolo(int idhistorico /*string curp, int id*/)
        {
            List<Protocolo> lista = new List<Protocolo>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Protocolo>(cnn, "sp_psicologia_selecciona_protocolo", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Protocolo>)returnDapper;

            return lista;
        }
    }
}