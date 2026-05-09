using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using RouteUI.Logic;
using RouteUI.Models;
using RouteUI.Rendering;

namespace RouteUI
{
    public partial class MainForm : Form
    {
        private RouteManager _routeManager;
        private GridState _gridState;
        private Stack<UserAction> _undoStack = new Stack<UserAction>();

        private bool _isSelectingStart = false;
        private bool _isSelectingEnd = false;

        public MainForm()
        {
            InitializeComponent();
            UpdateStatus("Lütfen harita boyutlarını girip 'Harita Oluştur'a basın.");
        }

        private void btnCreateMap_Click(object sender, EventArgs e)
        {
            int w = (int)numWidth.Value;
            int h = (int)numHeight.Value;
            int cellSize = 25;

            _routeManager?.Dispose();
            _routeManager = new RouteManager(w, h);
            _gridState = new GridState(w, h, cellSize);
            _undoStack.Clear();
            dgvResults.Rows.Clear();

            _isSelectingStart = true;
            _isSelectingEnd = false;

            UpdateStatus("Harita oluşturuldu. Lütfen BAŞLANGIÇ noktasını seçin (Yeşil).");
            picGrid.Invalidate(); // Sadece çizim alanını yenile
        }

        private void picGrid_Paint(object sender, PaintEventArgs e)
        {
            if (_gridState != null)
            {
                // Çizim artık temiz bir tuvalde (PictureBox) yapılıyor
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
                UpdateStatus("Başlangıç seçildi. Lütfen BİTİŞ noktasını seçin (Mavi).");
            }
            else if (_isSelectingEnd)
            {
                if (targetNode.State == CellState.Start) return;
                _gridState.SetEndNode(targetNode);
                _undoStack.Push(new UserAction(ActionType.SetEnd, targetNode));
                _isSelectingEnd = false;
                UpdateStatus("Noktalar hazır. 'Rastgele Engel' ekleyebilir veya 'Rotayı Bul'a basabilirsiniz.");
            }
            // Manuel engel ekleme tamamen kaldırıldı.

            picGrid.Invalidate();
        }

        private void btnRandomObstacles_Click(object sender, EventArgs e)
        {
            if (_gridState == null || _isSelectingStart || _isSelectingEnd) return;

            Random rnd = new Random();
            List<NodeModel> addedObstacles = new List<NodeModel>();

            foreach (var node in _gridState.Nodes)
            {
                // Başlangıç, bitiş veya mevcut engel değilse %20 ihtimalle engel koy
                if (node.State == CellState.Empty && rnd.Next(100) < 20)
                {
                    node.State = CellState.Obstacle;
                    _routeManager.ToggleObstacle(node.Id);
                    addedObstacles.Add(node);
                }
            }

            if (addedObstacles.Count > 0)
            {
                _undoStack.Push(new UserAction(ActionType.AddRandomObstacles, addedObstacles));
                UpdateStatus($"{addedObstacles.Count} rastgele engel eklendi.");
                picGrid.Invalidate();
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_undoStack.Count == 0) return;

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
                    UpdateStatus("Son eklenen rastgele engeller kaldırıldı.");
                    break;
            }
            picGrid.Invalidate();
        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            if (_gridState?.StartNode == null || _gridState?.EndNode == null)
            {
                MessageBox.Show("Testi başlatmak için başlangıç ve bitiş noktalarını seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvResults.Rows.Clear();
            _routeManager.BuildConnections();

            // Sırasıyla 3 senaryoyu tamamen sıfırdan koştur
            for (int i = 0; i < 3; i++)
            {
                _gridState.ClearPath(); // C# tarafında görsel rotayı temizle

                Metrics metrics;
                var path = _routeManager.FindPath(_gridState.StartNode.Id, _gridState.EndNode.Id, i, out metrics);

                string queueName = i switch { 0 => "Dizi (Array)", 1 => "BST", 2 => "Min-Heap", _ => "Bilinmeyen" };

                dgvResults.Rows.Add(queueName, $"{metrics.TimeMicroseconds} μs", metrics.NodesExamined, metrics.RouteFound ? "Başarılı" : "Başarısız");

                // Sadece son algoritmanın (Min-Heap) bulduğu yolu ekrana çizdir
                if (i == 2 && path.Count > 0)
                {
                    foreach (var id in path)
                    {
                        var n = _gridState.GetNodeById(id);
                        if (n != null && n.State == CellState.Empty)
                        {
                            n.State = CellState.Path;
                        }
                    }
                }
            }

            UpdateStatus("Performans testi tamamlandı.");
            picGrid.Invalidate();
        }

        private void UpdateStatus(string text) => lblStatus.Text = "Durum: " + text;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _routeManager?.Dispose();
        }
    }
}