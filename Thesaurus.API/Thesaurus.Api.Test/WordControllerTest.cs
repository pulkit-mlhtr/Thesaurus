using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Test.Support;

namespace Thesaurus.Api.Test
{
    [TestClass]
    public class WordControllerTest
    {
        [TestMethod]
        public async Task AddWords_BadRequestTest()
        {
            Request request = new Request
            {
                Words = null
            };

           var response = await Models.wordController.AddWords(request);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task AddWords_ErrorTest()
        {
            Request request = new Request
            {
                Words = TestHelper.GetWords()
            };

            request.Words.Add(TestHelper.GetWords().First());

            var response = await Models.wordController.AddWords(request);

            Assert.IsInstanceOfType(response, typeof(StatusCodeResult));
        }
    }
}
