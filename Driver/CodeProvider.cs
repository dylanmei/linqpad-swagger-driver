﻿using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace SwaggerDriver
{
    public class CodeProvider
    {
        public static CodeDomProvider Default = CodeDomProvider.CreateProvider("CSharp",
            new Dictionary<string, string> { { "CompilerVersion", "v4.0" } });
    }
}
