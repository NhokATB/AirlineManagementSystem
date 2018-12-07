using System.Windows.Forms;

namespace AirportManagerSystem.View
{
    partial class RevenueDetailWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tcRevenueDetail = new System.Windows.Forms.TabControl();
            this.tpFromAmenities = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.cbAmenityViewType = new System.Windows.Forms.ComboBox();
            this.chartRevenueFromAmenities = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label14 = new System.Windows.Forms.Label();
            this.cbAmenities = new System.Windows.Forms.ComboBox();
            this.tpFromTickets = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.CbTicketViewType = new System.Windows.Forms.ComboBox();
            this.chartRevenueFromTicket = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.cbCabinType = new System.Windows.Forms.ComboBox();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcRevenueDetail.SuspendLayout();
            this.tpFromAmenities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueFromAmenities)).BeginInit();
            this.tpFromTickets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueFromTicket)).BeginInit();
            this.SuspendLayout();
            // 
            // tcRevenueDetail
            // 
            this.tcRevenueDetail.Controls.Add(this.tpFromAmenities);
            this.tcRevenueDetail.Controls.Add(this.tpFromTickets);
            this.tcRevenueDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRevenueDetail.Location = new System.Drawing.Point(0, 0);
            this.tcRevenueDetail.Name = "tcRevenueDetail";
            this.tcRevenueDetail.SelectedIndex = 0;
            this.tcRevenueDetail.Size = new System.Drawing.Size(1100, 740);
            this.tcRevenueDetail.TabIndex = 8;
            // 
            // tpFromAmenities
            // 
            this.tpFromAmenities.BackColor = System.Drawing.Color.White;
            this.tpFromAmenities.Controls.Add(this.label3);
            this.tpFromAmenities.Controls.Add(this.cbAmenityViewType);
            this.tpFromAmenities.Controls.Add(this.chartRevenueFromAmenities);
            this.tpFromAmenities.Controls.Add(this.label14);
            this.tpFromAmenities.Controls.Add(this.cbAmenities);
            this.tpFromAmenities.Location = new System.Drawing.Point(4, 25);
            this.tpFromAmenities.Name = "tpFromAmenities";
            this.tpFromAmenities.Padding = new System.Windows.Forms.Padding(3);
            this.tpFromAmenities.Size = new System.Drawing.Size(1092, 711);
            this.tpFromAmenities.TabIndex = 1;
            this.tpFromAmenities.Text = "From Amenities";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(587, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 23;
            this.label3.Text = "View by:";
            // 
            // cbAmenityViewType
            // 
            this.cbAmenityViewType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbAmenityViewType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbAmenityViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAmenityViewType.FormattingEnabled = true;
            this.cbAmenityViewType.Items.AddRange(new object[] {
            "Revenue",
            "Quantity"});
            this.cbAmenityViewType.Location = new System.Drawing.Point(653, 34);
            this.cbAmenityViewType.Name = "cbAmenityViewType";
            this.cbAmenityViewType.Size = new System.Drawing.Size(172, 24);
            this.cbAmenityViewType.TabIndex = 22;
            // 
            // chartRevenueFromAmenities
            // 
            chartArea6.Name = "ChartArea1";
            this.chartRevenueFromAmenities.ChartAreas.Add(chartArea6);
            this.chartRevenueFromAmenities.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend6.BackColor = System.Drawing.Color.Transparent;
            legend6.Name = "Legend1";
            this.chartRevenueFromAmenities.Legends.Add(legend6);
            this.chartRevenueFromAmenities.Location = new System.Drawing.Point(3, 128);
            this.chartRevenueFromAmenities.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueFromAmenities.Name = "chartRevenueFromAmenities";
            this.chartRevenueFromAmenities.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartRevenueFromAmenities.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(187))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(145)))), ((int)(((byte)(46))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))))};
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Revenue";
            this.chartRevenueFromAmenities.Series.Add(series6);
            this.chartRevenueFromAmenities.Size = new System.Drawing.Size(1086, 580);
            this.chartRevenueFromAmenities.TabIndex = 21;
            title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title6.Name = "Title1";
            title6.Text = "Revenue report";
            this.chartRevenueFromAmenities.Titles.Add(title6);
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(266, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "Choose Amenity:";
            // 
            // cbAmenities
            // 
            this.cbAmenities.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbAmenities.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbAmenities.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAmenities.FormattingEnabled = true;
            this.cbAmenities.Items.AddRange(new object[] {
            "Route",
            "Departure Airport",
            "Arrival Airport"});
            this.cbAmenities.Location = new System.Drawing.Point(386, 34);
            this.cbAmenities.Name = "cbAmenities";
            this.cbAmenities.Size = new System.Drawing.Size(172, 24);
            this.cbAmenities.TabIndex = 16;
            // 
            // tpFromTickets
            // 
            this.tpFromTickets.Controls.Add(this.label1);
            this.tpFromTickets.Controls.Add(this.CbTicketViewType);
            this.tpFromTickets.Controls.Add(this.chartRevenueFromTicket);
            this.tpFromTickets.Controls.Add(this.label2);
            this.tpFromTickets.Controls.Add(this.cbCabinType);
            this.tpFromTickets.Location = new System.Drawing.Point(4, 25);
            this.tpFromTickets.Name = "tpFromTickets";
            this.tpFromTickets.Padding = new System.Windows.Forms.Padding(3);
            this.tpFromTickets.Size = new System.Drawing.Size(1092, 711);
            this.tpFromTickets.TabIndex = 2;
            this.tpFromTickets.Text = "From tickets";
            this.tpFromTickets.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(588, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 28;
            this.label1.Text = "View by:";
            // 
            // CbTicketViewType
            // 
            this.CbTicketViewType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CbTicketViewType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CbTicketViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbTicketViewType.FormattingEnabled = true;
            this.CbTicketViewType.Items.AddRange(new object[] {
            "Revenue",
            "Quantity"});
            this.CbTicketViewType.Location = new System.Drawing.Point(654, 34);
            this.CbTicketViewType.Name = "CbTicketViewType";
            this.CbTicketViewType.Size = new System.Drawing.Size(172, 24);
            this.CbTicketViewType.TabIndex = 27;
            // 
            // chartRevenueFromTicket
            // 
            chartArea5.Name = "ChartArea1";
            this.chartRevenueFromTicket.ChartAreas.Add(chartArea5);
            this.chartRevenueFromTicket.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend5.BackColor = System.Drawing.Color.Transparent;
            legend5.Name = "Legend1";
            this.chartRevenueFromTicket.Legends.Add(legend5);
            this.chartRevenueFromTicket.Location = new System.Drawing.Point(3, 128);
            this.chartRevenueFromTicket.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRevenueFromTicket.Name = "chartRevenueFromTicket";
            this.chartRevenueFromTicket.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            this.chartRevenueFromTicket.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(160)))), ((int)(((byte)(187))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(145)))), ((int)(((byte)(46))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(79)))), ((int)(((byte)(76)))))};
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Revenue";
            this.chartRevenueFromTicket.Series.Add(series5);
            this.chartRevenueFromTicket.Size = new System.Drawing.Size(1086, 580);
            this.chartRevenueFromTicket.TabIndex = 26;
            title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            title5.Name = "Title1";
            title5.Text = "Revenue report";
            this.chartRevenueFromTicket.Titles.Add(title5);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Choose cabin type:";
            // 
            // cbCabinType
            // 
            this.cbCabinType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbCabinType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cbCabinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCabinType.FormattingEnabled = true;
            this.cbCabinType.Items.AddRange(new object[] {
            "Route",
            "Departure Airport",
            "Arrival Airport"});
            this.cbCabinType.Location = new System.Drawing.Point(389, 34);
            this.cbCabinType.Name = "cbCabinType";
            this.cbCabinType.Size = new System.Drawing.Size(172, 24);
            this.cbCabinType.TabIndex = 24;
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
            // RevenueDetailWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 740);
            this.Controls.Add(this.tcRevenueDetail);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "RevenueDetailWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revenue Detail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RevenueDetailWindow_Load);
            this.tcRevenueDetail.ResumeLayout(false);
            this.tpFromAmenities.ResumeLayout(false);
            this.tpFromAmenities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueFromAmenities)).EndInit();
            this.tpFromTickets.ResumeLayout(false);
            this.tpFromTickets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenueFromTicket)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private TabControl tcRevenueDetail;
        private TabPage tpFromAmenities;
        private ComboBox cbAmenities;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column1;
        private Label label14;
        private Label label3;
        private ComboBox cbAmenityViewType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueFromAmenities;
        private TabPage tpFromTickets;
        private Label label1;
        private ComboBox CbTicketViewType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenueFromTicket;
        private Label label2;
        private ComboBox cbCabinType;
    }
}