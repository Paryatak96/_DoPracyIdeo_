using System;
using System.Collections.Generic;
using System.Text;

namespace TreeManagementFolderMVC.Application.ViewModels.Node
{

    [Serializable]
    public class LeafVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NodeVM Parent { get; set; }
    }
}
