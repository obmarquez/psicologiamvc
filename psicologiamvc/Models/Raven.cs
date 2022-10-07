using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Raven
    {
        public int idhistorico { get; set; }
        public string evaluado { get; set; }
        public string nacimiento { get; set; }
        public string edad { get; set; }
        public string horainicio { get; set; }
        public string horafin { get; set; }
        public string sexo { get; set; }
        public string serie_a { get; set; }
        public string serie_b { get; set; }
        public string serie_c { get; set; }
        public string serie_d { get; set; }
        public string serie_e { get; set; }

        public static List<Raven> getRaven(int IdH)
        {
            List<Raven> restultadoRaven = new List<Raven>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Raven>(cnn, "sp_psicologia_obtener_raven_resultado", new { @idHistorico = IdH }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            restultadoRaven = (List<Raven>)returnDapper;

            return restultadoRaven;
        }
    }
}