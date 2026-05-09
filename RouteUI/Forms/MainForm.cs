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

        private const int GridWidth = 40;
        private const int GridHeight = 25;
        private const int CellSize = 25;

        public MainForm()
        {
            InitializeComponent();

            // Titremeyi önlemek için
            this.DoubleBuffered = true;

            // Altyapıyı kur
            _routeManager = new RouteManager(GridWidth, GridHeight);
            _gridState = new GridState(GridWidth, GridHeight, CellSize);

            // Form boyutunu ızgaraya göre ayarla
            this.ClientSize = new Size(GridWidth * CellSize, GridHeight * CellSize + 50); // +50 butonlar için
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Tüm ızgarayı ve hücre durumlarını çiz
            GridRenderer.Draw(e.Graphics, _gridState);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            var targetNode = _gridState.GetNodeAt(e.Location);
            if (targetNode == null) return;

            // CTRL basılıysa Başlangıç ata
            if (ModifierKeys == Keys.Control)
            {
                _gridState.SetStartNode(targetNode);
            }
            // SHIFT basılıysa Bitiş ata
            else if (ModifierKeys == Keys.Shift)
            {
                _gridState.SetEndNode(targetNode);
            }
            // Sadece tıklama ise Engel koy/kaldır
            else
            {
                _routeManager.ToggleObstacle(targetNode.Id);

                // C# tarafındaki görsel durumu güncelle
                targetNode.State = (targetNode.State == CellState.Obstacle)
                    ? CellState.Empty
                    : CellState.Obstacle;
            }

            this.Invalidate(); // Ekranı yenile
        }

        // "Yolu Bul" Butonuna tıklandığında (butona bağlanacak)
        private void btnFindPath_Click(object sender, EventArgs e)
        {
            if (_gridState.StartNode == null || _gridState.EndNode == null)
            {
                MessageBox.Show("Lütfen başlangıç ve bitiş noktalarını seçin!");
                return;
            }

            // 1. Önceki yolu temizle
            _gridState.ClearPath();

            // 2. C++ tarafındaki komşulukları güncelle
            _routeManager.BuildConnections();

            // 3. Yolu hesapla (Örn: MinHeap [2] kullanarak)
            Metrics metrics;
            var pathIds = _routeManager.FindPath(
                _gridState.StartNode.Id,
                _gridState.EndNode.Id,
                2, // MinHeap
                out metrics);

            if (pathIds.Count > 0)
            {
                // 4. Gelen ID'leri görsel olarak işaretle
                foreach (int id in pathIds)
                {
                    var node = _gridState.GetNodeById(id);
                    if (node != null && node.State == CellState.Empty)
                    {
                        node.State = CellState.Path;
                    }
                }

                // İstatistikleri göster
                Console.WriteLine($"Süre: {metrics.TimeMicroseconds} us, İncelenen Node: {metrics.NodesExamined}");
            }
            else
            {
                MessageBox.Show("Yol bulunamadı!");
            }

            this.Invalidate(); // Sonucu çizdir
        }

        // Bellek sızıntısını önlemek için C++ nesnesini yok et
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _routeManager?.Dispose();
        }
    }
}