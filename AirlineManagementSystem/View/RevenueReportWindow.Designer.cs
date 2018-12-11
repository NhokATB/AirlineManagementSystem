using System.Windows.Forms;

namespace AirportManagerSystem.View
{
    partial class RevenueReportWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RevenueReportWindow));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterBySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbChartType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chartSummaryRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbByTime = new System.Windows.Forms.RadioButton();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.rdbByYear = new System.Windows.Forms.RadioButton();
            this.rdbByQuarter = new System.Windows.Forms.RadioButton();
            this.rdbByMonth = new System.Windows.Forms.RadioButton();
            this.rdbByWeek = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.cbCriteria = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.chartRevenueRouteDetail = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.dgvRevenueOfRouteData = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartRevenueSummaryOfRoute = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSummaryRevenue)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueRouteDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueOfRouteData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueSummaryOfRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterBySelectedToolStripMenuItem,
            this.resetFilterToolStripMenuItem,
            this.hideLabelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 70);
            // 
            // filterBySelectedToolStripMenuItem
            // 
            this.filterBySelectedToolStripMenuItem.Name = "filterBySelectedToolStripMenuItem";
            this.filterBySelectedToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.filterBySelectedToolStripMenuItem.Text = "Filter by selected";
            this.filterBySelectedToolStripMenuItem.Click += new System.EventHandler(this.filterBySelectedToolStripMenuItem_Click);
            // 
            // resetFilterToolStripMenuItem
            // 
            this.resetFilterToolStripMenuItem.Name = "resetFilterToolStripMenuItem";
            this.resetFilterToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.resetFilterToolStripMenuItem.Text = "Reset Filter";
            this.resetFilterToolStripMenuItem.Click += new System.EventHandler(this.resetFilterToolStripMenuItem_Click);
            // 
            // hideLabelToolStripMenuItem
            // 
            this.hideLabelToolStripMenuItem.Name = "hideLabelToolStripMenuItem";
            this.hideLabelToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.hideLabelToolStripMenuItem.Text = "Show label";
            this.hideLabelToolStripMenuItem.Click += new System.EventHandler(this.HideLabelToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.cbChartType);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.chartSummaryRevenue);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1092, 711);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Summary";
            // 
            // cbChartType
            // 
            this.cbChartType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbChartType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChartType.FormattingEnabled = true;
            this.cbChartType.Items.AddRange(new object[] {
            "Column",
            "Line"});
            this.cbChartType.Location = new System.Drawing.Point(348, 183);
            this.cbChartType.Name = "cbChartType";
            this.cbChartType.Size = new System.Drawing.Size(121, 24);
            this.cbChartType.TabIndex = 24;
            this.cbChartType.SelectedIndexChanged += new System.EventHandler(this.cbChartType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(265, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 17);
            this.label6.TabIndex = 23;
            this.label6.Text = "Chart type:";
            // 
            // chartSummaryRevenue
            // 
            chartArea1.Name = "ChartArea1";
            this.chartSummaryRevenue.ChartAreas.Add(chartArea1);
            this.chartSummaryRevenue.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.Name = "Legend1";
            this.chartSummaryRevenue.Legends.Add(legend1);
            this.chartSummaryRevenue.Location = new System.Drawing.Point(3, 215);
            this.chartSummaryRevenue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartSummaryRevenue.Name = "chartSummaryRevenue";
            this.chartSummaryRevenue.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartSummaryRevenue.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(187))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(145)))), ((int)(((byte)(46))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))))};
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Revenue";
            series1.ToolTip = "Click to view revenue detail of #VALX";
            this.chartSummaryRevenue.Series.Add(series1);
            this.chartSummaryRevenue.Size = new System.Drawing.Size(1086, 493);
            this.chartSummaryRevenue.TabIndex = 13;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title1.Name = "Title1";
            title1.Text = "Revenue report";
            this.chartSummaryRevenue.Titles.Add(title1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.cbYear);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rdbByTime);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.rdbByYear);
            this.groupBox1.Controls.Add(this.rdbByQuarter);
            this.groupBox1.Controls.Add(this.rdbByMonth);
            this.groupBox1.Controls.Add(this.rdbByWeek);
            this.groupBox1.Location = new System.Drawing.Point(264, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 162);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View Mode";
            // 
            // cbYear
            // 
            this.cbYear.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(274, 90);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(121, 24);
            this.cbYear.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(9, 22);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(3, 135);
            this.label13.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(469, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(3, 135);
            this.label12.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(122, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(2, 135);
            this.label11.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 132);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(460, 2);
            this.label10.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(12, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 2);
            this.label9.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(9, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(460, 2);
            this.label8.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(12, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(460, 2);
            this.label7.TabIndex = 15;
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnView.Location = new System.Drawing.Point(478, 118);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(81, 38);
            this.btnView.TabIndex = 15;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Chose year:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "To:";
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(348, 19);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(103, 23);
            this.dtpTo.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "From:";
            // 
            // rdbByTime
            // 
            this.rdbByTime.AutoSize = true;
            this.rdbByTime.Checked = true;
            this.rdbByTime.Location = new System.Drawing.Point(16, 22);
            this.rdbByTime.Name = "rdbByTime";
            this.rdbByTime.Size = new System.Drawing.Size(77, 21);
            this.rdbByTime.TabIndex = 5;
            this.rdbByTime.TabStop = true;
            this.rdbByTime.Text = "By Time";
            this.rdbByTime.UseVisualStyleBackColor = true;
            this.rdbByTime.CheckedChanged += new System.EventHandler(this.rdbByTime_CheckedChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(197, 20);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(103, 23);
            this.dtpFrom.TabIndex = 4;
            // 
            // rdbByYear
            // 
            this.rdbByYear.AutoSize = true;
            this.rdbByYear.Location = new System.Drawing.Point(16, 136);
            this.rdbByYear.Name = "rdbByYear";
            this.rdbByYear.Size = new System.Drawing.Size(76, 21);
            this.rdbByYear.TabIndex = 3;
            this.rdbByYear.Text = "By Year";
            this.rdbByYear.UseVisualStyleBackColor = true;
            this.rdbByYear.CheckedChanged += new System.EventHandler(this.rdbByYear_CheckedChanged);
            // 
            // rdbByQuarter
            // 
            this.rdbByQuarter.AutoSize = true;
            this.rdbByQuarter.Location = new System.Drawing.Point(16, 108);
            this.rdbByQuarter.Name = "rdbByQuarter";
            this.rdbByQuarter.Size = new System.Drawing.Size(95, 21);
            this.rdbByQuarter.TabIndex = 2;
            this.rdbByQuarter.Text = "By Quarter";
            this.rdbByQuarter.UseVisualStyleBackColor = true;
            this.rdbByQuarter.CheckedChanged += new System.EventHandler(this.rdbByQuarter_CheckedChanged);
            // 
            // rdbByMonth
            // 
            this.rdbByMonth.AutoSize = true;
            this.rdbByMonth.Location = new System.Drawing.Point(16, 78);
            this.rdbByMonth.Name = "rdbByMonth";
            this.rdbByMonth.Size = new System.Drawing.Size(85, 21);
            this.rdbByMonth.TabIndex = 1;
            this.rdbByMonth.Text = "By Month";
            this.rdbByMonth.UseVisualStyleBackColor = true;
            this.rdbByMonth.CheckedChanged += new System.EventHandler(this.rdbByMonth_CheckedChanged);
            // 
            // rdbByWeek
            // 
            this.rdbByWeek.AutoSize = true;
            this.rdbByWeek.Location = new System.Drawing.Point(16, 49);
            this.rdbByWeek.Name = "rdbByWeek";
            this.rdbByWeek.Size = new System.Drawing.Size(104, 21);
            this.rdbByWeek.TabIndex = 0;
            this.rdbByWeek.Text = "By this week";
            this.rdbByWeek.UseVisualStyleBackColor = true;
            this.rdbByWeek.CheckedChanged += new System.EventHandler(this.rdbByWeek_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1100, 740);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.cbCriteria);
            this.tabPage2.Controls.Add(this.btnApply);
            this.tabPage2.Controls.Add(this.chartRevenueRouteDetail);
            this.tabPage2.Controls.Add(this.dtpTime);
            this.tabPage2.Controls.Add(this.dgvRevenueOfRouteData);
            this.tabPage2.Controls.Add(this.chartRevenueSummaryOfRoute);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1092, 711);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "By Route";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(534, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 17);
            this.label15.TabIndex = 19;
            this.label15.Text = "Month:";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(250, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "Criteria:";
            // 
            // cbCriteria
            // 
            this.cbCriteria.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCriteria.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCriteria.FormattingEnabled = true;
            this.cbCriteria.Items.AddRange(new object[] {
            "Route",
            "Departure Airport",
            "Arrival Airport"});
            this.cbCriteria.Location = new System.Drawing.Point(313, 14);
            this.cbCriteria.Name = "cbCriteria";
            this.cbCriteria.Size = new System.Drawing.Size(172, 24);
            this.cbCriteria.TabIndex = 16;
            this.cbCriteria.SelectedIndexChanged += new System.EventHandler(this.cbCriteria_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnApply.Location = new System.Drawing.Point(755, 8);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(87, 34);
            this.btnApply.TabIndex = 13;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.BtnApply_Click);
            // 
            // chartRevenueRouteDetail
            // 
            chartArea2.Name = "ChartArea1";
            this.chartRevenueRouteDetail.ChartAreas.Add(chartArea2);
            this.chartRevenueRouteDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.chartRevenueRouteDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend2.Name = "Legend1";
            this.chartRevenueRouteDetail.Legends.Add(legend2);
            this.chartRevenueRouteDetail.Location = new System.Drawing.Point(3, 350);
            this.chartRevenueRouteDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueRouteDetail.Name = "chartRevenueRouteDetail";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartRevenueRouteDetail.Series.Add(series2);
            this.chartRevenueRouteDetail.Size = new System.Drawing.Size(1086, 358);
            this.chartRevenueRouteDetail.TabIndex = 12;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title2.Name = "Title1";
            title2.Text = "Revenue Detail";
            this.chartRevenueRouteDetail.Titles.Add(title2);
            // 
            // dtpTime
            // 
            this.dtpTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpTime.CustomFormat = "MM/yyyy";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(591, 14);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(158, 23);
            this.dtpTime.TabIndex = 15;
            // 
            // dgvRevenueOfRouteData
            // 
            this.dgvRevenueOfRouteData.AllowUserToAddRows = false;
            this.dgvRevenueOfRouteData.AllowUserToDeleteRows = false;
            this.dgvRevenueOfRouteData.AllowUserToResizeRows = false;
            this.dgvRevenueOfRouteData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvRevenueOfRouteData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenueOfRouteData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRevenueOfRouteData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6});
            this.dgvRevenueOfRouteData.Location = new System.Drawing.Point(551, 55);
            this.dgvRevenueOfRouteData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvRevenueOfRouteData.Name = "dgvRevenueOfRouteData";
            this.dgvRevenueOfRouteData.ReadOnly = true;
            this.dgvRevenueOfRouteData.RowHeadersVisible = false;
            this.dgvRevenueOfRouteData.RowTemplate.Height = 25;
            this.dgvRevenueOfRouteData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRevenueOfRouteData.Size = new System.Drawing.Size(336, 250);
            this.dgvRevenueOfRouteData.TabIndex = 11;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Creteria";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Revenue";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // chartRevenueSummaryOfRoute
            // 
            this.chartRevenueSummaryOfRoute.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea3.Name = "ChartArea1";
            this.chartRevenueSummaryOfRoute.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartRevenueSummaryOfRoute.Legends.Add(legend3);
            this.chartRevenueSummaryOfRoute.Location = new System.Drawing.Point(205, 55);
            this.chartRevenueSummaryOfRoute.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueSummaryOfRoute.Name = "chartRevenueSummaryOfRoute";
            this.chartRevenueSummaryOfRoute.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartRevenueSummaryOfRoute.Series.Add(series3);
            this.chartRevenueSummaryOfRoute.Size = new System.Drawing.Size(340, 286);
            this.chartRevenueSummaryOfRoute.TabIndex = 10;
            title3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title3.Name = "Title1";
            title3.Text = "Rate of Revenue";
            this.chartRevenueSummaryOfRoute.Titles.Add(title3);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Revenue";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Airport Departure";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // RevenueReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 740);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RevenueReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revenue Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RevenueReportWindow_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartSummaryRevenue)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueRouteDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueOfRouteData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueSummaryOfRoute)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filterBySelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetFilterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideLabelToolStripMenuItem;
        private TabPage tabPage1;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private ComboBox cbCriteria;
        private Button btnApply;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueRouteDetail;
        private DateTimePicker dtpTime;
        private DataGridView dgvRevenueOfRouteData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueSummaryOfRoute;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private GroupBox groupBox1;
        private RadioButton rdbByMonth;
        private RadioButton rdbByWeek;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSummaryRevenue;
        private DateTimePicker dtpFrom;
        private RadioButton rdbByYear;
        private RadioButton rdbByQuarter;
        private Label label5;
        private Label label4;
        private DateTimePicker dtpTo;
        private Label label3;
        private RadioButton rdbByTime;
        private Label label11;
        private Label label10;
        private Label label8;
        private Label label7;
        private Button btnView;
        private Label label13;
        private Label label12;
        private ComboBox cbYear;
        private Label label9;
        private ComboBox cbChartType;
        private Label label6;
        private Label label15;
        private Label label14;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
    }
}