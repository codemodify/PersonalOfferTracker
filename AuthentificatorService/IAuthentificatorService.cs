using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace AuthentificatorService
{
    [ServiceContract]
    public interface IAuthentificatorService
    {
        [OperationContract]
        Guid SetUserAndGetCookie( UserIdentification userIdentification );

        [OperationContract]
        UserIdentification GetUserByCookie( Guid cookie );
    }

    [DataContract]
    public class UserIdentification
    {
        [DataMember]
        public String DisplayName { get; set; }

        [DataMember]
        public String Id { get; set; }
    }
}
