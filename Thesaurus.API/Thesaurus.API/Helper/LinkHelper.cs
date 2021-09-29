using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Thesaurus.Api.Business.Model;

namespace Thesaurus.Api.Helper
{
    public static class LinkHelper
    {
        public static Uri CreateLink(IUrlHelper urlHelper, string routeName, string toBeSearchedText)
        {            
            return new Uri(urlHelper.Link(routeName, new {word = toBeSearchedText }));
        }

        public static Uri CreatePageLink(IUrlHelper urlHelper, string routeName, int currentPage, int pageSize)
        {
            return new Uri(urlHelper.Link(routeName, new { currentPage, pageSize }));
        }
    }
}
