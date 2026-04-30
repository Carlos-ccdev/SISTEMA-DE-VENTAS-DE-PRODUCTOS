using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_Permiso objcd_permiso = new CD_Permiso();

        // Método para listar permisos por IdRol (cambio de IdUsuario a IdRol)
        public List<Permiso> ListarPorRol(int IdRol)
        {
            return objcd_permiso.ListarPorRol(IdRol);
        }

        // Método para listar todos los permisos
        public List<Permiso> ListarTodos()
        {
            return objcd_permiso.ListarTodos(); // Este método debe implementarse en la capa de datos
        }

        public List<Permiso> Listar(int IdUsuario)
        {
            // Implementación para listar permisos por IdUsuario
            return new CD_Permiso().Listar(IdUsuario);
        }

        // Método para registrar un nuevo permiso
        public int Registrar(Permiso obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones antes de registrar
            if (obj.oRol == null || obj.oRol.IdRol == 0)
            {
                Mensaje += "Es necesario seleccionar un rol válido para el permiso.\n";
            }

            if (string.IsNullOrWhiteSpace(obj.NombreMenu))
            {
                Mensaje += "Es necesario indicar el nombre del menú.\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0; // Retornar 0 si hay errores de validación
            }
            else
            {
                return objcd_permiso.Registrar(obj, out Mensaje); // Llamar a la capa de datos para registrar
            }
        }

        // Método para editar un permiso existente
        public bool Editar(Permiso obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validaciones antes de editar
            if (obj.oRol == null || obj.oRol.IdRol == 0)
            {
                Mensaje += "Es necesario seleccionar un rol válido para el permiso.\n";
            }

            if (string.IsNullOrWhiteSpace(obj.NombreMenu))
            {
                Mensaje += "Es necesario indicar el nombre del menú.\n";
            }

            if (obj.IdPermiso == 0)
            {
                Mensaje += "Es necesario especificar un Id de Permiso válido.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false; // Retornar false si hay errores de validación
            }
            else
            {
                return objcd_permiso.Editar(obj, out Mensaje); // Llamar a la capa de datos para editar
            }
        }

        // Método para eliminar un permiso existente
        public bool Eliminar(Permiso obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            // Validación antes de eliminar
            if (obj.IdPermiso == 0)
            {
                Mensaje = "Es necesario especificar un Id de Permiso válido.";
                return false;
            }

            return objcd_permiso.Eliminar(obj, out Mensaje); // Llamar a la capa de datos para eliminar
        }
    }
}