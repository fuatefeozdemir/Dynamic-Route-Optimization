using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using RouteUI.Logic;
using RouteUI.Models;
using RouteUI.Rendering;

namespace RouteUI
{
    public partial class MainForm : Form
    {
        private RouteManager? _routeManager;
        private GridState? _gridState;
        private Stack<UserAction> _undoStack = new Stack<UserAction>();

        private bool _isSelectingStart = false;
        private bool _isSelectingEnd = false;

        public MainForm()
        {
            InitializeComponent();
            SetupModernUI();

            // Uygulamayı her zaman tam ekran başlatır
            this.WindowState = FormWindowState.Maximized;

            UpdateStatus("Harita boyutlarını girip 'Harita Oluştur' butonuna tıklayın.");
        }

        // Tasarımı minimal ve flat hale getiren görsel ayarlar
        private void SetupModernUI()
        {
            // Panel ve kenarlık ayarları
            panelSidebar.BackColor = Color.FromArgb(248, 249, 250); // Açık, temiz gri

            // Tabloyu (DataGridView) modernize etme
            dgvResults.BackgroundColor = Color.FromArgb(248, 249, 250);
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvResults.DefaultCellStyle.SelectionBackColor = Color.White; // Seçim rengini iptal et
            dgvResults.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvResults.DefaultCellStyle.Padding = new Padding(5);
            dgvResults.RowTemplate.Height = 45;
            dgvResults.ColumnHeadersHeight = 50;
            dgvResults.Font = new Font("Segoe UI", 10F);
            dgvResults.EnableHeadersVisualStyles = false;

            // Tablo Başlıkları
            dgvResults.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvResults.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 233, 236);
            dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvResults.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvResults.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            // Butonlara Hover (Üzerine gelme) efektleri ekleme
            btnCreateMap.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnRandomObstacles.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
            btnUndo.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);

            // Ana buton (Test Başlat) için Hover rengi
            btnRunTest.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 100, 190);
        }

        private void btnCreateMap_Click(object sender, EventArgs e)
        {
            int w = (int)numWidth.Value;
            int h = (int)numHeight.Value;

            _routeManager?.Dispose();
            _routeManager = new RouteManager(w, h);
            _gridState = new GridState(w, h, picGrid.Size);

            _undoStack.Clear();
            dgvResults.Rows.Clear();

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
            if (_gridState == null) return;

            var targetNode = _gridState.GetNodeAt(e.Location);
            if (targetNode == null) return;

            if (_isSelectingStart)
            {
                _gridState.SetStartNode(targetNode);
                _undoStack.Push(new UserAction(ActionType.SetStart, targetNode));
                _isSelectingStart = false;
                _isSelectingEnd = true;
                UpdateStatus("Başlangıç seçildi. Şimdi BİTİŞ noktasını seçin.");
            }
            else if (_isSelectingEnd)
            {
                if (targetNode.State == CellState.Start) return;
                _gridState.SetEndNode(targetNode);
                _undoStack.Push(new UserAction(ActionType.SetEnd, targetNode));
                _isSelectingEnd = false;
                UpdateStatus("Noktalar hazır. Engel ekleyebilir veya testi başlatabilirsiniz.");
            }

            picGrid.Invalidate();
        }

        private async void btnRandomObstacles_Click(object sender, EventArgs e)
        {
            if (_gridState == null || _isSelectingStart || _isSelectingEnd || _routeManager == null) return;

            btnRandomObstacles.Enabled = false;
            btnCreateMap.Enabled = false;
            btnRunTest.Enabled = false;
            UpdateStatus("Engeller hesaplanıyor, lütfen bekleyin...");

            Random rnd = new Random();
            List<NodeModel> addedObstacles = new List<NodeModel>();

            await Task.Run(() =>
            {
                foreach (var node in _gridState.Nodes)
                {
                    if (node.State == CellState.Empty && rnd.Next(100) < 20)
                    {
                        node.State = CellState.Obstacle;
                        _routeManager.ToggleObstacle(node.Id);
                        addedObstacles.Add(node);
                    }
                }
            });

            if (addedObstacles.Count > 0)
            {
                _undoStack.Push(new UserAction(ActionType.AddRandomObstacles, addedObstacles));
                UpdateStatus($"{addedObstacles.Count} adet engel eklendi.");
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
            if (_undoStack.Count == 0 || _gridState == null || _routeManager == null) return;

            var lastAction = _undoStack.Pop();
            switch (lastAction.Type)
            {
                case ActionType.SetStart:
                    lastAction.Node.State = CellState.Empty;
                    _isSelectingStart = true;
                    _isSelectingEnd = false;
                    UpdateStatus("Başlangıç seçimi geri alındı.");
                    break;
                case ActionType.SetEnd:
                    lastAction.Node.State = CellState.Empty;
                    _isSelectingEnd = true;
                    UpdateStatus("Bitiş seçimi geri alındı.");
                    break;
                case ActionType.AddRandomObstacles:
                    foreach (var n in lastAction.Nodes)
                    {
                        n.State = CellState.Empty;
                        _routeManager.ToggleObstacle(n.Id);
                    }
                    UpdateStatus("Engeller geri alındı.");
                    break;
            }
            picGrid.Invalidate();
        }

        private async void btnRunTest_Click(object sender, EventArgs e)
        {
            if (_gridState?.StartNode == null || _gridState?.EndNode == null || _routeManager == null)
            {
                MessageBox.Show("Başlangıç ve bitiş noktalarını belirlemeden test başlatılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnRunTest.Enabled = false;
            dgvResults.Rows.Clear();
            _routeManager.BuildConnections();

            string[] queueNames = { "Dizi (Array)", "BST", "Min-Heap" };
            int[] cppQueueIds = { 1, 3, 2 };

            for (int i = 0; i < 3; i++)
            {
                UpdateStatus($"{queueNames[i]} çalışıyor...");

                _gridState.ClearPath();
                picGrid.Invalidate();
                await Task.Delay(500);

                Metrics metrics;
                var result = _routeManager.FindPath(_gridState.StartNode.Id, _gridState.EndNode.Id, cppQueueIds[i], out metrics);

                int totalVisited = result.Visited.Count;
                double uiDurationMs = metrics.TimeMicroseconds * 0.2;
                uiDurationMs = Math.Max(1500, Math.Min(uiDurationMs, 20000));

                int totalFrames = Math.Max(1, (int)(uiDurationMs / 15.0));
                int batchSize = Math.Max(1, totalVisited / totalFrames);
                int stepCounter = 0;

                foreach (var id in result.Visited)
                {
                    var n = _gridState.GetNodeById(id);
                    if (n != null && n.State == CellState.Empty)
                    {
                        n.State = CellState.Visited;
                        stepCounter++;

                        if (stepCounter % batchSize == 0)
                        {
                            picGrid.Invalidate();
                            await Task.Delay(15);
                        }
                    }
                }

                picGrid.Invalidate();

                if (result.Path.Count > 0)
                {
                    foreach (var id in result.Path)
                    {
                        var n = _gridState.GetNodeById(id);
                        if (n != null && (n.State == CellState.Visited || n.State == CellState.Empty))
                        {
                            n.State = CellState.Path;
                        }
                    }
                    picGrid.Invalidate();
                }

                dgvResults.Rows.Add(queueNames[i], $"{metrics.TimeMicroseconds} μs", metrics.NodesExamined, metrics.RouteFound ? "Başarılı" : "Başarısız");

                if (i < 2) await Task.Delay(2000);
            }

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