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
        public void SortNodes(NodeVM parentNode)
        {
            if(parentNode.Sort == Sorting.ascending)
            {
                parentNode.Nodes.Sort(CompareAsc);
            }
            else
            {
                parentNode.Nodes.Sort(CompareDesc);
            }
        }
        public void SortLeafes(NodeVM parentNode)
        {
            if (parentNode.Sort == Sorting.ascending)
            {
                parentNode.Leafes.Sort(CompareAsc);
            }
            else
            {
                parentNode.Leafes.Sort(CompareDesc);
            }
        }

        private NodeVM ChangeToVM(Node node, NodeVM parent)
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
            nodeVM.Sort = Sorting.ascending;
            nodeVM.Nodes.Sort(CompareAsc);
            nodeVM.Leafes.Sort(CompareAsc);
            return nodeVM;
        }
        private LeafVM ChangeToVM(Leaf leaf, NodeVM parent)
        {
            LeafVM leafVM = new LeafVM();
            leafVM.Id = leaf.Id;
            leafVM.Parent = parent;
            leafVM.Name = leaf.Name;
            return leafVM;
        }
        private int CompareAsc(NodeVM node1, NodeVM node2)
        {
            return node1.Name.CompareTo(node2.Name);
        }
        private int CompareDesc(NodeVM node1, NodeVM node2)
        {
            return node1.Name.CompareTo(node2.Name)*(-1);
        }
        private int CompareAsc(LeafVM leaf1, LeafVM leaf2)
        {
            return leaf1.Name.CompareTo(leaf2.Name);
        }
        private int CompareDesc(LeafVM leaf1, LeafVM leaf2)
        {
            return leaf1.Name.CompareTo(leaf2.Name) * (-1);
        }

        public void ResetTree()
        {
            Node root = _nodeRepo.GetRoot();
            DeleteNode(root.Id);

            root = new Node();
            root.Name = "Korzeń";
            root.ParentId = null;
            root.Id = _nodeRepo.AddNode(root);


            Node nodeAdam = new Node();
            nodeAdam.ParentId = root.Id;
            nodeAdam.Name = "Adam";
            nodeAdam.Id = _nodeRepo.AddNode(nodeAdam);

            Node nodeEwa = new Node();
            nodeEwa.ParentId = root.Id;
            nodeEwa.Name = "Ewa";
            nodeEwa.Id = _nodeRepo.AddNode(nodeEwa);

            Node nodeKain = new Node();
            nodeKain.ParentId = nodeEwa.Id;
            nodeKain.Name = "Kain";
            nodeKain.Id = _nodeRepo.AddNode(nodeKain);

            Node nodeAbel = new Node();
            nodeAbel.ParentId = nodeEwa.Id;
            nodeAbel.Name = "Abel";
            nodeAbel.Id = _nodeRepo.AddNode(nodeAbel);


            Leaf jablko = new Leaf();
            jablko.Name = "jablko";
            jablko.ParentId = nodeEwa.Id;
            jablko.Id = _nodeRepo.AddLeaf(jablko);

            Leaf motyka = new Leaf();
            motyka.Name = "motyka";
            motyka.ParentId = nodeAbel.Id;
            motyka.Id = _nodeRepo.AddLeaf(motyka);

            Leaf taczka = new Leaf();
            taczka.Name = "taczka";
            taczka.ParentId = nodeAbel.Id;
            taczka.Id = _nodeRepo.AddLeaf(taczka);

            Leaf doniczka = new Leaf();
            doniczka.Name = "doniczka";
            doniczka.ParentId = nodeAbel.Id;
            doniczka.Id = _nodeRepo.AddLeaf(doniczka);

            Leaf gruszka = new Leaf();
            gruszka.Name = "gruszka";
            gruszka.ParentId = nodeEwa.Id;
            gruszka.Id = _nodeRepo.AddLeaf(gruszka);

            Leaf miecz = new Leaf();
            miecz.Name = "miecz";
            miecz.ParentId = nodeKain.Id;
            miecz.Id = _nodeRepo.AddLeaf(miecz);

            Leaf tarcza = new Leaf();
            tarcza.Name = "tarcza";
            tarcza.ParentId = nodeKain.Id;
            tarcza.Id = _nodeRepo.AddLeaf(tarcza);
        }

    }
}
