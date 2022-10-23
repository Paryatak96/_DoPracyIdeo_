using System;
using System.Collections.Generic;
using System.Text;

namespace TreeManagementFolderMVC.Application.ViewModels.Node
{
    public class NodeVM
    {
        public NodeVM()
        {
            Nodes = new List<NodeVM>();
            Leafes = new List<LeafVM>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public NodeVM Parent { get; set; }
        public ICollection<NodeVM> Nodes { get; set; }
        public ICollection<LeafVM> Leafes { get; set; }
        
    }
}
