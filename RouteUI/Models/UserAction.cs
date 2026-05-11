using System.Collections.Generic;

namespace RouteUI.Models
{
    public class UserAction
    {
        public ActionType Type { get; set; }

        public NodeModel? Node { get; set; }

        public List<NodeModel>? Nodes { get; set; }

        // Tekli eylem için (başlangıç, bitiş noktası)
        public UserAction(ActionType type, NodeModel node)
        {
            Type = type;
            Node = node;
            Nodes = null;
        }

        // Çoklu eylem için (rastgele 1000 engel ekleme)
        public UserAction(ActionType type, List<NodeModel> nodes)
        {
            Type = type;
            Node = null;
            Nodes = nodes;
        }
    }
}