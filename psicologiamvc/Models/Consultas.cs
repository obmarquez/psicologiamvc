using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;

namespace psicologiamvc.Models
{
    public class Consultas
    {
        public int IdH { get; set; }
        public int opcion { get; set; }
        public Int32 IdE { get; set; }
        public String evaluado { get; set; }
        public String evaluacion { get; set; }
        public Int32 estatus { get; set; }
        public String fecha { get; set; }
        public String dependencia { get; set; }
        public Int32 NoCon { get; set; }
        public String curp { get; set; }
        public Int32 Hay { get; set; }
        public Int32 Vinculo { get; set; }

        public String id { get; set; }
        public String criterio { get; set; }

        public String UsuarioSise { get; set; }
        public String Nombre { get; set; }

        public String cve_mun { get; set; }
        public String mun_desc { get; set; }

        [NotMapped]
        public String puesto { get; set; }
        [NotMapped]
        public String comision { get; set; }
        [NotMapped]
        public String cAdscripcion { get; set; }
        [NotMapped]
        public String departamento { get; set; }
        [NotMapped]
        public String observacionpublica { get; set; }
        [NotMapped]
        public String recomendacion { get; set; }
        [NotMapped]
        public Int32 observacionesPendientes { get; set; }
        [NotMapped]
        public Int32 Obs { get; set; }
        [NotMapped]
        public Int32 idO { get; set; }
        [NotMapped]
        public Int32 Indice { get; set; }
        [NotMapped]
        public string evaluadoReferido { get; set; }
        [NotMapped]
        public string genero { get; set; }
        [NotMapped]
        public string alias { get; set; }
        [NotMapped]
        public string referencia { get; set; }
        [NotMapped]
        public string area { get; set; }
        [NotMapped]
        public int Protocolo { get; set; }
        [NotMapped]
        public string edad { get; set; }
        [NotMapped]
        public string rfc { get; set; }
        [NotMapped]
        public string direccion { get; set; }
        [NotMapped]
        public string telefonos { get; set; }
        [NotMapped]
        public string categoriaPuesto { get; set; }
        [NotMapped]
        public string funcion { get; set; }
        [NotMapped]
        public string funcionInst { get; set; }
        [NotMapped]
        public string codigoevaluado { get; set; }
        [NotMapped]
        public string escolaridad { get; set; }
        [NotMapped]
        public string subdependencia { get; set; }
        [NotMapped]
        public string hayVinculo { get; set; }
        [NotMapped]
        public int TotalE { get; set; }
        [NotMapped]
        public string name { get; set; }
        [NotMapped]
        public int Cantidad { get; set; }
        public int asignado { get; set; }
        public int supervision { get; set; }
        public int correccion { get; set; }
        public int endireccion { get; set; }
        public int total2020 { get; set; }
        public int liberado { get; set; }
        public string vinculador { get; set;}
        public string nombreReferido { get; set; }
        public string paternoReferido { get; set; }
        public string maternoReferido { get; set; }
        public string municipio { get; set; }
        public string fechaCus { get; set; }
        public string observacionCus { get; set; }
        public int clave { get; set; }
        public string hayTat { get; set; }
        public string hayTatDen { get; set; }
        public int C1 { get; set; }
        public int C2 { get; set; }
        public int C3 { get; set; }
        public int C4 { get; set; }
        public int CA { get; set; }
        public int CB { get; set; }
        public int CC { get; set; }
        public int Hombre { get; set; }
        public int Mujer { get; set; }
        public string TOTAL { get; set; }
        public string mes { get; set; }
        public int totalRaven { get; set; }
        public int totalMine { get; set; }
        public int totalTerman { get; set; }
        public int totalCleaver { get; set; }
        public int ra { get; set; }
        public int rb { get; set; }
        public int rm { get; set; }
        public int nc { get; set; }
        public string diferenciada { get; set; }

        public static List<Consultas> getListaConsultaEvaluado(string pid_user)
        {
            List<Consultas> lista = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_Psicologia_listado_psicologos_pendientes", new { @idPsicologo = pid_user }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Consultas>)returnDapper;

            return lista;
        }

