using System.Collections.Generic;

namespace RouteUI.Models
{
    public class UserAction
    {
        public ActionType Type { get; set; }
        public NodeModel Node { get; set; }
        public List<NodeModel> Nodes { get; set; }

        public UserAction(ActionType type, NodeModel node)
        {
            Type = type;
            Node = node;
            Nodes = new List<NodeModel>();
        }

        public UserAction(ActionType type, List<NodeModel> nodes)
        {
            Type = type;
            Nodes = nodes;
        }
    }
}