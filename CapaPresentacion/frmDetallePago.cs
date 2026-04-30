//using CapaEntidad;
//using CapaNegocio;
//using CapaPresentacion.Utilidades;
//using ClosedXML.Excel;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Drawing;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace CapaPresentacion
//{
//    public partial class frmDetallePago : Form
//    {
//        public frmDetallePago()
//        {
//            InitializeComponent();
//            frmDetallePago_Load(this, EventArgs.Empty); // Forzar la llamada al método de carga
//            this.Load += frmDetallePago_Load;
//        }

//        private void frmDetallePago_Load(object sender, EventArgs e)
//        {
//            foreach (DataGridViewColumn columna in dgvdata.Columns)
//            {
//                cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
//            }
//            cbobusqueda.DisplayMember = "Texto";
//            cbobusqueda.ValueMember = "Valor";
//            cbobusqueda.SelectedIndex = 0;

//            // Asignar el evento CellFormatting al DataGridView
//            this.dgvdata.CellFormatting += new DataGridViewCellFormattingEventHandler(dgvdata_CellFormatting);
//        }
//        private void dgvdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
//        {
//            if (e.ColumnIndex == dgvdata.Columns["Foto"].Index && e.Value != null)
//            {
//                // Verificar si el valor es un arreglo de bytes (byte[])
//                if (e.Value is byte[] imageBytes)
//                {
//                    using (MemoryStream ms = new MemoryStream(imageBytes))
//                    {
//                        // Convertir el byte[] a una imagen
//                        Image img = Image.FromStream(ms);

//                        // Redimensionar la imagen
//                        e.Value = new Bitmap(img, new Size(100, 100)); // Ajusta el tamaño de la imagen aquí
//                    }
//                }
//            }
//        }

//        private void btnbuscarreporte_Click(object sender, EventArgs e)
//        {
//            // Obtén la lista de reportes de pago
//            List<ReportePago> lista = new CN_Reporte().Pago(txtfechainicio.Value.ToString("yyyy-MM-dd"), txtfechafin.Value.ToString("yyyy-MM-dd"));

//            // Limpia los datos actuales en el DataGridView
//            dgvdata.Rows.Clear();

//            // Agrega cada reporte de pago a dgvdata
//            foreach (ReportePago rv in lista)
//            {
//                dgvdata.Rows.Add(new object[] {
//                    rv.FechaRegistro,
//                    rv.NumeroDocumento,
//                    rv.UsuarioRegistro,
//                    rv.DocumentoCliente,
//                    rv.NombreCliente,
//                    rv.NombreProducto,
//                    rv.Cantidad,
//                    rv.MetodoPago,
//                    rv.MontoTotal,
//                    rv.ComprobanteQR,
//                });
//            }
//        }
//        private void btnbuscar_Click(object sender, EventArgs e)
//        {
//            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

//            if (dgvdata.Rows.Count > 0)
//            {
//                foreach (DataGridViewRow row in dgvdata.Rows)
//                {

//                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
//                        row.Visible = true;
//                    else
//                        row.Visible = false;
//                }
//            }
//        }
//        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
//        {
//            txtbusqueda.Text = "";
//            foreach (DataGridViewRow row in dgvdata.Rows)
//            {
//                row.Visible = true;
//            }
//        }
//        private void btnexportar_Click(object sender, EventArgs e)
//        {
//            if (dgvdata.Rows.Count < 1)
//            {
//                MessageBox.Show("No hay registros para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                return;
//            }

//            using (SaveFileDialog savefile = new SaveFileDialog())
//            {
//                savefile.FileName = $"DetallePago.xlsx";
//                savefile.Filter = "Excel Files | *.xlsx";

//                if (savefile.ShowDialog() == DialogResult.OK)
//                {
//                    try
//                    {
//                        using (XLWorkbook wb = new XLWorkbook())
//                        {
//                            var hoja = wb.Worksheets.Add("Informe");

//                            // Encabezados
//                            for (int i = 0; i < dgvdata.Columns.Count; i++)
//                            {
//                                hoja.Cell(1, i + 1).Value = dgvdata.Columns[i].HeaderText;
//                            }

//                            // Agregar datos de cada fila
//                            for (int i = 0; i < dgvdata.Rows.Count; i++)
//                            {
//                                for (int j = 0; j < dgvdata.Columns.Count; j++)
//                                {
//                                    var cellValue = dgvdata.Rows[i].Cells[j].Value;

