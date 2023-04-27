using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLBCS
{
    public partial class flights : Form
    {
        public string conf = System.Configuration.ConfigurationManager.AppSettings["conf"];
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        public static string SetValueForText3 = "";
        public static string SetValueForText4 = "";
        public flights()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("SELECT   [flightno],[flightdatetime]  FROM [Imigration].[dbo].[Flights] where [flightno] like '%" + textBox1.Text + "%' or  [flightdatetime] like '%" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public string TheValue3
        {
            get { return SetValueForText3; }
        }
        public string TheValue4
        {
            get { return SetValueForText4; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void flights_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imigrationDataSet2.Flights' table. You can move, or remove it, as needed.
            this.flightsTableAdapter.Fill(this.imigrationDataSet2.Flights);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                //DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                SetValueForText3 = dataGridView1[1, e.RowIndex].Value.ToString();
                SetValueForText4 = dataGridView1[0, e.RowIndex].Value.ToString();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                //DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                SetValueForText3 = dataGridView1[1, e.RowIndex].Value.ToString();
                SetValueForText4 = dataGridView1[0, e.RowIndex].Value.ToString();
            }
        }
    }
}
