using System;
using System.Collections.Generic;
using System.Text;

namespace TreeManagementFolderMVC.Application.ViewModels.Node
{
    public class TreeVM
    {
        public NodeVM Root { get; set; }
        public string Name { get; set; }
        public int SelectedId { get; set; }
        public int SelectedParentFeafId { get; set; }
        public int SelectedLeafIdChangeName { get; set; }
        public int SelectedNodeIdChangeName { get; set; } 
    }
}
