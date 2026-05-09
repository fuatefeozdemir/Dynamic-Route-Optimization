namespace RouteUI.Models
{
    public enum QueueType : int
    {
        Array = 0,
        BST = 1,
        MinHeap = 2
    }

    public enum CellState
    {
        Empty = 0,
        Obstacle = 1,
        Start = 2,
        End = 3,
        Path = 4,
        Visited = 5
    }

    public enum ActionType
    {
        SetStart,
        SetEnd,
        ToggleObstacle,
        AddRandomObstacles
    }
}