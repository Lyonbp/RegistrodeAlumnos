using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'DataSet1.tb_alumno' Puede moverla o quitarla según sea necesario.
            this.tb_alumnoTableAdapter.Fill(this.DataSet1.tb_alumno);

            this.reportViewer1.RefreshReport();
        }
    }
}
