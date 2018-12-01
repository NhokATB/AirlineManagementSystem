namespace AirportManagerSystem.View
{
    partial class SeatManagementWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.cbScheduleList = new System.Windows.Forms.ComboBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.flpSeats = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SeatChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbSeatChange = new System.Windows.Forms.RadioButton();
            this.rdbDualCheckin = new System.Windows.Forms.RadioButton();
            this.rdbSingleCheckin = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeatChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Checked-in";
            // 
            // cbScheduleList
            // 
            this.cbScheduleList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScheduleList.FormattingEnabled = true;
            this.cbScheduleList.Location = new System.Drawing.Point(150, 19);
            this.cbScheduleList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbScheduleList.Name = "cbScheduleList";
            this.cbScheduleList.Size = new System.Drawing.Size(312, 24);
            this.cbScheduleList.TabIndex = 1;
            this.cbScheduleList.SelectedIndexChanged += new System.EventHandler(this.cbScheduleList_SelectedIndexChanged);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(475, 13);
            this.btnApply.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(110, 35);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(22, 145);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 20);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Empty";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(144, 145);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Dual Empty";
            this.label3.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(238, 145);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 20);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // flpSeats
            // 
            this.flpSeats.AutoScroll = true;
            this.flpSeats.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpSeats.Location = new System.Drawing.Point(23, 177);
            this.flpSeats.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flpSeats.Name = "flpSeats";
            this.flpSeats.Size = new System.Drawing.Size(562, 372);
            this.flpSeats.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Choose schedule:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "First clas:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(97, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 17);
            this.label8.TabIndex = 12;
            this.label8.Text = "Empty";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "Dual empty";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "Business:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 17);
            this.label11.TabIndex = 11;
            this.label11.Text = "Economy:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(111, 50);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 17);
            this.label12.TabIndex = 14;
            this.label12.Text = "label12";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(111, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 17);
            this.label13.TabIndex = 15;
            this.label13.Text = "label13";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(111, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 17);
            this.label14.TabIndex = 16;
            this.label14.Text = "label14";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(191, 92);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(54, 17);
            this.label15.TabIndex = 19;
            this.label15.Text = "label15";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(191, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(54, 17);
            this.label16.TabIndex = 18;
            this.label16.Text = "label16";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(191, 50);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(54, 17);
            this.label17.TabIndex = 17;
            this.label17.Text = "label17";
            // 
            // SeatChart
            // 
            chartArea2.Name = "ChartArea1";
            this.SeatChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.SeatChart.Legends.Add(legend2);
            this.SeatChart.Location = new System.Drawing.Point(594, 137);
            this.SeatChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SeatChart.Name = "SeatChart";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Left";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Right";
            this.SeatChart.Series.Add(series3);
            this.SeatChart.Series.Add(series4);
            this.SeatChart.Size = new System.Drawing.Size(275, 214);
            this.SeatChart.TabIndex = 20;
            this.SeatChart.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Location = new System.Drawing.Point(594, 355);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(275, 193);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Help";
            // 
            // label20
            // 
            this.label20.Dock = System.Windows.Forms.DockStyle.Top;
            this.label20.Location = new System.Drawing.Point(3, 133);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(269, 60);
            this.label20.TabIndex = 2;
            this.label20.Text = "3. Seat Change: Choose Task Single Check-In -> Click Checked-in Seat -> Click Emp" +
    "ty Seat in the same cabin";
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Top;
            this.label19.Location = new System.Drawing.Point(3, 76);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(269, 57);
            this.label19.TabIndex = 1;
            this.label19.Text = "2. Dual Check-in: Choose Task Dual Check-In -> Click Dual Seat -> Choose Passenge" +
    "rs";
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Top;
            this.label18.Location = new System.Drawing.Point(3, 18);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(269, 58);
            this.label18.TabIndex = 0;
            this.label18.Text = "1. Single Check-in: Choose Task Single Check-In -> Click Empty Seat -> Choose Pas" +
    "senger";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbSeatChange);
            this.groupBox2.Controls.Add(this.rdbDualCheckin);
            this.groupBox2.Controls.Add(this.rdbSingleCheckin);
            this.groupBox2.Location = new System.Drawing.Point(22, 53);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(563, 79);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Task";
            // 
            // rdbSeatChange
            // 
            this.rdbSeatChange.AutoSize = true;
            this.rdbSeatChange.Location = new System.Drawing.Point(398, 33);
            this.rdbSeatChange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdbSeatChange.Name = "rdbSeatChange";
            this.rdbSeatChange.Size = new System.Drawing.Size(108, 21);
            this.rdbSeatChange.TabIndex = 2;
            this.rdbSeatChange.Text = "Seat Change";
            this.rdbSeatChange.UseVisualStyleBackColor = true;
            this.rdbSeatChange.CheckedChanged += new System.EventHandler(this.RdbSeatChange_CheckedChanged);
            // 
            // rdbDualCheckin
            // 
            this.rdbDualCheckin.AutoSize = true;
            this.rdbDualCheckin.Location = new System.Drawing.Point(234, 33);
            this.rdbDualCheckin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdbDualCheckin.Name = "rdbDualCheckin";
            this.rdbDualCheckin.Size = new System.Drawing.Size(114, 21);
            this.rdbDualCheckin.TabIndex = 1;
            this.rdbDualCheckin.Text = "Dual Check-in";
            this.rdbDualCheckin.UseVisualStyleBackColor = true;
            this.rdbDualCheckin.CheckedChanged += new System.EventHandler(this.RdbDualCheckin_CheckedChanged);
            // 
            // rdbSingleCheckin
            // 
            this.rdbSingleCheckin.AutoSize = true;
            this.rdbSingleCheckin.Checked = true;
            this.rdbSingleCheckin.Location = new System.Drawing.Point(60, 33);
            this.rdbSingleCheckin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rdbSingleCheckin.Name = "rdbSingleCheckin";
            this.rdbSingleCheckin.Size = new System.Drawing.Size(124, 21);
            this.rdbSingleCheckin.TabIndex = 0;
            this.rdbSingleCheckin.TabStop = true;
            this.rdbSingleCheckin.Text = "Single Check-in";
            this.rdbSingleCheckin.UseVisualStyleBackColor = true;
            this.rdbSingleCheckin.CheckedChanged += new System.EventHandler(this.RdbSingleCheckin_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Location = new System.Drawing.Point(594, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(275, 119);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Seat information";
            // 
            // SeatManagementWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 564);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SeatChart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.flpSeats);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbScheduleList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SeatManagementWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seat Management";
            this.Load += new System.EventHandler(this.FrmCheckInSystem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SeatChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbScheduleList;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.FlowLayoutPanel flpSeats;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataVisualization.Charting.Chart SeatChart;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbSeatChange;
        private System.Windows.Forms.RadioButton rdbDualCheckin;
        private System.Windows.Forms.RadioButton rdbSingleCheckin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}