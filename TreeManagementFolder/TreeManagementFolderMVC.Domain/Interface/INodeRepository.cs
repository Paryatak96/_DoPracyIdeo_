using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeManagementFolderMVC.Domain.Model;

namespace TreeManagementFolderMVC.Domain.Interface
{
    public interface INodeRepository
    {
        
        int AddNode(Node node);
        int AddLeaf(Leaf leaf);
        void DeleteNode(int id);
        void DeleteLeaf(int id);

        IQueryable<Node> GetAllNodesForList();
        Node GetRoot();

        void CreateRootIfNotExist();

        Node GetNodeById(int id);

        List<Node> GetAllNodesForParentId(int parentId);
        List<Leaf> GetAllLeafsForParentId(int parentId);
    }
}
