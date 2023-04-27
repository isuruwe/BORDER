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
   
    public partial class login2 : Form
    {
        public string conf = System.Configuration.ConfigurationManager.AppSettings["conf"];

        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        public Form1 form1 = new Form1();
        public login2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("select *  FROM [Imigration].[dbo].[Users] where [uname]='"+textBox1.Text+ "' and [pword]='" + textBox2.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            con.Close();
            if (dt.Rows.Count>0)
            {
                Properties.Settings.Default.uname = textBox1.Text;
                Properties.Settings.Default.Save();
                form1.Show();
                form1.panel1.Visible = true;
                form1.panel3.Visible = false;
                var form3 = new login();
                form3.Closed += (s, args) => this.Close();
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Inccorect Username or Pasword!");
            }
           
            
            //form1.Closed += (s, args) => this.Close();
          


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
