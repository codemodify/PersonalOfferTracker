using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers.Searches.Definitions;

namespace ContentProviderDefinitions
{
    public interface IContentProvider
    {
        ISearchResultEntity ProcessSearch( ISearchEntity searchEntity );
    }
}
