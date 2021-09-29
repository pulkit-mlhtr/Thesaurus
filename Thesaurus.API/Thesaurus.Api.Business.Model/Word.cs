using System;
using System.Collections.Generic;
using System.Text;

namespace Thesaurus.Api.Business.Model
{
    public class Word
    {
        public string Id { get; set; }
        public string Primary { get; set; }
        public IDictionary<string,Uri> Synonyms { get; set; }
    }
}
