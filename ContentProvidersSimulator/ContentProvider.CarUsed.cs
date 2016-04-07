using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class CarUsed : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Cars )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId = this.GetType().ToString();
                searchResult.Offer = "Used cars - everything";
                searchResult.Specifications = "Refurbished";
                searchResult.StartPrice = (int)PriceRange.P_450;
                searchResult.EndPrice = (int)PriceRange.P_400;

            return searchResult;
        }
    }
}
