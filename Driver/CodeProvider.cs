using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwaggerDriver
{
    public class CodeProvider
    {
        public static CodeDomProvider Default = CodeDomProvider.CreateProvider("CSharp",
                new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
    }
}
