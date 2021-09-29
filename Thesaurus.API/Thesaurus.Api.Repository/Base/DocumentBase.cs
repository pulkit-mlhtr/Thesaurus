using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.Api.Repository.Base
{
    public abstract class DocumentBase : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
