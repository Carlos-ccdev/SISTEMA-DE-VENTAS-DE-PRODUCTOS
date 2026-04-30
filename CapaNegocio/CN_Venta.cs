using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_venta = new CD_Venta();

        public bool RestarStock(int idproducto, int cantidad) {
            return objcd_venta.RestarStock(idproducto, cantidad);
        }

        public bool SumarStock(int idproducto, int cantidad) {
            return objcd_venta.SumarStock(idproducto, cantidad);
        }

        public int ObtenerCorrelativo()
        {
            return objcd_venta.ObtenerCorrelativo();
        }

        //public int RegistrarVenta(Venta obj, DataGridView dgvdata)
        //{
        //    DataTable detalleVenta = new DataTable();
        //    detalleVenta.Columns.Add("IdProducto", typeof(int));
        //    detalleVenta.Columns.Add("PrecioVenta", typeof(decimal));
        //    detalleVenta.Columns.Add("Cantidad", typeof(int));
        //    detalleVenta.Columns.Add("SubTotal", typeof(decimal));

        //    foreach (DataGridViewRow row in dgvdata.Rows)
        //    {
        //        detalleVenta.Rows.Add(
        //            Convert.ToInt32(row.Cells["IdProducto"].Value),
        //            Convert.ToDecimal(row.Cells["Precio"].Value), // CORREGIDO
        //            Convert.ToInt32(row.Cells["Cantidad"].Value),
        //            Convert.ToDecimal(row.Cells["SubTotal"].Value)
        //        );
        //    }

        //    string mensaje;
        //    bool resultado = objcd_venta.Registrar(obj, detalleVenta, out mensaje);

        //    if (!resultado)
        //    {
        //        MessageBox.Show(mensaje, "Error al registrar venta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return -1;
        //    }

        //    return obj.IdVenta; // EL ID VIENE DEL SP
        //}


        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            return objcd_venta.Registrar(obj, DetalleVenta, out Mensaje);
        }

        public Venta ObtenerVenta(string numero)
        {
            Venta oVenta = objcd_venta.ObtenerVenta(numero);

            if (oVenta.IdVenta != 0)
            {
                List<Detalle_Venta> oDetalleVenta = objcd_venta.ObtenerDetalleVenta(oVenta.IdVenta);
                oVenta.oDetalle_Venta = oDetalleVenta;
            }

            return oVenta;
        }


    }
}
