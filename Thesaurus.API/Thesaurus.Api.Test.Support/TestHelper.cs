using System;
using System.Collections.Generic;
using System.Text;
using Thesaurus.Api.Business.Model;
using Thesaurus.Api.Helper;

namespace Thesaurus.Api.Test.Support
{
    public static class TestHelper
    {
        public static IDictionary<string,Uri> GetSynonymsWithUri()
        {
            var synonyms = new Dictionary<string, Uri>();

            synonyms.Add("Apology", new Uri("http://hostname"));
            synonyms.Add("Guilt", new Uri("http://hostname"));
            synonyms.Add("Feel Bad", new Uri("http://hostname"));            

            return synonyms;
        }

        public static string[] GetSynonyms()
        {      
            return new string[] { "Apology","Guilt","Feel Bad" };
        }

        public static ICollection<Word> GetWords()
        {
            return new List<Word>
            {
                new Word
                {
                    Primary="Sorry",
                    Synonyms = GetSynonymsWithUri()
                },               
            };
        }
    }
}
