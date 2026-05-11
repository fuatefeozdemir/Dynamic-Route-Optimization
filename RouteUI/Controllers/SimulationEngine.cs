using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using RouteUI.Logic;
using RouteUI.Models;

namespace RouteUI.Controllers
{
    public class SimulationEngine
    {
        private RouteManager _routeManager;
        private GridState _gridState;
        private Control _renderTarget;

        public SimulationEngine(RouteManager routeManager, GridState gridState, Control renderTarget)
        {
            _routeManager = routeManager;
            _gridState = gridState;
            _renderTarget = renderTarget;
        }

        public async Task<Metrics> RunAlgorithmAsync(int queueType)
        {
            if (_gridState.StartNode == null || _gridState.EndNode == null)
                return new Metrics();

            // Her yeni testten önce eski rotayı temizle ve bekle
            _gridState.ClearPath();
            _renderTarget.Invalidate();
            await Task.Delay(500);

            Metrics metrics;
            var result = _routeManager.FindPath(_gridState.StartNode.Id, _gridState.EndNode.Id, queueType, out metrics);

            int totalVisited = result.Visited.Count;

            double uiDurationMs = metrics.TimeMicroseconds * 0.2;
            uiDurationMs = Math.Max(1500, Math.Min(uiDurationMs, 20000)); // En az 1.5 sn, en çok 20 sn

            int totalFrames = Math.Max(1, (int)(uiDurationMs / 15.0));
            int batchSize = Math.Max(1, totalVisited / totalFrames);
            int stepCounter = 0;

            // Arama Animasyonu
            foreach (var id in result.Visited)
            {
                var n = _gridState.GetNodeById(id);
                if (n != null && n.State == CellState.Empty)
                {
                    n.State = CellState.Visited;
                    stepCounter++;

                    // Ekranı belirlenen boyuta göre güncelle
                    if (stepCounter % batchSize == 0)
                    {
                        _renderTarget.Invalidate();
                        await Task.Delay(15);
                    }
                }
            }

            _renderTarget.Invalidate();

            // Rotayı çiz
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
                _renderTarget.Invalidate();
            }

            return metrics;
        }
    }
}