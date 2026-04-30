using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Proveedor
    {

        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();


        public List<Proveedor> Listar()
        {
            return objcd_Proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.RazonSocial))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del usuario\n";

            // Validación de duplicados
            if (objcd_Proveedor.ExisteCorreo(obj.Correo, obj.IdProveedor))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_Proveedor.ExisteDocumento(obj.Documento, obj.IdProveedor))
                Mensaje += "El documento ya está registrado\n";

            if (objcd_Proveedor.ExisteRazonSocial(obj.RazonSocial, obj.IdProveedor))
                Mensaje += "La razón social ya está registrada\n";


            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return 0; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_Proveedor.Registrar(obj, out Mensaje);
            }

        }


        public bool Editar(Proveedor obj, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.Documento))
                Mensaje += "Es necesario el documento del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.RazonSocial))
                Mensaje += "Es necesario el nombre completo del usuario\n";

            if (string.IsNullOrWhiteSpace(obj.Correo))
                Mensaje += "Es necesario el Correo del usuario\n";

            // Validación de duplicados
            if (objcd_Proveedor.ExisteCorreo(obj.Correo, obj.IdProveedor))
                Mensaje += "El correo ya está registrado\n";

            if (objcd_Proveedor.ExisteDocumento(obj.Documento, obj.IdProveedor))
                Mensaje += "El documento ya está registrado\n";

            if (objcd_Proveedor.ExisteRazonSocial(obj.RazonSocial, obj.IdProveedor))
                Mensaje += "La razón social ya está registrada\n";

            // Si hubo errores en las validaciones
            if (Mensaje != string.Empty)
            {
                return false; // no registra
            }
            else
            {
                // llama al procedimiento de registro en la capa de datos
                return objcd_Proveedor.Editar(obj, out Mensaje);
            }

        }


        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objcd_Proveedor.Eliminar(obj, out Mensaje);
        }


    }
}
