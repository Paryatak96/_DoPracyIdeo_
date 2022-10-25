using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TreeManagementFolderMVC.Domain.Interface;
using TreeManagementFolderMVC.Domain.Model;

namespace TreeManagementFolderMVC.Infrastructure.Repositories
{
    public class NodeRepsitory : INodeRepository
    {
        private readonly Context _context;

        public NodeRepsitory(Context context)
        {
            _context = context;
        }

        public int AddNode (Node node)
        {
            _context.Nodes.Add(node);
            _context.SaveChanges();
            return node.Id;
        }

        public int AddLeaf(Leaf leaf)
        {
            _context.Leafes.Add(leaf);
            _context.SaveChanges();
            return leaf.Id;
        }

        public void CreateRootIfNotExist()
        {
            if(_context.Nodes.Count()==0)
            {
                Node root = new Node();
                root.ParentId = null;
                root.Leafes = new List<Leaf>();
                root.Nodes = new List<Node>();
                root.Name = "ROOT";
                AddNode(root);
            }
        }

        public IQueryable<Node> GetAllNodesForList()
        {
            throw new NotImplementedException();
        }

        public Node GetRoot()
        {
            return _context.Nodes.Where(node => node.ParentId == null).First<Node>();
        }

        public Node GetNodeById(int id)
        {
            return _context.Nodes.Find(id);
        }

        public Leaf GetLeafById(int id)
        {
            return _context.Leafes.Find(id);
        }

        public void UpdateNode(Node node)
        {
            _context.Nodes.Update(node);
            _context.SaveChanges();
        }

        public void UpdateLeaf(Leaf leaf)
        {
            _context.Leafes.Update(leaf);
            _context.SaveChanges();
        }

        public List<Node> GetAllNodesForParentId(int parentId)
        {
            return _context.Nodes.Where(node => node.ParentId == parentId).ToList<Node>();
        }

        public List<Leaf> GetAllLeafsForParentId(int parentId)
        {
            return _context.Leafes.Where(leaf => leaf.ParentId == parentId).ToList<Leaf>();
        }

        public void DeleteNode(int id)
        {
            var node = _context.Nodes.Find(id);
            List<Node> nodes = GetAllNodesForParentId(id);
            foreach (var nodeItem in nodes)
            {
                DeleteNode(nodeItem.Id);
            }

            List<Leaf> leafs = GetAllLeafsForParentId(id);
            foreach(var leafItem in leafs)
            {
                DeleteLeaf(leafItem.Id);
            }
            
            if (node != null)
            {
                _context.Nodes.Remove(node);
                _context.SaveChanges();
            }
        }

        public void DeleteLeaf(int id)
        {
            var leaf = _context.Leafes.Find(id);
            if (leaf != null)
            {
                _context.Leafes.Remove(leaf);
                _context.SaveChanges();
            }
        }
    }
}
