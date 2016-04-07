using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class CarTruck : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Cars )
                return null;

            if( !searchEntity.Keywords.ToLower().Contains( "truck" ) )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId = this.GetType().ToString();
                searchResult.Offer = "Used trucks";
                searchResult.Specifications = "Non expensive used trucks. Waranty. Prices range from 40k - 50k";
                searchResult.StartPrice = (int)PriceRange.Custom;
                searchResult.EndPrice = (int)PriceRange.Custom;
                searchResult.StartPriceCustom = "40 k";
                searchResult.EndPriceCustom = "50 k";

            return searchResult;
        }
    }
}
