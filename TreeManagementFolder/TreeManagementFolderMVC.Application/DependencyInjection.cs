using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TreeManagementFolderMVC.Application.Interface;
using TreeManagementFolderMVC.Application.Service;

namespace TreeManagementFolderMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplications(this IServiceCollection services)
        {
            services.AddTransient<INodeService, NodeService>();
            return services;
        }
    }
}
