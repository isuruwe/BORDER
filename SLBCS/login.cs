using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLBCS
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        public static string SetValueForText7 = "";
        private void button1_Click(object sender, EventArgs e)
        {
            //using (main form3 = new main())
            //{
            //    if (form3.ShowDialog() == DialogResult.OK)
            //    {

            //    }
            //}
            if (!String.IsNullOrEmpty( comboBox1.Text)&& !String.IsNullOrEmpty(textBox1.Text)) {
                Properties.Settings.Default.xx = comboBox1.Text;
                Properties.Settings.Default.loc = textBox1.Text;
                Properties.Settings.Default.Save();

                this.Hide();
                var form1 = new Form1();
                form1.Closed += (s, args) => this.Close();
                form1.panel1.Visible = false;
                form1.panel3.Visible = true;
                form1.Show();
            }
            else
            {
                MessageBox.Show("Please Select Lounge & Location");
            }

          //  this.Close();
        }
        public string TheValue7
        {
            get { return SetValueForText7; }
        }
        private void login_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imigrationDataSet.loc' table. You can move, or remove it, as needed.
           // this.locTableAdapter.Fill(this.imigrationDataSet.loc);

        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = form2.TheValue;
                    textBox1.Text = form2.TheValue2;
                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                textBox2.Text = form2.TheValue;
                textBox1.Text = form2.TheValue2;
            }

        }

        private void login_Enter(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                textBox2.Text = form2.TheValue;
                textBox1.Text = form2.TheValue2;
            }
        }

        private void textBox2_MouseEnter_1(object sender, EventArgs e)
        {

        }

        private void login_MouseEnter(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                textBox2.Text = form2.TheValue;
                textBox1.Text = form2.TheValue2;
            }
        }

        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            using (loc form2 = new loc())
            {
                textBox2.Text = form2.TheValue;
                textBox1.Text = form2.TheValue2;
            }
        }

        private void login_Activated(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                textBox2.Text = form2.TheValue;
                textBox1.Text = form2.TheValue2;
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            using (loc form2 = new loc())
            {
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = form2.TheValue;
                    textBox1.Text = form2.TheValue2;
                }
            }
        }
    }
}
