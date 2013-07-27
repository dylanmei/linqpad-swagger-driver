using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using SwaggerDriver.Builders;

namespace SwaggerDriver
{
    public class ProxyCompiler
    {
        readonly CodeDomProvider codeProvider;
        const string ErrorHeading = "Cannot compile service proxy. ";
        static readonly string[] AssemblyReferences = "System.dll System.Core.dll System.Xml.dll System.Runtime.Serialization.dll".Split();

        public ProxyCompiler(CodeDomProvider codeProvider)
        {
            this.codeProvider = codeProvider ?? CodeProvider.Default;
        }

        public ProxyReference CompileDiscovery(Discovery discovery, string @namespace)
        {
            return CompileDiscovery(discovery, null, @namespace);
        }

        public ProxyReference CompileDiscovery(Discovery discovery, AssemblyName assemblyName, string @namespace)
        {
            var builder = new CodeBuilder(CodeProvider.Default, @namespace);
            foreach (var api in discovery.GetResources())
                builder.BuildResources(api.Path, api.Resources);

            var reference = new ProxyReference {
                CodeDom = builder.TargetCompileUnit,
                CodeProvider = builder.CodeDomProvider
            };

            var results = Compile(builder.TargetCompileUnit, assemblyName);
            CheckCompileResults(results.Errors);
            reference.CompiledAssembly = results.CompiledAssembly;
            return reference;
        }

        CompilerResults Compile(CodeCompileUnit compileUnit, AssemblyName assemblyName)
        {
            var options = assemblyName != null
                ? new CompilerParameters(AssemblyReferences, assemblyName.CodeBase, true)
                : new CompilerParameters(AssemblyReferences) { GenerateInMemory = true };
            return codeProvider.CompileAssemblyFromDom(options, compileUnit);
        }

        static void CheckCompileResults(CompilerErrorCollection errors)
        {
            if (errors.Count > 0)
            {
                throw new Exception(ErrorHeading +
                    errors[0].ErrorText + " (line " + errors[0].Line + ")");
            }
        }
    }
}
