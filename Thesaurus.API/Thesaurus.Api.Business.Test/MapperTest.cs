using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Test.Support;

namespace Thesaurus.Api.Business.Test
{
    [TestClass]
    public class MapperTest
    {
        [TestMethod]
        public void WordMapperTest()
        {
            var input = new Words
            {
                Word = "Sorry",
                Synonyms = TestHelper.GetSynonyms()
            };

            var expected = new Word
            {
                Primary = "Sorry",
                Synonyms = TestHelper.GetSynonymsWithUri()
            };

            var actual = Models.wordMapper.Map(input);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Synonyms);
            Assert.IsTrue(actual.Synonyms.Count>0);
            Assert.AreEqual(expected.Primary, actual.Primary);
            Assert.AreEqual(expected.Synonyms.Count, actual.Synonyms.Count);

        }

        [TestMethod]
        public void ThesaurusMapperTest()
        {
            var expected = new Words
            {
                Word = "sorry",
                Synonyms = TestHelper.GetSynonyms()
            };

            var input = new Word
            {
                Primary = "Sorry",
                Synonyms = TestHelper.GetSynonymsWithUri()
            };

            var actual = Models.thesaurusRepoMapper.Map(input);

            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Synonyms);
            Assert.IsTrue(actual.Synonyms.Length > 0);
            Assert.AreEqual(expected.Word, actual.Word);
            Assert.AreEqual(expected.Synonyms.Length, actual.Synonyms.Length);

        }
    }
}
