namespace RouteUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRunTest;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRandomObstacles;
        private System.Windows.Forms.Button btnCreateMap;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.PictureBox picGrid;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRunTest = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRandomObstacles = new System.Windows.Forms.Button();
            this.btnCreateMap = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.picGrid = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).BeginInit();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.pnlSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSidebar.Controls.Add(this.lblTitle);
            this.pnlSidebar.Controls.Add(this.dgvResults);
            this.pnlSidebar.Controls.Add(this.btnRunTest);
            this.pnlSidebar.Controls.Add(this.btnUndo);
            this.pnlSidebar.Controls.Add(this.btnRandomObstacles);
            this.pnlSidebar.Controls.Add(this.btnCreateMap);
            this.pnlSidebar.Controls.Add(this.numHeight);
            this.pnlSidebar.Controls.Add(this.lblHeight);
            this.pnlSidebar.Controls.Add(this.numWidth);
            this.pnlSidebar.Controls.Add(this.lblWidth);
            this.pnlSidebar.Controls.Add(this.lblStatus);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(320, 720);

            // Başlık
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            this.lblTitle.Location = new System.Drawing.Point(20, 25);
            this.lblTitle.Size = new System.Drawing.Size(280, 35);
            this.lblTitle.Text = "Dinamik Rota Optimizasyonu";

            // Etiketler
            this.lblWidth.ForeColor = System.Drawing.Color.DimGray;
            this.lblWidth.Location = new System.Drawing.Point(20, 85);
            this.lblWidth.Text = "Genişlik (X)";
            this.numWidth.Location = new System.Drawing.Point(20, 105);
            this.numWidth.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numWidth.Size = new System.Drawing.Size(130, 25);

            this.lblHeight.ForeColor = System.Drawing.Color.DimGray;
            this.lblHeight.Location = new System.Drawing.Point(165, 85);
            this.lblHeight.Text = "Yükseklik (Y)";
            this.numHeight.Location = new System.Drawing.Point(165, 105);
            this.numHeight.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.numHeight.Size = new System.Drawing.Size(130, 25);

            // Butonlar
            StyleLightButton(this.btnCreateMap, "Harita Oluştur", 160);
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);

            StyleLightButton(this.btnRandomObstacles, "Rastgele Engel Ekle", 210);
            this.btnRandomObstacles.Click += new System.EventHandler(this.btnRandomObstacles_Click);

            StyleLightButton(this.btnUndo, "Son İşlemi Geri Al", 260);
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            // Başlat Butonu
            this.btnRunTest.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnRunTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunTest.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnRunTest.ForeColor = System.Drawing.Color.White;
            this.btnRunTest.Location = new System.Drawing.Point(20, 320);
            this.btnRunTest.Size = new System.Drawing.Size(275, 50);
            this.btnRunTest.Text = "PERFORMANS TESTİ BAŞLAT";
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);

            // DataGridView
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            this.dgvResults.Location = new System.Drawing.Point(10, 390);
            this.dgvResults.Size = new System.Drawing.Size(300, 230);
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.ColumnCount = 4;
            this.dgvResults.Columns[0].Name = "Veri Yapısı";
            this.dgvResults.Columns[1].Name = "Süre";
            this.dgvResults.Columns[2].Name = "Düğüm";
            this.dgvResults.Columns[3].Name = "Durum";

            // lblStatus
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(83, 92, 104);
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Size = new System.Drawing.Size(320, 90);
            this.lblStatus.Padding = new Padding(15, 0, 15, 10);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopLeft;

            // picGrid
            this.picGrid.BackColor = System.Drawing.Color.FromArgb(242, 245, 248);
            this.picGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGrid.Location = new System.Drawing.Point(320, 0);
            this.picGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrid_Paint);
            this.picGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseDown);

            // MainForm
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.picGrid);
            this.Controls.Add(this.pnlSidebar);
            this.Text = "Dinamik Rota Optimizasyonu";
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).EndInit();
            this.ResumeLayout(false);
        }

        private void StyleLightButton(System.Windows.Forms.Button btn, string text, int y)
        {
            btn.Text = text;
            btn.Location = new System.Drawing.Point(20, y);
            btn.Size = new System.Drawing.Size(275, 38);
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.BackColor = System.Drawing.Color.White;
            btn.ForeColor = System.Drawing.Color.FromArgb(45, 52, 54);
            btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(210, 214, 218);
            btn.Font = new Font("Segoe UI", 9.5F);
        }
    }
}