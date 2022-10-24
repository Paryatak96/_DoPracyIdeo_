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
            //tree.Root = _nodeService.GetRoot();
            //tree.Root = _nodeService.GetRoot();
            tree.Root = session.Root;
            return View(tree);
        }

        
        [HttpPost]
        public IActionResult AddNode(TreeVM tree)
        {

            NewItem newNode = new NewItem();
            newNode.ParentId = tree.SelectedId;
            newNode.Name = tree.Name;

            var id = _nodeService.AddNode(newNode);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            session.Root = _nodeService.GetRoot();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddLeaf(TreeVM tree)
        {

            NewItem newLeaf = new NewItem();
            newLeaf.ParentId = tree.SelectedParentFeafId;
            newLeaf.Name = tree.Name;

            var id = _nodeService.AddLeaf(newLeaf);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            session.Root = _nodeService.GetRoot();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteNode(int id)
        {
            _nodeService.DeleteNode(id);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            session.Root = _nodeService.GetRoot();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteLeaf(int id)
        {
            _nodeService.DeleteLeaf(id);
            SessionManager session = new SessionManager(HttpContext.Session, _nodeService);
            session.Root = _nodeService.GetRoot();

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
