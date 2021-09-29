
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thesaurus.Api.Business.Model.Extension;

namespace Thesaurus.Api.Business.Test
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void ToListLowerTest()
        {
            var source = new List<string>
            {
                "ABC","DEF","GHI"
            };

            var response = source.ToListLower();

            Assert.IsFalse(response.ToList().Any(x=>x.Any(char.IsUpper)));
        }

        [TestMethod]
        public void ToDictTest()
        {
            var source = new string[]
            {
                "ABC","DEF","GHI"
            };

            var response = source.ToDict();

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Count, source.Length);
        }
    }
}
