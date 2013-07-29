using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LINQPad.Extensibility.DataContext;
using SwaggerDriver.Dialogs;

namespace SwaggerDriver
{
    public class Driver : DynamicDataContextDriver
    {
        public override string Name
        {
            get { return "Swagger"; }
        }

        public override string Author
        {
            get { return "github.com/dylanmei"; }
        }

        public override string GetConnectionDescription(IConnectionInfo connectionInfo)
        {
            var model = new ConnectionModel(connectionInfo);
            var uri = new Uri(model.Uri);
            var host = uri.Port == 80 ? uri.Host : string.Concat(uri.Host, ':', uri.Port);
            var path = uri.AbsolutePath;
            return string.Concat(host, path);
        }

        public override bool AreRepositoriesEquivalent(IConnectionInfo r1, IConnectionInfo r2)
        {
            var m1 = new ConnectionModel(r1);
            var m2 = new ConnectionModel(r2);
            return Equals(m1.Uri, m2.Uri);
        }

        //public override IEnumerable<string> GetAssembliesToAdd()
        //{
        //    // We need the following assembly for compiliation and autocompletion:
        //    return new string[0];
        //}

        //public override IEnumerable<string> GetNamespacesToAdd()
        //{
        //    // Import the commonly used namespaces as a courtesy to the user:
        //    return new string[0];
        //}

        public override bool ShowConnectionDialog(IConnectionInfo connectionInfo, bool isNewConnection)
        {
            var model = new ConnectionModel(connectionInfo,
                new ConnectionHistoryReader(GetHistoryPath()).Read());
            return new Dialog(model).ShowDialog() == true;
        }

        public override List<ExplorerItem> GetSchemaAndBuildAssembly(IConnectionInfo connectionInfo,
            AssemblyName assemblyToBuild, ref string @namespace, ref string typeName)
        {
            var model = new ConnectionModel(connectionInfo);
            var discovery = new Discovery(model.Uri, null);
            
            // swagger service description
            var service = discovery.GetApi();

            new DiscoveryCompiler(CodeProvider.Default)
                .CompileDiscovery(discovery, assemblyToBuild, @namespace);

            var explorer = new ExplorerBuilder("Api").Build(service);

            typeName = explorer.TypeName;
            return explorer.Resources.ToList();
        }
        
        string GetHistoryPath()
        {
            return Path.Combine(GetDriverFolder(), "uri.history");
        }
    }
}