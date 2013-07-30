using System.Linq;
using System.Collections.Generic;
using LINQPad.Extensibility.DataContext;

namespace SwaggerDriver
{
    public class Explorer
    {
        public string TypeName { get; set; }
        public IEnumerable<ExplorerItem> Resources { get; set; }
    }

    public class ExplorerBuilder
    {
        readonly string typeName;

        public ExplorerBuilder(string typeName)
        {
            this.typeName = typeName;
        }

        public Explorer Build(IEnumerable<DiscoveryResource> resources)
        {
            return new Explorer {
                TypeName = typeName,
                Resources = resources.Select(BuildResource)
            };
        }

        public ExplorerItem BuildResource(DiscoveryResource resource)
        {
            var resourceItem = new ExplorerItem(resource.Name, ExplorerItemKind.Category, ExplorerIcon.View) {
                Children = new List<ExplorerItem>()
            };
            foreach (var api in resource.Apis)
            {
                resourceItem.Children.Add(new ExplorerItem(api.Path, ExplorerItemKind.QueryableObject, ExplorerIcon.StoredProc)
                {
                    DragText = resource.Name + ".GET(\"" + api.Path + "\")"
                });
            }

            return resourceItem;
        }
    }
}
