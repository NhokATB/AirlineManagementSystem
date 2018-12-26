namespace AirportManagerSystem.View
{
    partial class CommissionDetailWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommissionDetailWindow));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.rdbByYear = new System.Windows.Forms.RadioButton();
            this.rdbByMonth = new System.Windows.Forms.RadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rdbByDate = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            legend1.Title = "Commission Earned";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 150);
            this.chart1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.Teal;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Commission Earned";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(984, 461);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.cbMonth);
            this.groupBox1.Controls.Add(this.rdbByYear);
            this.groupBox1.Controls.Add(this.rdbByMonth);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.rdbByDate);
            this.groupBox1.Location = new System.Drawing.Point(36, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(917, 126);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View by";
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnApply.Location = new System.Drawing.Point(620, 47);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 37);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // cbMonth
            // 
            this.cbMonth.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(454, 54);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(147, 24);
            this.cbMonth.TabIndex = 4;
            // 
            // rdbByYear
            // 
            this.rdbByYear.AutoSize = true;
            this.rdbByYear.Location = new System.Drawing.Point(316, 84);
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
            this.rdbByMonth.Location = new System.Drawing.Point(316, 55);
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
            this.dtpDate.Location = new System.Drawing.Point(454, 25);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.ShowUpDown = true;
            this.dtpDate.Size = new System.Drawing.Size(147, 23);
            this.dtpDate.TabIndex = 1;
            // 
            // rdbByDate
            // 
            this.rdbByDate.AutoSize = true;
            this.rdbByDate.Checked = true;
            this.rdbByDate.Location = new System.Drawing.Point(316, 28);
            this.rdbByDate.Name = "rdbByDate";
            this.rdbByDate.Size = new System.Drawing.Size(134, 21);
            this.rdbByDate.TabIndex = 0;
            this.rdbByDate.TabStop = true;
            this.rdbByDate.Text = "By Date in Month";
            this.rdbByDate.UseVisualStyleBackColor = true;
            this.rdbByDate.CheckedChanged += new System.EventHandler(this.rdbByDate_CheckedChanged);
            // 
            // CommissionDetailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chart1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CommissionDetailWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commission detail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.RadioButton rdbByYear;
        private System.Windows.Forms.RadioButton rdbByMonth;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rdbByDate;
    }
}