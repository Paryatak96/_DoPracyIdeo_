using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using TreeManagementFolderMVC.Application.Interface;
using TreeManagementFolderMVC.Application.ViewModels.Node;

namespace TreeManagementFolder
{
    public class SessionManager
    {
        private ISession session;
        private INodeService nodeService;

        public SessionManager(ISession session, INodeService nodeService)
        {
            this.session = session;
            this.nodeService = nodeService;
        }

        public NodeVM Root
        {
            get
            {
                NodeVM root;
                byte[] bytes = null;
              
                if (session.TryGetValue("root", out bytes))
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            IFormatter br = new BinaryFormatter();
                            root = (NodeVM)br.Deserialize(ms);
                        }
                    }
                }
                else
                {
                    root = nodeService.GetRoot();
                    using (MemoryStream ms = new MemoryStream())
                    {
                        IFormatter br = new BinaryFormatter();
                        br.Serialize(ms, root);
                        bytes = ms.ToArray();
                        session.Set("root", bytes);
                    }

                }
                return root;
            }
            set
            {
                byte[] bytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    IFormatter br = new BinaryFormatter();
                    br.Serialize(ms, value);
                    bytes = ms.ToArray();
                    session.Set("root", bytes);
                }

            }
        }
    }
}
