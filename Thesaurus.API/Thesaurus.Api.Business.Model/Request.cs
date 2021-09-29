using System;
using System.Collections.Generic;

namespace Thesaurus.Api.Business.Model
{
    public class Request
    {
        public int CurrentPage { get; set; }        
        public int Limit { get; set; }
        public ICollection<Word> Words { get; set; }
    }
}
