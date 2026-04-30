using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Usuario
    {

        private CD_Usuario objcd_usuario = new CD_Usuario();


        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }


        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Clave))
                Mensaje += "Es necesario la clave del usuario\n";

            //// Validación de duplicados
            if (objcd_usuario.ExisteCorreo(obj.Correo, obj.IdUsuario))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_usuario.ExisteDocumento(obj.Documento, obj.IdUsuario))
                Mensaje += "El documento ya está registrado\n";

            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return 0; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_usuario.Registrar(obj, out Mensaje);
            }
        }


        public bool Editar(Usuario obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Clave))
                Mensaje += "Es necesario la clave del usuario\n";

            // Validación de duplicados
            if (objcd_usuario.ExisteCorreo(obj.Correo, obj.IdUsuario))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_usuario.ExisteDocumento(obj.Documento, obj.IdUsuario))
                Mensaje += "El documento ya está registrado\n";

            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return false; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_usuario.Editar(obj, out Mensaje);
            }



        }


        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objcd_usuario.Eliminar(obj, out Mensaje);
        }

    }
}
