using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Helpers.Storage;
using Helpers.Searches.Definitions;

namespace RequestorService
{
    [ServiceContract]
    public interface IRequestorService
    {
        [OperationContract]
        Dictionary<SearchEntityProxy,List<SearchResultEntityProxy>> GetSearchesForUser( String userId );

        [OperationContract]
        void AddNewSearchForUser( String userId, SearchEntityProxy searchEntityProxy );
    }

    #region SearchEntityProxy

    [DataContract]
    public class SearchEntityProxy : ISearchEntity, ISearchAttributesEntity
    {
        #region ISearchEntity

        [DataMember] public String      Id                  { get; set; }

        [DataMember] public String      Owner               { get; set; }

        [DataMember] public String      Keywords            { get; set; }
        [DataMember] public String      Specifications      { get; set; }

        [DataMember] public int         Category            { get; set; }
        [DataMember] public String      CategoryCustom      { get; set; }
        [DataMember] public String      CategoryAsValue     { get; set; }

        [DataMember] public int         State               { get; set; }
        [DataMember] public String      StateCustom         { get; set; }

        [DataMember] public int         StartPrice          { get; set; }
        [DataMember] public int         EndPrice            { get; set; }
        [DataMember] public String      StartPriceCustom    { get; set; }
        [DataMember] public String      EndPriceCustom      { get; set; }

        #endregion

        #region ISearchAttributesEntity

        [DataMember] public String      CurrentHash         { get; set; }
        [DataMember] public String      LastHash            { get; set; }
        [DataMember] public bool        NotifyOnNextOffer   { get; set; }

        #endregion

        [DataMember] public Category CategoryEnum;
        [DataMember] public State StateEnum;
        [DataMember] public PriceRange PriceRangeEnum;
    }

    #endregion

    #region SearchResultEntityProxy

    [DataContract]
    public class SearchResultEntityProxy : ISearchResultEntity
    {
        [DataMember] public String      Id              { get; set; }

        [DataMember] public String      SearchId        { get; set; }

        [DataMember] public String      Offer           { get; set; }
        [DataMember] public String      Specifications  { get; set; }

        [DataMember] public int         State           { get; set; }
        [DataMember] public String      StateCustom     { get; set; }

        [DataMember] public int         StartPrice      { get; set; }
        [DataMember] public int         EndPrice        { get; set; }
        [DataMember] public String      StartPriceCustom{ get; set; }
        [DataMember] public String      EndPriceCustom  { get; set; }


        [DataMember] public Category CategoryEnum;
        [DataMember] public State StateEnum;
        [DataMember] public PriceRange PriceRangeEnum;
    }

    #endregion
}
