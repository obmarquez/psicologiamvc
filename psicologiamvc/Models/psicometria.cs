using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace psicologiamvc.Models
{
    public class psicometria
    {
        public int idhistorico { get; set; }
        public string curp { get; set; }
        public string evaluado { get; set; }
        public string cevaluacion { get; set; }
        public string fechaAlta { get; set; }
        public string fechaAplicacion { get; set; }
        public bool mac { get; set; }
        public bool fig { get; set; }
        public bool the { get; set; }
        public bool min { get; set; }
        public bool bio { get; set; }
        public bool cle { get; set; }
        public bool lus { get; set; }
        public bool ape { get; set; }
        public bool rav { get; set; }
        public bool bet2 { get; set; }
        public bool htp { get; set; }
        public bool sac { get; set; }
        public bool b16 { get; set; }
        public bool bet3 { get; set; }
        public bool moca { get; set; }

        public static List<psicometria> getPsico (string evaluadillo)
        {
            List<psicometria> listaPsicometria = new List<psicometria>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<psicometria>(cnn, "sp_psicologia_get_psicometria", new { @evaluado = evaluadillo }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaPsicometria = (List<psicometria>)returnDapper;
            return listaPsicometria;
        }
    }
}