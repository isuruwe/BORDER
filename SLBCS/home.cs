using gx;
using pr;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLBCS
{
    public partial class home : Form
    {
        public string conf = System.Configuration.ConfigurationManager.AppSettings["conf"];
        SqlConnection con;
        SqlDataAdapter adapt;
        DataTable dt;
        string text1;
        string text2;
        string text3;
        string text4;
        string text5;
        private bool closePending;
        Thread t;
        PassportReader pr;
        public home()
        {
            InitializeComponent();
             t = new Thread(new ThreadStart(Mainrt));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
            this.Dispose();
            
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_Click(object sender, EventArgs e)
        {

            flights form2 = new flights();
            form2.Show();
            textBox4.Text = form2.TheValue4;
            textBox24.Text = form2.TheValue3;
           
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            label22.Text = "Passport Mode";
            panel11.BackColor = Color.Lime;
        }

        private void home_Load(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
            }
            textBox1.Text = Properties.Settings.Default.uname;
            textBox2.Text = DateTime.Now.Date.ToLongDateString();
            textBox23.Text = Properties.Settings.Default.loc.ToUpper();
            
        }
        public void Mainrt()
        {
            Lib lib = new Lib();
            string text;
             pr = new PassportReader();
            string[] ec = { "Ok", "Warning", "Error" };
            string[] statxt = {
                "No document detected.",
                "Document is moving.",
                "Take out the document!",
                "",
                "Ready for reading.",
                "Button is pressed."
            };

            try
            {
                /* Opening the PR system */
                lib.FunctionStart("Opening system files");
                  /* object for the PR system */
                lib.FunctionEnd();

                /* Validity check */
                if (!pr.IsValid())
                {
                    lib.Error("Failed to initialize!");
                   // return 0;
                }

                /* Connecting to the first device */
                lib.FunctionStart("Connecting to the first device");
                pr.UseDevice(0, (int)PR_USAGEMODE.PR_UMODE_FULL_CONTROL);
                lib.FunctionEnd();

                /* Testing power switch */
                try
                {
                    lib.FunctionStart("Testing power switch");
                    int state = pr.TestPowerState();
                    if ((state & (int)PR_POWER_STATE.PR_PWR_MASK) == (int)PR_POWER_STATE.PR_PWR_OFF)
                        lib.WriteLine("Power is off! Please turn power on!");

                    while ((state & (int)PR_POWER_STATE.PR_PWR_MASK) == (int)PR_POWER_STATE.PR_PWR_OFF)
                    {
                        try
                        {
                            state = pr.TestPowerState();
                        }
                        catch (gxException e)
                        {
                            lib.DisplExcp(e);
                        }
                        lib.Wait(300);
                    }
                    lib.FunctionEnd();
                }
                catch (gxException e)
                {
                    lib.DisplExcp(e);
                }

                /* Enabling motion detection */
                try
                {
                    lib.FunctionStart("Enabling motion detection");
                    pr.SetProperty("freerun_mode", (int)PR_FREERUNMODE.PR_FRMODE_TESTDOCUMENT);
                    lib.FunctionEnd();
                }
                catch (gxException e)
                {
                    lib.DisplExcp(e);
                }

                while (!lib.KbHit())
                {
                    try
                    {
                        /* Testing the Start button */
                        int state = pr.TestButton((int)PR_KEY_CODE.PR_KEY_START);

                        if (state == (int)PR_KEY_STATE.PR_KEY_PRESSED) state = 5;
                        else
                        {
                            /* If the start button is not pressed testing the document detection */
                            state = pr.TestDocument(0);

                            /* Turning the display leds depending on the status */
                            int color = (int)PR_STATUS_LED_COLOR.PR_SLC_OFF;
                            switch (state)
                            {
                                case (int)PR_TESTDOC.PR_TD_OUT: color = (int)PR_STATUS_LED_COLOR.PR_SLC_GREEN; break;
                                case (int)PR_TESTDOC.PR_TD_MOVE: color = (int)PR_STATUS_LED_COLOR.PR_SLC_ANY; break;
                                case (int)PR_TESTDOC.PR_TD_NOMOVE: color = (int)PR_STATUS_LED_COLOR.PR_SLC_RED; break;
                            }
                            pr.SetStatusLed(0xff, color);
                        }
                        if (state == 4)
                        {
                            try
                            {
                                SystemSounds.Hand.Play();
                                /* Capturing images */
                                lib.FunctionStart("Capturing images");
                                pr.Capture();
                                lib.FunctionEnd();


                                lib.FunctionStart("Saving image");
                                pr.SaveImage(0, (int)PR_LIGHT.PR_LIGHT_WHITE, (int)PR_IMAGE_TYPE.PR_IT_ORIGINAL, "white.bmp", (int)GX_IMGFILEFORMATS.GX_BMP);
                                lib.FunctionEnd();

                                lib.FunctionStart("Saving image");
                                pr.SaveImage(0, (int)PR_LIGHT.PR_LIGHT_INFRA, (int)PR_IMAGE_TYPE.PR_IT_ORIGINAL, "ir.bmp", (int)GX_IMGFILEFORMATS.GX_BMP);
                                lib.FunctionEnd();

                                lib.FunctionStart("Saving image");
                                pr.SaveImage(0, (int)PR_LIGHT.PR_LIGHT_UV, (int)PR_IMAGE_TYPE.PR_IT_ORIGINAL, "uv.bmp", (int)GX_IMGFILEFORMATS.GX_BMP);
                                lib.FunctionEnd();
                                /* Getting document data */
                                lib.FunctionStart("Recognizing.");
                                prDoc doc = pr.Recognize(0);
                                lib.FunctionEnd();

                                if (!doc.IsValid()) lib.WriteLine("No data found.");
                                else
                                {
                                    /* Displaying document type */
                                    lib.WriteLine("Document type: " + doc.Code() + ", status: " + ec[doc.Status() / 100]);
                                    text1 = doc.Field((int)PR_DOCFIELD.PR_DF_NAME);
                                    //textBox8.Invoke((MethodInvoker)delegate {
                                    //    // Running on the UI thread

                                    //});
                                    /* Get some fixed fields and displaying them */
                                    text2 = doc.Field((int)PR_DOCFIELD.PR_DF_DOCUMENT_NUMBER);
                                    text3 = doc.Field((int)PR_DOCFIELD.PR_DF_NATIONALITY);
                                    text4 = doc.Field((int)PR_DOCFIELD.PR_DF_SEX);
                                    text5 = doc.Field((int)PR_DOCFIELD.PR_DF_BIRTH_DATE);
                                    //if (text != "") lib.WriteLine("NAME \"" + text + "\" [" + ec[doc.FieldStatus((int)PR_DOCFIELD.PR_DF_NAME) / 100] + "]");

                                    //textBox3.Invoke((MethodInvoker)delegate {
                                    //    // Running on the UI thread



                                    //});
                                    while (!this.IsHandleCreated)
                                    {
                                        this.CreateControl();
                                        System.Threading.Thread.Sleep(100);

                                    }
                                        

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        pictureBox10.Image = null;
                                       
                                        textBox8.Text = text1;
                                        textBox3.Text = text2;
                                        textBox6.Text = text3;
                                        textBox25.Text = text4;
                                        textBox11.Text = text5;
                                        using (var bmpTemp = new Bitmap("white.bmp"))
                                        {
                                            pictureBox7.Image = new Bitmap(bmpTemp);
                                        }
                                        using (var bmpTemp = new Bitmap("ir.bmp"))
                                        {
                                            pictureBox8.Image = new Bitmap(bmpTemp);
                                        }
                                        using (var bmpTemp = new Bitmap("uv.bmp"))
                                        {
                                            pictureBox9.Image = new Bitmap(bmpTemp);
                                        }
                                        using (var bmpTemp = new Bitmap("2400.jpg"))
                                        {
                                            pictureBox10.Image = new Bitmap(bmpTemp);
                                        }

                                    });

                                    



                                    // textBox3.Text = doc.Field((int)PR_DOCFIELD.PR_DF_DOCUMENT_NUMBER);
                                    //if (text != "") lib.WriteLine("DOCUMENT NUMBER \"" + text + "\" [" + ec[doc.FieldStatus((int)PR_DOCFIELD.PR_DF_DOCUMENT_NUMBER) / 100] + "]");


                                    text = doc.Field((int)PR_DOCFIELD.PR_DF_EXPIRY_DATE);
                                    //if (text != "") lib.WriteLine("EXPIRY DATE \"" + text + "\" [" + ec[doc.FieldStatus((int)PR_DOCFIELD.PR_DF_EXPIRY_DATE) / 100] + "]");

                                    /* Searching for fields and displaying them */
                                    gxVariant pdoc = doc.ToVariant();
                                    gxVariant fieldlist = new gxVariant();
                                    pdoc.GetChild(fieldlist, (int)GX_VARIANT_FLAGS.GX_VARIANT_BY_ID, (int)PR_VAR_ID.PRV_FIELDLIST, 0);
                                    int nitems = fieldlist.GetNItems();
                                    for (int i = 0; i < nitems; i++)
                                    {
                                        gxVariant field = new gxVariant();
                                        fieldlist.GetItem(field, (int)GX_VARIANT_FLAGS.GX_VARIANT_BY_INDEX, 0, i);
                                        int field_code = field.GetInt();
                                        text = doc.Field(field_code);
                                        if (text != "") lib.WriteLine("[" + field_code + "] \"" + text + "\" [" + ec[doc.FieldStatus(field_code) / 100] + "]");

                                        if (field_code >= (int)PR_DOCFIELD.PR_DF_FORMATTED) continue;
                                        try
                                        {
                                            gxImage img = doc.FieldImage(field_code);
                                            if (img.IsValid()) img.Save(field_code + ".jpg", (int)GX_IMGFILEFORMATS.GX_JPEG);
                                        }
                                        catch (gxException e)
                                        {
                                            lib.DisplExcp(e);
                                        }
                                    }
                                }
                            }
                            catch (gxException e)
                            {
                                lib.DisplExcp(e);
                            }


                            lib.Write("State: " + statxt[state] + "          \r");
                        }
                        lib.Write("State of the device: " + statxt[state] + "          \r");
                        lib.Wait(200);
                        if (state == (int)PR_TESTDOC.PR_TD_IN || state == 5) lib.Wait(800);
                    }
                    catch (gxException e)
                    {
                        lib.DisplExcp(e);
                    }
                }

                lib.WriteLine("");

                /* Turning off the lights */
                try
                {
                    lib.FunctionStart("Turning off the lights");
                    pr.SetProperty("freerun_mode", (int)PR_FREERUNMODE.PR_FRMODE_NULL);
                    lib.FunctionEnd();
                }
                catch (gxException e)
                {
                    lib.DisplExcp(e);
                }

                /* Turning off the display leds */
                try
                {
                    lib.FunctionStart("Turning off the display leds");
                    pr.SetStatusLed(0xFF, (int)PR_STATUS_LED_COLOR.PR_SLC_OFF);
                    lib.FunctionEnd();
                }
                catch (gxException e)
                {
                    lib.DisplExcp(e);
                }

                /* Closing the device */
                lib.FunctionStart("Closing the device");
                pr.CloseDevice();
                lib.FunctionEnd();

            }
            catch (gxException e)
            {
                pr.CloseDevice();
                lib.FunctionEnd();
                lib.DisplExcp(e);
                //return 0;
               
            }

            //return 1;
        }
        private void textBox5_Click(object sender, EventArgs e)
        {
            label22.Text = "Card Mode";
            panel11.BackColor = Color.Lime;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            pictureBox10.Image = null;
            pictureBox11.Image = null;
            // textBox8.Text = "";
            // textBox9.Text = "";
            textBox16.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox10.Text = "";
            //textBox6.Text = "";
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
            adapt = new SqlDataAdapter("select a.PassportNo, a.b "+
"from( " +
  "  select PassportNo as PassportNo, 1 as b from dbo.SIS "+
   "  union all " +
   "                                         select PassportNo, 2 as b from[dbo].[Interpole] "+
"    union all " +
 "   select PassportNo, 3 as b from[dbo].[Prosecution] " +

 "       union all " +
"    select PassportNo, 4 as b from[dbo].WatchList " +
" ) a " +
"where a.PassportNo='"+textBox3.Text+"'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
               string er= row["b"].ToString();
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

                                cs.BackColor = Color.Gold;
                            }
                        }


                            
                    }

                    
                }
            }
           
            adapt = new SqlDataAdapter("SELECT       * "+
" FROM            TravelDoc where PassportNo='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
               // textBox8.Text = row["LastName"].ToString();
               // textBox9.Text = row["FirstName"].ToString();
                textBox25.Text = row["gender"].ToString();
                textBox11.Text = row["dob"].ToString();
                textBox20.Text = row["nicexp"].ToString();
                textBox10.Text = row["nic"].ToString();
               // textBox6.Text = row["nationality"].ToString();
                textBox7.Text = row["nationalitycode"].ToString();
                textBox16.Text = row["tdtype"].ToString();
                textBox15.Text = row["endype"].ToString();
            }
            adapt = new SqlDataAdapter("select pimage FROM [Imigration].[dbo].[PaasportImage] where passportno='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                pictureBox11.Image = ByteToImage((byte[])row["pimage"]);
            }

            con.Close();
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            try
            {
                MemoryStream mStream = new MemoryStream();
                byte[] pData = blob;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                Bitmap bm = new Bitmap(mStream, false);
                mStream.Dispose();
                return bm;
            }
            catch (Exception ex)
            {
                return null;
            }
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

            adapt = new SqlDataAdapter("SELECT        * " +
" FROM            TravelDoc where PassportNo='" + textBox3.Text + "'", con);
            dt = new DataTable();
            adapt.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                //textBox8.Text = row["LastName"].ToString();
               // textBox9.Text = row["FirstName"].ToString();
                textBox25.Text = row["gender"].ToString();
                textBox11.Text = row["dob"].ToString();
                textBox20.Text = row["nicexp"].ToString();
                textBox10.Text = row["nic"].ToString();
                //textBox6.Text = row["nationality"].ToString();
                textBox7.Text = row["nationalitycode"].ToString();
                textBox16.Text = row["tdtype"].ToString();
                textBox15.Text = row["endype"].ToString();
            }


            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(conf);
            con.Open();
            adapt = new SqlDataAdapter("INSERT INTO [dbo].[BorderMaster] ([PassportNo],[Nationality],[Reason],[Counrt],[createddate],[loggeduser],[Status],[FlghtNo],[FlightDate]) " +
 "    VALUES  ('" + textBox3.Text + "','" + textBox6.Text + "','" + textBox13.Text + "','" + Properties.Settings.Default.loc.ToUpper() + "','" + DateTime.Now + "','" + textBox1.Text + "','1','" + textBox4.Text + "','" + textBox15.Text + "') ", con);
            dt = new DataTable();
            adapt.Fill(dt);

            con.Close();
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
            MessageBox.Show("Saved!");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Mainrt();
        }
        protected override void OnHandleCreated(EventArgs e)
        {

            base.OnHandleCreated(e);
            // backgroundWorker1.RunWorkerAsync();
            
            t.Start();
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
           // backgroundWorker1.RunWorkerAsync();
        }

        private void home_FormClosing(object sender, FormClosingEventArgs e)
        {
            //backgroundWorker1.CancelAsync();
            pr.CloseDevice();
            t.Abort();
            
            
        }
    }
}
