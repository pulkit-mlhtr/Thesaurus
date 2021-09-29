using System;
using System.Collections.Generic;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Extension;
using Thesaurus.Api.Business.Model.Interface;

namespace Thesaurus.Api.Business.Model.Mapper
{
    public class WordMapper : IMapper<Words, Word>
    {
        public Word Map(Words source)
        {
            return new Word
            {
                Id = source.Id.ToString(),
                Primary = source.Word,
                Synonyms = source.Synonyms!=null?source.Synonyms.ToDict():null
            };
        }      
    }
}
