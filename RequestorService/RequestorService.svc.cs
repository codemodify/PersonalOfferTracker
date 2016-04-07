using System.Collections.Generic;

using Helpers.Storage;

namespace RequestorService
{
    public class RequestorService : IRequestorService
    {
        public Dictionary<SearchEntityProxy, List<SearchResultEntityProxy>> GetSearchesForUser( string userId )
        {
            var searchesForUser = new Dictionary<SearchEntityProxy, List<SearchResultEntityProxy>>();

            #region Connect to Azure Table and see what is in there ...

            var searchesOffersTable = new SearchesOffersTable();

            var searchesTable  = new SearchesTable();
            var searchEntities = searchesTable.GetSearchesByUser( userId );

            foreach( SearchEntity searchEntity in searchEntities )
            {
                var searchOffres = searchesOffersTable.GetOffersBySearch( searchEntity.Id );
             
                var searchOffresProxy = new List<SearchResultEntityProxy>();
                foreach( SearchResultEntity searchResultEntity in searchOffres )
                {
                    searchOffresProxy.Add( ConvertFromSearchResultEntityToSearchResultEntityProxy(searchResultEntity) );
                }

                searchesForUser.Add( ConvertFromSearchEntityToSearchEntityProxy( searchEntity ), searchOffresProxy );
            }

            #endregion

            return searchesForUser;
        }

        public void AddNewSearchForUser( string userId, SearchEntityProxy searchEntityProxy )
        {
            var searchEntity = ConvertFromSearchEntityProxyToSearchEntity( searchEntityProxy );
                searchEntity.Owner = userId;

            var searchesTable = new SearchesTable();
                searchesTable.AddEntity( searchEntity );
        }

        #region Helpers

        #region ConvertFromSearchEntityToSearchEntityProxy()

        public SearchEntityProxy ConvertFromSearchEntityToSearchEntityProxy( SearchEntity searchEntity )
        {
            var searchEntityProxy = new SearchEntityProxy();

                searchEntityProxy.Id                = searchEntity.Id               ;

                searchEntityProxy.Owner             = searchEntity.Owner            ;

                searchEntityProxy.Keywords          = searchEntity.Keywords         ;
                searchEntityProxy.Specifications    = searchEntity.Specifications   ;

                searchEntityProxy.Category          = searchEntity.Category         ;
                searchEntityProxy.CategoryCustom    = searchEntity.CategoryCustom   ;

                searchEntityProxy.State             = searchEntity.State            ;
                searchEntityProxy.StateCustom       = searchEntity.StateCustom      ;

                searchEntityProxy.StartPrice        = searchEntity.StartPrice       ;
                searchEntityProxy.EndPrice          = searchEntity.EndPrice         ;
                searchEntityProxy.StartPriceCustom  = searchEntity.StartPriceCustom ;
                searchEntityProxy.EndPriceCustom    = searchEntity.EndPriceCustom   ;

                searchEntityProxy.LastHash          = searchEntity.LastHash         ;
                searchEntityProxy.CurrentHash       = searchEntity.CurrentHash      ;
                searchEntityProxy.NotifyOnNextOffer = searchEntity.NotifyOnNextOffer;

            return searchEntityProxy;        
        }

        #endregion

        #region ConvertFromSearchEntityProxyToSearchEntity()

        public SearchEntity ConvertFromSearchEntityProxyToSearchEntity( SearchEntityProxy searchEntityProxy )
        {
            var searchEntity = new SearchEntity();

                searchEntity.Id                = searchEntityProxy.Id               ;
                                                                                    ;
                searchEntity.Owner             = searchEntityProxy.Owner            ;
                                                                                    ;
                searchEntity.Keywords          = searchEntityProxy.Keywords         ;
                searchEntity.Specifications    = searchEntityProxy.Specifications   ;
                                                                                    ;
                searchEntity.Category          = searchEntityProxy.Category         ;
                searchEntity.CategoryCustom    = searchEntityProxy.CategoryCustom   ;
                                                                                    ;
                searchEntity.State             = searchEntityProxy.State            ;
                searchEntity.StateCustom       = searchEntityProxy.StateCustom      ;
                                                                                    ;
                searchEntity.StartPrice        = searchEntityProxy.StartPrice       ;
                searchEntity.EndPrice          = searchEntityProxy.EndPrice         ;
                searchEntity.StartPriceCustom  = searchEntityProxy.StartPriceCustom ;
                searchEntity.EndPriceCustom    = searchEntityProxy.EndPriceCustom   ;
                                                                                    ;
                searchEntity.LastHash          = searchEntityProxy.LastHash         ;
                searchEntity.CurrentHash       = searchEntityProxy.CurrentHash      ;
                searchEntity.NotifyOnNextOffer = searchEntityProxy.NotifyOnNextOffer;

            return searchEntity;
        }

        #endregion

        #region ConvertFromSearchResultEntityToSearchResultEntityProxy()

        public SearchResultEntityProxy ConvertFromSearchResultEntityToSearchResultEntityProxy( SearchResultEntity searchResultEntity )
        {
            var searchResultEntityProxy = new SearchResultEntityProxy();

                searchResultEntityProxy.Id                = searchResultEntity.Id               ;

                searchResultEntityProxy.SearchId          = searchResultEntity.SearchId         ;

                searchResultEntityProxy.Offer             = searchResultEntity.Offer            ;
                searchResultEntityProxy.Specifications    = searchResultEntity.Specifications   ;

                searchResultEntityProxy.State             = searchResultEntity.State            ;
                searchResultEntityProxy.StateCustom       = searchResultEntity.StateCustom      ;

                searchResultEntityProxy.StartPrice        = searchResultEntity.StartPrice       ;
                searchResultEntityProxy.EndPrice          = searchResultEntity.EndPrice         ;
                searchResultEntityProxy.StartPriceCustom  = searchResultEntity.StartPriceCustom ;
                searchResultEntityProxy.EndPriceCustom    = searchResultEntity.EndPriceCustom   ;

            return searchResultEntityProxy;        
        }

        #endregion

        #endregion
    }
}
