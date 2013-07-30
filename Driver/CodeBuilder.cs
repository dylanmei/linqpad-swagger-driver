using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class CodeBuilder
    {
        readonly CodeDomProvider codeProvider;
        readonly CodeNamespace codeNamespace;

        public CodeTypeDeclaration ApiType { get; private set; }
        public IEnumerable<CodeTypeDeclaration> ResourceTypes { get; private set; }

        public readonly CodeCompileUnit TargetCompileUnit = new CodeCompileUnit();
        static readonly string[] SupportedMethods = "GET".Split();

        public CodeBuilder(CodeDomProvider codeProvider, string codeNamespace)
        {
            this.codeProvider = codeProvider;
            this.codeNamespace = new CodeNamespace(codeNamespace);
            InitializeTypes();
            TargetCompileUnit.Namespaces.Add(this.codeNamespace);
        }

        public CodeDomProvider CodeDomProvider
        {
            get { return codeProvider; }
        }

        void InitializeTypes()
        {
            ResourceTypes = new List<CodeTypeDeclaration>();
            ApiType = new CodeTypeDeclaration
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public,
                Name = "Api"
            };
            codeNamespace.Types.Add(ApiType);
        }

        public void Build(IEnumerable<DiscoveryResource> resources)
        {
            foreach (var resource in resources)
            {
                BuildResourceType(resource.Name,
                    resource.BasePath, resource.Apis);
            }
        }

        void BuildResourceType(string resourceName, string basePath, IEnumerable<Api> apis)
        {
            var resourceType = ResourceTypes.SingleOrDefault(t => t.Name == resourceName);
            if (resourceType == null)
            {
                resourceType = new CodeTypeDeclaration {
                    Name = resourceName,
                    IsClass = true,
                    TypeAttributes = TypeAttributes.NestedPublic,
                };

                foreach (var methodName in SupportedMethods)
                {
                    var requestMethod = new CodeMemberMethod
                    {
                        Name = methodName,
                        Attributes = MemberAttributes.Public | MemberAttributes.Static,
                        ReturnType = new CodeTypeReference(typeof(string))
                    };
                    requestMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "path"));
                    var code = string.Format("return \"{0} {1}/\" + path;", methodName, basePath);
                    requestMethod.Statements.Add(new CodeSnippetStatement(code));
                    resourceType.Members.Add(requestMethod);
                }

                ApiType.Members.Add(resourceType);
                ((List<CodeTypeDeclaration>)ResourceTypes).Add(resourceType);
            }
        }
    }
}
