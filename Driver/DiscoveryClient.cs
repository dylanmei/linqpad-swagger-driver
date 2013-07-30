using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    class DiscoveryClient : IDocumentationClient
    {
        readonly string url;
        readonly ICredentials credentials;

        public DiscoveryClient(string url, ICredentials credentials)
        {
            this.url = url;
            this.credentials = credentials ?? CredentialCache.DefaultCredentials;
        }

        public ServiceDocumentation Service()
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ServiceDocumentation>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }

        public IEnumerable<ApiDocumentation> Apis(string basePath, IEnumerable<ApiReference> references)
        {
            var results = new List<ApiDocumentation>();
            foreach (var resourceReference in references)
                results.Add(FetchResource(basePath ?? url, resourceReference));
            return results;
        }

        ApiDocumentation FetchResource(string apiPath, ApiReference reference)
        {
            var client = new HttpClient();
            var response = client.GetAsync(apiPath + reference.DocPath).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ApiDocumentation>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }
    }
}