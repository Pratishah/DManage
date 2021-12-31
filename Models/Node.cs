using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class Node
    {
        public Node()
        {
            NodeEdgeEndNodes = new HashSet<NodeEdge>();
            NodeEdgeStartNodes = new HashSet<NodeEdge>();
            Pallates = new HashSet<Pallate>();
        }

        public int NodeId { get; set; }
        public string NodeName { get; set; }
        public string Zone { get; set; }
        public string Createdby { get; set; }
        public string Modifiedby { get; set; }
        public DateTime? CratedDateTime { get; set; }
        public DateTime? ModifiedDatetime { get; set; }

        public virtual ICollection<NodeEdge> NodeEdgeEndNodes { get; set; }
        public virtual ICollection<NodeEdge> NodeEdgeStartNodes { get; set; }
        public virtual ICollection<Pallate> Pallates { get; set; }
    }
}
