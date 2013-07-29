using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class CodeBuilder
    {
        readonly CodeDomProvider codeProvider;
        readonly CodeNamespace codeNamespace;
        CodeTypeDeclaration apiType;

        public readonly CodeCompileUnit TargetCompileUnit = new CodeCompileUnit();

        public CodeBuilder(CodeDomProvider codeProvider, string codeNamespace)
        {
            this.codeProvider = codeProvider;
            this.codeNamespace = new CodeNamespace(codeNamespace);
            TargetCompileUnit.Namespaces.Add(this.codeNamespace);
        }

        public CodeDomProvider CodeDomProvider
        {
            get { return codeProvider; }
        }

        CodeTypeDeclaration ApiType
        {
            get
            {
                if (apiType == null)
                {
                    apiType = new CodeTypeDeclaration {
                        IsClass = true,
                        TypeAttributes = TypeAttributes.Public,
                        Name = "Api"
                    };
                    codeNamespace.Types.Add(apiType);
                }
                return apiType;
            }
        }

        public void BuildResources(string basePath, IEnumerable<Resource> apis)
        {
            var type = new CodeTypeDeclaration {
                IsClass = true,
                TypeAttributes = TypeAttributes.NestedPublic,
                Name = basePath.Replace("/", "_").Replace("-", "_"),
            };

            var method = new CodeMemberMethod {
                Name = "Get",
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                ReturnType = new CodeTypeReference(typeof(string))
            };

            var code = string.Format("return \"Hello {0}\";", basePath);
            method.Statements.Add(new CodeSnippetStatement(code));

            type.Members.Add(method);
            ApiType.Members.Add(type);
        }
    }
}
