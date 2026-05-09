namespace RouteUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Form üzerindeki bileşenler
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnFindPath;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblMetrics;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblMetrics = new System.Windows.Forms.Label();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlControls (Üst Kontrol Paneli)
            // 
            this.pnlControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlControls.Controls.Add(this.lblMetrics);
            this.pnlControls.Controls.Add(this.lblStatus);
            this.pnlControls.Controls.Add(this.btnFindPath);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(800, 50);
            this.pnlControls.TabIndex = 0;

            // 
            // btnFindPath (Yolu Bul Butonu)
            // 
            this.btnFindPath.Location = new System.Drawing.Point(12, 10);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(120, 30);
            this.btnFindPath.TabIndex = 0;
            this.btnFindPath.Text = "En Kısa Yolu Bul";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);

            // 
            // lblStatus (Bilgilendirme Yazısı)
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(150, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(250, 15);
            this.lblStatus.Text = "CTRL: Başlangıç | SHIFT: Bitiş | Tık: Engel";

            // 
            // lblMetrics (Hız ve Verimlilik Verileri)
            // 
            this.lblMetrics.AutoSize = true;
            this.lblMetrics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMetrics.Location = new System.Drawing.Point(450, 18);
            this.lblMetrics.Name = "lblMetrics";
            this.lblMetrics.Size = new System.Drawing.Size(60, 15);
            this.lblMetrics.Text = "Süre: - us";

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlControls);
            this.Name = "MainForm";
            this.Text = "C++ Dijkstra Visualizer";
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}