﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SwaggerDriver.Swagger
{
    [DataContract]
    public class ApiDocument : SwaggerDocument
    {
        [DataMember(Name = "resourcePath")]
        public string DocPath { get; set; }

        [DataMember(Name = "apis")]
        public List<Api> Apis { get; set; }
    }
}