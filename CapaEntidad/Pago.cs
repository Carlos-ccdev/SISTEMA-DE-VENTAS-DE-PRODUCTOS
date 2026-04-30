using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Pago
    {
        public int IdPago { get; set; }
        public Venta oVenta { get; set; }
        public int IdVenta { get; set; }
        public string MetodoPago { get; set; }
        public byte[] ComprobanteQR { get; set; }
        public bool EstadoPago { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NumeroDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal TotalAPagar { get; set; }
        public decimal MontoPago { get; set; }
        public int IdUsuario { get; set; }
        public byte[] ImagenQR { get; set; }


        // Constructor para inicializar valores por defecto, si es necesario
        public Pago()
        {
            FechaRegistro = DateTime.Now;  // Fecha de registro por defecto
        }
    }
}


