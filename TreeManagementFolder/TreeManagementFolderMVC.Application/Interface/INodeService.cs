using System;
using System.Collections.Generic;
using System.Text;
using TreeManagementFolderMVC.Application.ViewModels.Node;

namespace TreeManagementFolderMVC.Application.Interface
{
    public interface INodeService
    {
        int AddNode(NewItem node);
        int AddLeaf(NewItem leaf);
        void DeleteNode(int id);
        void DeleteLeaf(int id);


        NodeVM GetRoot();

    }
}
