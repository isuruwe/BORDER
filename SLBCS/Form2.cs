using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace SLBCS
{
    public partial class loc : Form
    {
        public static string SetValueForText1 = "";
        public static string SetValueForText2 = "";
        public string conf = System.Configuration.ConfigurationManager.AppSettings["conf"];
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;

        public loc()
        {
            InitializeComponent();
        }
        public string TheValue
        {
            get { return SetValueForText1; }
        }
        public string TheValue2
        {
            get { return SetValueForText2; }
        }
        private void loc_Load(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("SELECT  [Code]  ,[Desc]  FROM [Imigration].[dbo].[loc]", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

            // TODO: This line of code loads data into the 'imigrationDataSet.loc' table. You can move, or remove it, as needed.
            //this.locTableAdapter.Fill(this.imigrationDataSet.loc);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        
        
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                //DataGridViewRow row = this.dataGridView1.SelectedRows[0];
               SetValueForText1 = dataGridView1[1, e.RowIndex].Value.ToString();
                SetValueForText2 = dataGridView1[0, e.RowIndex].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("SELECT  [Code]  ,[Desc]  FROM [Imigration].[dbo].[loc] where [Desc] like '%" + textBox1.Text + "%' or  [Code] like '%" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("SELECT  [Code]  ,[Desc]  FROM [Imigration].[dbo].[loc] where [Desc] like '%" + textBox1.Text + "%' or [Code] like '%" + textBox1.Text + "%'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                //DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                SetValueForText1 = dataGridView1[1, e.RowIndex].Value.ToString();
                SetValueForText2 = dataGridView1[0, e.RowIndex].Value.ToString();
            }
        }
    }
}
