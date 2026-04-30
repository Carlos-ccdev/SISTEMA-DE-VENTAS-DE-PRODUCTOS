using System.Drawing;

namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.menutitulo = new System.Windows.Forms.MenuStrip();
            this.contenedor = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuusuarios = new FontAwesome.Sharp.IconMenuItem();
            this.submenuusuario = new FontAwesome.Sharp.IconMenuItem();
            this.submenurol = new FontAwesome.Sharp.IconMenuItem();
            this.submenupermiso = new FontAwesome.Sharp.IconMenuItem();
            this.submenunegocio = new FontAwesome.Sharp.IconMenuItem();
            this.menucompras = new FontAwesome.Sharp.IconMenuItem();
            this.submenuproveedores = new FontAwesome.Sharp.IconMenuItem();
            this.submenucategoria = new FontAwesome.Sharp.IconMenuItem();
            this.submenuproductos = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarcompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuventas = new FontAwesome.Sharp.IconMenuItem();
            this.submenuclientes = new FontAwesome.Sharp.IconMenuItem();
            this.submenuregistrarventa = new FontAwesome.Sharp.IconMenuItem();
            this.submenupago = new FontAwesome.Sharp.IconMenuItem();
            this.menureportes = new FontAwesome.Sharp.IconMenuItem();
            this.submenureportecompras = new System.Windows.Forms.ToolStripMenuItem();
            this.submenureporteventas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuacercade = new FontAwesome.Sharp.IconMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lblusuario = new System.Windows.Forms.Label();
            this.btnsalir = new FontAwesome.Sharp.IconButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.paneltoptitulo = new System.Windows.Forms.Panel();
            this.panelmenulateral = new System.Windows.Forms.Panel();
            this.panelmenu = new System.Windows.Forms.Panel();
            this.panelcontenedor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.paneltoptitulo.SuspendLayout();
            this.panelmenu.SuspendLayout();
            this.panelcontenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // menutitulo
            // 
            this.menutitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menutitulo.AutoSize = false;
            this.menutitulo.BackColor = System.Drawing.Color.DodgerBlue;
            this.menutitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.menutitulo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menutitulo.Location = new System.Drawing.Point(0, 0);
            this.menutitulo.Name = "menutitulo";
            this.menutitulo.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menutitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menutitulo.Size = new System.Drawing.Size(1782, 100);
            this.menutitulo.TabIndex = 1;
            this.menutitulo.Text = "menuStrip2";
            // 
            // contenedor
            // 
            this.contenedor.BackColor = System.Drawing.SystemColors.Control;
            this.contenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contenedor.Location = new System.Drawing.Point(0, 0);
            this.contenedor.Margin = new System.Windows.Forms.Padding(5);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(1595, 653);
            this.contenedor.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DodgerBlue;
            this.pictureBox2.Image = global::CapaPresentacion.Properties.Resources.logoenblancocabecera1;
            this.pictureBox2.Location = new System.Drawing.Point(83, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(388, 132);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.White;
            this.menu.Dock = System.Windows.Forms.DockStyle.Left;
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuusuarios,
            this.menucompras,
            this.menuventas,
            this.menureportes,
            this.menuacercade});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.menu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menu.Size = new System.Drawing.Size(135, 653);
            this.menu.TabIndex = 4;
            this.menu.Text = "menuStrip1";
            // 
            // menuusuarios
            // 
            this.menuusuarios.AutoSize = false;
            this.menuusuarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuusuario,
            this.submenurol,
            this.submenupermiso,
            this.submenunegocio});
            this.menuusuarios.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.menuusuarios.IconColor = System.Drawing.Color.DodgerBlue;
            this.menuusuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuusuarios.IconSize = 50;
            this.menuusuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuusuarios.Margin = new System.Windows.Forms.Padding(0, 50, 0, 0);
            this.menuusuarios.Name = "menuusuarios";
            this.menuusuarios.Size = new System.Drawing.Size(135, 69);
            this.menuusuarios.Text = "Administrador";
            this.menuusuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuusuario
            // 
            this.submenuusuario.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuusuario.IconColor = System.Drawing.Color.Black;
            this.submenuusuario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuusuario.Name = "submenuusuario";
            this.submenuusuario.Size = new System.Drawing.Size(149, 26);
            this.submenuusuario.Text = "Usuario";
            this.submenuusuario.Click += new System.EventHandler(this.submenuusuario_Click);
            // 
            // submenurol
            // 
            this.submenurol.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenurol.IconColor = System.Drawing.Color.Black;
            this.submenurol.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenurol.Name = "submenurol";
            this.submenurol.Size = new System.Drawing.Size(149, 26);
            this.submenurol.Text = "Rol";
            this.submenurol.Click += new System.EventHandler(this.submenurol_Click);
            // 
            // submenupermiso
            // 
            this.submenupermiso.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenupermiso.IconColor = System.Drawing.Color.Black;
            this.submenupermiso.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenupermiso.Name = "submenupermiso";
            this.submenupermiso.Size = new System.Drawing.Size(149, 26);
            this.submenupermiso.Text = "Permiso";
            this.submenupermiso.Click += new System.EventHandler(this.submenupermiso_Click);
            // 
            // submenunegocio
            // 
            this.submenunegocio.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenunegocio.IconColor = System.Drawing.Color.Black;
            this.submenunegocio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenunegocio.Name = "submenunegocio";
            this.submenunegocio.Size = new System.Drawing.Size(149, 26);
            this.submenunegocio.Text = "Negocio";
            this.submenunegocio.Click += new System.EventHandler(this.submenunegocio_Click);
            // 
            // menucompras
            // 
            this.menucompras.AutoSize = false;
            this.menucompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuproveedores,
            this.submenucategoria,
            this.submenuproductos,
            this.submenuregistrarcompra});
            this.menucompras.IconChar = FontAwesome.Sharp.IconChar.Dolly;
            this.menucompras.IconColor = System.Drawing.Color.DodgerBlue;
            this.menucompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menucompras.IconSize = 50;
            this.menucompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menucompras.Name = "menucompras";
            this.menucompras.Size = new System.Drawing.Size(90, 69);
            this.menucompras.Text = "Compras";
            this.menucompras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuproveedores
            // 
            this.submenuproveedores.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuproveedores.IconColor = System.Drawing.Color.Black;
            this.submenuproveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuproveedores.Name = "submenuproveedores";
            this.submenuproveedores.Size = new System.Drawing.Size(174, 26);
            this.submenuproveedores.Text = "Proveedores";
            this.submenuproveedores.Click += new System.EventHandler(this.submenuproveedores_Click);
            // 
            // submenucategoria
            // 
            this.submenucategoria.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenucategoria.IconColor = System.Drawing.Color.Black;
            this.submenucategoria.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenucategoria.Name = "submenucategoria";
            this.submenucategoria.Size = new System.Drawing.Size(174, 26);
            this.submenucategoria.Text = "Categoria";
            this.submenucategoria.Click += new System.EventHandler(this.submenucategoria_Click);
            // 
            // submenuproductos
            // 
            this.submenuproductos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuproductos.IconColor = System.Drawing.Color.Black;
            this.submenuproductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuproductos.Name = "submenuproductos";
            this.submenuproductos.Size = new System.Drawing.Size(174, 26);
            this.submenuproductos.Text = "Productos";
            this.submenuproductos.Click += new System.EventHandler(this.submenuproductos_Click);
            // 
            // submenuregistrarcompra
            // 
            this.submenuregistrarcompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuregistrarcompra.IconColor = System.Drawing.Color.Black;
            this.submenuregistrarcompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuregistrarcompra.Name = "submenuregistrarcompra";
            this.submenuregistrarcompra.Size = new System.Drawing.Size(174, 26);
            this.submenuregistrarcompra.Text = "Registrar";
            this.submenuregistrarcompra.Click += new System.EventHandler(this.submenuregistrarcompra_Click);
            // 
            // menuventas
            // 
            this.menuventas.AutoSize = false;
            this.menuventas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenuclientes,
            this.submenuregistrarventa,
            this.submenupago});
            this.menuventas.IconChar = FontAwesome.Sharp.IconChar.Tasks;
            this.menuventas.IconColor = System.Drawing.Color.DodgerBlue;
            this.menuventas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuventas.IconSize = 50;
            this.menuventas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuventas.Name = "menuventas";
            this.menuventas.Size = new System.Drawing.Size(90, 69);
            this.menuventas.Text = "Ventas";
            this.menuventas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenuclientes
            // 
            this.submenuclientes.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuclientes.IconColor = System.Drawing.Color.Black;
            this.submenuclientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuclientes.Name = "submenuclientes";
            this.submenuclientes.Size = new System.Drawing.Size(151, 26);
            this.submenuclientes.Text = "Clientes";
            this.submenuclientes.Click += new System.EventHandler(this.submenuclientes_Click);
            // 
            // submenuregistrarventa
            // 
            this.submenuregistrarventa.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenuregistrarventa.IconColor = System.Drawing.Color.Black;
            this.submenuregistrarventa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenuregistrarventa.Name = "submenuregistrarventa";
            this.submenuregistrarventa.Size = new System.Drawing.Size(151, 26);
            this.submenuregistrarventa.Text = "Registrar";
            this.submenuregistrarventa.Click += new System.EventHandler(this.submenuregistrarventa_Click);
            // 
            // submenupago
            // 
            this.submenupago.IconChar = FontAwesome.Sharp.IconChar.None;
            this.submenupago.IconColor = System.Drawing.Color.Black;
            this.submenupago.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.submenupago.Name = "submenupago";
            this.submenupago.Size = new System.Drawing.Size(151, 26);
            this.submenupago.Text = "QR";
            this.submenupago.Click += new System.EventHandler(this.submenupago_Click);
            // 
            // menureportes
            // 
            this.menureportes.AutoSize = false;
            this.menureportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.submenureportecompras,
            this.submenureporteventas});
            this.menureportes.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.menureportes.IconColor = System.Drawing.Color.DodgerBlue;
            this.menureportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menureportes.IconSize = 50;
            this.menureportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menureportes.Name = "menureportes";
            this.menureportes.Size = new System.Drawing.Size(90, 69);
            this.menureportes.Text = "Reportes";
            this.menureportes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // submenureportecompras
            // 
            this.submenureportecompras.Name = "submenureportecompras";
            this.submenureportecompras.Size = new System.Drawing.Size(208, 26);
            this.submenureportecompras.Text = "Reporte Compras";
            this.submenureportecompras.Click += new System.EventHandler(this.submenureportecompras_Click);
            // 
            // submenureporteventas
            // 
            this.submenureporteventas.Name = "submenureporteventas";
            this.submenureporteventas.Size = new System.Drawing.Size(208, 26);
            this.submenureporteventas.Text = "Reporte Ventas";
            this.submenureporteventas.Click += new System.EventHandler(this.submenureporteventas_Click);
            // 
            // menuacercade
            // 
            this.menuacercade.AutoSize = false;
            this.menuacercade.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.menuacercade.IconColor = System.Drawing.Color.DodgerBlue;
            this.menuacercade.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuacercade.IconSize = 50;
            this.menuacercade.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuacercade.Name = "menuacercade";
            this.menuacercade.Size = new System.Drawing.Size(100, 69);
            this.menuacercade.Text = "Acerca de";
            this.menuacercade.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.menuacercade.Click += new System.EventHandler(this.menuacercade_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DodgerBlue;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1265, 41);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Usuario:";
            // 
            // lblusuario
            // 
            this.lblusuario.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblusuario.AutoSize = true;
            this.lblusuario.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusuario.ForeColor = System.Drawing.Color.White;
            this.lblusuario.Location = new System.Drawing.Point(1351, 41);
            this.lblusuario.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(81, 20);
            this.lblusuario.TabIndex = 5;
            this.lblusuario.Text = "lblusuario";
            // 
            // btnsalir
            // 
            this.btnsalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnsalir.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnsalir.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnsalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsalir.IconChar = FontAwesome.Sharp.IconChar.Directions;
            this.btnsalir.IconColor = System.Drawing.Color.White;
            this.btnsalir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnsalir.IconSize = 52;
            this.btnsalir.Location = new System.Drawing.Point(1547, 23);
            this.btnsalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnsalir.Name = "btnsalir";
            this.btnsalir.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.btnsalir.Size = new System.Drawing.Size(79, 63);
            this.btnsalir.TabIndex = 6;
            this.btnsalir.UseVisualStyleBackColor = false;
            this.btnsalir.Click += new System.EventHandler(this.btnsalir_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1547, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(81, 62);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // paneltoptitulo
            // 
            this.paneltoptitulo.Controls.Add(this.pictureBox2);
            this.paneltoptitulo.Controls.Add(this.btnsalir);
            this.paneltoptitulo.Controls.Add(this.lblusuario);
            this.paneltoptitulo.Controls.Add(this.label2);
            this.paneltoptitulo.Controls.Add(this.menutitulo);
            this.paneltoptitulo.Controls.Add(this.pictureBox1);
            this.paneltoptitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltoptitulo.Location = new System.Drawing.Point(0, 0);
            this.paneltoptitulo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.paneltoptitulo.Name = "paneltoptitulo";
            this.paneltoptitulo.Size = new System.Drawing.Size(1782, 100);
            this.paneltoptitulo.TabIndex = 4;
            // 
            // panelmenulateral
            // 
            this.panelmenulateral.Location = new System.Drawing.Point(3, 2);
            this.panelmenulateral.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelmenulateral.Name = "panelmenulateral";
            this.panelmenulateral.Size = new System.Drawing.Size(155, 654);
            this.panelmenulateral.TabIndex = 5;
            // 
            // panelmenu
            // 
            this.panelmenu.BackColor = System.Drawing.Color.DodgerBlue;
            this.panelmenu.Controls.Add(this.menu);
            this.panelmenu.Controls.Add(this.panelmenulateral);
            this.panelmenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelmenu.Location = new System.Drawing.Point(0, 100);
            this.panelmenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelmenu.Name = "panelmenu";
            this.panelmenu.Size = new System.Drawing.Size(187, 653);
            this.panelmenu.TabIndex = 5;
            // 
            // panelcontenedor
            // 
            this.panelcontenedor.Controls.Add(this.contenedor);
            this.panelcontenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelcontenedor.Location = new System.Drawing.Point(187, 100);
            this.panelcontenedor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelcontenedor.Name = "panelcontenedor";
            this.panelcontenedor.Size = new System.Drawing.Size(1595, 653);
            this.panelcontenedor.TabIndex = 6;
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1782, 753);
            this.Controls.Add(this.panelcontenedor);
            this.Controls.Add(this.panelmenu);
            this.Controls.Add(this.paneltoptitulo);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Inicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.paneltoptitulo.ResumeLayout(false);
            this.paneltoptitulo.PerformLayout();
            this.panelmenu.ResumeLayout(false);
            this.panelmenu.PerformLayout();
            this.panelcontenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.MenuStrip menutitulo;
        private System.Windows.Forms.Panel contenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblusuario;
        private FontAwesome.Sharp.IconButton btnsalir;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menu;
        private FontAwesome.Sharp.IconMenuItem menuusuarios;
        private FontAwesome.Sharp.IconMenuItem submenuusuario;
        private FontAwesome.Sharp.IconMenuItem submenurol;
        private FontAwesome.Sharp.IconMenuItem submenupermiso;
        private FontAwesome.Sharp.IconMenuItem submenunegocio;
        private FontAwesome.Sharp.IconMenuItem menucompras;
        private FontAwesome.Sharp.IconMenuItem submenuproveedores;
        private FontAwesome.Sharp.IconMenuItem submenucategoria;
        private FontAwesome.Sharp.IconMenuItem submenuproductos;
        private FontAwesome.Sharp.IconMenuItem submenuregistrarcompra;
        private FontAwesome.Sharp.IconMenuItem menuventas;
        private FontAwesome.Sharp.IconMenuItem submenuclientes;
        private FontAwesome.Sharp.IconMenuItem submenuregistrarventa;
        private FontAwesome.Sharp.IconMenuItem submenupago;
        private FontAwesome.Sharp.IconMenuItem menureportes;
        private System.Windows.Forms.ToolStripMenuItem submenureportecompras;
        private System.Windows.Forms.ToolStripMenuItem submenureporteventas;
        private FontAwesome.Sharp.IconMenuItem menuacercade;
        private System.Windows.Forms.Panel paneltoptitulo;
        private System.Windows.Forms.Panel panelmenulateral;
        private System.Windows.Forms.Panel panelmenu;
        private System.Windows.Forms.Panel panelcontenedor;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

