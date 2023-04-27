using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLBCS
{
    public partial class Form1 : MetroForm
    {
        public string x = System.Configuration.ConfigurationManager.AppSettings["x"];
        login newMDIChild;
        home newMDIChild1;
        embark newMDIChild2;
        public Form1()
        {
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imigrationDataSet1.BorderMaster' table. You can move, or remove it, as needed.
            this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            // TODO: This line of code loads data into the 'imigrationDataSet1.BorderMaster' table. You can move, or remove it, as needed.
            //this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[6].Value) == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                if (Convert.ToInt32(row.Cells[6].Value) == 4)
                {
                    row.DefaultCellStyle.BackColor = Color.Lime;
                }
                if (Convert.ToInt32(row.Cells[6].Value) == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.Fuchsia;
                }

            }
            x = System.Configuration.ConfigurationManager.AppSettings["x"];
            // Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
            label15.Text = Properties.Settings.Default.uname.ToUpper();
           
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                 newMDIChild = new login();
            // Set the Parent Form of the Child window.
            //newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.ShowDialog();
        }

        private void disembarcationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x = Properties.Settings.Default.xx;
            if (x == "ARIVAL")
            {
                using (home frm = new home())
                {
                    DialogResult res = frm.ShowDialog();
                    // Do your other stuff after
                    frm.Dispose();
                }
                
            }
            else
            {
                MessageBox.Show("Please Change Location!");
            }
        }

        private void embacationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            x = Properties.Settings.Default.xx;
            if (x == "DEPARTURE")
            {
                using (embark frm = new embark())
                {
                    DialogResult res = frm.ShowDialog();
                    // Do your other stuff after
                    frm.Dispose();
                }
               
            }

            else
            {
                MessageBox.Show("Please Change Location!");
            }

        }
          

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn &&
                e.RowIndex >= 0)
            {
                Properties.Settings.Default.id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

               
                main newMDIChild3 = new main();
                newMDIChild3.Show();
            }
            //     foreach (DataGridViewRow row in dataGridView1.Rows) {

            //        row.DefaultCellStyle.BackColor = Color.Cyan;

            //}

        }

        private void flightCrewInspectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void overStaysToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login newMDIChild4 = new login();
            newMDIChild4.Show();
        }
    }
}
