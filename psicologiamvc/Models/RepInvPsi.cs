using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace psicologiamvc.Models
{
    public class RepInvPsi
    {
        public string curp { get; set; }
        public string evaluado { get; set; }
        public string fnacimiento { get; set; }
        public string sexo { get; set; }
        public string domicilio { get; set; }
        public string rfc { get; set; }
        public string telefonos { get; set; }
        public string puesto { get; set; }
        public string cDescdano { get; set; }
        public string codigoevaluado { get; set; }
        public string desc_dependencia { get; set; }
        public string cRecomendaciones { get; set; }
        public string cResultado { get; set; }
        public string clave_psicologo { get; set; }
        public string cBender { get; set; }
        public string edad { get; set; }
        public string cLugarnac { get; set; }
        public string cGrado { get; set; }
        public string cEscolaridad { get; set; }
        public string fEvalpsi { get; set; }
        public string cDiagnostico { get; set; }
        public string cLaboralpsi { get; set; }
        public string cAdscripcion { get; set; }
        public string cevaluacion { get; set; }
        public string comision { get; set; }
        public string funcion { get; set; }
        public string idSupPsi { get; set; }
        public string funcionInstitucional { get; set; }
        public string SerialElement { get; set; }
        public int idhistorico { get; set; }
        public int estatuspsi { get; set; }
        public bool bJuicio { get; set; }
        public bool bAutoestima { get; set; }
        public bool bManejo { get; set; }
        public bool bAlteraciones { get; set; }
        public bool bApego { get; set; }
        public bool bTolerancia { get; set; }
        public bool bRelaciones { get; set; }
        public string fortaleza_riesgo { get; set; }
        public string notas { get; set; }
        public string cCompetencia { get; set; }
        public byte[] Picture { get; set; }
        public string puestoTabular { get; set; }

        public static List<RepInvPsi> getRepInvPsi(int idhistorico)
        {
            List<RepInvPsi> repPsi = new List<RepInvPsi>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<RepInvPsi>(cnn, "sp_psicologia_get_rep_investigacion", new { @idhistorico = idhistorico }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            repPsi = (List<RepInvPsi>)returnDapper;

            return repPsi;
        }
    }
}