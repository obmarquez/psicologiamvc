using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Asistencia
    {
        public string curp { get; set; }            // @curp
        public int idevaluacion { get; set; }       // @idevaluacion
	    public int area { get; set; }
        public string UserAsist { get; set; }
	    public bool diferenciada { get; set; }
        public int idHistorico { get; set; }

        //---------------------------psicologia
        public bool bPsi1in { get; set; }
        public bool bPsi1out { get; set; }
        public bool bPsi2in { get; set; }
        public bool bPsi2out { get; set; }
        public string fPsico1in { get; set; }
        public string fPsico1out { get; set; }
        public string fPsico2in { get; set; }
        public string fPsico2out { get; set; }

        [NotMapped]
        public string evaluado { get; set; }
        [NotMapped]
        public string desc_dependencia { get; set; }
        [NotMapped]
        public string fecha_alta { get; set; }
        [NotMapped]
        public string primerdia { get; set; }
        [NotMapped]
        public string cevaluacion { get; set; }

        public static List<Asistencia> getListaAsistencia(string fechita)
        {
            List<Asistencia> listaA = new List<Asistencia>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Asistencia>(cnn, "sp_admin_asistencia_lista", new { @id = 3, @fecha = fechita }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaA = (List<Asistencia>)returnDapper;

            return listaA;
        }

        public static string actualizaAsistencia(Boolean bToxIn, String fToxIn, Boolean bToxOut, String fTox, Boolean bMedIn, String fMedIn, Boolean bMedOut, String fMed, Int32 area, String UserAsist, Boolean diferenciada, Int32 idHistorico)
        {
            string result = "ERROR";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@bToxIn", tipodato = "boleano", valorBoleano = bToxIn });
                lparametros.Add(new parametros { parametro = "@fToxIn", tipodato = "string", valorCadena = fToxIn });
                lparametros.Add(new parametros { parametro = "@bToxOut", tipodato = "boleano", valorBoleano = bToxOut });
                lparametros.Add(new parametros { parametro = "@fTox", tipodato = "string", valorCadena = fTox });
                lparametros.Add(new parametros { parametro = "@bMedIn", tipodato = "boleano", valorBoleano = bMedIn });
                lparametros.Add(new parametros { parametro = "@fMedIn", tipodato = "string", valorCadena = fMedIn });
                lparametros.Add(new parametros { parametro = "@bMedOut", tipodato = "boleano", valorBoleano = bMedOut });
                lparametros.Add(new parametros { parametro = "@fMed", tipodato = "string", valorCadena = fMed });
                lparametros.Add(new parametros { parametro = "@area", tipodato = "int", valorEntero = area });
                lparametros.Add(new parametros { parametro = "@UserAsist", tipodato = "string", valorCadena = UserAsist });
                lparametros.Add(new parametros { parametro = "@diferenciada", tipodato = "boleano", valorBoleano = diferenciada });
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idHistorico });

                result = BDConn.EjecutarStoreProc_string("sp_admin_asistencia_actualiza_area", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;

        }

        public static List<Asistencia> getAsistncia(int idHistorio)
        {
            List<Asistencia> lista = new List<Asistencia>();

            IDbConnection cnn = BDConn.AbreConexion();

            //var returnDapper = Dapper.SqlMapper.Query<Asistencia>(cnn, "sp_recupera_asistencia", new { @idHistorico = idHistorio }, commandType: CommandType.StoredProcedure);
            var returnDapper = Dapper.SqlMapper.Query<Asistencia>(cnn, "sp_admin_asistencia_recupera_area", new { @idHistorico = idHistorio, @idArea = 3 }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Asistencia>)returnDapper;

            return lista;
        }
    }
}