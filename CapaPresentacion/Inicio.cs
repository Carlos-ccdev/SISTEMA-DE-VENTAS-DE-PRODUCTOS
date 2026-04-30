using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using FontAwesome.Sharp;


namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;


        public Inicio(Usuario objusuario = null)
        {
            if (objusuario == null)
                usuarioActual = new Usuario() { NombreCompleto = "ADMIN PREDEFINIDO", IdUsuario = 1 };
            else
                usuarioActual = objusuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> ListaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);

            foreach (IconMenuItem iconmenu in menu.Items)
            {

                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false)
                {
                    iconmenu.Visible = false;
                }

            }



            lblusuario.Text = usuarioActual.NombreCompleto;
        }


        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {

            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }

            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.DodgerBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();


        }
        private void submenuregistrarventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmVentas(usuarioActual));
        }

        private void submenuregistrarcompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCompras(usuarioActual));
        }

        private void submenureportecompras_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteCompras());
        }

        private void submenureporteventas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmReporteVentas());
        }

        private void menuacercade_Click(object sender, EventArgs e)
        {
            mdAcercade md = new mdAcercade();
            md.ShowDialog();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void submenuusuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuusuarios, new frmUsuarios());
        }

        private void submenurol_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuusuarios, new frmRol());
        }

        private void submenuclientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new frmClientes());
        }

        private void submenuproveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmProveedores());
        }

        private void submenuproductos_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmProducto());
        }

        private void submenucategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new frmCategoria());
        }

        private void submenupermiso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuusuarios, new frmPermiso());
        }

        private void submenureportepago_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmDetallePago());
        }

        private void submenupago_Click(object sender, EventArgs e)
        {
            // Crea una venta con el usuario actual
            Venta venta = new Venta()
            {
                oUsuario = usuarioActual,  // Asegúrate de asignar el usuario actual
                FechaRegistro = DateTime.Now.ToString("dd/MM/yyyy"),
                TipoDocumento = "Boleta",  // Ejemplo de valores predeterminados
                DocumentoCliente = "",  // Puedes dejar vacío o manejarlo según la lógica
                NombreCliente = "",
                MontoTotal = 0
            };

            // Abre el formulario con los parámetros Usuario y Venta
            AbrirFormulario(menuventas, new frmPago(usuarioActual, venta));
            
        }

        private void submenunegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuusuarios, new frmNegocio());
        }

        private void submenuverdetallecompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmDetalleCompra());
        }

        private void submenuverdetalleventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmDetalleVenta());
        }

      
    }
}