        public static List<Consultas> getListaParaSupervisor(string pEvaluado)
        {
            List<Consultas> lista = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_listado_evalauados_supervisor", new { @evaluado = pEvaluado }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Consultas>)returnDapper;

            return lista;
        }

        public static List<Consultas> getCriterios01(int idConsulta)
        {
            List<Consultas> losCriterios = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_criterios", new { @idConsulta = idConsulta }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);

            losCriterios = (List<Consultas>)returnDapper;

            return losCriterios;
        }

        public static List<Consultas> getSupervisores(int rol)
        {
            List<Consultas> losSupervisores = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_supervisor_area_lista", new { @area = rol }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);

            losSupervisores = (List<Consultas>)returnDapper;

            return losSupervisores;
        }

        public static List<Consultas> getListaAsociarFecha(string fecha, int area)
        {
            List<Consultas> listaAsociar = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_listado_asociar_evalauados", new { @fecha = fecha, @area = area }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaAsociar = (List<Consultas>)returDapper;

            return listaAsociar;
        }

        public static List<Consultas> getMunicipiosEstado()
        {
            List<Consultas> listaMunicipios = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDatpper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_Psicologia_ObtieneMunicipios_Estado", commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaMunicipios = (List<Consultas>)returnDatpper;

            return listaMunicipios;
        }

        public static List<Consultas> getDependencias()
        {
            List<Consultas> listaDependencias = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_get_dependencias", commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaDependencias = (List<Consultas>)returnDapper;
            return listaDependencias;
        }

        public static List<Consultas> getEntradaDiaria(string fecha)
        {
            List<Consultas> listaEntradaDiaria = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_entrada_diaria", new { @fecha = fecha }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaEntradaDiaria = (List<Consultas>)returnDapper;

            return listaEntradaDiaria;
        }

        public static List<Consultas> getListaObservacionDiaria(int accion, string fecha, int idObservacion)
        {
            List<Consultas> listaObservacionDiaria = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_general_lista_observaciones", new { @accion = accion, @fecha = fecha, @idObservacion = idObservacion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaObservacionDiaria = (List<Consultas>)returnDapper;

            return listaObservacionDiaria;
        }

        public static List<Consultas> getListaObservacionDiariaxEvaluado(int sIdH, int area)
        {
            List<Consultas> listaObservacionDiariaxEvaluado = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_lista_observacion_x_evaluado", new { @idH = sIdH, @area = area }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaObservacionDiariaxEvaluado = (List<Consultas>)returnDapper;

            return listaObservacionDiariaxEvaluado;
        }

        public static List<Consultas> getListaReferenciasRed(string datos)
        {
            List<Consultas> listaReferenciaRed = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_consulta_vinculados", new { @nombre = datos, @area = 3 }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaReferenciaRed = (List<Consultas>)returnDapper;

            return listaReferenciaRed;
        }

        public static List<Consultas> get_Datos_Cabeceras_Reporte(int IdH, int opcion)
        {
            List<Consultas> listaCabecera = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_datos_reporte_investigacion", new { @idhistorico = IdH, @opcion = opcion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaCabecera = (List<Consultas>)returnDapper;

            return listaCabecera;
        }

        public static List<Consultas> getTotalEvaluadoTipoPsicologia(int p_Opcion, string p_Usuario)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_total_evauluaciones", new { @opcion = p_Opcion, @usuario = p_Usuario }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }

        //obtener datos para la grafica de paste de estatus pendientes por evaluador por el momento no lo pude ver, interntarlo en otra pagina para direcccion
        public static List<Consultas> ListStatus(string pEvaluado, int pidrol)
        {
            List<Consultas> listaEstatus = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_evaluadorStatusPendientes", new { @idpsi = pEvaluado, @idrol = pidrol }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaEstatus = (List<Consultas>)returnDapper;
            return listaEstatus;
        }

        public static List<Consultas> get_investigador_area(int area)
        {
            List<Consultas> losevaluadores = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_evaluador_area_lista", new { @area = area }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            losevaluadores = (List<Consultas>)returnDapper;
            return losevaluadores;
        }

        public static List<Consultas> getGeneralPsicologo()
        {
            List<Consultas> losPendientes = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_general_expedientes_usuario", null, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            losPendientes = (List<Consultas>)returnDapper;
            return losPendientes;
        }

