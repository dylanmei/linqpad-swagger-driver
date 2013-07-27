using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class Operation
    {
        [DataMember(Name = "httpMethod")]
        public string HttpMethod { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "responseClass")]
        public string ResponseClass { get; set; }

        // parameters
        // errorResponses
    }
}
