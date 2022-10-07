using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Terman
    {
        public int IdH { get; set; }
        public string nombre { get; set; }
        public string puesto { get; set; }
        public string edad { get; set; }
        public string sexo { get; set; }
        public string fecha { get; set; }
        public int serie1 { get; set; }
        public int serie2 { get; set; }
        public int serie3 { get; set; }
        public int serie4 { get; set; }
        public int serie5 { get; set; }
        public int serie6 { get; set; }
        public int serie7 { get; set; }
        public int serie8 { get; set; }
        public int serie9 { get; set; }
        public int serie10 { get; set; }
        public int grantotal { get; set; }
        public int ci { get; set; }
        public int interpretacionci { get; set; }
        public string rangoci { get; set; }

        public static List<Terman> getTerman(int IdH)
        {
            List<Terman> listaTerman = new List<Terman>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Terman>(cnn, "sp_psicologia_obtener_terman_resultado", new { @idHistorico = IdH }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaTerman = (List<Terman>)returnDapper;

            return listaTerman;
        }
    }
}