using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TreeManagementFolder.Models;
using TreeManagementFolderMVC.Application.Interface;
using TreeManagementFolderMVC.Application.ViewModels.Node;

namespace TreeManagementFolder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly INodeService _nodeService;

        public HomeController(ILogger<HomeController> logger, INodeService nodeService)
        {
            _logger = logger;
            _nodeService = nodeService;
        }

        public IActionResult Index()
        {
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);

            TreeVM tree = new TreeVM();
            tree.Root = session.Root;
            return View(tree);
        }

        [HttpPost]
        public IActionResult AddNode(TreeVM tree)
        {

            NewItem newNode = new NewItem();
            newNode.ParentId = tree.SelectedId;
            newNode.Name = tree.Name;

            NodeVM subNode = new NodeVM();
            subNode.Id = _nodeService.AddNode(newNode);
            subNode.Name = tree.Name;
            

            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;
            NodeVM parent = FindNodeOfId(root, tree.SelectedId);
            subNode.Parent = parent;
            parent.Nodes.Add(subNode);

            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddLeaf(TreeVM tree)
        {

            NewItem newLeaf = new NewItem();
            newLeaf.ParentId = tree.SelectedParentFeafId;
            newLeaf.Name = tree.Name;

            LeafVM leaf = new LeafVM();
            leaf.Id = _nodeService.AddLeaf(newLeaf);
            leaf.Name = tree.Name;


            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;
            NodeVM parent = FindNodeOfId(root, tree.SelectedParentFeafId);
            leaf.Parent = parent;
            parent.Leafes.Add(leaf);

            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteNode(int id)
        {
            _nodeService.DeleteNode(id);
            
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;
            NodeVM deletedNode = FindNodeOfId(root, id);
            NodeVM parent = deletedNode.Parent;
            parent.Nodes.Remove(deletedNode);

            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteLeaf(int id)
        {
            _nodeService.DeleteLeaf(id);
            
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;
            NodeVM parent = FindNodeWithLeafOfId(root, id);
            parent.Leafes.Remove(parent.Leafes.First(leafe => leafe.Id == id));
            
            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MoveNode(int id)
        {
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            TreeItemMoveVM model = new TreeItemMoveVM();
            model.Root = session.Root;
            model.SelectedItemId = id;
            model.IsNodeMoved = true;

            return View("MoveItem", model);
        }
        [HttpGet]
        public IActionResult MoveNodeFinish(int targetId, int selectedId)
        {
            _nodeService.MoveNode(targetId, selectedId);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;

            NodeVM targetNode = FindNodeOfId(root, targetId);
            NodeVM selectedNode = FindNodeOfId(root, selectedId);
            NodeVM oldParentNode = selectedNode.Parent;

            selectedNode.Parent = targetNode;
            oldParentNode.Nodes.Remove(selectedNode);
            targetNode.Nodes.Add(selectedNode);

            session.Root = root;

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult MoveLeaf(int id)
        {
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            TreeItemMoveVM model = new TreeItemMoveVM();
            model.Root = session.Root;
            model.SelectedItemId = id;
            model.IsNodeMoved = false;


            return View("MoveItem", model);
        }

        [HttpGet]
        public IActionResult MoveLeafFinish(int targetId,int selectedId)
        {
            _nodeService.MoveLeaf(targetId, selectedId);

            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;

            NodeVM targetNode = FindNodeOfId(root, targetId);
            NodeVM oldParentNode = FindNodeWithLeafOfId(root, selectedId);
            LeafVM selectedNode = oldParentNode.Leafes.First(leaf => leaf.Id ==selectedId);

            selectedNode.Parent = targetNode;
            oldParentNode.Leafes.Remove(selectedNode);
            targetNode.Leafes.Add(selectedNode);

            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangeExpandeNode(int id)
        {
            _nodeService.DeleteLeaf(id);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            NodeVM root = session.Root;
            NodeVM node = FindNodeOfId(root, id);
            node.Expanded = !node.Expanded;
            session.Root = root;

            return RedirectToAction("Index");
        }

        [HttpPost]
        IActionResult EditNodeName(TreeVM tree)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        IActionResult EditLeafName(TreeVM tree)
        {
            return RedirectToAction("Index");
        }


        private NodeVM FindNodeOfId(NodeVM root, int id)
        {
            if(root.Id == id)
            {
                return root;
            }
            foreach(var node in root.Nodes)
            {
                NodeVM finded;
                finded = FindNodeOfId(node, id);
                if(finded!=null)
                {
                    return finded;
                }
            }
            return null;
        }

        private NodeVM FindNodeWithLeafOfId(NodeVM root, int id)
        {
            if (root.Leafes.Count(leaf => leaf.Id == id)==1)
            {
                return root;
            }
           
            foreach (var node in root.Nodes)
            {
                NodeVM finded;
                finded = FindNodeWithLeafOfId(node, id);
                if (finded != null)
                {
                    return finded;
                }
            }
            return null;
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
