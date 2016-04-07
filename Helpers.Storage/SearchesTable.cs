using System;
using System.Linq;
using System.Data.Services.Client;
using System.Collections.Generic;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

using Helpers.Searches.Definitions;

namespace Helpers.Storage
{
    #region SearchEntity

    public class SearchEntity : TableServiceEntity, ISearchEntity, ISearchAttributesEntity
    {
        #region ISearchEntity
        
        public String       Id { get { return RowKey; } set { } }

        public String       Owner               { get; set; }

        public String       Keywords            { get; set; }
        public String       Specifications      { get; set; }

        public int          Category            { get; set; }
        public String       CategoryCustom      { get; set; }

        public int          State               { get; set; }
        public String       StateCustom         { get; set; }

        public int          StartPrice          { get; set; }
        public int          EndPrice            { get; set; }
        public String       StartPriceCustom    { get; set; }
        public String       EndPriceCustom      { get; set; }

        #endregion

        #region ISearchAttributesEntity

        public String       LastHash            { get; set; }
        public String       CurrentHash         { get; set; }
        public bool         NotifyOnNextOffer   { get; set; }

        #endregion

        public SearchEntity() :
            base( String.Empty, Guid.NewGuid().ToString() )
        {}
    }

    #endregion

    #region SearchesTable

    public class SearchesTable : TableServiceContext
    {
        public static string Name = "Searches";

        #region SearchesTable

        public SearchesTable() :
            base( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials )
        {}

        public SearchesTable( string baseAddress, StorageCredentials credentials ) :
            base( baseAddress, credentials )
        {}

        #endregion

        #region CreateIfNotExist()

        public static void CreateIfNotExist()
        {
            var cloudTableClient = new CloudTableClient( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials );
                cloudTableClient.CreateTableIfNotExist( Name );
        }

        #endregion

        #region AddEntity()

        public void AddEntity( SearchEntity searchEntity )
        {
            this.AddObject( Name, searchEntity );
            this.SaveChanges();
        }

        #endregion

        #region UpdateEntity()

        public void UpdateEntity( SearchEntity searchEntity )
        {
            this.UpdateObject( searchEntity );
            this.SaveChanges();
        }

        #endregion

        #region GetSearches()

        public List<SearchEntity> GetSearches()
        {
            var tableQuery = this.CreateQuery<SearchEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return new List<SearchEntity>();

            var entities = from u in tableQuery select u;

            return entities.ToList();
        }

        #endregion

        #region GetSearchesByUser()

        public List<SearchEntity> GetSearchesByUser( String userId )
        {
            var tableQuery = this.CreateQuery<SearchEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return new List<SearchEntity>();

            var entities = from u in tableQuery where u.Owner.Equals(userId) select u;

            return entities.ToList();
        }

        #endregion
    }

    #endregion
}
