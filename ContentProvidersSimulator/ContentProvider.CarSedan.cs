using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class CarSedan : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Cars )
                return null;

            if( !searchEntity.Keywords.ToLower().Contains( "sedan" ) )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId = this.GetType().ToString();
                searchResult.Offer = "Sedan VW, used but in very good shape";
                searchResult.Specifications = "The car has 80000 KM, used in town, no country side usage";
                searchResult.StartPrice = (int)PriceRange.Custom;
                searchResult.EndPrice = (int)PriceRange.Custom;
                searchResult.StartPriceCustom = "40 k";
                searchResult.EndPriceCustom = "35 k";

            return searchResult;
        }
    }
}
