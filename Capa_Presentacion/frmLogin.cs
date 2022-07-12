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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            ControlForm.Exit("¿Esta seguro de salir del programa?");
        }

        private void BtnIngresar_Click(object sender, EventArgs e)
        {
            if (CN_Usuario.UsuarioValido(txtContraseña.Text, txtUsuario.Text, out Usuario usuario, out string mensaje))
            {
                frmMenu menu = new frmMenu(usuario);
                Hide();
                menu.ShowDialog();
                Limpiar();
                Show();
            }
            else
                ControlForm.MensajeError(mensaje);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/nativeschacha");
        }
        public void Limpiar()
        {
            txtContraseña.Clear();
            txtUsuario.Clear();
            txtContraseña.Focus();
        }
    }
}
