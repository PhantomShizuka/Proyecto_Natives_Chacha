using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Entidad;
using FontAwesome.Sharp;
using Capa_Negocio;

namespace Capa_Presentacion
{
    public partial class frmMenu : Form
    {
        public static Usuario usuarioactual;
        IconMenuItem SubMenuActivo = null;
        Form FormActivo = null;
        int m, mx, my;

        public frmMenu(Usuario usuario)
        {
            usuarioactual = usuario;
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            //Permisos SubMenus
            foreach (IconMenuItem SubMenu in menuSubMenus.Items)
                if (!usuarioactual.oRol.oListaPermiso.Any(m => ("btn" + m.NombreMenu) == SubMenu.Name))
                    SubMenu.Visible = false;

            //Mostrar hora ingreso y nombre del usuario actual
            lblFechaHora.Text = DateTime.Now.ToString();
            lblUsuario.Text = usuarioactual.oEmpleado.NombreCompleto;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlForm.Cerrar(this, "¿Desea cerrar sesion?");
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1;
            mx = e.X;
            my = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my);
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmPedido());
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmProducto());
        }

        private void btnPromociones_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmPromocion());
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmProveedor());
        }

        private void btnInsumos_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmInsumo());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmUsuario());
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmEmpleado());
        }

        private void btnAcercade_Click(object sender, EventArgs e)
        {
            AbrirSubMenu((IconMenuItem)sender, new frmAcercade());
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }
        public void AbrirSubMenu(IconMenuItem submenu, Form form)
        {
            if (SubMenuActivo != null)
                SubMenuActivo.BackColor = Color.FromArgb(115, 75, 2);

            submenu.BackColor = Color.FromArgb(255, 204, 102);
            SubMenuActivo = submenu;

            if (FormActivo != null)
                FormActivo.Close();

            FormActivo = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.BackColor = Color.FromArgb(217, 160, 91);
            pnlSubMenu.Controls.Add(form);
            form.Show();
        }
    }
}
