using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPermiso : Form
    {
        public frmPermiso()
        {
            InitializeComponent();
        }

        private void frmPermiso_Load(object sender, EventArgs e)
        {
            // Cargar roles disponibles en el ComboBox
            List<Rol> listaRol = new CN_Rol().Listar();

            foreach (Rol item in listaRol)
            {
                cborol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            cborol.DisplayMember = "Texto";
            cborol.ValueMember = "Valor";
            cborol.SelectedIndex = 0;

            // Cargar nombres de los menús en el ComboBox
            cbonombremenu.Items.Add(new OpcionCombo() { Valor = "menuusuarios", Texto = "Usuarios" });
            cbonombremenu.Items.Add(new OpcionCombo() { Valor = "menuventas", Texto = "Ventas" });
            cbonombremenu.Items.Add(new OpcionCombo() { Valor = "menucompras", Texto = "Compras" });
            cbonombremenu.Items.Add(new OpcionCombo() { Valor = "menureportes", Texto = "Reportes" });
            cbonombremenu.Items.Add(new OpcionCombo() { Valor = "menuacercade", Texto = "Acerca de" });
            cbonombremenu.DisplayMember = "Texto";
            cbonombremenu.ValueMember = "Valor";
            cbonombremenu.SelectedIndex = 0;

            // Configurar ComboBox de búsqueda
            foreach (DataGridViewColumn columna in dgvdata.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            // Cargar permisos del rol seleccionado
            CargarPermisos();
        }
        private void CargarPermisos()
        {
            // Listar todos los permisos
            List<Permiso> listaPermiso = new CN_Permiso().ListarTodos();

            // Limpiar registros existentes en el DataGridView
            dgvdata.Rows.Clear();

            // Agregar cada permiso al DataGridView
            foreach (Permiso item in listaPermiso)
            {
                dgvdata.Rows.Add(new object[] { "", item.IdPermiso, item.oRol.IdRol, item.oRol.Descripcion, item.NombreMenu });
            }
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Permiso objpermiso = new Permiso()
            {
                IdPermiso = Convert.ToInt32(txtid.Text),
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cborol.SelectedItem).Valor) },
                NombreMenu = ((OpcionCombo)cbonombremenu.SelectedItem).Valor.ToString()
            };

            if (objpermiso.IdPermiso == 0)
            {
                // Registrar nuevo permiso
                int idpermisoGenerado = new CN_Permiso().Registrar(objpermiso, out mensaje);

                if (idpermisoGenerado != 0)
                {
                    dgvdata.Rows.Add(new object[] { "", idpermisoGenerado, ((OpcionCombo)cborol.SelectedItem).Valor, ((OpcionCombo)cborol.SelectedItem).Texto, objpermiso.NombreMenu });
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                // Editar permiso existente
                bool resultado = new CN_Permiso().Editar(objpermiso, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvdata.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["Id"].Value = txtid.Text;
                    row.Cells["IdRol"].Value = ((OpcionCombo)cborol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cborol.SelectedItem).Texto.ToString();
                    row.Cells["nombremenu"].Value = objpermiso.NombreMenu;

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }

        private void Limpiar()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            cborol.SelectedIndex = 0;
            cbonombremenu.SelectedIndex = 0;
            txtpermiso.Text = "";
        }

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvdata.Rows[indice].Cells["Id"].Value.ToString();
                    cborol.SelectedValue = dgvdata.Rows[indice].Cells["IdRol"].Value;
                    txtpermiso.Text = dgvdata.Rows[indice].Cells["nombremenu"].Value.ToString();

                    foreach (OpcionCombo oc in cbonombremenu.Items)
                    {
                        if (oc.Valor.ToString() == txtpermiso.Text)
                        {
                            int indice_combo = cbonombremenu.Items.IndexOf(oc);
                            cbonombremenu.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el permiso?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Permiso objpermiso = new Permiso()
                    {
                        IdPermiso = Convert.ToInt32(txtid.Text)
                    };

                    bool respuesta = new CN_Permiso().Eliminar(objpermiso, out mensaje);

                    if (respuesta)
                    {
                        dgvdata.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    Limpiar();
                }
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString();

            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Text = "";
            foreach (DataGridViewRow row in dgvdata.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnlimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dgvdata_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check20.Width;
                var h = Properties.Resources.check20.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check20, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }
    }
}