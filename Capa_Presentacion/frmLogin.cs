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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            ControlForm.Exit("¿Esta seguro de salir del programa?");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/nativeschacha");
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (new CN_Usuario().UsuarioValido(txtContraseña.Text, out Usuario usuarioactual))
            {
                ControlForm.AbrirMenu(this, usuarioactual);
            }
                
        }
    }
}