//                                    if (j == dgvdata.Columns["Foto"].Index && cellValue is byte[] imageBytes)
//                                    {
//                                        using (MemoryStream ms = new MemoryStream(imageBytes))
//                                        {
//                                            var xlImage = hoja.AddPicture(ms)
//                                                              .MoveTo(hoja.Cell(i + 2, j + 1))
//                                                              .WithSize(144, 144); // Tamaño 2x2 pulgadas (144x144 píxeles)
//                                        }
//                                        hoja.Row(i + 2).Height = 108; // Ajustar la altura de la fila para la imagen
//                                    }
//                                    else
//                                    {
//                                        hoja.Cell(i + 2, j + 1).Value = cellValue?.ToString() ?? "";
//                                    }
//                                }
//                            }

//                            hoja.ColumnsUsed().AdjustToContents();
//                            wb.SaveAs(savefile.FileName);
//                        }

//                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    }
//                    catch (Exception ex)
//                    {
//                        MessageBox.Show($"Error al generar reporte: {ex.Message}", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
//                    }
//                }
//            }
//        }

//    }
//}

using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmDetallePago : Form
    {
        public frmDetallePago()
        {
            InitializeComponent();
            this.Load += frmDetallePago_Load; // solo una vez
        }

        private void frmDetallePago_Load(object sender, EventArgs e)
        {
            // Cargar columnas en el ComboBox de búsqueda
            cbobusqueda.Items.Clear();
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                cbobusqueda.Items.Add(new OpcionCombo()
                {
                    Valor = columna.Name,
                    Texto = columna.HeaderText
                });
            }

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            if (cbobusqueda.Items.Count > 0)
                cbobusqueda.SelectedIndex = 0;

            // Evento para formatear las celdas con imagen
            dgvdata.CellFormatting += dgvdata_CellFormatting;
        }

        private void dgvdata_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Mostrar imágenes almacenadas como byte[]
            if (dgvdata.Columns.Contains("ComprobanteQR") &&
                e.ColumnIndex == dgvdata.Columns["ComprobanteQR"].Index &&
                e.Value is byte[] imageBytes)
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);
                    e.Value = new Bitmap(img, new Size(100, 100)); // tamaño ajustado
                }
            }
        }

        private void btnbuscarreporte_Click(object sender, EventArgs e)
        {
            // Obtener la lista de pagos desde la capa de negocio
            List<ReportePago> lista = new CN_Reporte().Pago(
                txtfechainicio.Value.ToString("yyyy-MM-dd"),
                txtfechafin.Value.ToString("yyyy-MM-dd")
            );

            dgvdata.Rows.Clear();

            // Agregar resultados al DataGridView
            foreach (ReportePago rv in lista)
            {
                dgvdata.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.NumeroDocumento,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.NombreProducto,
                    rv.Cantidad,
                    rv.MetodoPago,
                    rv.MontoTotal,
                    rv.ComprobanteQR
                });
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    string cellValue = row.Cells[columnaFiltro].Value?.ToString() ?? "";
                    row.Visible = cellValue.Trim().ToUpper()
                        .Contains(txtbusqueda.Text.Trim().ToUpper());
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
                row.Visible = true;
        }

        private void btnexportar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count < 1)
            {
                MessageBox.Show("No hay registros para exportar", "Mensaje",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            using (SaveFileDialog savefile = new SaveFileDialog())
            {
                savefile.FileName = "DetallePago.xlsx";
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            var hoja = wb.Worksheets.Add("Informe");

                            // Encabezados
                            for (int i = 0; i < dgvdata.Columns.Count; i++)
                            {
                                hoja.Cell(1, i + 1).Value = dgvdata.Columns[i].HeaderText;
                            }

                            // Datos
                            for (int i = 0; i < dgvdata.Rows.Count; i++)
                            {
                                for (int j = 0; j < dgvdata.Columns.Count; j++)
                                {
                                    var cellValue = dgvdata.Rows[i].Cells[j].Value;

                                    if (dgvdata.Columns[j].Name == "ComprobanteQR" && cellValue is byte[] imageBytes)
                                    {
                                        using (MemoryStream ms = new MemoryStream(imageBytes))
                                        {
                                            hoja.AddPicture(ms)
                                                .MoveTo(hoja.Cell(i + 2, j + 1))
                                                .WithSize(144, 144); // imagen 2x2 pulgadas
                                        }
                                        hoja.Row(i + 2).Height = 108;
                                    }
                                    else
                                    {
                                        hoja.Cell(i + 2, j + 1).Value = cellValue?.ToString() ?? "";
                                    }
                                }
                            }

                            hoja.ColumnsUsed().AdjustToContents();
                            wb.SaveAs(savefile.FileName);
                        }

                        MessageBox.Show("Reporte generado correctamente", "Mensaje",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al generar reporte: {ex.Message}", "Mensaje",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
