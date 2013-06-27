using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace SwaggerDriver
{
    public class DiscoveryCompiler
    {
        readonly CodeDomProvider codeProvider;

        public DiscoveryCompiler(CodeDomProvider codeProvider = null)
        {
            this.codeProvider = codeProvider ?? CodeProvider.Default;
        }

        public DiscoveryReference CompileDiscovery(Discovery discovery, AssemblyName assemblyName, string @namespace)
        {
            var declaration = discovery.GetDeclaration();

            //var generator = new ServiceGenerator()
            //generator.NamespaceMappings.Add("*", @namespace);
            //foreach (var api in declaration.Apis)
            //{
            //    var resource = discovery.GetResource(api);
            //    generator.GenerateResource(resource);
            //}

            //var reference = new DiscoveryReference();
            //    reference.CodeDom = generator.TargetCompileUnit;

            //    var results = Compile(generator.TargetCompileUnit, assemblyName);
            //    CheckCompileResults(results.Errors);
            //    reference.CompiledAssembly = results.CompiledAssembly;

            //return reference;
            return new DiscoveryReference();
        }

        CompilerResults Compile(CodeCompileUnit compileUnit, AssemblyName assemblyName)
        {
            var options = new CompilerParameters(
                "System.dll System.Core.dll System.Xml.dll System.Runtime.Serialization.dll".Split(),
                assemblyName.CodeBase, true);

            return codeProvider.CompileAssemblyFromDom(options, compileUnit);
        }

        /*

        static void CheckCompileResults(CompilerErrorCollection errors)
        {
            if (errors.Count > 0)
            {
                throw new Exception(ErrorHeading +
                    errors[0].ErrorText + " (line " + errors[0].Line + ")");
            }
        }
        */
    }
}
