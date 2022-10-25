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
        void MoveNode(int targetNodeId, int movedId);
        void MoveLeaf(int targetNodeId, int movedId);
        void EditNodeName(int id, string name);
        void EditLeafName(int id, string name);
        void SortNodes(NodeVM parentNode);
        void SortLeafes(NodeVM parentNode);

        void ResetTree();
    }
}
