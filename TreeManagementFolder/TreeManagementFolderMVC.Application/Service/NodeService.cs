using System;
using System.Collections.Generic;
using System.Text;
using TreeManagementFolderMVC.Application.Interface;
using TreeManagementFolderMVC.Application.ViewModels.Node;
using TreeManagementFolderMVC.Domain.Interface;
using TreeManagementFolderMVC.Domain.Model;

namespace TreeManagementFolderMVC.Application.Service
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _nodeRepo;

        public NodeService(INodeRepository nodeRepo)
        {
            _nodeRepo = nodeRepo;
        }

        public int AddNode(NewItem node)
        {
            Node newElement = new Node();
            newElement.ParentId = node.ParentId;
            newElement.Name = node.Name;
            var nodeId = _nodeRepo.AddNode(newElement);
            return nodeId;
        }

        public int AddLeaf(NewItem leaf)
        {
            Leaf newElement = new Leaf();
            newElement.ParentId = leaf.ParentId;
            newElement.Name = leaf.Name;
            var nodeId = _nodeRepo.AddLeaf(newElement);
            return nodeId;
        }

        public NodeVM GetRoot()
        {
            _nodeRepo.CreateRootIfNotExist();
            Node root = _nodeRepo.GetRoot();
            return ChangeToVM(root, null);
        }

        NodeVM ChangeToVM(Node node, NodeVM parent)
        {
            NodeVM nodeVM = new NodeVM();
            nodeVM.Parent = parent;
            nodeVM.Id = node.Id;
            nodeVM.Name = node.Name;
            nodeVM.Expanded = true;
            foreach (Leaf leaf in _nodeRepo.GetAllLeafsForParentId(node.Id))
            {
                nodeVM.Leafes.Add(ChangeToVM(leaf, nodeVM));
            }

            foreach (Node subNode in _nodeRepo.GetAllNodesForParentId(node.Id))
            {
                nodeVM.Nodes.Add(ChangeToVM(subNode, nodeVM));
            }

            return nodeVM;
        }

        LeafVM ChangeToVM(Leaf leaf, NodeVM parent)
        {
            LeafVM leafVM = new LeafVM();
            leafVM.Id = leaf.Id;
            leafVM.Parent = parent;
            leafVM.Name = leaf.Name;
            return leafVM;
        }

        public void DeleteNode(int id)
        {
            _nodeRepo.DeleteNode(id);
        }
        public void DeleteLeaf(int id)
        {
            _nodeRepo.DeleteLeaf(id);
        }


        public void MoveNode(int targetNodeId, int movedId)
        {
            Node moved = _nodeRepo.GetNodeById(movedId);
            moved.ParentId = targetNodeId;
            _nodeRepo.UpdateNode(moved);
        }

        public void MoveLeaf(int targetNodeId, int movedId)
        {
            Leaf moved = _nodeRepo.GetLeafById(movedId);
            moved.ParentId = targetNodeId;
            _nodeRepo.UpdateLeaf(moved);
        }

        public void EditNodeName(int id, string name)
        {
            Node node = _nodeRepo.GetNodeById(id);
            node.Name = name;
            _nodeRepo.UpdateNode(node);
        }

        public void EditLeafName(int id, string name)
        {
            Leaf leaf = _nodeRepo.GetLeafById(id);
            leaf.Name = name;
            _nodeRepo.UpdateLeaf(leaf);
        }
    }
}
