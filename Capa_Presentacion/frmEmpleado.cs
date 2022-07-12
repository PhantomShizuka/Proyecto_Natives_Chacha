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
    public partial class frmEmpleado : Form
    {
        int idactual = 0, indice = -1;

        public frmEmpleado()
        {
            InitializeComponent();
        }

        private void frmEmplado_Load(object sender, EventArgs e)
        {
            cboestado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboestado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            cboestado.DisplayMember = "Texto";
            cboestado.ValueMember = "Valor";
            cboestado.SelectedIndex = 0;

            Limpiar();

            foreach (DataGridViewColumn columna in dgvdata.Columns)
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            foreach (Empleado item in CN_Empleado.Listar)
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdEmpleado,
                    item.Documento,
                    item.NombreCompleto,
                    item.Telefono,
                    item.Correo,
                    item.Estado== true ? "Activo" : "No Activo"
                });
        }
        
        private void btnguardar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado()
            {
                IdEmpleado = idactual,
                Documento = txtdocumento.Text,
                NombreCompleto = txtnombrecompleto.Text,
                Telefono = txttelefono.Text,
                Correo = txtcorreo.Text,
                Estado = Convert.ToInt32(((OpcionCombo)cboestado.SelectedItem).Valor) == 1
            };

            if (empleado.IdEmpleado == 0)
            {
                empleado.IdEmpleado = CN_Empleado.Registrar(empleado, out string mensaje);

                if (empleado.IdEmpleado != 0)
                {
                    dgvdata.Rows.Add(new object[] {
                        "",
                        empleado.IdEmpleado,
                        empleado.Documento,
                        empleado.NombreCompleto,
                        empleado.Telefono,
                        empleado.Correo,
                        empleado.Estado == true ? "Activo" : "No Activo"
                    });
                    Limpiar();
                }
                else
                    MessageBox.Show(mensaje);
            }
            else
            {
                if (CN_Empleado.Editar(empleado, out string mensaje) && indice>=0)
                {
                    DataGridViewRow row = dgvdata.Rows[indice];
                    row.Cells["Id"].Value = empleado.IdEmpleado;
                    row.Cells["Documento"].Value = empleado.Documento;
                    row.Cells["NombreCompleto"].Value = empleado.NombreCompleto;
                    row.Cells["Telefono"].Value = empleado.Telefono;
                    row.Cells["Correo"].Value = empleado.Correo;
                    row.Cells["Estado"].Value = empleado.Estado == true ? "Activo" : "No Activo";
                    Limpiar();
                }
                else
                    MessageBox.Show(mensaje);
            }      
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

        private void dgvdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvdata.Columns[e.ColumnIndex].Name == "btnseleccionar" && e.RowIndex >= 0)
            {
                indice = e.RowIndex;
                idactual = Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["Id"].Value);
                txtdocumento.Text = dgvdata.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
                txtnombrecompleto.Text = dgvdata.Rows[e.RowIndex].Cells["NombreCompleto"].Value.ToString();
                txttelefono.Text = dgvdata.Rows[e.RowIndex].Cells["Telefono"].Value.ToString();
                txtcorreo.Text = dgvdata.Rows[e.RowIndex].Cells["Correo"].Value.ToString();

                foreach (OpcionCombo oc in cboestado.Items)
                    if (oc.Texto == (dgvdata.Rows[e.RowIndex].Cells["Estado"].Value).ToString())
                    {
                        cboestado.SelectedIndex = cboestado.Items.IndexOf(oc);
                        break;
                    }
            }
        }
       
        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (idactual != 0 && ControlForm.MensajePregunta("¿Desea eliminar el usuario"))
            {
                Empleado empleado = new Empleado() { IdEmpleado = idactual };

                if (CN_Empleado.Eliminar(empleado, out string mensaje))
                    dgvdata.Rows.RemoveAt(indice);
                else
                    ControlForm.MensajeError(mensaje);
            }
            Limpiar();
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (dgvdata.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvdata.Rows)
                {
                    if (row.Cells[((OpcionCombo)cbobusqueda.SelectedItem).Valor.ToString()].Value.ToString().Trim().ToUpper().Contains(txtbusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;
                }
            }
        }

        private void btnlimpiarbuscador_Click(object sender, EventArgs e)
        {
            txtbusqueda.Clear();

            foreach (DataGridViewRow row in dgvdata.Rows)
                row.Visible = true;
        }

        private void btnlimpiar_Click(object sender, EventArgs e) => Limpiar();

        private void Limpiar()
        {
            indice = -1;
            idactual = 0;
            txtdocumento.Clear();
            txtnombrecompleto.Clear();
            txttelefono.Clear();
            txtcorreo.Clear();
            cboestado.SelectedIndex = 0;
            txtdocumento.Select();          
        }
    }
}
