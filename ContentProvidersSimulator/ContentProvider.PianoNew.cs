using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class PianoNew : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Music )
                return null;

            if( !searchEntity.Keywords.Contains( "Piano" ) )
                return null;

            if( !searchEntity.Keywords.Contains( "New" ) )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId =  this.GetType().ToString();
                searchResult.Offer = "Branded Pianos. Quality and satifaction is as minimum.";
                searchResult.Specifications = "We're well known company exclusively focusing on on quality music instruments.";
                searchResult.StartPrice = (int)PriceRange.P_800;
                searchResult.EndPrice = (int)PriceRange.P_900;

            return searchResult;
        }
    }
}
