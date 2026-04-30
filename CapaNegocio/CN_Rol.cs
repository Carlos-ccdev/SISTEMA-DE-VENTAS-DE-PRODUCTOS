using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Rol
    {

        private CD_Rol objcd_rol = new CD_Rol();

        // Método para listar los roles
        public List<Rol> Listar()
        {
            return objcd_rol.Listar();
        }

        // Método para registrar un nuevo rol
        public int Registrar(Rol obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripción del rol.\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_rol.Registrar(obj, out Mensaje);
            }
        }

        // Método para editar un rol existente
        public bool Editar(Rol obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripción del rol.\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_rol.Editar(obj, out Mensaje);
            }
        }

        // Método para eliminar un rol
        public bool Eliminar(Rol obj, out string Mensaje)
        {
            return objcd_rol.Eliminar(obj, out Mensaje);
        }
    }
}
