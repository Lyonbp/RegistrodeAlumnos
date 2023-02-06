using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using CrystalDecisions.CrystalReports.Engine;


namespace CRUD
{
    public partial class txtFN : Form
    {
        public txtFN()
        {
            InitializeComponent();
        }

        //------TRAER LOS DATOS A LA TABLA----------//
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Conexion.Conectar();
                dataGridView1.DataSource = llenar_grid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("a ocurrido un error:\n" + ex);
            }
            
            
        }

        //------TRAER LOS DATOS A LA TABLA----------//

        public DataTable llenar_grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string Consulta = "SELECT * FROM tb_alumno";
            SqlCommand cmd = new SqlCommand(Consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        //---------------------AGREGAR DATOS A LA BASE DE DATOS-------------//

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "INSERT INTO tb_alumno(codigo, primer_nombre,segundo_nombre,primer_apellido,segundo_apellido,telefono, celular,direccion,email,fecha_nacimiento,observaciones) values" +
                "(@codigo,@primer_nombre,@segundo_nombre,@primer_apellido,@segundo_apellido,@telefono, @celular,@direccion,@email,@fecha_nacimiento,@observaciones)";
            SqlCommand cmd1 = new SqlCommand(insertar, Conexion.Conectar());

            try
            {
                //separar nombres y apellidos
                string Noms = txtNombre.Text;
                string Nom1 = string.Join(" ", Noms.Split(' ').Skip(0).Take(1).ToArray());
                string Nom2 = string.Join(" ", Noms.Split(' ').Skip(1).Take(1).ToArray());
                string apes = txtApellido.Text;
                string ape1 = string.Join(" ", apes.Split(' ').Skip(0).Take(1).ToArray());
                string ape2 = string.Join(" ", apes.Split(' ').Skip(1).Take(1).ToArray());

                //Ingreso de los datos
                cmd1.Parameters.AddWithValue("@codigo", txtId.Text);
                cmd1.Parameters.AddWithValue("@primer_nombre", Nom1);
                cmd1.Parameters.AddWithValue("@segundo_nombre", Nom2);
                cmd1.Parameters.AddWithValue("@primer_apellido", ape1);
                cmd1.Parameters.AddWithValue("@segundo_apellido", ape2);
                cmd1.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd1.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd1.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd1.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd1.Parameters.Add(new SqlParameter("@fecha_nacimiento", SqlDbType.DateTime)).Value = dateTimePicker1.Value.Date;
                cmd1.Parameters.AddWithValue("@observaciones", txtObs.Text);

                cmd1.ExecuteNonQuery();

                MessageBox.Show("Los datos fueron agregados exitosamente");
                Limpiar();
                dataGridView1.DataSource = llenar_grid();
            }
            catch(Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }

        //--------------ELIMINAR DADOS DE LA BASE DE DATOS----------//

        private void button4_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            try
            {
                string Eliminar = "delete from tb_alumno where codigo= @codigo";
                SqlCommand cmd2 = new SqlCommand(Eliminar, Conexion.Conectar());
                cmd2.Parameters.AddWithValue("@codigo", txtId.Text);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Los datos fueron eliminados con exito");
                Limpiar();
                dataGridView1.DataSource = llenar_grid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }



        //-------LLENAR LOS txt CON LA COLUMNA SELECCIONADA-------//

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string Nom1 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string Nom2 = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtNombre.Text = Nom1+" "+Nom2;
                string Ape1 = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string Ape2 = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtApellido.Text = Ape1 + " " + Ape2;
                txtTelefono.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtCelular.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                txtDireccion.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                txtEmail.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                txtObs.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }


        //-------------------ACTUALIZAR DATOS DE LA BASE DE DATOS----------------//
        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string Actualizar = "UPDATE tb_alumno SET primer_nombre = @primer_nombre,segundo_nombre = @segundo_nombre,primer_apellido = @primer_apellido,segundo_apellido = @segundo_apellido," +
                "telefono=@telefono , celular=@celular , direccion=@direccion , email=@email , fecha_nacimiento=@fecha_nacimiento , observaciones=@observaciones" +
                " where codigo = @codigo";
            try
            {
                SqlCommand cmd3 = new SqlCommand(Actualizar, Conexion.Conectar());
                string Noms = txtNombre.Text;
                string Nom1 = string.Join(" ", Noms.Split(' ').Skip(0).Take(1).ToArray());
                string Nom2 = string.Join(" ", Noms.Split(' ').Skip(1).Take(2).ToArray());
                string apes = txtApellido.Text;
                string ape1 = string.Join(" ", apes.Split(' ').Skip(0).Take(1).ToArray());
                string ape2 = string.Join(" ", apes.Split(' ').Skip(1).Take(1).ToArray());
                cmd3.Parameters.AddWithValue("@codigo", txtId.Text);
                cmd3.Parameters.AddWithValue("@primer_nombre", Nom1);
                cmd3.Parameters.AddWithValue("@segundo_nombre", Nom2);
                cmd3.Parameters.AddWithValue("@primer_apellido", ape1);
                cmd3.Parameters.AddWithValue("@segundo_apellido", ape2);
                cmd3.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                cmd3.Parameters.AddWithValue("@celular", txtCelular.Text);
                cmd3.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                cmd3.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd3.Parameters.Add(new SqlParameter("@fecha_nacimiento", SqlDbType.DateTime)).Value = dateTimePicker1.Value.Date;
                cmd3.Parameters.AddWithValue("@observaciones", txtObs.Text);

                cmd3.ExecuteNonQuery();
                MessageBox.Show("Los datos fueron actualizados con exito");
                Limpiar();
                dataGridView1.DataSource = llenar_grid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("error" + ex.Message);
            }
        }



        //-------------------LIMPIAR LOS txt ------------------//
        public void Limpiar()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txtObs.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        //-------------------CERRAR EL PROGRAMA-----------------//
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //---------CREAR REPORTE--------//
        private void button6_Click(object sender, EventArgs e)
        {
            Form3 ver = new Form3();
            ver.Show();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("Solo Números", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