        public static List<Consultas> getConcentradoPsicologo()
        {
            List<Consultas> elConcentrado = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_general_expedientes_usuario_concentrado", null, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            elConcentrado = (List<Consultas>)returnDapper;
            return elConcentrado;
        }

        public static List<Consultas> getConcentradoSupervisor()
        {
            List<Consultas> concentradoSupervisor = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_general_expedientes_supervisor_concentrado", null, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            concentradoSupervisor = (List<Consultas>)returnDapper;
            return concentradoSupervisor;
        }

        public static List<Consultas> getConcentradoResultado(string idpsi, string fecha1, string fecha2)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_evaluadorConcentradoResultados", new { @idpsi = idpsi, @fecha1 = fecha1, @fecha2 = fecha2 }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }

        public static List<Consultas> getListaObservacionCustodia(int sIdH)
        {
            List<Consultas> listaObservacionCustodia = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_observacionCustodia", new { @idHistorico = sIdH }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaObservacionCustodia = (List<Consultas>)returnDapper;

            return listaObservacionCustodia;
        }

        public static List<Consultas> get_sp_general_consultas_redVinculos(int opcion, string nombre_alias, string municipio, int cveinstitucion)
        {
            List<Consultas> lista_Consultas_RedVinculos = new List<Consultas>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_general_consultas_redVinculos", new { @opcion = opcion, @nombre_alias = nombre_alias, @municipio = municipio, @cveinstitucion = cveinstitucion }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista_Consultas_RedVinculos = (List<Consultas>)returnDapper;

            return lista_Consultas_RedVinculos;
        }

        public static List<Consultas> obtenerListaCritreriosMayoresMenores(string supervisor, string f1, string f2)
        {
            List<Consultas> losCriteriorsMyM = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_conteo_criteriosMayoresMenores", new { @supervisor = supervisor, @f1 = f1, @f2 = f2 }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            losCriteriorsMyM = (List<Consultas>)returnDapper;
            return losCriteriorsMyM;
        }

        public static List<Consultas> obtenerGrados()
        {
            List<Consultas> losGrados = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_obtener_grados", commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            losGrados = (List<Consultas>)returnDapper;
            return losGrados;
        }

        public static List<Consultas> obtenerListaTerman(string fecha01, string fecha02)
        {
            List<Consultas> concentradoTerman = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_obtener_lista_terman", new { @fecha01 = fecha01, @fecha02 = fecha02 }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            concentradoTerman = (List<Consultas>)returnDapper;
            return concentradoTerman;
        }

        public static List<Consultas> obtenerListaTests(string fecha01, string fecha02, string opcionTest)
        {
            List<Consultas> concentradoTerman = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_obtener_lista_tests", new { @fecha01 = fecha01, @fecha02 = fecha02, @test = opcionTest }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            concentradoTerman = (List<Consultas>)returnDapper;
            return concentradoTerman;
        }

        public static List<Consultas> getDatosHomeIndex(int p_opcion)
        {
            List<Consultas> DatosHomeIndex = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_datos_index", new { @opcion = p_opcion }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            DatosHomeIndex = (List<Consultas>)returnDapper;
            return DatosHomeIndex;
        }

        public static List<Consultas> obtenerDatosTest()
        {
            List<Consultas> losTest = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_datos_test", commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            losTest = (List<Consultas>)returnDapper;
            return losTest;
        }

        public static List<Consultas> obtenerEvaluacionesMensuales()
        {
            List<Consultas> lasEvaluacionesMensuales = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_obtener_evaluaciones_mensuales", commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lasEvaluacionesMensuales = (List<Consultas>)returnDapper;
            return lasEvaluacionesMensuales;
        }

        public static List<Consultas> getEvolucionEvaluacionPsicologo(int opcion, string idpsi)
        {
            List<Consultas> lista = new List<Consultas>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Consultas>(cnn, "sp_psicologia_evolucion_evaluacion_psicologo", new { @opcion = opcion, @idpsi = idpsi }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            lista = (List<Consultas>)returnDapper;
            return lista;
        }
    }
}