using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.Api.Repository.Base
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public MongoRepository(IMongoClient mongoClient, IConfiguration configuration)
        {
            var database = mongoClient.GetDatabase(configuration.GetSection("MongoDbSettings").GetSection("DatabaseName").Value);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public virtual IQueryable<TDocument> AsQueryable()
        {
            return _collection.AsQueryable();
        }
       
        public virtual IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression)
        {
            return _collection.Find(filterExpression).ToEnumerable();
        }
       
        public virtual IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        {
            return _collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
        }                

        public async virtual Task InsertOneAsync(TDocument document)
        {            
            await _collection.WithWriteConcern(WriteConcern.WMajority).InsertOneAsync(document);
        }       

        public async virtual  Task InsertManyAsync(ICollection<TDocument> documents)
        {
            await _collection.WithWriteConcern(WriteConcern.WMajority).InsertManyAsync(documents);
        }

        public async virtual Task<TDocument> ReplaceOneAsync(TDocument document)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            return await _collection.FindOneAndReplaceAsync(filter, document);
        }      

        public async Task DeleteManyAsync(ICollection<string> ids)
        {
            var objectIds = ids.ToList().ConvertAll(x => ObjectId.Parse(x));
            var filter = Builders<TDocument>.Filter.Where(doc => objectIds.Contains(doc.Id));
            await _collection.DeleteManyAsync(filter);
        }
        public async Task DeleteOneAsync(string documentId)
        {
            var objectId = ObjectId.Parse(documentId);
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, objectId);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
