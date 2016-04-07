using System;
using System.Linq;
using System.Data.Services.Client;
using System.Collections.Generic;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Helpers.Storage
{
    #region ContentProviderEntity

    public class ContentProviderEntity : TableServiceEntity
    {
        public string Id { get { return RowKey; } set { } }
        public string DisplayName { get; set; }
        public int Rank { get; set; }

        public ContentProviderEntity( string id ) :
            base( String.Empty, id ) // PartitionKey + RowKey
        {
            DisplayName = "Unknown Content Provider";
            Rank = 10;
        }

        #region Parameterless constructor required byt the Azure

        public ContentProviderEntity() :
            this( Guid.NewGuid().ToString() )
        {}

        #endregion
    }

    #endregion

    #region ContentProvidersTable

    public class ContentProvidersTable : TableServiceContext
    {
        public static string Name = "ContentProviders";

        #region ContentProvidersTable

        public ContentProvidersTable() :
            base( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials )
        { }

        public ContentProvidersTable( string baseAddress, StorageCredentials credentials ) :
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

        public void AddEntity( ContentProviderEntity contentProviderEntity )
        {
            if( GetcontentProviderById( contentProviderEntity.Id ) != null ) // if exist don't add it
                return;

            this.AddObject( Name, contentProviderEntity );
            this.SaveChanges();
        }

        #endregion

        #region UpdateEntity()

        public void UpdateEntity( ContentProviderEntity contentProviderEntity )
        {
            this.UpdateObject( contentProviderEntity );
            this.SaveChanges();
        }

        #endregion

        #region GetcontentProviderById()

        public ContentProviderEntity GetcontentProviderById( string id )
        {
            var tableQuery = this.CreateQuery<ContentProviderEntity>( Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return null;

            var entities = from u in tableQuery
                           where u.Id.Equals( id ) // u.PartitionKey.Equals( Name ) &&
                           select u;

            List<ContentProviderEntity> entitiesAsList = entities.ToList();

            return entitiesAsList.Count != 0 ? entitiesAsList[ 0 ] : null;
        }

        #endregion
    }

    #endregion
}
