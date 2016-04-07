using System;
using System.Linq;
using System.Data.Services.Client;
using System.Collections.Generic;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

using Helpers.Searches.Definitions;

namespace Helpers.Storage
{
    #region SearchResultEntity

    public class SearchResultEntity : TableServiceEntity, ISearchResultEntity
    {
        public String       Id { get { return RowKey; } set { } }

        public String       SearchId        { get; set; }

        public String       ProviderId      { get; set; }

        public String       Offer           { get; set; }
        public String       Specifications  { get; set; }

        public int          State           { get; set; }
        public String       StateCustom     { get; set; }

        public int          StartPrice      { get; set; }
        public int          EndPrice        { get; set; }
        public String       StartPriceCustom{ get; set; }
        public String       EndPriceCustom  { get; set; }

        public SearchResultEntity() :
            base( String.Empty, Guid.NewGuid().ToString() )
        {}

        public SearchResultEntity( String id ) :
            base( String.Empty, id )
        {}
    }

    #endregion

    #region SearchesOffersTable

    public class SearchesOffersTable : TableServiceContext
    {
        public static string Name = "SearchesOffers";

        #region SearchesTable

        public SearchesOffersTable() :
            base( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials )
        { }

        public SearchesOffersTable( string baseAddress, StorageCredentials credentials ) :
            base( baseAddress, credentials )
        { }

        #endregion

        #region CreateIfNotExist()

        public static void CreateIfNotExist()
        {
            var cloudTableClient = new CloudTableClient( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials );
                cloudTableClient.CreateTableIfNotExist( Name );
        }

        #endregion

        #region AddEntity()

        public void AddEntity( SearchResultEntity searchResultEntity )
        {
            //if( GetOffersById( searchResultEntity.Id ) != null ) // if exist don't add it
            //    return;

            this.AddObject( Name, searchResultEntity );
            this.SaveChanges();
        }

        #endregion

        #region UpdateEntity()

        public void UpdateEntity( SearchResultEntity searchResultEntity )
        {
            this.UpdateObject( searchResultEntity );
            this.SaveChanges();
        }

        #endregion

        #region GetOffers()

        public List<SearchEntity> GetOffers()
        {
            var tableQuery = this.CreateQuery<SearchEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return new List<SearchEntity>();

            var entities = from u in tableQuery select u;

            return entities.ToList();
        }

        #endregion

        #region GetOfferById()

        public SearchResultEntity GetOfferById( String id )
        {
            var tableQuery = this.CreateQuery<SearchResultEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return null;

            var entities = from u in tableQuery where u.Id.Equals( id ) select u;

            var entitiesAsList = entities.ToList();

            return ( entitiesAsList.Count == 0 ) ? null : entitiesAsList[ 0 ];
        }

        #endregion

        #region GetOffersByProviderId()

        public List<SearchResultEntity> GetOffersByProviderId( String id )
        {
            var tableQuery = this.CreateQuery<SearchResultEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return null;

            var entities = from u in tableQuery where u.ProviderId.Equals( id ) select u;

            var entitiesAsList = entities.ToList();

            return entitiesAsList;
        }

        #endregion

        #region GetOffersBySearch()

        public List<SearchResultEntity> GetOffersBySearch( String searchId )
        {
            var tableQuery = this.CreateQuery<SearchResultEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return new List<SearchResultEntity>();

            var entities = from u in tableQuery where u.SearchId.Equals( searchId ) select u;

            return entities.ToList();
        }

        #endregion
    }

    #endregion
}
