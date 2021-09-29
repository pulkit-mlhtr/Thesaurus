using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Business.Model.Collection;
using Thesaurus.Api.Test.Support;

namespace Thesaurus.Api.Business.Test
{
    [TestClass]
    public class WordLogicTest
    {
        [TestMethod]
        public void FindWordTest()
        {
            var expected = new List<Word> {
                new Word
                {
                    Primary="sorry",
                    Synonyms=TestHelper.GetSynonymsWithUri()
                }
            };
            var word = TestHelper.GetWords().First();
            var toBeSerchedWord = word.Primary.ToLower();            

            var actual = Models.wordLogic.FindWord(toBeSerchedWord);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.First().Primary, actual.First().Primary);            
        }

        [TestMethod]
        public async Task AddDeleteByIdWordTest()
        {
            var wordToAdd = "UT_Test_Word";
            var words = TestHelper.GetWords();

            words.First().Primary = wordToAdd;
            
            //Add
            await Models.wordLogic.AddWords(words);

            //Get
            var response = Models.wordLogic.FindWord(wordToAdd);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count()>0);
            Assert.IsTrue(response.First().Primary==wordToAdd.ToLower());

            //Delete
            await Models.wordLogic.DeleteById(response.First().Id);

            //Get
            response = Models.wordLogic.FindWord(wordToAdd);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() == 0);
        }

        [TestMethod]
        public void GetWordTest()
        {           
            var words = TestHelper.GetWords();                    

            //Get
            var response = Models.wordLogic.FindWord(words.First().Primary);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() > 0);
            Assert.IsTrue(response.First().Primary == words.First().Primary.ToLower());           
        }

        [TestMethod]
        public async Task DeleteWordsTest()
        {
            var wordToDelete = "UT_Test_Word";
            var words = TestHelper.GetWords();

            words.First().Primary = wordToDelete;

            //Add
            await Models.wordLogic.AddWords(words);

            //Get
            var response = Models.wordLogic.FindWord(wordToDelete);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() > 0);
            Assert.IsTrue(response.First().Primary == wordToDelete.ToLower());

            //Delete
            await Models.wordLogic.Delete(new List<string> { response.First().Id });
   
            //Get
            response = Models.wordLogic.FindWord(wordToDelete);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() == 0);
        }

        [TestMethod]
        public async Task UpdateWordTest()
        {
            var wordToUpdate = "abbess";
            var words = TestHelper.GetWords();

            words.First().Primary = wordToUpdate;          

            //Get
            var response = Models.wordLogic.FindWord(wordToUpdate);

            Assert.IsNotNull(response);
            Assert.IsTrue(response.Count() > 0);
            Assert.IsTrue(response.First().Primary == wordToUpdate.ToLower());

            var actual = response.First().Synonyms.Count();

            response.First().Synonyms.Add("ut_test_synonym", null);

            //Update
            var updatedWord = await Models.wordLogic.Update(response.First());            

            Assert.IsNotNull(updatedWord);
            Assert.IsTrue(updatedWord.Synonyms.Count>0);            

            Assert.AreEqual(updatedWord.Synonyms.Count, actual + 1);

            updatedWord.Synonyms.Remove("ut_test_synonym");
            
            //update
            updatedWord = await Models.wordLogic.Update(updatedWord);

            Assert.IsNotNull(updatedWord);
            Assert.AreEqual(updatedWord.Synonyms.Count, actual);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var response = Models.wordLogic.GetAll(0, 20);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count(),20);
        }
    }
}
