using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using psicologiamvc.Models;

using System.Security;
using System.Security.Cryptography;

using System.Net;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations.Schema;

namespace psicologiamvc.Models
{
    public class Usuarios
    {
        public Int32 Id_Usuario { get; set; }
        public Int32 Id_Rol { get; set; }


        [NotMapped]
        public String NombreCompleto { get; set; }

        public String Nombre { get; set; }
        public String Ap_Paterno { get; set; }
        public String Ap_Materno { get; set; }

        [NotMapped]
        public string Desc_Rol { get; set; }
        
        public String Correo { get; set; }
        
        [NotMapped]
        public String Celular { get; set; }
        
        public Int32 Activo { get; set; }
        public String Foto { get; set; }

        [NotMapped]
        public String Nom_Sucursal { get; set; }

        public String Contrasena { get; set; }
        public Boolean Activo2 { get; set; }
        public String UsuarioSise { get; set; }


        /// <summary>
        /// Actualizar la foto del cliente seleccionado.
        /// </summary>
        /// <param name="pid_usuario"></param>
        /// <param name="pid_controlador_accion"></param>
        /// <param name="pid_cliente"></param>
        /// <param name="pfoto"></param>
        /// <returns></returns>
        public static string update_foto(int pid_usuario, int pid_controlador_accion, string pfoto)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pid_usuario });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pid_controlador_accion });                
                lparametros.Add(new parametros { parametro = "@Foto", tipodato = "string", valorCadena = pfoto });

                result = BDConn.EjecutarStoreProc_string("[sp_usuarios_foto_update]", lparametros, true, "@Result");
            }
            catch { result = "ERROR"; }
            return result;
        }

        /// <summary>
        /// método para obtener la lista de usuario
        /// </summary>
        /// <returns></returns>
        public static List<Usuarios> getLista(int pid_rol)
        {
            List<Usuarios> lista = new List<Usuarios>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_usuarios_list", new { @Id_Rol = pid_rol }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Usuarios>)returnDapper;
            return lista;
        }

        public static List<Usuarios> getListaUsuariosArea(int idArea)
        {
            List<Usuarios> listaUsu = new List<Usuarios>();
            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_general_obtener_usuarios_por_area", new { @idArea = idArea }, commandType: CommandType.StoredProcedure);
            BDConn.CierraConexion(cnn);
            listaUsu = (List<Usuarios>)returnDapper;
            return listaUsu;
        }

        public static List<Usuarios> select(int pid_usuario)
        {
            List<Usuarios> lista = new List<Usuarios>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Usuarios>(cnn, "spUsuarios_sel", new { @Id_Usuario = pid_usuario }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Usuarios>)returnDapper;
            return lista;
        }
        #region contraseña
        /// <summary>
        /// método para actualizar
        /// </summary>
        /// <param name="pId_UsuarioAdmin"></param>
        /// <param name="pId_Usuario"></param>
        /// <param name="pContrasena_Actual"></param>
        /// <param name="pContrasena_Nueva"></param>
        /// <param name="pContrasena_Confirmar"></param>        
        /// <returns></returns>
        public static string updateContrasena(int pId_Usuario, int pId_Rol, string pNombre, string pPaterno, string pMaterno, string pContrasena, bool pActivo2, string pUsuarioSise)
        {
            string result = "ERROR";
            try
            {

                //string pPassActual = Logeo.GenerateSHA1Hash(pContrasena); //Helpers.SHA1.Encode(pactual);
                string pPassNew = Logeo.GenerateSHA1Hash(pContrasena);  //Helpers.SHA1.Encode(pnueva);

                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
                lparametros.Add(new parametros { parametro = "@Id_Rol", tipodato = "int", valorEntero = pId_Rol });
                lparametros.Add(new parametros { parametro = "@Nombre", tipodato = "string", valorCadena = pNombre });
                lparametros.Add(new parametros { parametro = "@Ap_Paterno", tipodato = "string", valorCadena = pPaterno });
                lparametros.Add(new parametros { parametro = "@Ap_Materno", tipodato = "string", valorCadena = pMaterno });
                lparametros.Add(new parametros { parametro = "@Contrasena", tipodato = "string", valorCadena = pPassNew });
                lparametros.Add(new parametros { parametro = "@Activo2", tipodato = "boleano", valorBoleano = pActivo2 });
                lparametros.Add(new parametros { parametro = "@UsuarioSise", tipodato = "string", valorCadena = pUsuarioSise });
                result = BDConn.EjecutarStoreProc_string("sp_usuarios_upd_contrasena", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        //--------------------------------------------------------------------------------------------------------- Original del codigo de arriba
        //public static string updateContrasena(int pId_UsuarioAdmin, int pId_Controladores_Acciones, int pId_Usuario, string pContrasena_Actual, string pContrasena_Nueva)
        //{
        //    string result = "ERROR";
        //    try
        //    {

        //        string pPassActual = Logeo.GenerateSHA1Hash(pContrasena_Actual); //Helpers.SHA1.Encode(pactual);
        //        string pPassNew = Logeo.GenerateSHA1Hash(pContrasena_Nueva);  //Helpers.SHA1.Encode(pnueva);

        //        List<parametros> lparametros = new List<parametros>();
        //        lparametros.Add(new parametros { parametro = "@Id_UsuarioAdmin", tipodato = "int", valorEntero = pId_UsuarioAdmin });
        //        lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pId_UsuarioAdmin });
        //        lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
        //        lparametros.Add(new parametros { parametro = "@ContrasenaActual", tipodato = "string", valorCadena = pPassActual });
        //        lparametros.Add(new parametros { parametro = "@ContrasenaNueva", tipodato = "string", valorCadena = pPassNew });
        //        result = BDConn.EjecutarStoreProc_string("sp_usuarios_upd_contrasena", lparametros, true, "@Result");

        //    }
        //    catch
        //    {
        //        result = "ERROR";
        //    }

        //    return result;
        //}
        //--------------------------------------------------------------------------------------------------------- Original
                    
        #endregion

        public static Usuarios selectOne(int pid_usuario)
        {
            Usuarios lista = new Usuarios();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.QueryFirstOrDefault<Usuarios>(cnn, "spUsuarios_sel", new { @Id_Usuario = pid_usuario }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (Usuarios)returnDapper;
            return lista;
        }

        /// <summary>
        /// método para guardar
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <param name="pId_Rol"></param>
        /// <param name="pNombre"></param>
        /// <param name="pCorreo"></param>
        /// <param name="pCelular"></param>
        /// <param name="pContrasena"></param>
        /// <returns></returns>
        public static string insert(int pId_Usuario, int pId_Rol, int pId_Controladores_Acciones, string pNombre, string pAp_Paterno, string pAp_Materno, string pCorreo, string pContrasena, string pUsuarioSise)
        {
            string result = "ERROR";
            try
            {
                //string pContrasena = pCorreo.Substring(0, pCorreo.IndexOf("@"));
                string pPass = Logeo.GenerateSHA1Hash(pContrasena);

                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pId_Controladores_Acciones });
                lparametros.Add(new parametros { parametro = "@Id_Rol", tipodato = "int", valorEntero = pId_Rol });
                lparametros.Add(new parametros { parametro = "@Nombre", tipodato = "string", valorCadena = pNombre });
                lparametros.Add(new parametros { parametro = "@Ap_Paterno", tipodato = "string", valorCadena = pAp_Paterno });
                lparametros.Add(new parametros { parametro = "@Ap_Materno", tipodato = "string", valorCadena = pAp_Materno });
                lparametros.Add(new parametros { parametro = "@Correo", tipodato = "string", valorCadena = pCorreo });
                lparametros.Add(new parametros { parametro = "@Contrasena", tipodato = "string", valorCadena = pPass });
                lparametros.Add(new parametros { parametro = "@UsuarioSise", tipodato = "string", valorCadena = pUsuarioSise });

                result = BDConn.EjecutarStoreProc_string("sp_usuarios_ins", lparametros, true, "@Result");
              
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        /// <summary>
        /// método para editar
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public static SelectList getEdit(int pId)
        {
            Dictionary<string, string> info = new Dictionary<string, string>();
            Dictionary<string, string> dParametros = new Dictionary<string, string>();

            dParametros.Add("Id_Usuario", pId.ToString());

            DataTable dt = BDConn.ConsultarBD("spUsuarios_select", dParametros);

            foreach (DataColumn dc in dt.Columns)
            {
                info.Add(dc.ColumnName, dt.Rows[0][dc.ColumnName].ToString());
            }

            SelectList returnValores = new SelectList(info, "Key", "Value");

            return returnValores;
        }

        /// <summary>
        /// método para actualizar
        /// </summary>
        /// <param name="pId_UsuarioAdmin"></param>
        /// <param name="pId_Usuario"></param>
        /// <param name="pId_Rol"></param>
        /// <param name="pNombre"></param>
        /// <param name="pCorreo"></param>
        /// <param name="pCelular"></param>
        /// <param name="pContrasena"></param>
        /// <returns></returns>
        public static string update(int pId_UsuarioAdmin, int pId_Controladores_Acciones, int pId_Usuario, int pId_Rol, string pNombre, string pAp_Paterno, string pAp_Materno, string pCorreo, int pid_sucursal)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_UsuarioAdmin", tipodato = "int", valorEntero = pId_UsuarioAdmin });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pId_UsuarioAdmin });
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
                lparametros.Add(new parametros { parametro = "@Id_Rol", tipodato = "int", valorEntero = pId_Rol });
                lparametros.Add(new parametros { parametro = "@Nombre", tipodato = "string", valorCadena = pNombre });
                lparametros.Add(new parametros { parametro = "@Ap_Paterno", tipodato = "string", valorCadena = pAp_Paterno });
                lparametros.Add(new parametros { parametro = "@Ap_Materno", tipodato = "string", valorCadena = pAp_Materno });
                lparametros.Add(new parametros { parametro = "@Correo", tipodato = "string", valorCadena = pCorreo });
                lparametros.Add(new parametros { parametro = "@Id_Sucursal", tipodato = "int", valorEntero = pid_sucursal });

                result = BDConn.EjecutarStoreProc_string("sp_usuarios_upd", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }
        /// <summary>
        /// método para actualizar
        /// </summary>
        /// <param name="pId_UsuarioAdmin"></param>
        /// <param name="pId_Usuario"></param>
        /// <param name="pNombre"></param>
        /// <param name="pCorreo"></param>
        /// <param name="pCelular"></param>
        /// <param name="pContrasena"></param>
        /// <returns></returns>
        public static string updatePerfil(int pId_UsuarioAdmin, int pId_Controladores_Acciones, int pId_Usuario, string pNombre, string pAp_Paterno, string pAp_Materno, string pCorreo)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_UsuarioAdmin", tipodato = "int", valorEntero = pId_UsuarioAdmin });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pId_UsuarioAdmin });
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
            
                lparametros.Add(new parametros { parametro = "@Nombre", tipodato = "string", valorCadena = pNombre });
                lparametros.Add(new parametros { parametro = "@Ap_Paterno", tipodato = "string", valorCadena = pAp_Paterno });
                lparametros.Add(new parametros { parametro = "@Ap_Materno", tipodato = "string", valorCadena = pAp_Materno });
                lparametros.Add(new parametros { parametro = "@Correo", tipodato = "string", valorCadena = pCorreo });
                
                result = BDConn.EjecutarStoreProc_string("sp_usuarios_upd_perfil", lparametros, true, "@Result");

            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        /// <summary>
        /// método para eliminar
        /// </summary>
        /// <param name="pUsuarioID"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        public static string delete(int pUsuarioID, int pId_Controladores_Acciones, int pId)
        {
            string result = "ERROR";
            List<parametros> lparametros = new List<parametros>();
            try
            {
                lparametros.Add(new parametros { parametro = "@Id_UsuarioAdmin", tipodato = "int", valorEntero = pUsuarioID });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pId_Controladores_Acciones });
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId });

                result = BDConn.EjecutarStoreProc_string("sp_Usuarios_del", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        /// <summary>
        /// método para obtener la lista de roles.
        /// </summary>
        /// <returns></returns>
        public static SelectList getRolesCbo(string selected = "[Selecciona]")
        {
            Dictionary<int, string> dicccionario = new Dictionary<int, string>();
            Dictionary<string, string> dParametros = new Dictionary<string, string>();

            DataTable dt = BDConn.ConsultarBD("spUsuariosCargos_obt", dParametros);

            dicccionario.Add(0, selected);

            foreach (DataRow dr in dt.Rows)
            {
                dicccionario.Add(Convert.ToInt32(dr["Id_Rol"]), dr["Desc_Rol"].ToString());
            }

            SelectList select = new SelectList(dicccionario, "Key", "Value");

            return select;
        }

        /// <summary>
        /// método para actualizar la contraseña
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <param name="pactual"></param>
        /// <param name="pnueva"></param>
        /// <returns></returns>
        public static string mupdContrasena(int pId_Usuario, string pactual, string pnueva)
        {
            string result = "ERROR";
            try
            {
                string pPassActual = Logeo.GenerateSHA1Hash(pactual);
                string pPassNew = Logeo.GenerateSHA1Hash(pnueva);


                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pId_Usuario });
                lparametros.Add(new parametros { parametro = "@ContrasenaActual", tipodato = "string", valorCadena = pPassActual });
                lparametros.Add(new parametros { parametro = "@ContrasenaNew", tipodato = "string", valorCadena = pPassNew });

                result = BDConn.EjecutarStoreProc_string("spUsuarios_changePass", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }

            return result;
        }

        public static List<Usuarios> get_Usuario_GetUsuarios()
        {
            List<Usuarios> listaUser = new List<Usuarios>();

            IDbConnection cnn = BDConn.AbreConexion();

            var returnDapper = Dapper.SqlMapper.Query<Usuarios>(cnn, "sp_Usuario_GetUsuarios", commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            listaUser = (List<Usuarios>)returnDapper;

            return listaUser;
        }

    }

    public class Bitacora
    {
        public int Id_Bitacora { get; set; }
        public String Desc_Controlador { get; set; }
        public String Descripcion { get; set; }
        public String Desc_Accion { get; set; }
        public String Fecha_Registro { get; set; }
        public String LabelAccion { get; set; }

        public static List<Bitacora> lista(int pid_usuario)
        {
            List<Bitacora> lista = new List<Bitacora>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<Bitacora>(cnn, "sp_bitacora_list", new { Id_Usuario = pid_usuario }, commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<Bitacora>)returnDapper;
            return lista;
        }
    }

    public class UsuariosRoles
    {
        public int Id_Rol { get; set; }
        public int Id_Aplicacion { get; set; }
        public string Desc_Rol { get; set; }

        public static List<UsuariosRoles> getLista()
        {
            List<UsuariosRoles> lista = new List<UsuariosRoles>();

            IDbConnection cnn = BDConn.AbreConexion();
            var returnDapper = Dapper.SqlMapper.Query<UsuariosRoles>(cnn, "sp_roles_list", commandType: CommandType.StoredProcedure);

            BDConn.CierraConexion(cnn);

            lista = (List<UsuariosRoles>)returnDapper;
            return lista;
        }

        public static string insert(int pid_usuario, int pid_rol, int pid_controlador_accion, string pdesc_rol, string plist_permisos)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pid_usuario });
                lparametros.Add(new parametros { parametro = "@Id_Rol", tipodato = "int", valorEntero = pid_rol });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pid_controlador_accion });

                lparametros.Add(new parametros { parametro = "@Descripcion", tipodato = "string", valorCadena = pdesc_rol });
                lparametros.Add(new parametros { parametro = "@ListPermisos", tipodato = "string", valorCadena = plist_permisos });

                result = BDConn.EjecutarStoreProc_string("sp_roles_ins", lparametros, true, "@Result");
            }
            catch { result = "ERROR"; }

            return result;
        }

        public static string update(int pid_usuario, int pid_rol, int pid_controlador_accion, int pid_rolupd, string pdesc_rol, string plist_permisos)
        {
            string result = "ERROR";
            try
            {
                List<parametros> lparametros = new List<parametros>();
                lparametros.Add(new parametros { parametro = "@Id_Usuario", tipodato = "int", valorEntero = pid_usuario });
                lparametros.Add(new parametros { parametro = "@Id_Rol", tipodato = "int", valorEntero = pid_rol });
                lparametros.Add(new parametros { parametro = "@Id_Controladores_Acciones", tipodato = "int", valorEntero = pid_controlador_accion });

                lparametros.Add(new parametros { parametro = "@Id_RolUpd", tipodato = "int", valorEntero = pid_rolupd });
                lparametros.Add(new parametros { parametro = "@Descripcion", tipodato = "string", valorCadena = pdesc_rol });
                lparametros.Add(new parametros { parametro = "@ListPermisos", tipodato = "string", valorCadena = plist_permisos });

                result = BDConn.EjecutarStoreProc_string("sp_roles_upd", lparametros, true, "@Result");
            }
            catch { result = "ERROR"; }

            return result;
        }

    }

}