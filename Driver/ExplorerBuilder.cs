using System.Linq;
using System.Collections.Generic;
using LINQPad.Extensibility.DataContext;
using SwaggerDriver.Swagger;

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

        public Explorer Build(ApiDeclaration api)
        {
            return new Explorer {
                TypeName = typeName,
                Resources = api.Resources
                    .Select(r => new ExplorerItem(r.Path, ExplorerItemKind.QueryableObject, ExplorerIcon.View) {
                            DragText = r.Path.Replace("/", "_").Replace("-", "_") + ".Get()"})
            };
        }
    }
}
