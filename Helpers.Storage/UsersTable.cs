using System;
using System.Linq;
using System.Data.Services.Client;
using System.Collections.Generic;

using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace Helpers.Storage
{
    #region UserEntity

    public class UserEntity : TableServiceEntity
    {
        public string Id { get{ return RowKey; } set{} }
        public string DisplayName { get; set; }
        public Guid Cookie { get; set; }

        public UserEntity( string partition, string userId )
        {
            // PartitionKey - 
            //      Used by WAStorage if it needs to distribute the tables entities over multiple storage nodes.

            // RowKey - 
            //      The ID of the entity within the partition it belongs to.

            // Within each table an entity is identified as PartitionKey-RowKey;

            PartitionKey = partition;
            RowKey = userId;
            DisplayName = "You";
        }

        #region Parameterless constructor required byt the Azure

        public UserEntity() :
            this( String.Empty, Guid.NewGuid().ToString() )
        {}

        #endregion
    }

    #endregion

    #region FacebookUserEntity

    public class FacebookUserEntity : UserEntity
    {
        public static string Name = "Facebook";

        public FacebookUserEntity( string userId ) :
            base( "Facebook", userId )
        {}

        #region Parameterless constructor required byt the Azure

        public FacebookUserEntity() :
            this( Guid.NewGuid().ToString() )
        {}

        #endregion
    }

    #endregion

    #region UsersTable

    public class UsersTable : TableServiceContext
    {
        public static string Name = "Users";

        #region UsersTable

        public UsersTable() :
            base( Helpers.Storage.Account.GetCloudStorageAccount().TableEndpoint.ToString(), Helpers.Storage.Account.GetCloudStorageAccount().Credentials )
        {}

        public UsersTable( string baseAddress, StorageCredentials credentials ) :
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

        public void AddEntity( UserEntity userEntity )
        {
            if( GetUserById( userEntity.Id ) != null ) // if exist don't add it
                return;

            this.AddObject( Name, userEntity );
            this.SaveChanges();
        }

        #endregion

        #region UpdateEntity()

        public void UpdateEntity( UserEntity userEntity )
        {
            this.UpdateObject( userEntity );
            this.SaveChanges();
        }

        #endregion

        #region GetUserById()

        public UserEntity GetUserById( string id )
        {
            var tableQuery = this.CreateQuery<UserEntity>( UsersTable.Name );
            
            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return null;

            var userEntity = from u in tableQuery
                             where u.PartitionKey.Equals( FacebookUserEntity.Name ) && u.Id.Equals( id )
                             select u;

            List<UserEntity> userEntityAsList = userEntity.ToList();

            return userEntityAsList.Count != 0 ? userEntityAsList[0] : null;
        }

        #endregion

        #region GetUserByCookie()

        public UserEntity GetUserByCookie( Guid cookie )
        {
            var tableQuery = this.CreateQuery<UserEntity>( UsersTable.Name );

            bool isTableEmpty = !tableQuery.GetEnumerator().MoveNext();
            if( isTableEmpty )
                return null;

            var userEntity = from u in tableQuery
                             where u.PartitionKey.Equals( FacebookUserEntity.Name ) && u.Cookie.Equals( cookie )
                             select u;

            List<UserEntity> userEntityAsList = userEntity.ToList();

            return userEntityAsList.Count != 0 ? userEntityAsList[0] : null;
        }

        #endregion
    }

    #endregion
}
