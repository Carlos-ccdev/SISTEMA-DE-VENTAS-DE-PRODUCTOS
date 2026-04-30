using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CapaEntidad
{
    public class Detalle_Pago
    {
        // -----------------------------
        // Datos del pago
        // -----------------------------
        public int IdDetallePago { get; set; } // Clave primaria
        public Pago oPago { get; set; }        // Relación con la clase Pago
        public string Descripcion { get; set; } // Descripción del detalle de pago
        public decimal Monto { get; set; }      // Monto del detalle de pago
        public string FechaRegistro { get; set; } // Fecha de registro del detalle

        // -----------------------------
        // Datos del detalle de venta
        // -----------------------------
        public string NombreProducto { get; set; } // Nombre del producto
        public decimal Precio { get; set; }        // Precio unitario
        public int Cantidad { get; set; }          // Cantidad vendida
        public decimal SubTotal { get; set; }      // Subtotal = Precio * Cantidad
    }
}
