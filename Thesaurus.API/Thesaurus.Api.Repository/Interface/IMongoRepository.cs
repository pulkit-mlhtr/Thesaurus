using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus.Api.Repository.Interface
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Returns query object of the collection without executing
        /// </summary>
        /// <returns></returns>
        IQueryable<TDocument> AsQueryable();

        /// <summary>
        /// Find document list based on filter
        /// </summary>
        /// <typeparam name="TProjected"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="projectionExpression"></param>
        /// <returns></returns>
        IEnumerable<TDocument> FilterBy(
            Expression<Func<TDocument, bool>> filterExpression);

        /// <summary>
        /// Find and project document list based on filter
        /// </summary>
        /// <typeparam name="TProjected"></typeparam>
        /// <param name="filterExpression"></param>
        /// <param name="projectionExpression"></param>
        /// <returns></returns>
        IEnumerable<TProjected> FilterBy<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression);

        /// <summary>
        /// Insert one document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        Task InsertOneAsync(TDocument document);

        /// <summary>
        /// Insert bulk documents
        /// </summary>
        /// <param name="documents"></param>
        /// <returns></returns>
        Task InsertManyAsync(ICollection<TDocument> documents);

        /// <summary>
        /// update single existing document
        /// </summary>
        /// <param name="document"></param>
        /// <returns>Return updated document</returns>
        Task<TDocument> ReplaceOneAsync(TDocument document);      

        /// <summary>
        /// Delete documents based on their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteManyAsync(ICollection<string> documentIds);

        /// <summary>
        /// Delete first matched document based on document id
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task DeleteOneAsync(string documentId);
    }
}
