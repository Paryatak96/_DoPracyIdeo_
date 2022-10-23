using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeManagementFolderMVC.Application.Service;

namespace TreeManagementFolder.Controllers
{
    public class NodeController : Controller
    {
        private readonly NodeService nodeService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
