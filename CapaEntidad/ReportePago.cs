using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ReportePago
    {
        public string FechaRegistro { get; set; }
        public string NumeroDocumento { get; set; }
        public string UsuarioRegistro { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProducto { get; set; }
        public string Cantidad { get; set; }
        public string MetodoPago { get; set; }
        public string MontoTotal { get; set; }
        public byte[] ComprobanteQR { get; set; }
    }
}
