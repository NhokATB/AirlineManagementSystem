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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.filterBySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFilterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.chartRevenueSummary = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvRevenueOfRouteData = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.chartRevenueRouteDetail = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.cbCriteria = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueOfRouteData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueRouteDetail)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1092, 711);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Summary";
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
            // chartRevenueSummary
            // 
            this.chartRevenueSummary.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea2.Name = "ChartArea1";
            this.chartRevenueSummary.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartRevenueSummary.Legends.Add(legend2);
            this.chartRevenueSummary.Location = new System.Drawing.Point(205, 55);
            this.chartRevenueSummary.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueSummary.Name = "chartRevenueSummary";
            this.chartRevenueSummary.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartRevenueSummary.Series.Add(series2);
            this.chartRevenueSummary.Size = new System.Drawing.Size(340, 286);
            this.chartRevenueSummary.TabIndex = 10;
            title2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title2.Name = "Title1";
            title2.Text = "Rate of Revenue";
            this.chartRevenueSummary.Titles.Add(title2);
            // 
            // dgvRevenueOfRouteData
            // 
            this.dgvRevenueOfRouteData.AllowUserToAddRows = false;
            this.dgvRevenueOfRouteData.AllowUserToDeleteRows = false;
            this.dgvRevenueOfRouteData.AllowUserToResizeRows = false;
            this.dgvRevenueOfRouteData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvRevenueOfRouteData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenueOfRouteData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            // dtpTime
            // 
            this.dtpTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtpTime.CustomFormat = "MM/yyyy";
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTime.Location = new System.Drawing.Point(591, 12);
            this.dtpTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.Size = new System.Drawing.Size(158, 23);
            this.dtpTime.TabIndex = 15;
            // 
            // chartRevenueRouteDetail
            // 
            chartArea1.Name = "ChartArea1";
            this.chartRevenueRouteDetail.ChartAreas.Add(chartArea1);
            this.chartRevenueRouteDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.chartRevenueRouteDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Name = "Legend1";
            this.chartRevenueRouteDetail.Legends.Add(legend1);
            this.chartRevenueRouteDetail.Location = new System.Drawing.Point(3, 350);
            this.chartRevenueRouteDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueRouteDetail.Name = "chartRevenueRouteDetail";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartRevenueRouteDetail.Series.Add(series1);
            this.chartRevenueRouteDetail.Size = new System.Drawing.Size(1086, 358);
            this.chartRevenueRouteDetail.TabIndex = 12;
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title1.Name = "Title1";
            title1.Text = "Revenue Detail";
            this.chartRevenueRouteDetail.Titles.Add(title1);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 14;
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
            // cbCriteria
            // 
            this.cbCriteria.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 17;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cbCriteria);
            this.tabPage2.Controls.Add(this.btnApply);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.chartRevenueRouteDetail);
            this.tabPage2.Controls.Add(this.dtpTime);
            this.tabPage2.Controls.Add(this.dgvRevenueOfRouteData);
            this.tabPage2.Controls.Add(this.chartRevenueSummary);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1092, 711);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Route";
            // 
            // RevenueReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 740);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RevenueReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revenue Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RevenueReportWindow_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenueOfRouteData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueRouteDetail)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private Label label1;
        private ComboBox cbCriteria;
        private Button btnApply;
        private Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueRouteDetail;
        private DateTimePicker dtpTime;
        private DataGridView dgvRevenueOfRouteData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueSummary;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
    }
}