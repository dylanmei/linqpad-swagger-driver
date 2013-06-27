using System;
using System.Collections.Generic;
using System.Reflection;
using LINQPad.Extensibility.DataContext;

namespace SwaggerDriver
{
    public class SoapContextDriver : DynamicDataContextDriver
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
            throw new NotImplementedException();
        }

        public override bool AreRepositoriesEquivalent(IConnectionInfo r1, IConnectionInfo r2)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetAssembliesToAdd()
        {
            // We need the following assembly for compiliation and autocompletion:
            return new string[0];
        }

        public override IEnumerable<string> GetNamespacesToAdd()
        {
            // Import the commonly used namespaces as a courtesy to the user:
            return new string[0];
        }

        public override bool ShowConnectionDialog(IConnectionInfo connectionInfo, bool isNewConnection)
        {
            throw new NotImplementedException();
        }

        public override List<ExplorerItem> GetSchemaAndBuildAssembly(IConnectionInfo connectionInfo, AssemblyName assemblyToBuild, ref string nameSpace, ref string typeName)
        {
            throw new NotImplementedException();
        }
    }
}