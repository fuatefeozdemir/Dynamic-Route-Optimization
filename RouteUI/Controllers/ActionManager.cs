using System.Collections.Generic;
using RouteUI.Logic;
using RouteUI.Models;

namespace RouteUI.Controllers
{
    public class ActionManager
    {
        private Stack<UserAction> _undoStack;
        private RouteManager _routeManager;
        private GridState _gridState;

        public ActionManager(RouteManager routeManager, GridState gridState)
        {
            _undoStack = new Stack<UserAction>();
            _routeManager = routeManager;
            _gridState = gridState;
        }

        public void AddAction(UserAction action)
        {
            _undoStack.Push(action);
        }

        public void ClearHistory()
        {
            _undoStack.Clear();
        }

        public bool HasHistory()
        {
            return _undoStack.Count > 0;
        }

        // Son işlemi geri alır
        public string UndoLastAction(ref bool isSelectingStart, ref bool isSelectingEnd)
        {
            if (_undoStack.Count == 0) return "Geri alınacak işlem yok.";

            var lastAction = _undoStack.Pop();
            switch (lastAction.Type)
            {
                case ActionType.SetStart:
                    lastAction.Node.State = CellState.Empty;
                    _gridState.SetStartNode(null);
                    isSelectingStart = true;
                    isSelectingEnd = false;
                    return "Başlangıç seçimi geri alındı.";

                case ActionType.SetEnd:
                    lastAction.Node.State = CellState.Empty;
                    _gridState.SetEndNode(null);
                    isSelectingEnd = true;
                    return "Bitiş seçimi geri alındı.";

                case ActionType.AddRandomObstacles:
                    if (lastAction.Nodes != null && lastAction.Nodes.Count > 0)
                    {
                        List<int> obstacleIds = new List<int>();
                        foreach (var n in lastAction.Nodes)
                        {
                            n.State = CellState.Empty;
                            obstacleIds.Add(n.Id);
                        }

                        _routeManager.RemoveObstaclesBatch(obstacleIds);
                    }
                    return "Rastgele eklenen engeller geri alındı.";

                default:
                    return "İşlem geri alındı.";
            }
        }
    }
}