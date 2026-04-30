using CapaDatos;
using CapaEntidad;
using System;
using CapaDatos;
using CapaEntidad;
using System;

namespace CapaNegocio
{
    public class CN_Pago
    {
        private CD_Pago objcd_pago = new CD_Pago();

        // Método para registrar un pago
        public bool RegistrarPago(Pago pago, out string mensaje)
        {
            mensaje = string.Empty;

            // Validación básica del pago
            if (pago == null)
            {
                mensaje = "El objeto de pago es nulo.";
                return false;
            }

            if (string.IsNullOrEmpty(pago.MetodoPago))
            {
                mensaje = "Debe seleccionar un método de pago.";
                return false;
            }

            // Llamar al método de CD_Pago para registrar el pago
            return objcd_pago.RegistrarPago(pago, out mensaje);
        }

        // Método para obtener un pago por su ID
        public Pago ObtenerPago(int idPago)
        {
            if (idPago <= 0)
            {
                throw new ArgumentException("El ID del pago no es válido.");
            }

            return objcd_pago.ObtenerPago(idPago);
        }

        // Método para obtener un pago por el número de la venta
        public Pago ObtenerPagoPorVenta(int idVenta)
        {
            if (idVenta <= 0)
            {
                throw new ArgumentException("El ID de la venta no es válido.");
            }

            return objcd_pago.ObtenerPagoPorVenta(idVenta);
        }

    }
}




