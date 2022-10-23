using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            TreeVM tree = new TreeVM();
            tree.Root = _nodeService.GetRoot();

            return View(tree);
        }

        
        [HttpPost]
        public IActionResult AddNode(TreeVM tree)
        {

            NewItem newNode = new NewItem();
            newNode.ParentId = tree.SelectedId;
            newNode.Name = tree.Name;

            var id = _nodeService.AddNode(newNode);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddLeaf(TreeVM tree)
        {

            NewItem newLeaf = new NewItem();
            newLeaf.ParentId = tree.SelectedParentFeafId;
            newLeaf.Name = tree.Name;

            var id = _nodeService.AddLeaf(newLeaf);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteNode(int id)
        {
            _nodeService.DeleteNode(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteLeaf(int id)
        {
            _nodeService.DeleteLeaf(id);
            return RedirectToAction("Index");
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
