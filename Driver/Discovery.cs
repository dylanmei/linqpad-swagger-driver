using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using RestSharp;
using SwaggerDriver.Swagger;

namespace SwaggerDriver
{
    public class Service
    {
        public string Uri { get; set; }
        public IEnumerable<Api> Apis { get; set; }
    }

    public class Discovery
    {
        readonly string uri;
        readonly ICredentials credentials;
        ServiceDeclaration serviceDeclaration;

        public Discovery(string uri, ICredentials credentials)
        {
            this.uri = uri;
            this.credentials = credentials;
        }

        public ServiceDeclaration GetDeclaration()
        {
            return serviceDeclaration ?? (serviceDeclaration = DiscoverService());
        }

        ServiceDeclaration DiscoverService()
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET);
            var response = client.Execute<ServiceDeclaration>(request);
            return response.Data;
        }
    }
}
