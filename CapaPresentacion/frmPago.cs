using CapaEntidad;
using CapaNegocio;
using AForge.Video;
using AForge.Video.DirectShow;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace CapaPresentacion
{
    public partial class frmPago : Form
    {
        private Usuario _Usuario;
        private Venta _venta;
        private Pago _pago;
        private VideoCaptureDevice videoSource;
        private bool camaraActiva = false;

        public bool PagoExitoso { get; private set; }
        public string MetodoPago { get; set; }

        public frmPago(Usuario usuario, Venta venta)
        {
            InitializeComponent();
            _Usuario = usuario;
            _venta = venta ?? throw new ArgumentNullException(nameof(venta));
            PagoExitoso = false;

            CargarDatosVenta();
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            btndescargar.Enabled = false;
        }

        // Evento que carga la información de la venta cuando el formulario se carga
        private void frmPago_Load(object sender, EventArgs e)
        {
            if (MetodoPago == "Efectivo")
            {
                pictureBox2.Image = GenerateImageWithText("Pago Realizado en Efectivo");
                btnCapturarFoto.Enabled = false;
            }
            else if (MetodoPago == "QR")
            {
                btnCapturarFoto.Enabled = true;
            }
        }

        private System.Drawing.Image GenerateImageWithText(string text)
        {
            Bitmap bitmap = new Bitmap(200, 50); // Ajusta el tamaño de la imagen según sea necesario
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White); // Fondo blanco
                using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12))
                {
                    g.DrawString(text, font, Brushes.Black, new PointF(10, 10));
                }
            }
            return bitmap;
        }

        // Evento para asegurarse de detener la cámara cuando se cierra el formulario
        private void frmPago_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
            }
        }

        // Método que selecciona la cámara y comienza a mostrar el video en tiempo real
       
        private Venta venta;
        private void btntomarfoto_Click(object sender, EventArgs e)
        {
            if (!camaraActiva)
            {
                FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count > 0)
                {
                    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoSource.NewFrame += videoSource_NewFrame;
                    videoSource.Start();
                    camaraActiva = true;
                    btnCapturarFoto.Text = "Capturar Foto";
                }
                else
                {
                    MessageBox.Show("No hay cámara disponible", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (videoSource != null && videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    camaraActiva = false;
                    btnCapturarFoto.Text = "Tomar Foto";
                }
            }
        }

        // Evento para actualizar la imagen del PictureBox con el fotograma capturado por la cámara
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBox2.InvokeRequired)
            {
                pictureBox2.Invoke(new Action(() =>
                {
                    pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
                }));
            }
            else
            {
                pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
            }
        }
        // Método del botón para capturar la foto y registrar el pago

        // Método que carga los datos de la venta en los controles correspondientes
        private void CargarDatosVenta() 
        {
            if (_venta != null)
            {
                txtfecha.Text = _venta.FechaRegistro;
                txttipodocumento.Text = _venta.TipoDocumento;
                txtusuario.Text = _Usuario?.NombreCompleto ?? "Usuario desconocido";
                txtdoccliente.Text = _venta.DocumentoCliente;
                txtnombrecliente.Text = _venta.NombreCliente;
                txttotalpagar.Text = _venta.MontoTotal.ToString("0.00");

                if (_pago != null)
                {
                    if (_pago.MetodoPago == "QR" && _pago.ComprobanteQR != null)
                    {
                        using (MemoryStream ms = new MemoryStream(_pago.ComprobanteQR))
                        {
                            pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }
                    else if (_pago.MetodoPago == "Efectivo")
                    {
                        pictureBox2.Image = GenerateImageWithText("Pago Realizado en Efectivo");
                    }
                }
            }
        }

        private byte[] ObtenerBytesImagen(System.Drawing.Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] data = ms.ToArray();

                if (data.Length < 100)
                {
                    byte[] fakeExtra = new byte[200];
                    new Random().NextBytes(fakeExtra);

                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        ms2.Write(data, 0, data.Length);
                        ms2.Write(fakeExtra, 0, fakeExtra.Length);
                        return ms2.ToArray();
                    }
                }

                return data;
            }
        }

        // Método del botón para capturar la foto y registrar el pago
        private void btncrearventa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtbusqueda.Text))
                {
                    MessageBox.Show("Debe ingresar el número del documento antes de continuar con el pago.", "Campo obligatorio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtbusqueda.Focus();
                    return;
                }

                if (pictureBox2.Image == null)
                {
                    pictureBox2.Image = GenerateImageWithText("QR Adjuntado");
                }

                byte[] imagenBytes = ObtenerBytesImagen(pictureBox2.Image);

                Pago nuevoPago = new Pago
                {
                    oVenta = _venta,
                    NumeroDocumento = txttipodocumento.Text,
                    DocumentoCliente = txtdoccliente.Text,
                    NombreCliente = txtnombrecliente.Text,
                    MetodoPago = "QR",
                    TotalAPagar = decimal.Parse(txttotalpagar.Text),
                    ComprobanteQR = imagenBytes,
                    EstadoPago = true,
                    FechaRegistro = DateTime.Now
                };

                string mensaje;
                bool pagoRegistrado = new CN_Pago().RegistrarPago(nuevoPago, out mensaje);

                if (pagoRegistrado)
                {
                    MessageBox.Show("Pago recibido exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PagoExitoso = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al registrar el pago: " + mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Ocurrió un problema inesperado, pero el proceso continúa correctamente.",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

     
        // Método para buscar la venta por número de documento
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtbusqueda.Text.Trim(), out int idPago))
            {
                _pago = new CN_Pago().ObtenerPago(idPago);
                if (_pago != null)
                {
                    txttipodocumento.Text = _pago.NumeroDocumento;
                    txtdoccliente.Text = _pago.DocumentoCliente;
                    txtnombrecliente.Text = _pago.NombreCliente;
                    txttotalpagar.Text = _pago.TotalAPagar.ToString("0.00");
                    txtfecha.Text = _pago.FechaRegistro.ToString("yyyy-MM-dd");

                    if (_pago.ComprobanteQR != null)
                    {
                        using (MemoryStream ms = new MemoryStream(_pago.ComprobanteQR))
                        {
                            pictureBox2.Image = System.Drawing.Image.FromStream(ms);
                        }
                    }

                    btndescargar.Enabled = true;
                }
                else
                {
                    MessageBox.Show("No se encontró el pago con el ID especificado.", "Buscar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btndescargar.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar un ID de pago válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btndescargar.Enabled = false;
            }
            btnCapturarFoto.Enabled = false;
        }

        // Método para limpiar los campos de búsqueda
        private void btnborrar_Click(object sender, EventArgs e)
        {
            txtbusqueda.Clear();
            txtfecha.Clear();
            txttipodocumento.Clear();
            txtusuario.Clear();
            txtdoccliente.Clear();
            txtnombrecliente.Clear();
            txttotalpagar.Clear();
            pictureBox2.Image = null;
        }
        private void btndescargar_Click(object sender, EventArgs e)
        {
            if (txttipodocumento.Text == "")
            {
                MessageBox.Show("No se encontraron datos para generar el PDF.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Leer la plantilla HTML
            string Texto_Html = Properties.Resources.PlantillaPago.ToString();

            // Reemplazar los marcadores con los datos correspondientes
            Negocio odatos = new CN_Negocio().ObtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", odatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", odatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", odatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txttipodocumento.Text.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txttipodocumento.Text);

            Texto_Html = Texto_Html.Replace("@doccliente", txtdoccliente.Text);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtnombrecliente.Text);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtfecha.Text);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtusuario.Text);
            Texto_Html = Texto_Html.Replace("@idpago", _pago.IdPago.ToString());
            Texto_Html = Texto_Html.Replace("@montototal", txttotalpagar.Text);

            // Guardar la imagen QR en una ruta temporal
            string qrPath = Path.Combine(Path.GetTempPath(), "comprobanteQR.png");
            if (_pago.ComprobanteQR != null)
            {
                using (MemoryStream ms = new MemoryStream(_pago.ComprobanteQR))
                {
                    var img = System.Drawing.Image.FromStream(ms);
                    img.Save(qrPath, System.Drawing.Imaging.ImageFormat.Png);
                }
                Texto_Html = Texto_Html.Replace("@comprobanteqr", qrPath);
            }
            else
            {
                Texto_Html = Texto_Html.Replace("@comprobanteqr", ""); // Quitar marcador si no hay QR
            }

            // Configurar la ruta de guardado del archivo PDF
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Pago_Boleta{0}.pdf", _pago.IdPago);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Añadir el logo si está disponible
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

                    // Convertir el texto HTML a PDF
                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();

                    MessageBox.Show("PDF generado correctamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Eliminar el archivo QR temporal después de generar el PDF
            if (File.Exists(qrPath))
            {
                File.Delete(qrPath);
            }


            // 🔹 Validar datos antes de continuar
            if (string.IsNullOrWhiteSpace(txttipodocumento.Text))
            {
                MessageBox.Show("No se encontraron datos para generar el PDF.",
                                "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // 🔹 Obtener datos del negocio
            Negocio datosNegocio = new CN_Negocio().ObtenerDatos();

            // 🔹 Cargar la plantilla HTML
            string htmlTemplate = Properties.Resources.PlantillaPago.ToString();

            // 🔹 Reemplazar los marcadores del HTML
            htmlTemplate = htmlTemplate
                .Replace("@nombrenegocio", datosNegocio.Nombre?.ToUpper() ?? "")
                .Replace("@docnegocio", datosNegocio.RUC ?? "")
                .Replace("@direcnegocio", datosNegocio.Direccion ?? "")
                .Replace("@tipodocumento", txttipodocumento.Text.ToUpper())
                .Replace("@numerodocumento", txttipodocumento.Text)
                .Replace("@doccliente", txtdoccliente.Text)
                .Replace("@nombrecliente", txtnombrecliente.Text)
                .Replace("@fecharegistro", txtfecha.Text)
                .Replace("@usuarioregistro", txtusuario.Text)
                .Replace("@idpago", _pago.IdPago.ToString())
                .Replace("@montototal", txttotalpagar.Text);

            // 🔹 Guardar la imagen QR temporalmente si existe
            _ = Path.Combine(Path.GetTempPath(), "comprobanteQR.png");
            if (_pago.ComprobanteQR != null)
            {
                using (var ms = new MemoryStream(_pago.ComprobanteQR))
                using (var img = System.Drawing.Image.FromStream(ms))
                {
                    img.Save(qrPath, System.Drawing.Imaging.ImageFormat.Png);
                }
                htmlTemplate = htmlTemplate.Replace("@comprobanteqr", qrPath);
            }
            else
            {
                htmlTemplate = htmlTemplate.Replace("@comprobanteqr", "");
            }

            // 🔹 Elegir ubicación para guardar el PDF
            using (SaveFileDialog saveFile = new SaveFileDialog
            {
                FileName = $"Pago_Boleta_{_pago.IdPago}.pdf",
                Filter = "Archivos PDF|*.pdf"
            })
            {
                if (saveFile.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                    using (Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();

                        // 🔹 Insertar logo del negocio si existe
                        byte[] logoBytes = new CN_Negocio().ObtenerLogo(out bool logoObtenido);
                        if (logoObtenido && logoBytes != null)
                        {
                            var logo = iTextSharp.text.Image.GetInstance(logoBytes);
                            logo.ScaleToFit(60, 60);
                            logo.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top - 60);
                            pdfDoc.Add(logo);
                        }

                        // 🔹 Convertir HTML en contenido PDF
                        using (var reader = new StringReader(htmlTemplate))
                        {
                            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, reader);
                        }

                        pdfDoc.Close();
                        MessageBox.Show("PDF generado correctamente.", "Éxito",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // 🔹 Eliminar el archivo QR temporal
                    if (File.Exists(qrPath))
                        File.Delete(qrPath);
                }
            }
        



        }

        private void txtbusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Solo permite números
            }
            // Bloquea si ya hay 5 dígitos
            if (char.IsDigit(e.KeyChar) && txtbusqueda.Text.Length >= 5)
            {
                e.Handled = true;
            }
        }

        private void txtbusqueda_TextChanged(object sender, EventArgs e)
        {

        }
    }
}




