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

            picGrid.Invalidate();
        }

        private void btnRandomObstacles_Click(object sender, EventArgs e)
        {
            if (_gridState == null || _isSelectingStart || _isSelectingEnd) return;

            Random rnd = new Random();
            List<NodeModel> addedObstacles = new List<NodeModel>();

            foreach (var node in _gridState.Nodes)
            {
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

        private async void btnRunTest_Click(object sender, EventArgs e)
        {
            if (_gridState?.StartNode == null || _gridState?.EndNode == null)
            {
                MessageBox.Show("Başlangıç ve bitiş seçmelisiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Arayüz hazırlığı
            btnRunTest.Enabled = false;
            dgvResults.Rows.Clear();
            _routeManager.BuildConnections();

            // C# tarafındaki isimlendirme sırası
            string[] queueNames = { "Dizi (Array)", "BST", "Min-Heap" };

            // KRİTİK NOKTA: C++ tarafındaki switch-case yapısına uygun ID eşleştirmesi
            // C++'ta: 1=Array, 2=MinHeap, 3=BST olarak kodlanmış.
            int[] cppQueueIds = { 1, 3, 2 };

            for (int i = 0; i < 3; i++)
            {
                UpdateStatus($"{queueNames[i]} kullanılarak rota aranıyor...");

                // 1. Haritayı temizle (Önceki algoritmanın izlerini sil)
                foreach (var node in _gridState.Nodes)
                {
                    if (node.State == CellState.Path || node.State == CellState.Visited)
                    {
                        node.State = CellState.Empty;
                    }
                }
                picGrid.Invalidate();

                // Temizliği kullanıcının fark etmesi için kısa bir bekleme
                await Task.Delay(500);

                // 2. Algoritmayı çalıştır (cppQueueIds[i] ile doğru ID'yi gönderiyoruz)
                Metrics metrics;
                var result = _routeManager.FindPath(_gridState.StartNode.Id, _gridState.EndNode.Id, cppQueueIds[i], out metrics);

                // 3. Su dalgası animasyonu (Arama süreci)
                foreach (var id in result.Visited)
                {
                    var n = _gridState.GetNodeById(id);
                    if (n != null && n.State == CellState.Empty)
                    {
                        n.State = CellState.Visited;
                        picGrid.Invalidate(n.Bounds);
                        // Harita çok büyükse hızı artırmak için delay'i düşürebilirsin
                        await Task.Delay(2);
                    }
                }

                await Task.Delay(150); // Rota çizilmeden önce çok kısa bekle

                // 4. Bitiş yolu (Kırmızı Hat) çizimi
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

                // 5. Verileri tabloya ekle
                dgvResults.Rows.Add(
                    queueNames[i],
                    $"{metrics.TimeMicroseconds} μs",
                    metrics.NodesExamined,
                    metrics.RouteFound ? "Başarılı" : "Başarısız"
                );

                // 6. Bir sonraki algoritma başlamadan önce sonucu görmemiz için bekle
                if (i < 2)
                {
                    UpdateStatus($"{queueNames[i]} bitti. Sonraki bekleniyor...");
                    await Task.Delay(2000);
                }
            }

            UpdateStatus("Tüm performans testleri başarıyla tamamlandı.");
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