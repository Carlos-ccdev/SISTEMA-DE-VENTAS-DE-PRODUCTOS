using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using ClosedXML.Excel; // Para el manejo de la impresión


namespace CapaPresentacion
{
    public partial class frmDetalleVenta : Form
    {
        public frmDetalleVenta()
        {
            InitializeComponent();
        }

        private void frmDetalleVenta_Load(object sender, EventArgs e)
        {
            txtbusqueda.Select();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CN_Venta().ObtenerVenta(txtbusqueda.Text);

            if (oVenta.IdVenta != 0)
            {
                txtnumerodocumento.Text = oVenta.NumeroDocumento;
                txtfecha.Text = oVenta.FechaRegistro;
                txttipodocumento.Text = oVenta.TipoDocumento;
                txtusuario.Text = oVenta.oUsuario.NombreCompleto;
                txtdoccliente.Text = oVenta.DocumentoCliente;
                txtnombrecliente.Text = oVenta.NombreCliente;

                dgvdata.Rows.Clear();
                foreach (Detalle_Venta dv in oVenta.oDetalle_Venta)
                {
                    dgvdata.Rows.Add(new object[] { dv.oProducto.Nombre, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                }

                txtmontototal.Text = oVenta.MontoTotal.ToString("0.00");
                txtmontopago.Text = oVenta.MontoPago.ToString("0.00");
                txtmontocambio.Text = oVenta.MontoCambio.ToString("0.00");
            }
            else
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtfecha.Text = "";
            txttipodocumento.Text = "";
            txtusuario.Text = "";
            txtdoccliente.Text = "";
            txtnombrecliente.Text = "";
            txtbusqueda.Text = "";
            dgvdata.Rows.Clear();
            txtmontototal.Text = "0.00";
            txtmontopago.Text = "0.00";
            txtmontocambio.Text = "0.00";
            txtbusqueda.Focus();
        }

        private void btndescargar_Click(object sender, EventArgs e)
        {
            if (txttipodocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio odatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txttipodocumento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtnumerodocumento.Text);


            Texto_Html = Texto_Html.Replace("@doccliente", txtdoccliente.Text);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtnombrecliente.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtusuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtmontototal.Text);
            Texto_Html = Texto_Html.Replace("@pagocon", txtmontopago.Text);
            Texto_Html = Texto_Html.Replace("@cambio", txtmontocambio.Text);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txtnumerodocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_Negocio().ObtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnimprimir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnumerodocumento.Text))
            {
                MessageBox.Show("No se encontraron datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.FileName = $"DetalleVenta_{txtnumerodocumento.Text}.xlsx";
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add("DetalleVenta");

                            // Encabezado de columnas
                            hoja.Cell("A1").Value = "Número de Documento";
                            hoja.Cell("B1").Value = "Fecha";
                            hoja.Cell("C1").Value = "Tipo de Documento";
                            hoja.Cell("D1").Value = "Usuario";
                            hoja.Cell("E1").Value = "Documento Cliente";
                            hoja.Cell("F1").Value = "Nombre Cliente";
                            hoja.Cell("G1").Value = "Producto";
                            hoja.Cell("H1").Value = "Precio";
                            hoja.Cell("I1").Value = "Cantidad";
                            hoja.Cell("J1").Value = "SubTotal";
                            hoja.Cell("K1").Value = "Monto Total";
                            hoja.Cell("L1").Value = "Monto Pagado";
                            hoja.Cell("M1").Value = "Monto Cambio";

                            // Información principal de la venta
                            hoja.Cell("A2").Value = txtnumerodocumento.Text;
                            hoja.Cell("B2").Value = txtfecha.Text;
                            hoja.Cell("C2").Value = txttipodocumento.Text;
                            hoja.Cell("D2").Value = txtusuario.Text;
                            hoja.Cell("E2").Value = txtdoccliente.Text;
                            hoja.Cell("F2").Value = txtnombrecliente.Text;

                            int row = 2;

                            // Agregar detalles de la venta
                            foreach (DataGridViewRow dgvRow in dgvdata.Rows)
                            {
                                hoja.Cell(row, 7).Value = dgvRow.Cells["Producto"].Value?.ToString() ?? "";
                                hoja.Cell(row, 8).Value = dgvRow.Cells["Precio"].Value?.ToString() ?? "";
                                hoja.Cell(row, 9).Value = dgvRow.Cells["Cantidad"].Value?.ToString() ?? "";
                                hoja.Cell(row, 10).Value = dgvRow.Cells["SubTotal"].Value?.ToString() ?? "";
                                row++;
                            }

                            // Totales en las filas finales
                            hoja.Cell($"K2").Value = txtmontototal.Text;
                            hoja.Cell($"L2").Value = txtmontopago.Text;
                            hoja.Cell($"M2").Value = txtmontocambio.Text;

                            // Ajuste de ancho de columnas
                            hoja.Columns().AdjustToContents();
                            wb.SaveAs(savefile.FileName);
                        }

                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar reporte: {ex.Message}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }

        }

        private void txtbusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permitir números y controlar longitud
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea letras u otros caracteres
            }

            // Bloquea si ya hay 5 dígitos
            if (char.IsDigit(e.KeyChar) && txtbusqueda.Text.Length >= 5)
            {
                e.Handled = true;
            }
        }

    }
}
