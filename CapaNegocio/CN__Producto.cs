using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN__Producto
    {


        private CD_Producto objcd_Producto = new CD_Producto();


        public List<Producto> Listar()
        {
            return objcd_Producto.Listar();
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            obj.Codigo = obj.Codigo?.Trim();
            obj.Nombre = obj.Nombre?.Trim();
            obj.Descripcion = obj.Descripcion?.Trim();

            if (string.IsNullOrEmpty(obj.Codigo) || obj.Codigo.Length > 10)
                Mensaje += "El código es obligatorio y debe tener máximo 20 caracteres\n";

            if (string.IsNullOrEmpty(obj.Nombre) || obj.Nombre.Length > 30)
                Mensaje += "El nombre es obligatorio y debe tener máximo 30 caracteres\n";

            if (string.IsNullOrEmpty(obj.Descripcion) || obj.Descripcion.Length > 30)
                Mensaje += "La descripción es obligatoria y debe tener máximo 30 caracteres\n";

            if (obj.oCategoria == null || obj.oCategoria.IdCategoria <= 0)
                Mensaje += "Debe seleccionar una categoría válida\n";

            if (Mensaje != string.Empty)
                return 0;

            return objcd_Producto.Registrar(obj, out Mensaje);
        }


        public bool Editar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            obj.Codigo = obj.Codigo?.Trim();
            obj.Nombre = obj.Nombre?.Trim();
            obj.Descripcion = obj.Descripcion?.Trim();

            if (string.IsNullOrEmpty(obj.Codigo) || obj.Codigo.Length > 10)
                Mensaje += "El código es obligatorio y debe tener máximo 20 caracteres\n";

            if (string.IsNullOrEmpty(obj.Nombre) || obj.Nombre.Length > 30)
                Mensaje += "El nombre es obligatorio y debe tener máximo 30 caracteres\n";

            if (string.IsNullOrEmpty(obj.Descripcion) || obj.Descripcion.Length > 30)
                Mensaje += "La descripción es obligatoria y debe tener máximo 30 caracteres\n";

            if (obj.oCategoria == null || obj.oCategoria.IdCategoria <= 0)
                Mensaje += "Debe seleccionar una categoría válida\n";

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Producto.Editar(obj, out Mensaje);
            }
        }


        public bool Eliminar(Producto obj, out string Mensaje)
        {
            return objcd_Producto.Eliminar(obj, out Mensaje);
        }
    }
}
