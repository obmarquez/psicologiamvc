using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;

namespace psicologiamvc.Models
{
    public class Logeo
    {
        [Required(ErrorMessage = "Ingrese su usuario")]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ingrese su contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        
        public static Usuarios IsValid(string _username, string _password)
        {
            Usuarios usuario = new Usuarios();
            try
            {
                Dictionary<string, string> parametros = new Dictionary<string, string>();
                parametros.Add("username", _username);
                parametros.Add("contrasena", GenerateSHA1Hash(_password));

                //DataTable dt = BDConn.ConsultarBD("sp_logeo_validar", parametros);
                DataTable dt = BDConn.ConsultarBD("sp_admin_logeo_validar", parametros);
                Boolean bandera = false;

                foreach (DataRow dr in dt.Rows)
                {
                    bandera = true;
                    usuario.Activo = 1;
                    usuario.Id_Usuario = Convert.ToInt32(dr["Id_Usuario"]);
                    usuario.Id_Rol = Convert.ToInt32(dr["Id_Rol"]);
                    usuario.NombreCompleto = dr["NombreCompleto"].ToString();
                    usuario.Correo = dr["Correo"].ToString();
                    usuario.Foto = dr["Foto"].ToString();
                    usuario.UsuarioSise = dr["UsuarioSise"].ToString();
                    
                }
                if (bandera == false)
                {
                    usuario.Activo = 2; //Los datos son incorrectos;
                }
            }
            catch (Exception e)
            {
                usuario.Activo = 3; //Hubo un error
                usuario.Nombre = e.Message;
            }

            return usuario;
        }
        

        /// <summary>
        /// Encripta una cadena de texto /usar para contraseñas.
        /// </summary>
        /// <param name="SourceText"></param>
        /// <returns></returns>
        public static string GenerateSHA1Hash(string SourceText)
        {
            // Create an encoding object to ensure the encoding standard for the source text
            UnicodeEncoding Ue = new UnicodeEncoding();
            // Retrieve a byte array based on the source text
            byte[] ByteSourceText = Ue.GetBytes(SourceText);
            // Instantiate an MD5 Provider object
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();
            // Compute the hash value from the source
            byte[] ByteHash = SHA1.ComputeHash(ByteSourceText);
            // And convert it to String format for return
            return Convert.ToBase64String(ByteHash);
        }

        /// <summary>
        /// Genera un token para recuperar la contraseña de un usuario, y se le manda un correo con el link para recuperar.
        /// </summary>
        /// <param name="pemail"></param>
        /// <returns></returns>
        public static string getLinkRecuperacion(string pemail)
        {
            string result = "ERROR";
            string mensaje = "";
            string token = "";
            List<parametros> lparametros = new List<parametros>();
            try
            {
                lparametros.Add(new parametros { parametro = "@Correo", tipodato = "string", valorCadena = pemail });

                result = BDConn.EjecutarStoreProc_string("sp_login_recuperarpass_ins", lparametros, true, "@Result");
                if (result.Contains("OKAS-") == true)
                {
                    token = result.Remove(0, 5);
                    using (MailMessage mm = new MailMessage("vegueta84.v@gmail.com", pemail))
                    {
                        mm.Subject = "CRECE- Confirmación para cambio de tu Contraseña";
                        mensaje += "<table width='600' border='0' cellspacing='0' cellpadding='0'><tbody><tr>";
                        mensaje += "<td width='30'>&nbsp;</td>";
                        mensaje += "<td align='left' valign='top' style='font-family:Arial,Helvetica,sans-serif;color:#555555;font-size:14px'>";
                        mensaje += "<h2 style='color:#22aae4;font-size:20px'>Estimado/a  ,</h2>";
                        mensaje += "<p>Para restablecer la <span class='il'>contraseña</span> de acceso al Sistema CRECE, has click <a href='http://www.psicologiamvc.dyndns/validar/recuperar?token=" + token + "' target='_blank'><strong>aquí</strong></a> o copiá y pegá la siguiente URL en tu navegador:</p>";
                        mensaje += "<p style='font-size:13px'><em><a href='http://www.psicologiamvc.dyndns/home/recuperar?token=" + token + "' target='_blank'>http://www.psicologiamvc.dyndns/home/recuperar?token=" + token + "</a></em></p>";
                        mensaje += "<br><br><p><strong>IMPORTANTE:</strong> Si NO solicitaste restablecer tu <span class='il'>contraseña</span>, simplemente descartá este email.</p>";
                        mensaje += "<br>Atentamente<br><p>Soporte Técnico</p></td>";
                        mensaje += "<td width='30'>&nbsp;</td>";
                        mensaje += "</tr></tbody></table>";
                        mm.Body = mensaje;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("soporte.psicologiamvc@gmail.com", "Atrespasosymedio");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        result = "OK";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "ERROR";
            }
            return result;
        }

        /// <summary>
        /// Valida un token para actualizar la contraseña de un usuario.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string getLinkValidacion(string token)
        {
            string result = "ERROR";
            List<parametros> lparametros = new List<parametros>();
            try
            {
                lparametros.Add(new parametros { parametro = "@Token", tipodato = "string", valorCadena = token });

                result = BDConn.EjecutarStoreProc_string("sp_login_recuperarpass_validar", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }

        /// <summary>
        /// Actualiza la contraseña de un usuario mediante un link de recuperación de contraseña.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="pcontra1"></param>
        /// <returns></returns>
        public static string updContrasena(string token, string pcontra1)
        {
            string result = "ERROR";
            string pass = GenerateSHA1Hash(pcontra1);

            List<parametros> lparametros = new List<parametros>();
            try
            {
                lparametros.Add(new parametros { parametro = "@Token", tipodato = "string", valorCadena = token });
                lparametros.Add(new parametros { parametro = "@Contrasena", tipodato = "string", valorCadena = pass });

                result = BDConn.EjecutarStoreProc_string("sp_login_recuperarpass_actpass", lparametros, true, "@Result");
            }
            catch
            {
                result = "ERROR";
            }
            return result;
        }
        
    }
}