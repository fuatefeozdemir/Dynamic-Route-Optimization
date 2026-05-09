namespace RouteUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        // Form üzerindeki tüm bileşenler
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.PictureBox picGrid;

        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numHeight;

        private System.Windows.Forms.Button btnCreateMap;
        private System.Windows.Forms.Button btnRandomObstacles;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRunTest;

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblWidth = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.btnCreateMap = new System.Windows.Forms.Button();
            this.btnRandomObstacles = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRunTest = new System.Windows.Forms.Button();

            this.pnlRight = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();

            this.picGrid = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).BeginInit();
            this.SuspendLayout();

            // 
            // pnlTop (Üst Kontrol Paneli)
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.btnRunTest);
            this.pnlTop.Controls.Add(this.btnUndo);
            this.pnlTop.Controls.Add(this.btnRandomObstacles);
            this.pnlTop.Controls.Add(this.btnCreateMap);
            this.pnlTop.Controls.Add(this.numHeight);
            this.pnlTop.Controls.Add(this.lblHeight);
            this.pnlTop.Controls.Add(this.numWidth);
            this.pnlTop.Controls.Add(this.lblWidth);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1184, 75);
            this.pnlTop.TabIndex = 0;

            // Genişlik/Yükseklik Ayarları
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(11, 30);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(73, 15);
            this.lblWidth.Text = "Genişlik (X):";

            this.numWidth.Location = new System.Drawing.Point(90, 28);
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(55, 23);
            this.numWidth.Value = new decimal(new int[] { 30, 0, 0, 0 });

            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(160, 30);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(79, 15);
            this.lblHeight.Text = "Yükseklik (Y):";

            this.numHeight.Location = new System.Drawing.Point(245, 28);
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(55, 23);
            this.numHeight.Value = new decimal(new int[] { 20, 0, 0, 0 });

            // Butonlar
            this.btnCreateMap.Location = new System.Drawing.Point(335, 20);
            this.btnCreateMap.Size = new System.Drawing.Size(110, 35);
            this.btnCreateMap.Text = "Harita Oluştur";
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);

            this.btnRandomObstacles.Location = new System.Drawing.Point(455, 20);
            this.btnRandomObstacles.Size = new System.Drawing.Size(120, 35);
            this.btnRandomObstacles.Text = "Rastgele Engel";
            this.btnRandomObstacles.Click += new System.EventHandler(this.btnRandomObstacles_Click);

            this.btnUndo.Location = new System.Drawing.Point(585, 20);
            this.btnUndo.Size = new System.Drawing.Size(85, 35);
            this.btnUndo.Text = "Geri Al";
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            this.btnRunTest.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRunTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunTest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRunTest.ForeColor = System.Drawing.Color.White;
            this.btnRunTest.Location = new System.Drawing.Point(700, 20);
            this.btnRunTest.Size = new System.Drawing.Size(200, 35);
            this.btnRunTest.Text = "▶ ROTAYI BUL (TEST)";
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);

            // 
            // pnlRight (Sağ İstatistik Paneli)
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRight.Controls.Add(this.dgvResults);
            this.pnlRight.Controls.Add(this.lblStatus);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(834, 75);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(350, 586);
            this.pnlRight.TabIndex = 1;

            // Sonuç Tablosu
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(10, 10);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.Size = new System.Drawing.Size(328, 504);
            this.dgvResults.ColumnCount = 4;
            this.dgvResults.Columns[0].Name = "Veri Yapısı";
            this.dgvResults.Columns[1].Name = "Süre";
            this.dgvResults.Columns[2].Name = "Düğüm";
            this.dgvResults.Columns[3].Name = "Durum";

            // Durum Etiketi
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblStatus.Location = new System.Drawing.Point(10, 514);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(328, 60);
            this.lblStatus.Text = "Durum: Bekleniyor...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // picGrid (Merkezi Çizim Alanı)
            // 
            this.picGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.picGrid.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGrid.Location = new System.Drawing.Point(0, 75);
            this.picGrid.Name = "picGrid";
            this.picGrid.Size = new System.Drawing.Size(834, 586);
            this.picGrid.TabIndex = 2;
            this.picGrid.TabStop = false;
            this.picGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrid_Paint);
            this.picGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseDown);

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.picGrid);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlTop);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dinamik Rota Optimizasyonu";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).EndInit();
            this.ResumeLayout(false);
        }
    }
}