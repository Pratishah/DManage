using System;
using System.Collections.Generic;

#nullable disable

namespace DManage.Models
{
    public partial class NodeEdge
    {
        public Guid EdgeId { get; set; }
        public int StartNodeId { get; set; }
        public int EndNodeId { get; set; }
        public int DistanceM { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? CreateDatetime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }

        public virtual Node EndNode { get; set; }
        public virtual Node StartNode { get; set; }
    }
}
