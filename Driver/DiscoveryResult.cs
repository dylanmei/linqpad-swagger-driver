using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace SwaggerDriver
{
    public class DiscoveryResult
    {
        public CodeCompileUnit CodeDom { get; set; }
        public CodeDomProvider CodeProvider { get; set; }

        public Assembly CompiledAssembly { get; set; }
    }
}
