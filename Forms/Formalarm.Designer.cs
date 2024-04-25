namespace Eenergy_absorb_and_optimize.Forms
{
    partial class Formalarm
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
            this.label_alarm = new System.Windows.Forms.Label();
            this.picbox_alarm = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_alarm)).BeginInit();
            this.SuspendLayout();
            // 
            // label_alarm
            // 
            this.label_alarm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_alarm.AutoSize = true;
            this.label_alarm.Font = new System.Drawing.Font("思源黑体 CN", 11F);
            this.label_alarm.Location = new System.Drawing.Point(113, 53);
            this.label_alarm.Name = "label_alarm";
            this.label_alarm.Size = new System.Drawing.Size(156, 27);
            this.label_alarm.TabIndex = 0;
            this.label_alarm.Text = "请输入大于0的数";
            this.label_alarm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picbox_alarm
            // 
            this.picbox_alarm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picbox_alarm.Image = global::Eenergy_absorb_and_optimize.Properties.Resources.警告_64;
            this.picbox_alarm.Location = new System.Drawing.Point(39, 34);
            this.picbox_alarm.Name = "picbox_alarm";
            this.picbox_alarm.Size = new System.Drawing.Size(64, 64);
            this.picbox_alarm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picbox_alarm.TabIndex = 1;
            this.picbox_alarm.TabStop = false;
            // 
            // Formalarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(327, 134);
            this.Controls.Add(this.picbox_alarm);
            this.Controls.Add(this.label_alarm);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("思源黑体 CN", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Formalarm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "警告";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picbox_alarm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label_alarm;
        private System.Windows.Forms.PictureBox picbox_alarm;
    }
}