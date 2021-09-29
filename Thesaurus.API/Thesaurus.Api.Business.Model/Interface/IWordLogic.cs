using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus.Api.Business.Model.Interface
{
    public interface IWordLogic
    {       
        /// <summary>
        /// Add multiple words with their synonyms
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        Task AddWords(ICollection<Word> words);

        /// <summary>
        /// Get synonyms of searched word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        IEnumerable<Word> FindWord(string searchedWord);

        /// <summary>
        /// Get all records
        /// </summary>
        /// <param name="currentPage">default is 0</param>
        /// <param name="limit">default is 10</param>
        /// <returns></returns>
        IEnumerable<Word> GetAll(int currentPage, int limit);

        /// <summary>
        /// Delete documents based on their Id
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        Task Delete(ICollection<string> documentIds);

        /// <summary>
        /// Delete document based on document id
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        Task DeleteById(string documentId);

        /// <summary>
        /// Replace the entire document using Id
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        Task<Word> Update(Word word);
    }
}
