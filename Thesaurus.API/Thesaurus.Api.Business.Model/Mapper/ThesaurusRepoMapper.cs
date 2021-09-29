using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Extension;
using Thesaurus.Api.Business.Model.Interface;

namespace Thesaurus.Api.Business.Model.Mapper
{
    public class ThesaurusRepoMapper : IMapper<Word, Words>
    {
        public Words Map(Word source)
        {
            return new Words
            {
                Id = source.Id==null ? ObjectId.Empty : ObjectId.Parse(source.Id),
                Word = source.Primary.ToLower(),
                Synonyms= source.Synonyms.Select(x=>x.Key).ToListLower().ToArray()
            };
        }
    }
}
