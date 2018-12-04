using AirportManagerSystem.Model;
using System.Windows.Forms;

namespace AirportManagerSystem.View
{
    partial class CommissionReportWindow
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new AirportManagerSystem.Model.CommissionDataSet();
            this.btnExport = new System.Windows.Forms.Button();
            this.dgvCommission = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartDetail = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbByDate = new System.Windows.Forms.RadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.rdbByMonth = new System.Windows.Forms.RadioButton();
            this.rdbByYear = new System.Windows.Forms.RadioButton();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReportBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDetail)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReportBindingSource
            // 
            this.ReportBindingSource.DataMember = "CommisstionReport";
            this.ReportBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExport.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnExport.Location = new System.Drawing.Point(835, 162);
            this.btnExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(137, 37);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export Excel File";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvCommission
            // 
            this.dgvCommission.AllowUserToAddRows = false;
            this.dgvCommission.AllowUserToDeleteRows = false;
            this.dgvCommission.AllowUserToResizeRows = false;
            this.dgvCommission.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgvCommission.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCommission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommission.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvCommission.Location = new System.Drawing.Point(416, 12);
            this.dgvCommission.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvCommission.Name = "dgvCommission";
            this.dgvCommission.ReadOnly = true;
            this.dgvCommission.RowHeadersVisible = false;
            this.dgvCommission.RowTemplate.Height = 25;
            this.dgvCommission.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommission.Size = new System.Drawing.Size(556, 146);
            this.dgvCommission.TabIndex = 3;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "User";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Amenities Sold";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Tickets Sold";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 120F;
            this.Column4.HeaderText = "Commission Earned";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // chartDetail
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDetail.ChartAreas.Add(chartArea1);
            this.chartDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend1.Alignment = System.Drawing.StringAlignment.Center;
            legend1.BorderColor = System.Drawing.Color.Black;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend1.Name = "Legend1";
            legend1.Title = "Report";
            this.chartDetail.Legends.Add(legend1);
            this.chartDetail.Location = new System.Drawing.Point(0, 203);
            this.chartDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartDetail.Name = "chartDetail";
            this.chartDetail.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartDetail.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(75)))), ((int)(((byte)(102))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(187))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(145)))), ((int)(((byte)(46)))))};
            series1.ChartArea = "ChartArea1";
            series1.LabelToolTip = "Click to view amenities sold detai ofl  #VALX";
            series1.Legend = "Legend1";
            series1.Name = "Amenities Sold";
            series1.ToolTip = "Click to view amenities sold detai ofl  #VALX";
            series2.ChartArea = "ChartArea1";
            series2.LabelToolTip = "Click to view tickets sold detail of #VALX";
            series2.Legend = "Legend1";
            series2.Name = "Tickets Sold";
            series2.ToolTip = "Click to view tickets sold detail of #VALX";
            series3.ChartArea = "ChartArea1";
            series3.LabelToolTip = "Click to view commission earned detail of #VALX";
            series3.Legend = "Legend1";
            series3.Name = "Commission Earned";
            series3.ToolTip = "Click to view commission earned detail of #VALX";
            this.chartDetail.Series.Add(series1);
            this.chartDetail.Series.Add(series2);
            this.chartDetail.Series.Add(series3);
            this.chartDetail.Size = new System.Drawing.Size(984, 408);
            this.chartDetail.TabIndex = 4;
            this.chartDetail.Text = "chart1";
            // 
            // reportViewer1
            // 
            this.reportViewer1.DocumentMapWidth = 34;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "MyApp.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(783, 162);
            this.reportViewer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(46, 37);
            this.reportViewer1.TabIndex = 5;
            this.reportViewer1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.dtpMonth);
            this.groupBox1.Controls.Add(this.cbYear);
            this.groupBox1.Controls.Add(this.rdbByYear);
            this.groupBox1.Controls.Add(this.rdbByMonth);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.rdbByDate);
            this.groupBox1.Location = new System.Drawing.Point(13, 3);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(397, 196);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View by";
            // 
            // rdbByDate
            // 
            this.rdbByDate.AutoSize = true;
            this.rdbByDate.Checked = true;
            this.rdbByDate.Location = new System.Drawing.Point(49, 30);
            this.rdbByDate.Name = "rdbByDate";
            this.rdbByDate.Size = new System.Drawing.Size(74, 21);
            this.rdbByDate.TabIndex = 0;
            this.rdbByDate.TabStop = true;
            this.rdbByDate.Text = "By date";
            this.rdbByDate.UseVisualStyleBackColor = true;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(148, 27);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 23);
            this.dtpDate.TabIndex = 1;
            // 
            // rdbByMonth
            // 
            this.rdbByMonth.AutoSize = true;
            this.rdbByMonth.Location = new System.Drawing.Point(49, 67);
            this.rdbByMonth.Name = "rdbByMonth";
            this.rdbByMonth.Size = new System.Drawing.Size(85, 21);
            this.rdbByMonth.TabIndex = 2;
            this.rdbByMonth.Text = "By Month";
            this.rdbByMonth.UseVisualStyleBackColor = true;
            // 
            // rdbByYear
            // 
            this.rdbByYear.AutoSize = true;
            this.rdbByYear.Location = new System.Drawing.Point(49, 103);
            this.rdbByYear.Name = "rdbByYear";
            this.rdbByYear.Size = new System.Drawing.Size(76, 21);
            this.rdbByYear.TabIndex = 3;
            this.rdbByYear.Text = "By Year";
            this.rdbByYear.UseVisualStyleBackColor = true;
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(148, 102);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(200, 24);
            this.cbYear.TabIndex = 4;
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "MM/yyyy";
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(148, 64);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.ShowUpDown = true;
            this.dtpMonth.Size = new System.Drawing.Size(200, 23);
            this.dtpMonth.TabIndex = 5;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnApply.Location = new System.Drawing.Point(249, 142);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(99, 37);
            this.btnApply.TabIndex = 7;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // CommissionReportWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chartDetail);
            this.Controls.Add(this.dgvCommission);
            this.Controls.Add(this.btnExport);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CommissionReportWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Commission Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDetail)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private DataGridView dgvCommission;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDetail;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ReportBindingSource;
        private CommissionDataSet DataSet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.GroupBox groupBox1;
        private DateTimePicker dtpMonth;
        private ComboBox cbYear;
        private RadioButton rdbByYear;
        private RadioButton rdbByMonth;
        private DateTimePicker dtpDate;
        private RadioButton rdbByDate;
        private Button btnApply;
    }
}