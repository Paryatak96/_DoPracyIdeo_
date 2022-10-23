using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TreeManagementFolderMVC.Domain.Model;

namespace TreeManagementFolderMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Leaf> Leafes { get; set; }
        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
