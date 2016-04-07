using System;
using System.Data.Services;
using System.ServiceModel.Activation;
using System.ServiceModel;

using Helpers.Storage;

namespace AuthentificatorService
{
    [AspNetCompatibilityRequirements( RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed )]
    [ServiceBehavior( InstanceContextMode = InstanceContextMode.PerCall )]
    public class AuthentificatorService : IAuthentificatorService
    {
        public AuthentificatorService()
        {
            UsersTable.CreateIfNotExist();
        }

        public Guid SetUserAndGetCookie( UserIdentification userIdentification )
        {
            //var cookie = Guid.Parse( "bf06b7cf-a316-4f2c-a7e8-309ca8b6f40a" );

            var cookie = Guid.NewGuid();

            var usersTable = new UsersTable();
            var userEntity = usersTable.GetUserById( userIdentification.Id );

            if( userEntity == null )
            {
                userEntity = new FacebookUserEntity( userIdentification.Id );
                userEntity.DisplayName = userIdentification.DisplayName;
                userEntity.Cookie = cookie;

                try
                {
                    usersTable.AddEntity( userEntity );
                }
                catch( System.Data.Services.Client.DataServiceRequestException )
                { }
            }
            else
            {
                userEntity.Cookie = cookie;

                usersTable.UpdateEntity( userEntity );
            }

            return cookie;
        }

        public UserIdentification GetUserByCookie( Guid cookie )
        {
            var usersTable = new Helpers.Storage.UsersTable();
            var userEntity = usersTable.GetUserByCookie( cookie );

            UserIdentification userIdentification = null;

            if( userEntity != null )
            {
                userIdentification = new UserIdentification();
                userIdentification.Id = userEntity.Id;
                userIdentification.DisplayName = userEntity.DisplayName;
            }

            return userIdentification;
        }
    }
}
