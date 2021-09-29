using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Extension;
using Thesaurus.Api.Business.Model.Interface;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.Api.Business
{
    public class WordLogic : IWordLogic
    {
        private readonly IMongoRepository<Words> _thesaurusRepo;
        private readonly IMapper<Word, Words> _thesaurusRepoMapper;
        private readonly IMapper<Words, Word> _wordMapper;
        public WordLogic(IMongoRepository<Words> thesaurusRepo, IMapper<Words, Word> wordMapper,
                                                                IMapper<Word, Words> thesaurusRepoMapper)
        {
            _thesaurusRepo = thesaurusRepo;
            _wordMapper = wordMapper;
            _thesaurusRepoMapper = thesaurusRepoMapper;
        }

        private async Task AddWord(Word word)
        {
            if(_thesaurusRepo.FilterBy(x => x.Word == word.Primary.ToLower()).Any())
            {
                throw new DataMisalignedException($"Operation aborted. Error: {word.Primary} already exists!!");
            }
            
            await _thesaurusRepo.InsertOneAsync(_thesaurusRepoMapper.Map(word));
        }

        public IEnumerable<Word> GetAll(int currentPage, int limit)
        {
            int skip = currentPage * limit;

            return _thesaurusRepo.AsQueryable().Skip(skip)
                .Take(limit).ToList().ConvertAll(t => _wordMapper.Map(t));
        }

        public async Task AddWords(ICollection<Word> words)
        {
            if (words.Count == 1)
            {
                await AddWord(words.First());
            }
            else
            {
                var toBeAddedWords = words.Select(x => x.Primary.ToLower());

                if (_thesaurusRepo.FilterBy(x => toBeAddedWords.Contains(x.Word)).Any())
                {
                    throw new DataMisalignedException("Operation aborted. Error: Word already exist !");
                }
                var repoCollection = words.ToList().ConvertAll(c => _thesaurusRepoMapper.Map(c));
                await _thesaurusRepo.InsertManyAsync(repoCollection);
            }
        }

        public IEnumerable<Word> FindWord(string searchedWord)
        {
            searchedWord = searchedWord.ToLower();

            var repoCollection = _thesaurusRepo.FilterBy<Word>(x => x.Word == searchedWord, t => _wordMapper.Map(t)).ToList();

            if (repoCollection.Count == 0)
            {
                repoCollection = _thesaurusRepo.FilterBy(x => x.Synonyms.Contains(searchedWord), t => _wordMapper.Map(t)).ToList();
            }

            return repoCollection;
        }

        public async Task<Word> Update(Word word)
        {            
            var updateDocument = await _thesaurusRepo.ReplaceOneAsync(_thesaurusRepoMapper.Map(word));

            return _wordMapper.Map(updateDocument);
        }

        public async Task Delete(ICollection<string> documentIds)
        {
            await _thesaurusRepo.DeleteManyAsync(documentIds);
        }

        public async Task DeleteById(string documentId)
        {
            await _thesaurusRepo.DeleteOneAsync(documentId);
        }
    }
}
