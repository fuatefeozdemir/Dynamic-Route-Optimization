namespace RouteUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.lblCurrentAlgorithm = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colSiralama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVeriYapisi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDugum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDurum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRunTest = new System.Windows.Forms.Button();
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRandomObstacles = new System.Windows.Forms.Button();
            this.btnCreateMap = new System.Windows.Forms.Button();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.numHeight = new System.Windows.Forms.NumericUpDown();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picGrid = new System.Windows.Forms.PictureBox();
            this.panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelSidebar.Controls.Add(this.lblCurrentAlgorithm);
            this.panelSidebar.Controls.Add(this.lblStatus);
            this.panelSidebar.Controls.Add(this.dgvResults);
            this.panelSidebar.Controls.Add(this.btnRunTest);
            this.panelSidebar.Controls.Add(this.btnUndo);
            this.panelSidebar.Controls.Add(this.btnRandomObstacles);
            this.panelSidebar.Controls.Add(this.btnCreateMap);
            this.panelSidebar.Controls.Add(this.lblHeight);
            this.panelSidebar.Controls.Add(this.lblWidth);
            this.panelSidebar.Controls.Add(this.numHeight);
            this.panelSidebar.Controls.Add(this.numWidth);
            this.panelSidebar.Controls.Add(this.lblTitle);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(20);
            this.panelSidebar.Size = new System.Drawing.Size(420, 800);
            this.panelSidebar.TabIndex = 0;
            // 
            // lblCurrentAlgorithm
            // 
            this.lblCurrentAlgorithm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrentAlgorithm.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblCurrentAlgorithm.Location = new System.Drawing.Point(20, 390);
            this.lblCurrentAlgorithm.Name = "lblCurrentAlgorithm";
            this.lblCurrentAlgorithm.Size = new System.Drawing.Size(380, 30);
            this.lblCurrentAlgorithm.TabIndex = 11;
            this.lblCurrentAlgorithm.Text = "⏳ ŞU AN ÇALIŞAN: DİZİ (ARRAY)";
            this.lblCurrentAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblStatus.Location = new System.Drawing.Point(20, 740);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(380, 40);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Durum: Bekleniyor...";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeColumns = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSiralama,
            this.colVeriYapisi,
            this.colSure,
            this.colDugum,
            this.colDurum});
            this.dgvResults.Location = new System.Drawing.Point(20, 435);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 45;
            this.dgvResults.Size = new System.Drawing.Size(380, 240);
            this.dgvResults.TabIndex = 9;
            // 
            // colSiralama
            // 
            this.colSiralama.HeaderText = "#";
            this.colSiralama.Name = "colSiralama";
            this.colSiralama.ReadOnly = true;
            // 
            // colVeriYapisi
            // 
            this.colVeriYapisi.HeaderText = "Veri Yapısı";
            this.colVeriYapisi.Name = "colVeriYapisi";
            this.colVeriYapisi.ReadOnly = true;
            // 
            // colSure
            // 
            this.colSure.HeaderText = "Süre";
            this.colSure.Name = "colSure";
            this.colSure.ReadOnly = true;
            // 
            // colDugum
            // 
            this.colDugum.HeaderText = "Düğüm";
            this.colDugum.Name = "colDugum";
            this.colDugum.ReadOnly = true;
            // 
            // colDurum
            // 
            this.colDurum.HeaderText = "Durum";
            this.colDurum.Name = "colDurum";
            this.colDurum.ReadOnly = true;
            // 
            // btnRunTest
            // 
            this.btnRunTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRunTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRunTest.FlatAppearance.BorderSize = 0;
            this.btnRunTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunTest.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRunTest.ForeColor = System.Drawing.Color.White;
            this.btnRunTest.Location = new System.Drawing.Point(20, 320);
            this.btnRunTest.Name = "btnRunTest";
            this.btnRunTest.Size = new System.Drawing.Size(380, 45);
            this.btnRunTest.TabIndex = 8;
            this.btnRunTest.Text = "PERFORMANS TESTİNİ BAŞLAT";
            this.btnRunTest.UseVisualStyleBackColor = false;
            this.btnRunTest.Click += new System.EventHandler(this.btnRunTest_Click);
            // 
            // btnUndo
            // 
            this.btnUndo.BackColor = System.Drawing.Color.White;
            this.btnUndo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUndo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUndo.Location = new System.Drawing.Point(20, 255);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(380, 40);
            this.btnUndo.TabIndex = 7;
            this.btnUndo.Text = "Son İşlemi Geri Al";
            this.btnUndo.UseVisualStyleBackColor = false;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRandomObstacles
            // 
            this.btnRandomObstacles.BackColor = System.Drawing.Color.White;
            this.btnRandomObstacles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRandomObstacles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnRandomObstacles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandomObstacles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRandomObstacles.Location = new System.Drawing.Point(20, 205);
            this.btnRandomObstacles.Name = "btnRandomObstacles";
            this.btnRandomObstacles.Size = new System.Drawing.Size(380, 40);
            this.btnRandomObstacles.TabIndex = 6;
            this.btnRandomObstacles.Text = "Rastgele Engel Ekle";
            this.btnRandomObstacles.UseVisualStyleBackColor = false;
            this.btnRandomObstacles.Click += new System.EventHandler(this.btnRandomObstacles_Click);
            // 
            // btnCreateMap
            // 
            this.btnCreateMap.BackColor = System.Drawing.Color.White;
            this.btnCreateMap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateMap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCreateMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateMap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCreateMap.Location = new System.Drawing.Point(20, 155);
            this.btnCreateMap.Name = "btnCreateMap";
            this.btnCreateMap.Size = new System.Drawing.Size(380, 40);
            this.btnCreateMap.TabIndex = 5;
            this.btnCreateMap.Text = "Harita Oluştur";
            this.btnCreateMap.UseVisualStyleBackColor = false;
            this.btnCreateMap.Click += new System.EventHandler(this.btnCreateMap_Click);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeight.Location = new System.Drawing.Point(220, 90);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(73, 15);
            this.lblHeight.TabIndex = 4;
            this.lblHeight.Text = "Yükseklik (Y)";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblWidth.Location = new System.Drawing.Point(20, 90);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(65, 15);
            this.lblWidth.TabIndex = 3;
            this.lblWidth.Text = "Genişlik (X)";
            // 
            // numHeight
            // 
            this.numHeight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numHeight.Location = new System.Drawing.Point(220, 110);
            this.numHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHeight.Name = "numHeight";
            this.numHeight.Size = new System.Drawing.Size(180, 25);
            this.numHeight.TabIndex = 2;
            this.numHeight.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // numWidth
            // 
            this.numWidth.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numWidth.Location = new System.Drawing.Point(20, 110);
            this.numWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(180, 25);
            this.numWidth.TabIndex = 1;
            this.numWidth.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(306, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dinamik Rota Optimizasyonu";
            // 
            // picGrid
            // 
            this.picGrid.BackColor = System.Drawing.Color.White;
            this.picGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picGrid.Location = new System.Drawing.Point(420, 0);
            this.picGrid.Name = "picGrid";
            this.picGrid.Size = new System.Drawing.Size(764, 800);
            this.picGrid.TabIndex = 1;
            this.picGrid.TabStop = false;
            this.picGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.picGrid_Paint);
            this.picGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picGrid_MouseDown);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 800);
            this.Controls.Add(this.picGrid);
            this.Controls.Add(this.panelSidebar);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dijkstra Optimizasyon Testi";
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.NumericUpDown numHeight;
        private System.Windows.Forms.NumericUpDown numWidth;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Button btnCreateMap;
        private System.Windows.Forms.Button btnRandomObstacles;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRunTest;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox picGrid;
        private System.Windows.Forms.Label lblCurrentAlgorithm;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSiralama;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVeriYapisi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSure;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDugum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDurum;
    }
}