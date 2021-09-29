using Moq;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Business.Model.Interface;
using Thesaurus.Api.Repository.Interface;

namespace Thesaurus.Api.Test.Support
{
    public static class MockModels
    {
        public static Mock<IMongoRepository<Words>> mongoRepositoryMock = new Mock<IMongoRepository<Words>>();
        public static Mock<IWordLogic> wordLogic = new Mock<IWordLogic>();
    }
}
