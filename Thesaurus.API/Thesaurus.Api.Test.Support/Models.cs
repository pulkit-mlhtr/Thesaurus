using System;
using System.Collections.Generic;
using System.Text;
using Thesaurus.Api.Business;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Interface;
using Thesaurus.Api.Business.Model.Mapper;
using Thesaurus.Api.Controllers;
using Thesaurus.Api.Repository.Base;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.Api.Test.Support
{
    public static class Models
    {
        public static IMapper<Word, Words> thesaurusRepoMapper = new ThesaurusRepoMapper();
        public static IMapper<Words, Word> wordMapper = new WordMapper();        
        public static IMongoRepository<Words> mongoRepository = new MongoRepository<Words>(AppConfiguration.mongoClient,AppConfiguration.Config);
        public static IWordLogic wordLogic = new WordLogic(mongoRepository, wordMapper, thesaurusRepoMapper);
        public static WordController wordController = new WordController(MockModels.wordLogic.Object);
    }
}
