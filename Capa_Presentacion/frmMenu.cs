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
using Capa_Negocio;

namespace Capa_Presentacion
{
    public partial class frmMenu : Form
    {
        public static Usuario usuarioactual;
        int m, mx, my;

        public frmMenu(Usuario usuario)
        {
            usuarioactual = usuario;
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            ControlForm.PermisoSubMenu(ref menuSubMenus, usuarioactual);
            lblFechaHora.Text = DateTime.Now.ToString();
            lblUsuario.Text = usuarioactual.oEmpleado.NombreCompleto;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlForm.Cerrar(this, "¿Desea cerrar sesion?");
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            ControlForm.Minimizar(this);
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0;
        }
    }
}
