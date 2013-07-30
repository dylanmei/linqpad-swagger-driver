using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    class DiscoveryClient : ISwaggerClient
    {
        readonly string url;
        readonly ICredentials credentials;

        public DiscoveryClient(string url, ICredentials credentials)
        {
            this.url = url;
            this.credentials = credentials ?? CredentialCache.DefaultCredentials;
        }

        public ServiceDocument Service()
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ServiceDocument>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }

        public IEnumerable<ApiDocument> Apis(string basePath, IEnumerable<ApiReference> references)
        {
            var results = new List<ApiDocument>();
            foreach (var resourceReference in references)
                results.Add(FetchResource(basePath ?? url, resourceReference));
            return results;
        }

        ApiDocument FetchResource(string apiPath, ApiReference reference)
        {
            var client = new HttpClient();
            var response = client.GetAsync(apiPath + reference.DocPath).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ApiDocument>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }
    }
}