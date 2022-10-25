using System;
using System.Collections.Generic;
using System.Text;

namespace TreeManagementFolderMVC.Domain.Model
{
    public class Leaf
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
