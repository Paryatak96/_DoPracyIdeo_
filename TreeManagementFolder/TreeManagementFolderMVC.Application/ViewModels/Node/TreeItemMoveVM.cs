using System;
using System.Collections.Generic;
using System.Text;

namespace TreeManagementFolderMVC.Application.ViewModels.Node
{
    public class TreeItemMoveVM
    {
        public NodeVM Root { get; set; }
        public int SelectedItemId { get; set; }
        public bool IsNodeMoved { get; set; }

    }
}
