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
