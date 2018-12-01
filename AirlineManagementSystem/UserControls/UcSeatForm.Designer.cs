namespace AirportManagerSystem.UserControls
{
    partial class UcSeatForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSeat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSeat
            // 
            this.btnSeat.Location = new System.Drawing.Point(8, 8);
            this.btnSeat.Name = "btnSeat";
            this.btnSeat.Size = new System.Drawing.Size(85, 35);
            this.btnSeat.TabIndex = 0;
            this.btnSeat.Text = "Seat";
            this.btnSeat.UseVisualStyleBackColor = true;
            // 
            // UcSeat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSeat);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UcSeat";
            this.Size = new System.Drawing.Size(100, 50);
            this.Load += new System.EventHandler(this.UcSeat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnSeat;
    }
}
