using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class Discovery
    {
        ServiceDocumentation documentation;
        IEnumerable<ApiDocumentation> apiDocumentation;
        IEnumerable<DiscoveryResource> resources;
        readonly IDocumentationClient client;

        public Discovery(IDocumentationClient client)
        {
            this.client = client;
        }

        public Discovery(string url, ICredentials credentials)
            : this(new DiscoveryClient(url, credentials)) 
        {
        }

        public IEnumerable<DiscoveryResource> Resources
        {
            get
            {
                var service = GetServiceDocumentation();
                return GetResources(service.BasePath, GetApiDocumentation());
            }
        }

        ServiceDocumentation GetServiceDocumentation()
        {
            return documentation ?? (documentation = client.Service());
        }

        IEnumerable<ApiDocumentation> GetApiDocumentation()
        {
            if (apiDocumentation == null)
            {
                var serviceDocumentation = GetServiceDocumentation();
                apiDocumentation = client.Apis(serviceDocumentation.BasePath, serviceDocumentation.Apis);
            }

            return apiDocumentation;
        }

        static IEnumerable<DiscoveryResource> GetResources(string basePath, IEnumerable<ApiDocumentation> docs)
        {
            var apisByName = GetApisByName(docs);
            return apisByName.Select(apiGroup => new DiscoveryResource {
                Name = apiGroup.Key, Apis = apiGroup.ToArray(), BasePath = basePath
            });
        }

        static IEnumerable<IGrouping<string, Api>> GetApisByName(IEnumerable<ApiDocumentation> docs)
        {
            var apis = docs.SelectMany(doc => doc.Apis);
            return apis.GroupBy(GetResourceName);
        }

        static string GetResourceName(Api api)
        {
            var pathSegment = api.Path;
            if (pathSegment.StartsWith("/"))
                pathSegment = pathSegment.Substring(1);
            if (pathSegment.Contains("/"))
                pathSegment = pathSegment.Substring(0, pathSegment.IndexOf("/", StringComparison.Ordinal));
            return pathSegment;
        }
    }
}
