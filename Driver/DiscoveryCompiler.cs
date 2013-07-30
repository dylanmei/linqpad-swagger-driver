using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace SwaggerDriver
{
    public class DiscoveryCompiler
    {
        readonly CodeDomProvider codeProvider;
        const string ErrorHeading = "Cannot compile service proxy. ";
        static readonly string[] AssemblyReferences = "System.dll System.Core.dll System.Xml.dll System.Runtime.Serialization.dll".Split();

        public DiscoveryCompiler(CodeDomProvider codeProvider)
        {
            this.codeProvider = codeProvider ?? CodeProvider.Default;
        }

        public DiscoveryResult CompileDiscovery(Discovery discovery, string @namespace)
        {
            return CompileDiscovery(discovery, null, @namespace);
        }

        public DiscoveryResult CompileDiscovery(Discovery discovery, AssemblyName assemblyName, string @namespace)
        {
            var builder = new CodeBuilder(CodeProvider.Default, @namespace);
            builder.Build(discovery.Resources);

            var reference = new DiscoveryResult {
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
