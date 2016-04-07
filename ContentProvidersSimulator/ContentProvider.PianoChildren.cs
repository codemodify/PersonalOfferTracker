using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class PianoChildren : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Music )
                return null;

            if( !searchEntity.Specifications.Contains( "piano" ) )
                return null;

            if( !searchEntity.Specifications.Contains( "children" ) )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId =  this.GetType().ToString();
                searchResult.Offer = "Especially designed and made pianos for children";
                searchResult.Specifications = "Especially designed and made pianos for children, non expensive";
                searchResult.StartPrice = (int)PriceRange.P_300;
                searchResult.EndPrice = (int)PriceRange.P_400;

            return searchResult;
        }
    }
}
