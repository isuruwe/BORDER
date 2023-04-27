using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLBCS
{
    public partial class main : Form
    {
        public string conf = System.Configuration.ConfigurationManager.AppSettings["conf"];
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        String pid;
        public main()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }
        private void main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'imigrationDataSet1.BorderMaster' table. You can move, or remove it, as needed.
            this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            // TODO: This line of code loads data into the 'imigrationDataSet1.BorderMaster' table. You can move, or remove it, as needed.
            // this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            // TODO: This line of code loads data into the 'imigrationDataSet1.BorderMaster' table. You can move, or remove it, as needed.
            // this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            //this.borderMasterTableAdapter.Fill(this.imigrationDataSet1.BorderMaster);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[0].Value) == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                if (Convert.ToInt32(row.Cells[0].Value) == 4)
                {
                    row.DefaultCellStyle.BackColor = Color.Lime;
                }
                if (Convert.ToInt32(row.Cells[0].Value) == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.Fuchsia;
                }

            }
            pid = Properties.Settings.Default.id;

            textBox8.Text = "";
            textBox9.Text = "";
            textBox16.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox10.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox16.Text = "";
            textBox15.Text = "";
            textBox4.Text = "";
            textBox15.Text = "";
            foreach (Control c in tabControl1.Controls)
            {
                foreach (Control cs in c.Controls)
                {

                    if (cs.GetType() == typeof(System.Windows.Forms.Panel))
                    {
                        cs.BackColor = Color.WhiteSmoke;
                    }
                }
            }
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("select a.PassportNo, a.b " +
"from( " +
  "  select PassportNo as PassportNo, 1 as b from dbo.SIS " +
   "  union all " +
   "                                         select PassportNo, 2 as b from[dbo].[Interpole] " +
"    union all " +
 "   select PassportNo, 3 as b from[dbo].[Prosecution] " +

 "       union all " +
"    select PassportNo, 4 as b from[dbo].WatchList " +
" ) a " +
"where a.PassportNo='" + pid + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string er = row["b"].ToString();
                if (!String.IsNullOrEmpty(er))
                {
                    foreach (Control c in tabControl1.Controls)
                    {
                        foreach (Control cs in c.Controls)
                        {

                            if (cs.GetType() == typeof(System.Windows.Forms.Panel))
                            {
                                textBox14.ForeColor = Color.Red;
                                if (er.Equals("1"))
                                {
                                    textBox17.Text = "User Exist in SIS List!";
                                }
                                if (er.Equals("2"))
                                {
                                    textBox17.Text = "User Exist in Interpole List!";
                                }
                                if (er.Equals("3"))
                                {
                                    textBox17.Text = "User Exist in Prosecution List!";
                                }
                                if (er.Equals("4"))
                                {
                                    textBox17.Text = "User Exist in Watch List!";
                                }

                                cs.BackColor = Color.Gold;
                            }
                        }



                    }


                }
            }

            adapt = new SqlDataAdapter("SELECT        BorderMaster.* ,TravelDoc.FirstName,TravelDoc.LastName,TravelDoc.gender,TravelDoc.dob " +
" FROM            BorderMaster INNER JOIN TravelDoc ON BorderMaster.PassportNo=TravelDoc.PassportNo where BorderMaster.PassportNo='" + pid + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                textBox3.Text = row["PassportNo"].ToString();
                textBox7.Text = row["LastName"].ToString();
                textBox4.Text = row["FirstName"].ToString();
                textBox5.Text = row["gender"].ToString();
                textBox15.Text = row["dob"].ToString();
                 textBox8.Text = row["FlightDate"].ToString();
                textBox9.Text = row["FlghtNo"].ToString();
                textBox6.Text = row["nationality"].ToString();
                textBox10.Text = row["loggeduser"].ToString();
                textBox16.Text = row["Reason"].ToString();
               // textBox17.Text = row["endype"].ToString();
            }

            adapt = new SqlDataAdapter("select pimage FROM [Imigration].[dbo].[PaasportImage] where passportno='" + pid + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
               // pictureBox10.Image = ByteToImage((byte[])row["pimage"]);
            }
            con.Close();


        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
          
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("select a.PassportNo, a.b " +
"from( " +
  "  select PassportNo as PassportNo, 1 as b from dbo.SIS " +
   "  union all " +
   "                                         select PassportNo, 2 as b from[dbo].[Interpole] " +
"    union all " +
 "   select PassportNo, 3 as b from[dbo].[Prosecution] " +

 "       union all " +
"    select PassportNo, 4 as b from[dbo].WatchList " +
" ) a " +
"where a.PassportNo='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string er = row["b"].ToString();
                if (!String.IsNullOrEmpty(er))
                {
                    foreach (Control c in tabControl1.Controls)
                    {
                        foreach (Control cs in c.Controls)
                        {

                            if (cs.GetType() == typeof(System.Windows.Forms.Panel))
                            {
                                textBox14.ForeColor = Color.Red;
                                if (er.Equals("1"))
                                {
                                    textBox14.Text = "User Exist in SIS List!";
                                }
                                if (er.Equals("2"))
                                {
                                    textBox14.Text = "User Exist in Interpole List!";
                                }
                                if (er.Equals("3"))
                                {
                                    textBox14.Text = "User Exist in Prosecution List!";
                                }
                                if (er.Equals("4"))
                                {
                                    textBox14.Text = "User Exist in Watch List!";
                                }

                                cs.BackColor = Color.LightGoldenrodYellow;
                            }
                        }



                    }


                }
            }

            adapt = new SqlDataAdapter("SELECT       * " +
" FROM            TravelDoc where PassportNo='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                textBox8.Text = row["LastName"].ToString();
                textBox9.Text = row["FirstName"].ToString();
                textBox25.Text = row["gender"].ToString();
                textBox11.Text = row["dob"].ToString();
                textBox20.Text = row["nicexp"].ToString();
                textBox10.Text = row["nic"].ToString();
                textBox6.Text = row["nationality"].ToString();
                textBox7.Text = row["nationalitycode"].ToString();
                textBox16.Text = row["tdtype"].ToString();
                textBox15.Text = row["endype"].ToString();
            }


            con.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
