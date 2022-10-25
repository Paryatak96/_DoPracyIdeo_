using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TreeManagementFolderMVC.Domain.Model
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public ICollection<Node> Nodes { get; set; }
        [ForeignKey("ParentId")]
        public ICollection<Leaf> Leafes { get; set; }
    }
}
