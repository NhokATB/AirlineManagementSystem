namespace AirportManagerSystem.View
{
    partial class MyCommissionWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartDetail = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.rdbByYear = new System.Windows.Forms.RadioButton();
            this.rdbByMonth = new System.Windows.Forms.RadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rdbByDate = new System.Windows.Forms.RadioButton();
            this.cbCriterias = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartDetail
            // 
            chartArea4.Name = "ChartArea1";
            this.chartDetail.ChartAreas.Add(chartArea4);
            this.chartDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend4.Alignment = System.Drawing.StringAlignment.Center;
            legend4.BorderColor = System.Drawing.Color.Black;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            legend4.Title = "Commission Earned";
            this.chartDetail.Legends.Add(legend4);
            this.chartDetail.Location = new System.Drawing.Point(0, 135);
            this.chartDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartDetail.Name = "chartDetail";
            series4.BorderColor = System.Drawing.Color.Teal;
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Commission Earned";
            this.chartDetail.Series.Add(series4);
            this.chartDetail.Size = new System.Drawing.Size(984, 476);
            this.chartDetail.TabIndex = 8;
            this.chartDetail.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.cbYear);
            this.groupBox1.Controls.Add(this.rdbByYear);
            this.groupBox1.Controls.Add(this.rdbByMonth);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.rdbByDate);
            this.groupBox1.Location = new System.Drawing.Point(34, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(917, 76);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View by";
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnApply.Location = new System.Drawing.Point(752, 20);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 37);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbYear
            // 
            this.cbYear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.Enabled = false;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(462, 27);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(147, 24);
            this.cbYear.TabIndex = 4;
            // 
            // rdbByYear
            // 
            this.rdbByYear.AutoSize = true;
            this.rdbByYear.Location = new System.Drawing.Point(646, 30);
            this.rdbByYear.Name = "rdbByYear";
            this.rdbByYear.Size = new System.Drawing.Size(76, 21);
            this.rdbByYear.TabIndex = 3;
            this.rdbByYear.Text = "By Year";
            this.rdbByYear.UseVisualStyleBackColor = true;
            this.rdbByYear.CheckedChanged += new System.EventHandler(this.rdbByYear_CheckedChanged);
            // 
            // rdbByMonth
            // 
            this.rdbByMonth.AutoSize = true;
            this.rdbByMonth.Location = new System.Drawing.Point(322, 30);
            this.rdbByMonth.Name = "rdbByMonth";
            this.rdbByMonth.Size = new System.Drawing.Size(134, 21);
            this.rdbByMonth.TabIndex = 2;
            this.rdbByMonth.Text = "By Month in Year";
            this.rdbByMonth.UseVisualStyleBackColor = true;
            this.rdbByMonth.CheckedChanged += new System.EventHandler(this.rdbByMonth_CheckedChanged);
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(203, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(97, 23);
            this.dtpDate.TabIndex = 1;
            // 
            // rdbByDate
            // 
            this.rdbByDate.AutoSize = true;
            this.rdbByDate.Checked = true;
            this.rdbByDate.Location = new System.Drawing.Point(65, 30);
            this.rdbByDate.Name = "rdbByDate";
            this.rdbByDate.Size = new System.Drawing.Size(134, 21);
            this.rdbByDate.TabIndex = 0;
            this.rdbByDate.TabStop = true;
            this.rdbByDate.Text = "By Date in Month";
            this.rdbByDate.UseVisualStyleBackColor = true;
            this.rdbByDate.CheckedChanged += new System.EventHandler(this.rdbByDate_CheckedChanged);
            // 
            // cbCriterias
            // 
            this.cbCriterias.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCriterias.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbCriterias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriterias.FormattingEnabled = true;
            this.cbCriterias.Items.AddRange(new object[] {
            "Tickets Sold",
            "Amenities Sold",
            "Commission Earned"});
            this.cbCriterias.Location = new System.Drawing.Point(412, 12);
            this.cbCriterias.Name = "cbCriterias";
            this.cbCriterias.Size = new System.Drawing.Size(147, 24);
            this.cbCriterias.TabIndex = 8;
            this.cbCriterias.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Criterias:";
            // 
            // MyCommissionWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCriterias);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chartDetail);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MyCommissionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "My commission";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.RadioButton rdbByYear;
        private System.Windows.Forms.RadioButton rdbByMonth;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rdbByDate;
        private System.Windows.Forms.ComboBox cbCriterias;
        private System.Windows.Forms.Label label1;
    }
}