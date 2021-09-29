using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Thesaurus.Api.Business.Model.Extension
{
    public static class CollectionHandler
    {
        /// <summary>
        /// Convert all the list items to lower case
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<string> ToListLower(this IEnumerable<string> source)
        {
            return source.ToList().ConvertAll(c => c.ToLower());
        }

        public static IDictionary<string,Uri> ToDict(this string[] source)
        {
            var synonymsWithLinks = new Dictionary<string, Uri>();

            foreach (string word in source)
            {
                synonymsWithLinks.Add(word, null);
            }

            return synonymsWithLinks;
        }
    }
}
