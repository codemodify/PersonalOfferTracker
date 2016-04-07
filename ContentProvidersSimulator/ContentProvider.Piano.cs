using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class Piano : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Music )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId =  this.GetType().ToString();
                searchResult.Offer = "Piano. Not expensive.";
                searchResult.Specifications = "Classic piano, used but in good shape";
                searchResult.StartPrice = (int)PriceRange.P_300;
                searchResult.EndPrice = (int)PriceRange.P_400;

            return searchResult;
        }
    }
}
