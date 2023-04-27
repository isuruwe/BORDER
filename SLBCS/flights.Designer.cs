
namespace SLBCS
{
    partial class flights
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.flightnoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flightdatetimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flightsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imigrationDataSet2 = new SLBCS.ImigrationDataSet2();
            this.locBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imigrationDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.imigrationDataSet = new SLBCS.ImigrationDataSet();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.locTableAdapter = new SLBCS.ImigrationDataSetTableAdapters.locTableAdapter();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.imigrationDataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.flightsTableAdapter = new SLBCS.ImigrationDataSet2TableAdapters.FlightsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flightsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSet2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.locBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSetBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(42, 349);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "Find";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.flightnoDataGridViewTextBoxColumn,
            this.flightdatetimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.flightsBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(11, 62);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(451, 272);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // flightnoDataGridViewTextBoxColumn
            // 
            this.flightnoDataGridViewTextBoxColumn.DataPropertyName = "flightno";
            this.flightnoDataGridViewTextBoxColumn.HeaderText = "Flight No";
            this.flightnoDataGridViewTextBoxColumn.Name = "flightnoDataGridViewTextBoxColumn";
            this.flightnoDataGridViewTextBoxColumn.ReadOnly = true;
            this.flightnoDataGridViewTextBoxColumn.Width = 150;
            // 
            // flightdatetimeDataGridViewTextBoxColumn
            // 
            this.flightdatetimeDataGridViewTextBoxColumn.DataPropertyName = "flightdatetime";
            this.flightdatetimeDataGridViewTextBoxColumn.HeaderText = "Flight Date and Time";
            this.flightdatetimeDataGridViewTextBoxColumn.Name = "flightdatetimeDataGridViewTextBoxColumn";
            this.flightdatetimeDataGridViewTextBoxColumn.ReadOnly = true;
            this.flightdatetimeDataGridViewTextBoxColumn.Width = 300;
            // 
            // flightsBindingSource
            // 
            this.flightsBindingSource.DataMember = "Flights";
            this.flightsBindingSource.DataSource = this.imigrationDataSet2;
            // 
            // imigrationDataSet2
            // 
            this.imigrationDataSet2.DataSetName = "ImigrationDataSet2";
            this.imigrationDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // locBindingSource
            // 
            this.locBindingSource.DataMember = "loc";
            this.locBindingSource.DataSource = this.imigrationDataSetBindingSource;
            // 
            // imigrationDataSetBindingSource
            // 
            this.imigrationDataSetBindingSource.DataSource = this.imigrationDataSet;
            this.imigrationDataSetBindingSource.Position = 0;
            // 
            // imigrationDataSet
            // 
            this.imigrationDataSet.DataSetName = "ImigrationDataSet";
            this.imigrationDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Find";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(336, 20);
            this.textBox1.TabIndex = 6;
            // 
            // locTableAdapter
            // 
            this.locTableAdapter.ClearBeforeFill = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(326, 348);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(183, 349);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imigrationDataSetBindingSource1
            // 
            this.imigrationDataSetBindingSource1.DataSource = this.imigrationDataSet;
            this.imigrationDataSetBindingSource1.Position = 0;
            // 
            // flightsTableAdapter
            // 
            this.flightsTableAdapter.ClearBeforeFill = true;
            // 
            // flights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 376);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "flights";
            this.Text = "flights";
            this.Load += new System.EventHandler(this.flights_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flightsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.locBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imigrationDataSetBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource locBindingSource;
        private System.Windows.Forms.BindingSource imigrationDataSetBindingSource;
        private ImigrationDataSet imigrationDataSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private ImigrationDataSetTableAdapters.locTableAdapter locTableAdapter;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.BindingSource imigrationDataSetBindingSource1;
        private ImigrationDataSet2 imigrationDataSet2;
        private System.Windows.Forms.BindingSource flightsBindingSource;
        private ImigrationDataSet2TableAdapters.FlightsTableAdapter flightsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightnoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn flightdatetimeDataGridViewTextBoxColumn;
    }
}