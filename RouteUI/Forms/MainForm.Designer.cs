namespace RouteUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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

            // Form Settings
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1024, 600);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dinamik Rota Optimizasyonu";
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 70;
            this.pnlTop.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // X Ayarı
            this.lblWidth.Text = "Genişlik (X):";
            this.lblWidth.Location = new System.Drawing.Point(15, 25);
            this.lblWidth.AutoSize = true;
            this.numWidth.Value = 30;
            this.numWidth.Location = new System.Drawing.Point(90, 23);
            this.numWidth.Width = 60;

            // Y Ayarı
            this.lblHeight.Text = "Yükseklik (Y):";
            this.lblHeight.Location = new System.Drawing.Point(170, 25);
            this.lblHeight.AutoSize = true;
            this.numHeight.Value = 20;
            this.numHeight.Location = new System.Drawing.Point(250, 23);
            this.numHeight.Width = 60;

            // Butonlar
            this.btnCreateMap.Text = "Harita Oluştur";
            this.btnCreateMap.Location = new System.Drawing.Point(340, 18);
            this.btnCreateMap.Size = new System.Drawing.Size(120, 35);
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);

            this.btnRandomObstacles.Text = "Rastgele Engel";
            this.btnRandomObstacles.Location = new System.Drawing.Point(470, 18);
            this.btnRandomObstacles.Size = new System.Drawing.Size(120, 35);
            this.btnRandomObstacles.Click += new System.EventHandler(this.btnRandomObstacles_Click);

            this.btnUndo.Text = "Geri Al";
            this.btnUndo.Location = new System.Drawing.Point(600, 18);
            this.btnUndo.Size = new System.Drawing.Size(90, 35);
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);

            this.btnRunTest.Text = "▶ ROTAYI BUL (TEST)";
            this.btnRunTest.Location = new System.Drawing.Point(710, 18);
            this.btnRunTest.Size = new System.Drawing.Size(200, 35);
            this.btnRunTest.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnRunTest.ForeColor = System.Drawing.Color.White;
            this.btnRunTest.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRunTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);

            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblWidth, this.numWidth, this.lblHeight, this.numHeight,
                this.btnCreateMap, this.btnRandomObstacles, this.btnUndo, this.btnRunTest
            });

            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Width = 350;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Status Label
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Height = 60;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular);
            this.lblStatus.ForeColor = System.Drawing.Color.DarkSlateGray;

            // Tablo (DataGrid)
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.dgvResults.ColumnCount = 4;
            this.dgvResults.Columns[0].Name = "Veri Yapısı";
            this.dgvResults.Columns[1].Name = "Süre";
            this.dgvResults.Columns[2].Name = "Düğüm";
            this.dgvResults.Columns[3].Name = "Durum";

            this.pnlRight.Controls.Add(this.dgvResults);
            this.pnlRight.Controls.Add(this.lblStatus);

            // 
            // picGrid
            // 
            this.picGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGrid.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.picGrid.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrid_Paint);
            this.picGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseDown);

            // Form'a ekleme
            this.Controls.Add(this.picGrid);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlTop);

            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            this.ResumeLayout(false);
        }
    }
}