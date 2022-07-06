using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Capa_Entidad;

namespace Capa_Presentacion
{
    public partial class frmMenu : Form
    {
        private static Usuario usuarioactual;
        private static IconButton MenuActivo = null;
        private static Form FormActivo = null;
        public frmMenu(Usuario usuario)
        {
            usuarioactual = usuario;
            InitializeComponent();  
        }
        private void frmMenu_Load(object sender, EventArgs e)
        {
            lbUsuario.Text = usuarioactual.oEmpleado.NombreCompleto;
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            ControlForm.AbrirSubMenu(ref pnlSubMenu, ref MenuActivo, ref FormActivo, (IconButton)sender, new frmPedido());
        }

        private void btnProducto_Click(object sender, EventArgs e)
        {
            ControlForm.AbrirSubMenu(ref pnlSubMenu, ref MenuActivo, ref FormActivo,(IconButton)sender, new frmProducto());
        }

        private void btnPromocion_Click(object sender, EventArgs e)
        {

        }

        private void btnProveedor_Click(object sender, EventArgs e)
        {

        }

        private void btnIngrediente_Click(object sender, EventArgs e)
        {

        }

        private void btnUsuario_Click(object sender, EventArgs e)
        {

        }

        private void btnEmpleado_Click(object sender, EventArgs e)
        {

        }

        private void btnAcercade_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlForm.Cerrar(this, "¿Seguro que desea salir?");
        }
        private static void AbrirFormulario(IconButton menu, Form form_abrir)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.FromArgb(115, 75, 2);
            }
            menu.BackColor = Color.FromArgb(255, 204, 102);
            MenuActivo = menu;
        }
    }
}
