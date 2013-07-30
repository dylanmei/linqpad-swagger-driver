using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class Discovery
    {
        ServiceDocument serviceDocument;
        IEnumerable<ApiDocument> apiDocuments;
        readonly ISwaggerClient client;

        public Discovery(ISwaggerClient client)
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
                var service = GetServiceDocument();
                return GetResources(service.BasePath, GetApiDocument());
            }
        }

        ServiceDocument GetServiceDocument()
        {
            return serviceDocument ?? (serviceDocument = client.Service());
        }

        IEnumerable<ApiDocument> GetApiDocument()
        {
            if (apiDocuments == null)
            {
                var serviceDocumentation = GetServiceDocument();
                apiDocuments = client.Apis(serviceDocumentation.BasePath, serviceDocumentation.Apis);
            }

            return apiDocuments;
        }

        static IEnumerable<DiscoveryResource> GetResources(string basePath, IEnumerable<ApiDocument> docs)
        {
            var apisByName = GetApisByName(docs);
            return apisByName.Select(apiGroup => new DiscoveryResource {
                Name = apiGroup.Key, Apis = apiGroup.ToArray(), BasePath = basePath
            });
        }

        static IEnumerable<IGrouping<string, Api>> GetApisByName(IEnumerable<ApiDocument> docs)
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
