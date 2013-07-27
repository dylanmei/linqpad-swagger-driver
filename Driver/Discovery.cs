using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class Discovery
    {
        readonly string url;
        readonly ICredentials credentials;
        ApiDeclaration apiDeclaration;
        IEnumerable<ResourceDeclaration> resourceDeclarations;

        public Discovery(string url, ICredentials credentials)
        {
            this.url = url;
            this.credentials = credentials ?? CredentialCache.DefaultCredentials;
        }

        public ApiDeclaration GetApi()
        {
            return apiDeclaration ?? (apiDeclaration = FetchApi());
        }

        public IEnumerable<ResourceDeclaration> GetResources()
        {
            return resourceDeclarations ?? (resourceDeclarations = FetchResources());
        }

        ApiDeclaration FetchApi()
        {
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ApiDeclaration>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }

        IEnumerable<ResourceDeclaration> FetchResources()
        {
            var results = new List<ResourceDeclaration>();
            var api = GetApi();
            foreach (var resourceReference in api.Resources)
                results.Add(FetchResource(api.BasePath ?? url, resourceReference));
            return results;
        }

        ResourceDeclaration FetchResource(string apiPath, ResourceReference reference)
        {
            var client = new HttpClient();
            var response = client.GetAsync(apiPath + reference.Path).Result;
            response.EnsureSuccessStatusCode();

            var declaration = JsonConvert.DeserializeObject<ResourceDeclaration>(
                response.Content.ReadAsStringAsync().Result);

            return declaration;
        }
    }
}
