using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Thesaurus.Api.Repository.Base;

namespace Thesaurus.Api.Business.Model.Collection
{
    [BsonCollection("words")]
    public class Words : DocumentBase
    {
        [BsonElement("word")]
        public string Word { get; set; }

        [BsonElement("key")]
        public string Key { get; set; }

        [BsonElement("pos")]
        public string Pos { get; set; }

        [BsonElement("synonyms")]
        public string[] Synonyms { get; set; }
    }
}
