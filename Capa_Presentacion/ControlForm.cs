using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using Capa_Entidad;

namespace Capa_Presentacion
{
    public static class ControlForm
    {
        public static void Cerrar(Form form, string mensaje)
        {
            if (MensajePregunta(mensaje))
                form.Close();
        }
        public static void Exit(string mensaje)
        {
            if (MensajePregunta(mensaje))
                Application.Exit();
        }
        public static void AbrirMenu(Form form, Usuario usuario)
        {
            if (usuario.oRol.Descripcion == "Admin")
            {
                frmMenuAdmin newform = new frmMenuAdmin(usuario);
                form.Hide();
                newform.ShowDialog();
                form.Show();
            }
            else
            {
                frmMenuUsuario newform = new frmMenuUsuario(usuario);
                form.Hide();
                newform.ShowDialog();
                form.Show();
            }           
        }
        public static void AbrirSubMenu(ref Panel pnlSubMenu, ref IconButton MenuActivo,ref Form FormActivo, IconButton menu, Form form)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.FromArgb(115, 75, 2);
            }
            menu.BackColor = Color.FromArgb(255, 204, 102);
            MenuActivo = menu;

            if (FormActivo != null)
            {
                FormActivo.Close();
            }
            FormActivo = form;
            form.TopLevel = false;
            form.FormBorderStyle=FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.BackColor = Color.FromArgb(217, 160, 91);
            pnlSubMenu.Controls.Add(form);
            form.Show();
        }
        public static void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje,"", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public static bool MensajePregunta(string mensaje)
        {
            if (MessageBox.Show(mensaje, "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                return true;
            else
                return false;
        }
    }
}