//            con = new SqlConnection(conf);
//            con.Open();
//            adapt = new SqlDataAdapter("select a.PassportNo, a.b " +
//"from( " +
//  "  select PassportNo as PassportNo, 1 as b from dbo.SIS " +
//   "  union all " +
//   "                                         select PassportNo, 2 as b from[dbo].[Interpole] " +
//"    union all " +
// "   select PassportNo, 3 as b from[dbo].[Prosecution] " +

// "       union all " +
//"    select PassportNo, 4 as b from[dbo].WatchList " +
//" ) a " +
//"where a.PassportNo='" + textBox3.Text + "'", con);
//            dt = new DataTable();
//            adapt.Fill(dt);
//            foreach (DataRow row in dt.Rows)
//            {
//                string er = row["b"].ToString();
//                if (!String.IsNullOrEmpty(er))
//                {
//                    foreach (Control c in tabControl1.Controls)
//                    {
//                        foreach (Control cs in c.Controls)
//                        {

//                            if (cs.GetType() == typeof(System.Windows.Forms.Panel))
//                            {
//                                textBox14.ForeColor = Color.Red;
//                                if (er.Equals("1"))
//                                {
//                                    textBox14.Text = "User Exist in SIS List!";
//                                }
//                                if (er.Equals("2"))
//                                {
//                                    textBox14.Text = "User Exist in Interpole List!";
//                                }
//                                if (er.Equals("3"))
//                                {
//                                    textBox14.Text = "User Exist in Prosecution List!";
//                                }
//                                if (er.Equals("4"))
//                                {
//                                    textBox14.Text = "User Exist in Watch List!";
//                                }

//                                cs.BackColor = Color.LightGoldenrodYellow;
//                            }
//                        }



//                    }


//                }
//            }

//            adapt = new SqlDataAdapter("SELECT       * " +
//" FROM            TravelDoc where PassportNo='" + textBox3.Text + "'", con);
//            dt = new DataTable();
//            adapt.Fill(dt);

//            foreach (DataRow row in dt.Rows)
//            {
//                textBox8.Text = row["LastName"].ToString();
//                textBox9.Text = row["FirstName"].ToString();
//                textBox25.Text = row["gender"].ToString();
//                textBox11.Text = row["dob"].ToString();
//                textBox20.Text = row["nicexp"].ToString();
//                textBox10.Text = row["nic"].ToString();
//                textBox6.Text = row["nationality"].ToString();
//                textBox7.Text = row["nationalitycode"].ToString();
//                textBox16.Text = row["tdtype"].ToString();
//                textBox15.Text = row["endype"].ToString();
//            }


//            con.Close();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("INSERT INTO [dbo].[BorderMaster] ([PassportNo],[Nationality],[Reason],[Counrt],[createddate],[loggeduser],[Status],[FlghtNo],[FlightDate]) "+
 "    VALUES  ('" + textBox3.Text + "','" + textBox6.Text + "','" + textBox3.Text + "') where a.PassportNo='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);

            con.Close();
        }
    }
}
