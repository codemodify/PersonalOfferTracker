using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers.Searches.Definitions;

namespace ContentProviderDefinitions
{
    public abstract class AbstractContentProvider : IContentProvider
    {
        public abstract ISearchResultEntity ProcessSearch( ISearchEntity searchEntity );
    }
}
