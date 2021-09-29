using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Thesaurus.Api.Business.Model
{
    public class Response
    {        
        public IDictionary<string,Uri> Words { get; set; }        
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }        
    }
}
