namespace Eenergy_absorb_and_optimize
{
    partial class Formstart
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formstart));
            this.labelsftname = new System.Windows.Forms.Label();
            this.labelinitial = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.pictureBoxsftlogo = new System.Windows.Forms.PictureBox();
            this.panelbox = new System.Windows.Forms.Panel();
            this.panelprogress = new System.Windows.Forms.Panel();
            this.label_startver = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsftlogo)).BeginInit();
            this.panelbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelsftname
            // 
            this.labelsftname.BackColor = System.Drawing.Color.Transparent;
            this.labelsftname.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelsftname.Font = new System.Drawing.Font("思源黑体 CN", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelsftname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelsftname.Location = new System.Drawing.Point(40, 190);
            this.labelsftname.Name = "labelsftname";
            this.labelsftname.Size = new System.Drawing.Size(576, 80);
            this.labelsftname.TabIndex = 2;
            this.labelsftname.Text = "欢迎使用\r\n汽车安全结构智能设计软件";
            this.labelsftname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelinitial
            // 
            this.labelinitial.BackColor = System.Drawing.Color.Transparent;
            this.labelinitial.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelinitial.Font = new System.Drawing.Font("思源宋体 CN", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelinitial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.labelinitial.Location = new System.Drawing.Point(40, 270);
            this.labelinitial.Name = "labelinitial";
            this.labelinitial.Size = new System.Drawing.Size(576, 65);
            this.labelinitial.TabIndex = 3;
            this.labelinitial.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 15;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // pictureBoxsftlogo
            // 
            this.pictureBoxsftlogo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxsftlogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBoxsftlogo.Image = global::Eenergy_absorb_and_optimize.Properties.Resources.图标128;
            this.pictureBoxsftlogo.Location = new System.Drawing.Point(40, 40);
            this.pictureBoxsftlogo.Name = "pictureBoxsftlogo";
            this.pictureBoxsftlogo.Size = new System.Drawing.Size(576, 150);
            this.pictureBoxsftlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxsftlogo.TabIndex = 1;
            this.pictureBoxsftlogo.TabStop = false;
            // 
            // panelbox
            // 
            this.panelbox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelbox.Controls.Add(this.panelprogress);
            this.panelbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelbox.Location = new System.Drawing.Point(40, 335);
            this.panelbox.Margin = new System.Windows.Forms.Padding(0);
            this.panelbox.Name = "panelbox";
            this.panelbox.Size = new System.Drawing.Size(576, 12);
            this.panelbox.TabIndex = 5;
            // 
            // panelprogress
            // 
            this.panelprogress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(160)))), ((int)(((byte)(218)))));
            this.panelprogress.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelprogress.Location = new System.Drawing.Point(0, 0);
            this.panelprogress.Margin = new System.Windows.Forms.Padding(0);
            this.panelprogress.Name = "panelprogress";
            this.panelprogress.Size = new System.Drawing.Size(50, 10);
            this.panelprogress.TabIndex = 0;
            // 
            // label_startver
            // 
            this.label_startver.BackColor = System.Drawing.Color.Transparent;
            this.label_startver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_startver.Font = new System.Drawing.Font("思源宋体 CN", 9F);
            this.label_startver.ForeColor = System.Drawing.Color.DimGray;
            this.label_startver.Location = new System.Drawing.Point(40, 347);
            this.label_startver.Margin = new System.Windows.Forms.Padding(8, 0, 4, 0);
            this.label_startver.Name = "label_startver";
            this.label_startver.Size = new System.Drawing.Size(576, 43);
            this.label_startver.TabIndex = 7;
            this.label_startver.Text = "1.0.0";
            this.label_startver.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Formstart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(656, 390);
            this.Controls.Add(this.label_startver);
            this.Controls.Add(this.panelbox);
            this.Controls.Add(this.labelinitial);
            this.Controls.Add(this.labelsftname);
            this.Controls.Add(this.pictureBoxsftlogo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("思源黑体 CN", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Formstart";
            this.Padding = new System.Windows.Forms.Padding(40, 40, 40, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsftlogo)).EndInit();
            this.panelbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxsftlogo;
        private System.Windows.Forms.Label labelsftname;
        private System.Windows.Forms.Label labelinitial;
        private System.Windows.Forms.Timer timer;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel panelbox;
        private System.Windows.Forms.Panel panelprogress;
        private System.Windows.Forms.Label label_startver;
    }
}