using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RouteUI.Logic;
using RouteUI.Models;
using RouteUI.Rendering;
using RouteUI.Controllers;

namespace RouteUI
{
    public partial class MainForm : Form
    {
        private RouteManager? _routeManager;
        private GridState? _gridState;
        private ActionManager? _actionManager;
        private SimulationEngine? _simulationEngine;

        private bool _isSelectingStart = false;
        private bool _isSelectingEnd = false;

        public MainForm()
        {
            InitializeComponent();
            SetupModernUI();

            this.WindowState = FormWindowState.Maximized;
            UpdateStatus("Harita boyutlarını girip 'Harita Oluştur' butonuna tıklayın.");
        }

        private void SetupModernUI()
        {
            panelSidebar.BackColor = Color.FromArgb(248, 249, 250);

            dgvResults.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.White;
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvResults.DefaultCellStyle.Padding = new Padding(5);
            dgvResults.RowTemplate.Height = 45;
            dgvResults.ColumnHeadersHeight = 50;
            dgvResults.Font = new Font("Segoe UI", 10F);
            dgvResults.EnableHeadersVisualStyles = false;

            dgvResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 233, 236);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvResults.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            // Sütun genişlikleri
            dgvResults.Columns[0].Width = 40;  // Sıralama
            dgvResults.Columns[1].Width = 100; // Veri Yapısı

            btnCreateMap.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnRandomObstacles.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnUndo.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnRunTest.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 190);

            lblCurrentAlgorithm.Visible = false;
        }

        private void btnCreateMap_Click(object sender, EventArgs e)
        {
            int w = (int)numWidth.Value;
            int h = (int)numHeight.Value;

            if (_routeManager == null)
            {
                _routeManager = new RouteManager(w, h);
            }
            else
            {
                _routeManager.ResetGraph(w, h);
            }

            _gridState = new GridState(w, h, picGrid.Size);
            _actionManager = new ActionManager(_routeManager, _gridState);
            _simulationEngine = new SimulationEngine(_routeManager, _gridState, picGrid);

            dgvResults.Rows.Clear();
            lblCurrentAlgorithm.Visible = false;
            _isSelectingStart = true;
            _isSelectingEnd = false;

            UpdateStatus("Harita ölçeklendi. Önce Başlangıç (Yeşil), sonra Bitiş (Mavi) seçin.");
            picGrid.Invalidate();
        }

        private void picGrid_Paint(object sender, PaintEventArgs e)
        {
            if (_gridState != null)
            {
                GridRenderer.Draw(e.Graphics, _gridState);
            }
        }

        private void picGrid_MouseDown(object sender, MouseEventArgs e)
        {
            if (_gridState == null || _actionManager == null) return;

            var targetNode = _gridState.GetNodeAt(e.Location);
            if (targetNode == null) return;

            if (_isSelectingStart)
            {
                _gridState.SetStartNode(targetNode);
                _actionManager.AddAction(new UserAction(ActionType.SetStart, targetNode));

                _isSelectingStart = false;
                _isSelectingEnd = true;
                UpdateStatus("Başlangıç seçildi. Şimdi BİTİŞ noktasını seçin.");
            }
            else if (_isSelectingEnd)
            {
                if (targetNode.State == CellState.Start) return;

                _gridState.SetEndNode(targetNode);
                _actionManager.AddAction(new UserAction(ActionType.SetEnd, targetNode));

                _isSelectingEnd = false;
                UpdateStatus("Noktalar hazır. Engel ekleyebilir veya testi başlatabilirsiniz.");
            }

            picGrid.Invalidate();
        }

        private void btnRandomObstacles_Click(object sender, EventArgs e)
        {
            if (_gridState == null || _isSelectingStart || _isSelectingEnd || _routeManager == null || _actionManager == null) return;

            btnRandomObstacles.Enabled = false;
            btnCreateMap.Enabled = false;
            btnRunTest.Enabled = false;
            UpdateStatus("Engeller C++ motorunda hesaplanıyor...");

            var obstacleIds = _routeManager.GenerateRandomObstacles(20);

            if (obstacleIds.Count > 0)
            {
                List<NodeModel> addedObstacles = new List<NodeModel>();

                foreach (var id in obstacleIds)
                {
                    var node = _gridState.GetNodeById(id);
                    if (node != null)
                    {
                        node.State = CellState.Obstacle;
                        addedObstacles.Add(node);
                    }
                }

                _actionManager.AddAction(new UserAction(ActionType.AddRandomObstacles, addedObstacles));
                UpdateStatus($"{obstacleIds.Count} adet engel eklendi.");
                picGrid.Invalidate();
            }
            else
            {
                UpdateStatus("Haritaya engel eklenmedi.");
            }

            btnRandomObstacles.Enabled = true;
            btnCreateMap.Enabled = true;
            btnRunTest.Enabled = true;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_actionManager == null || !_actionManager.HasHistory()) return;

            string statusMessage = _actionManager.UndoLastAction(ref _isSelectingStart, ref _isSelectingEnd);

            UpdateStatus(statusMessage);
            picGrid.Invalidate();
        }

        private async void btnRunTest_Click(object sender, EventArgs e)
        {
            if (_gridState?.StartNode == null || _gridState?.EndNode == null || _simulationEngine == null || _routeManager == null)
            {
                MessageBox.Show("Başlangıç ve bitiş noktalarını belirlemeden test başlatılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnRunTest.Enabled = false;
            dgvResults.Rows.Clear();
            _routeManager.BuildConnections();

            string[] queueNames = { "Dizi (Array)", "BST", "Min-Heap" };
            int[] cppQueueIds = { 1, 3, 2 };

            var performanceResults = new List<(DataGridViewRow Row, long Time)>();

            for (int i = 0; i < 3; i++)
            {
                lblCurrentAlgorithm.Text = $"⏳ ŞU AN ÇALIŞAN: {queueNames[i].ToUpper()}";
                lblCurrentAlgorithm.ForeColor = Color.DarkOrange;
                lblCurrentAlgorithm.Visible = true;
                UpdateStatus($"{queueNames[i]} algoritması çalışıyor...");

                Metrics metrics = await _simulationEngine.RunAlgorithmAsync(cppQueueIds[i]);

                int rowIndex = dgvResults.Rows.Add("-", queueNames[i], $"{metrics.TimeMicroseconds} μs", metrics.NodesExamined, metrics.RouteFound ? "Başarılı" : "Başarısız");
                performanceResults.Add((dgvResults.Rows[rowIndex], metrics.TimeMicroseconds));

                if (i < 2) await Task.Delay(2000);
            }

            // Testler bitince süreye göre sıralar
            performanceResults.Sort((a, b) => a.Time.CompareTo(b.Time));

            for (int rank = 0; rank < performanceResults.Count; rank++)
            {
                performanceResults[rank].Row.Cells[0].Value = $"{rank + 1}.";

                // 1. olanı yeşil renkle ve kalın fontla belirginleştir
                if (rank == 0)
                {
                    performanceResults[rank].Row.DefaultCellStyle.BackColor = Color.FromArgb(235, 255, 235);
                    performanceResults[rank].Row.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                }
            }

            lblCurrentAlgorithm.Text = "TESTLER TAMAMLANDI";
            lblCurrentAlgorithm.ForeColor = Color.MediumSeaGreen;
            UpdateStatus("Testler bitti. Sonuçları tablodan inceleyebilirsiniz.");
            btnRunTest.Enabled = true;
        }

        private void UpdateStatus(string text) => lblStatus.Text = "Durum: " + text;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _routeManager?.Dispose();
        }
    }
}