using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Controlsupervision
    {
        public int idHistorico { get; set; }
        public string observacion { get; set; }
        public string fCorreccion { get; set; }
        public string fDevolucion { get; set; }
        public string dx2 { get; set; }
        public string fEntrega { get; set; }

        //---------
        public int  accion { get; set; }
        public string f1 { get; set; }
        public string f2 { get; set; }
        public string evaluado { get; set; }
        public string evaluacion { get; set; }
        public string puesto { get; set; }
        public string idpsi { get; set; }
        public int estatus { get; set; }
        public string codigoevaluado { get; set; }
        public string dx1 { get; set; }
        public string fEvalpsi { get; set; }
        public string idh2 { get; set; }

        public static List<Controlsupervision> getConcentradoSuper(string idsupervisor, string fecha1, string fecha2)
        {
            List<Controlsupervision> lista = new List<Controlsupervision>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Controlsupervision>(cnn, "sp_psicologia_contro_supervision", new { @f1 = fecha1, @f2 = fecha2, @idsupervisor = idsupervisor }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Controlsupervision>)returnDapper;
            return lista;
        }

        public static string agregarActualizarControlSupervision(int idHistorico , string observacion, string fCorreccion, string fDevolucion, string dx2, string fEntrega, int accion)
        {
            string result = "Error";

            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@idHistorico", tipodato = "int", valorEntero = idHistorico });
                lparametros.Add(new parametros { parametro = "@observacion", tipodato = "string", valorCadena = observacion });
                lparametros.Add(new parametros { parametro = "@fCorreccion", tipodato = "string", valorCadena = fCorreccion });
                lparametros.Add(new parametros { parametro = "@fDevolucion", tipodato = "string", valorCadena = fDevolucion });
                lparametros.Add(new parametros { parametro = "@dx2", tipodato = "string", valorCadena = dx2 });
                lparametros.Add(new parametros { parametro = "@fEntrega", tipodato = "string", valorCadena = fEntrega });
                lparametros.Add(new parametros { parametro = "@accion", tipodato = "int", valorEntero = accion });

                result = BDConn.EjecutarStoreProc_string("sp_psicologia_agrega_actualiza_control_supervision", lparametros, true, "@Result");  //Si lo hizo bien regresará un "Ok"
            }
            catch
            {
                result = "Error";
            }

            return result;
        }

        public static List<Controlsupervision> obtenerDatosControl(int idHistorico)
        {
            List<Controlsupervision> listaControl = new List<Controlsupervision>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Controlsupervision>(cnn, "sp_psicologia_obtener_datos_control", new { @idHistorico = idHistorico }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaControl = (List<Controlsupervision>)returnDapper;
            return listaControl;
        }
    }
}