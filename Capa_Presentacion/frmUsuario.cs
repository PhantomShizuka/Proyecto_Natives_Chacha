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
    public partial class frmUsuario : Form
    {
        int idactual = 0, indice = -1;

        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            cbEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cbEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });
            
            cbEstado.DisplayMember = "Texto";
            cbEstado.ValueMember = "Valor";
            cbEstado.SelectedIndex = 0;

            foreach (Rol item in CN_Rol.Listar)
                cbRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });

            cbRol.DisplayMember = "Texto";
            cbRol.ValueMember = "Valor";
            cbRol.SelectedIndex = 0;

            foreach (Empleado item in CN_Empleado.EmpleadoSinUsuario())
                cbDocumento.Items.Add(new OpcionCombo() { Valor = item.IdEmpleado, Texto = item.Documento });
            
            if (cbDocumento.Items.Count == 0)
                cbDocumento.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Crea un nuevo empleado" });

            cbDocumento.DisplayMember = "Texto";
            cbDocumento.ValueMember = "Valor";
            cbDocumento.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvdata.Columns)
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                    cbobusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });

            cbobusqueda.DisplayMember = "Texto";
            cbobusqueda.ValueMember = "Valor";
            cbobusqueda.SelectedIndex = 0;

            foreach (Usuario item in CN_Usuario.Listar)
                dgvdata.Rows.Add(new object[]
                {
                    "",
                    item.IdUsuario,
                    item.oEmpleado.IdEmpleado,
                    item.oEmpleado.Documento,
                    item.oEmpleado.NombreCompleto,
                    item.oRol.Descripcion,
                    item.Estado== true ? "Activo" : "No Activo",
                    item.Contraseña
                });
        }

        private void btnguardar_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario()
            {
                IdUsuario = idactual,
                oEmpleado = CN_Empleado.Listar.FirstOrDefault(em => em.IdEmpleado == Convert.ToInt32(((OpcionCombo)cbDocumento.SelectedItem).Valor)), 
                oRol = CN_Rol.Listar.FirstOrDefault(r => r.IdRol == Convert.ToInt32(((OpcionCombo)cbRol.SelectedItem).Valor)),
                Contraseña = txtContraseña.Text,                
                Estado = Convert.ToInt32(((OpcionCombo)cbEstado.SelectedItem).Valor) == 1
            };

            if (usuario.IdUsuario == 0)
            {
                usuario.IdUsuario = CN_Usuario.Registrar(usuario, out string mensaje);

                if (usuario.IdUsuario != 0)
                {
                    dgvdata.Rows.Add(new object[]
                    {
                        "",
                        usuario.IdUsuario,
                        usuario.oEmpleado.IdEmpleado,
                        usuario.oEmpleado.Documento,
                        usuario.oEmpleado.NombreCompleto,
                        usuario.Estado == true ? "Activo" : "No Activo",
                        usuario.oRol.Descripcion,
                        usuario.Contraseña
                    });
                    Limpiar();
                }
                else
                    MessageBox.Show(mensaje);
            }
            else
            {
                DataGridViewRow row = dgvdata.Rows[indice];

                if (usuario.Contraseña == "")
                    usuario.Contraseña = row.Cells["Contraseña"].Value.ToString();

                if (CN_Usuario.Editar(usuario, out string mensaje) && indice >= 0)
                {
                    row.Cells["IdUsuario"].Value = usuario.IdUsuario;
                    row.Cells["IdEmpleado"].Value = usuario.oEmpleado.IdEmpleado;
                    row.Cells["Usuario"].Value = usuario.oEmpleado.Documento;
                    row.Cells["NombreCompleto"].Value = usuario.oEmpleado.NombreCompleto;
                    row.Cells["EstadoUsuario"].Value = usuario.Estado == true ? "Activo" : "No Activo";
                    row.Cells["Rol"].Value = usuario.oRol.Descripcion;
                    row.Cells["Contraseña"].Value = usuario.Contraseña;
                    Limpiar();
                }
                else
                    MessageBox.Show(mensaje);
            }
        }    

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if(indice >= 0)
            {
                if (idactual != 0 && ControlForm.MensajePregunta("¿Esta seguro de eliminar el usuario?"))
                {
                    Usuario usuario = new Usuario() { IdUsuario = idactual };

                    if (CN_Usuario.Eliminar(usuario, out string mensaje))
                        dgvdata.Rows.RemoveAt(indice);
                    else
                        ControlForm.MensajeError(mensaje);

                    Limpiar();
                }
            }
            else
                ControlForm.MensajeError("Seleccione un usuario para eliminar");
        }
        private void btnlimpiar_Click(object sender, EventArgs e) => Limpiar();

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
        private void cbDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empleado empleado = CN_Empleado.Listar.FirstOrDefault(a => a.Documento == cbDocumento.Text);

            if (empleado != null)
            {
                txtNombreCompleto.Text = empleado.NombreCompleto;
                txtTelefono.Text = empleado.Telefono;
                txtCorreo.Text = empleado.Correo;
                idactual = 0;
            }

            Usuario usuario = CN_Usuario.Listar.FirstOrDefault(u => u.oEmpleado.Documento == cbDocumento.Text);

            if (usuario != null)
            {
                txtNombreCompleto.Text = usuario.oEmpleado.NombreCompleto;
                txtTelefono.Text = usuario.oEmpleado.Telefono;
                txtCorreo.Text = usuario.oEmpleado.Correo;
                idactual = usuario.IdUsuario;
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
                txtContraseña.Clear();

                indice = e.RowIndex;
                idactual = Convert.ToInt32(dgvdata.Rows[e.RowIndex].Cells["IdUsuario"].Value);

                cbDocumento.Items.Clear();

                foreach (Empleado item in CN_Empleado.EmpleadoSinUsuario())
                    cbDocumento.Items.Add(new OpcionCombo() { Valor = item.IdEmpleado, Texto = item.Documento });

                Empleado empleado = CN_Empleado.Listar.FirstOrDefault(a => a.IdEmpleado == (int)dgvdata.Rows[e.RowIndex].Cells["IdEmpleado"].Value);
                cbDocumento.Items.Add(new OpcionCombo() { Valor = empleado.IdEmpleado, Texto = empleado.Documento });

                cbDocumento.Text = empleado.Documento;
                txtNombreCompleto.Text = empleado.NombreCompleto;
                txtTelefono.Text = empleado.Telefono;
                txtCorreo.Text = empleado.Correo;

                foreach (OpcionCombo oc in cbEstado.Items)
                {
                    if (oc.Texto == (dgvdata.Rows[e.RowIndex].Cells["EstadoUsuario"].Value).ToString())
                    {
                        cbEstado.SelectedIndex = cbEstado.Items.IndexOf(oc);
                        break;
                    }
                }

                foreach (OpcionCombo oc in cbRol.Items)
                {
                    if (oc.Texto == (dgvdata.Rows[e.RowIndex].Cells["Rol"].Value).ToString())
                    {
                        cbRol.SelectedIndex = cbRol.Items.IndexOf(oc);
                        break;
                    }
                }
            }
        }

        private void Limpiar()
        {
            cbDocumento.Items.Clear();
            indice = -1;
            idactual = 0;
            cbEstado.SelectedIndex = 0;
            txtNombreCompleto.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtContraseña.Clear();
            cbRol.SelectedIndex = 0;

            foreach (Empleado item in CN_Empleado.EmpleadoSinUsuario())
                cbDocumento.Items.Add(new OpcionCombo() { Valor = item.IdEmpleado, Texto = item.Documento });

            if (cbDocumento.Items.Count == 0)
                cbDocumento.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Crea un nuevo empleado" });

            
            cbDocumento.SelectedIndex = 0;
            cbDocumento.Select();
        }
    }
}
