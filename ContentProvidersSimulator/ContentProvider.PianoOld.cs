using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContentProviderDefinitions;
using Helpers.Searches.Definitions;
using Helpers.Storage;

namespace ContentProvidersSimulator.ContentProvider
{
    class PianoOld : AbstractContentProvider
    {
        public override ISearchResultEntity ProcessSearch( ISearchEntity searchEntity )
        {
            if( searchEntity.Category != (int)Category.Music )
                return null;

            if( !searchEntity.Keywords.Contains( "Piano" ) )
                return null;

            if( !searchEntity.Keywords.Contains( "Old" ) )
                return null;

            var searchResult = new SearchResultEntity();
                searchResult.ProviderId =  this.GetType().ToString();
                searchResult.Offer = "Can help to find old pianos. Not expensive.";
                searchResult.Specifications = "Classic piano, used but in good shape";
                searchResult.StartPrice = (int)PriceRange.P_300;
                searchResult.EndPrice = (int)PriceRange.P_400;

            return searchResult;
        }
    }
}
