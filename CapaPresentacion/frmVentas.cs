
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmVentas : Form
    {
        private Usuario _Usuario;
        private Venta _venta;
        private string _MetodoPago;

        // Controla el control del pago del QR
        private bool pagoQRRealizado = false;



        // ============================
        // CONSTRUCTORES
        // ============================
        public frmVentas(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        public frmVentas(string metodoPago = null)
        {
            InitializeComponent();
            _MetodoPago = metodoPago;
        }

        // ============================
        // LOAD
        // ============================

        private void frmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                // Tipo de documento
                cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Boleta", Texto = "Boleta" });
                cbotipodocumento.Items.Add(new OpcionCombo() { Valor = "Factura", Texto = "Factura" });
                cbotipodocumento.DisplayMember = "Texto";
                cbotipodocumento.ValueMember = "Valor";
                cbotipodocumento.SelectedIndex = 0;

                // Fecha
                txtfecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // Campos iniciales
                txtidproducto.Text = "0";
                cbometodopago.SelectedIndex = 0;
                txtpagocon.Text = "";
                txtcambio.Text = "";
                txttotalpagar.Text = "0";

                // Si viene el método desde frmPago
                if (!string.IsNullOrEmpty(_MetodoPago))
                    cbometodopago.SelectedItem = _MetodoPago;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando formulario: " + ex.Message);
            }
        }
        // ============================
        // BUSCAR CLIENTE
        // ============================
        #region Buscar Cliente / Producto
        private void btnbuscarcliente_Click(object sender, EventArgs e)
        {
           try
            {
                using (var modal = new mdCliente())
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        txtdocumentocliente.Text = modal._Cliente.Documento;
                        txtnombrecliente.Text = modal._Cliente.NombreCompleto;
                        txtcodproducto.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar cliente: " + ex.Message);
            }
        }

        private void btnbuscarproducto_Click(object sender, EventArgs e)
        {
            try
            {
                using (var modal = new mdProducto())
                {
                    if (modal.ShowDialog() == DialogResult.OK)
                    {
                        CargarProductoSeleccionado(modal._Producto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar producto: " + ex.Message);
            }
        }
        private void CargarProductoSeleccionado(Producto p)
        {
            txtidproducto.Text = p.IdProducto.ToString();
            txtcodproducto.Text = p.Codigo;
            txtproducto.Text = p.Nombre;
            txtprecio.Text = p.PrecioVenta.ToString("0.00");
            txtstock.Text = p.Stock.ToString();
            txtcantidad.Select();
        }

        private void txtcodproducto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData != Keys.Enter) return;

                Producto oProducto = new CN__Producto()
                    .Listar()
                    .Where(p => p.Codigo == txtcodproducto.Text && p.Estado)
                    .FirstOrDefault();

                if (oProducto != null)
                {
                    txtcodproducto.BackColor = Color.Honeydew;
                    CargarProductoSeleccionado(oProducto);
                }
                else
                {
                    txtcodproducto.BackColor = Color.MistyRose;
                    limpiarProducto();
                }
            }
            catch
            {
                MessageBox.Show("Error buscando producto.");
            }
        }
        #endregion

        #region Agregar / Quitar productos
        private void btnagregarproducto_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Validar cliente
                if (string.IsNullOrWhiteSpace(txtnombrecliente.Text))
                {
                    MessageBox.Show("Debe seleccionar un cliente.", "Advertencia");
                    return;
                }

                // 2) Validar producto
                if (!int.TryParse(txtidproducto.Text, out int idprod) || idprod == 0)
                {
                    MessageBox.Show("Debe seleccionar un producto.", "Advertencia");
                    return;
                }

                // 3) Validar precio
                if (!decimal.TryParse(txtprecio.Text, out decimal precio))
                {
                    MessageBox.Show("Precio inválido.", "Error");
                    txtprecio.Select();
                    return;
                }

                // 4) Validar stock
                if (Convert.ToInt32(txtstock.Text) < Convert.ToInt32(txtcantidad.Value))
                {
                    MessageBox.Show("La cantidad supera al stock.", "Error");
                    return;
                }

                // 5) Validar productos repetidos
                foreach (DataGridViewRow fila in dgvdata.Rows)
                {
                    if (fila.Cells["IdProducto"].Value.ToString() == txtidproducto.Text)
                    {
                        MessageBox.Show("El producto ya fue agregado.", "Error");
                        return;
                    }
                }

                // 6) Descontar stock
                if (!new CN_Venta().RestarStock(idprod, Convert.ToInt32(txtcantidad.Value)))
                {
                    MessageBox.Show("Error al descontar stock.", "Error");
                    return;
                }

                // 7) Agregar fila
                dgvdata.Rows.Add(new object[]
                {
                    txtidproducto.Text,
                    txtproducto.Text,
                    precio.ToString("0.00"),
                    txtcantidad.Value.ToString(),
                    (txtcantidad.Value * precio).ToString("0.00")
                });

                // Habilitar pagos
                if (dgvdata.Rows.Count == 1)
                    HabilitarControlesDePago(true);

                calcularTotal();
                limpiarProducto();
                txtcodproducto.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Asumiendo que tu columna de eliminar es la 5 (index 5) - deja tal cual si coincide
            if (e.ColumnIndex == 5)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.delete25.Width;
                var h = Properties.Resources.delete25.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.delete25, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btneliminar")
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvdata.Rows[index].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvdata.Rows[index].Cells["Cantidad"].Value.ToString()));

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(index);
                        calcularTotal();

                        // Deshabilitar componentes si el dgvdata queda vacío
                        if (dgvdata.Rows.Count == 0)
                        {
                            cbometodopago.Enabled = false;
                            txtpagocon.Enabled = false;
                            txtcambio.Enabled = false;
                            btncrearventa.Enabled = false;
                            btnpagarqr.Enabled = false;
                            limpiarProducto();
                        }
                    }
                }
            }
        }
        #endregion

        #region Validaciones UI
        private void txtprecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir números, control y punto
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtpagocon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == '.')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void calcularcambio()
        {
            if (!decimal.TryParse(txttotalpagar.Text, out decimal total)) return;

            if (string.IsNullOrWhiteSpace(txtpagocon.Text))
            {
                txtpagocon.Text = "0";
            }

            if (!decimal.TryParse(txtpagocon.Text, out decimal pagacon))
            {
                MessageBox.Show("El valor ingresado en 'Paga con' no es un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pagacon < total)
            {
                MessageBox.Show("El monto con el que se paga no puede ser menor al total a pagar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcambio.Text = "0.00";
                return;
            }

            txtcambio.Text = (pagacon - total).ToString("0.00");
        }

        private void txtpagocon_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtpagocon.Text) && txtpagocon.Text != "0")
                calcularcambio();
        }
        #endregion

        private void calcularTotal()
        {
            try
            {
                decimal total = 0;
                foreach (DataGridViewRow row in dgvdata.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());

                txttotalpagar.Text = total.ToString("0.00");
            }
            catch
            {
                txttotalpagar.Text = "0.00";
            }
        }

        private void limpiarProducto()
        {
            txtidproducto.Text = "0";
            txtcodproducto.Text = "";
            txtproducto.Text = "";
            txtprecio.Text = "";
            txtstock.Text = "";
            txtcantidad.Value = 1;
        }

        private byte[] ObtenerBytesImagen(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        // ============================
        // LIMPIAR FORMULARIO
        // ============================
        private void LimpiarFormulario()
        {
            txtdocumentocliente.Text = "";
            txtnombrecliente.Text = "";
            txtpagocon.Text = "";
            txtcambio.Text = "";
            txttotalpagar.Text = "0";
            dgvdata.Rows.Clear();
            HabilitarControlesDePago(false);
        }
        private void HabilitarControlesDePago(bool estado)
        {
            cbometodopago.Enabled = estado;
            txtpagocon.Enabled = estado;
            txtcambio.Enabled = estado;
            btncrearventa.Enabled = estado;
        }

        #region Método de pago: QR
        private void cbometodopago_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodo = cbometodopago.SelectedItem?.ToString() ?? "";

            if (metodo == "QR")
            {
                txtpagocon.Enabled = false;
                txtcambio.Enabled = false;
                btncrearventa.Enabled = false;
                btnpagarqr.Enabled = dgvdata.Rows.Count > 0;
            }
            else
            {
                txtpagocon.Enabled = true;
                txtcambio.Enabled = true;
                txtpagocon.Text = "";
                txtcambio.Text = "";
                
                btncrearventa.Enabled = dgvdata.Rows.Count > 0;
                btnpagarqr.Enabled = false;
            }
        }

        private void btnpagarqr_Click(object sender, EventArgs e)
        {
           
            try
            {
                // 1. Validar que haya productos (solo para demo, no obligatorio)
                if (dgvdata.Rows.Count == 0)
                {
                    MessageBox.Show("Para la demo, se agregan productos simulados automáticamente.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Agregar fila simulada
                    dgvdata.Rows.Add("Producto Demo", 1, 10.00m, 10.00m);
                }

                // 2. Validar cliente (simulación)
                if (string.IsNullOrWhiteSpace(txtdocumentocliente.Text) || string.IsNullOrWhiteSpace(txtnombrecliente.Text))
                {
                    txtdocumentocliente.Text = "SIM-1234";
                    txtnombrecliente.Text = "Cliente Demo 1";
                }

                // 3. Crear objeto venta preliminar si no existe
                if (_venta == null)
                {
                    _venta = new Venta()
                    {
                        oUsuario = _Usuario,
                        DocumentoCliente = txtdocumentocliente.Text,
                        NombreCliente = txtnombrecliente.Text,
                        MontoTotal = 0m, // Monto cero para simulación
                        FechaRegistro = DateTime.Now.ToString()
                    };
                }

                // 4. Abrir formulario QR simulador
                frmPago ventanaQR = new frmPago(_Usuario, _venta)
                {
                    MetodoPago = "QR" // Le indicamos que es pago QR
                };

                // 5. No necesitamos foto real para demo, solo mostramos la ventana
                ventanaQR.ShowDialog();

                // 6. Marcar pago realizado automáticamente en demo
                if (ventanaQR.PagoExitoso)
                {
                    btncrearventa.Enabled = true;
                    txtpagocon.Enabled = false;
                    txtcambio.Enabled = false;

                    MessageBox.Show("Pago QR simulado registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el pago QR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        

        }

        private void GuardarPagoConQR(Bitmap foto)
        {
            try
            {
                byte[] imagenBytes = ObtenerBytesImagen(foto);

                Pago pago = new Pago()
                {
                    IdVenta = _venta.IdVenta,
                    MetodoPago = "QR",
                    DocumentoCliente = _venta.DocumentoCliente,
                    MontoPago = _venta.MontoTotal,
                    IdUsuario = _Usuario.IdUsuario,
                    ImagenQR = imagenBytes
                };

                string mensaje = "";
                bool exito = new CN_Pago().RegistrarPago(pago, out mensaje);

                if (exito)
                {
                    pagoQRRealizado = true; // Marca que el pago QR se realizó
                    MessageBox.Show("Pago QR registrado correctamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el pago: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago QR: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion

        #region Crear Venta unificada
        private void btncrearventa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarPagoAntesDeRegistrar()) return;

                RegistrarVenta(cbometodopago.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear venta: " + ex.Message);
            }

        }
        private bool ValidarPagoAntesDeRegistrar()
        {
            string metodo = cbometodopago.SelectedItem.ToString();

            if (metodo == "QR" && !pagoQRRealizado)
            {
                MessageBox.Show("Primero debe realizar el pago QR.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void RegistrarVenta(string metodoPago)
        {
            try
            {
                // Validar cliente
                if (string.IsNullOrWhiteSpace(txtdocumentocliente.Text) ||
                    string.IsNullOrWhiteSpace(txtnombrecliente.Text))
                {
                    MessageBox.Show("Debe seleccionar cliente.", "Error");
                    return;
                }

                // Validar productos
                if (dgvdata.Rows.Count < 1)
                {
                    MessageBox.Show("Debe agregar productos.", "Error");
                    return;
                }

                // Validar pago efectivo
                if (metodoPago == "Efectivo")
                {
                    if (string.IsNullOrWhiteSpace(txtpagocon.Text) ||
                        string.IsNullOrWhiteSpace(txtcambio.Text))
                    {
                        MessageBox.Show("Ingrese monto de pago.", "Error");
                        return;
                    }
                }

                // Obtener correlativo
                int idcorrelativo = new CN_Venta().ObtenerCorrelativo();
                if (idcorrelativo == 0)
                {
                    MessageBox.Show("Error generando número documento.", "Error");
                    return;
                }

                string numeroDocumento = idcorrelativo.ToString("D5");

                // Crear objeto venta
                Venta oVenta = new Venta()
                {
                    oUsuario = _Usuario,
                    TipoDocumento = ((OpcionCombo)cbotipodocumento.SelectedItem).Texto,
                    NumeroDocumento = numeroDocumento,
                    DocumentoCliente = txtdocumentocliente.Text,
                    NombreCliente = txtnombrecliente.Text,
                    MontoTotal = decimal.Parse(txttotalpagar.Text),
                    MontoPago = metodoPago == "QR" ? 0 : decimal.Parse(txtpagocon.Text),
                    MontoCambio = metodoPago == "QR" ? 0 : decimal.Parse(txtcambio.Text),
                    MetodoPago = metodoPago,
                    FechaRegistro = DateTime.Now.ToString()
                };

                // Detalle venta
                DataTable detalle_venta = new DataTable();
                detalle_venta.Columns.Add("IdProducto", typeof(int));
                detalle_venta.Columns.Add("PrecioVenta", typeof(decimal));
                detalle_venta.Columns.Add("Cantidad", typeof(int));
                detalle_venta.Columns.Add("SubTotal", typeof(decimal));

                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    detalle_venta.Rows.Add(
                        Convert.ToInt32(row.Cells["IdProducto"].Value),
                        Convert.ToDecimal(row.Cells["Precio"].Value),
                        Convert.ToInt32(row.Cells["Cantidad"].Value),
                        Convert.ToDecimal(row.Cells["SubTotal"].Value)
                    );
                }

                // Registrar venta
                string mensaje = "";
                bool ventaRegistrada = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

                if (ventaRegistrada)
                {
                    MessageBox.Show("Venta registrada.\nN°: " + numeroDocumento, "Éxito");
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show("Error al registrar: " + mensaje);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar venta: " + ex.Message);
            }
        }

        

        #endregion
    }
}
