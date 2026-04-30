using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {

        private CD_Cliente objcd_Cliente = new CD_Cliente();


        public List<Cliente> Listar()
        {
            return objcd_Cliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del Cliente\n";

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
                Mensaje += "Es necesario el nombre completo del Cliente\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del Cliente\n";

            
            // Validación de duplicados
            if (objcd_Cliente.ExisteCorreo(obj.Correo, obj.IdCliente))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_Cliente.ExisteDocumento(obj.Documento, obj.IdCliente))
                Mensaje += "El documento ya está registrado\n";

            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return 0; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_Cliente.Registrar(obj, out Mensaje);
            }


        }


        public bool Editar(Cliente obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del Cliente\n";

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
                Mensaje += "Es necesario el nombre completo del Cliente\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del Cliente\n";


            // Validación de duplicados
            if (objcd_Cliente.ExisteCorreo(obj.Correo, obj.IdCliente))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_Cliente.ExisteDocumento(obj.Documento, obj.IdCliente))
                Mensaje += "El documento ya está registrado\n";

            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return false; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_Cliente.Editar(obj, out Mensaje);
            }

        }


        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objcd_Cliente.Eliminar(obj, out Mensaje);
        }

    }
}
