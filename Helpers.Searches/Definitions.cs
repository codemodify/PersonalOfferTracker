using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers.Searches.Definitions
{
    #region Category

    public enum Category : int
    {
        Start,

            Cars,
            Food,
            Gadgets,
            Computers,
            Music,
            Appratments,
            House,
            Pet,

            Custom,

        End
    }

    #endregion

    #region State

    public enum State : int
    {
        Start,

            Used,
            New,

            Custom,

        End
    }

    #endregion

    #region PriceRange

    public enum PriceRange : int
    {
        Start,

            P_5,
            P_10,
            P_20,
            P_50,
            P_100,
            P_150,
            P_200,
            P_250,
            P_300,
            P_350,
            P_400,
            P_450,
            P_500,
            P_550,
            P_600,
            P_650,
            P_700,
            P_750,
            P_800,
            P_850,
            P_900,
            P_950,
            P_1000,

            Custom,

        End
    }

    #endregion

    #region Search

    public interface ISearchEntity
    {
        String      Id                  { get; set; }

        String      Owner               { get; set; }

        String      Keywords            { get; set; }
        String      Specifications      { get; set; }

        int         Category            { get; set; }
        String      CategoryCustom      { get; set; }

        int         State               { get; set; }
        String      StateCustom         { get; set; }

        int         StartPrice          { get; set; }
        int         EndPrice            { get; set; }
        String      StartPriceCustom    { get; set; }
        String      EndPriceCustom      { get; set; }
    }

    public interface ISearchAttributesEntity
    {
        String      CurrentHash         { get; set; }
        String      LastHash            { get; set; }

        bool        NotifyOnNextOffer   { get; set; }
    }

    #endregion

    #region ISearchResultEntity

    public interface ISearchResultEntity
    {
        String      Id              { get; set; }

        String      SearchId        { get; set; }

        String      Offer           { get; set; }
        String      Specifications  { get; set; }

        int         State           { get; set; }
        String      StateCustom     { get; set; }

        int         StartPrice      { get; set; }
        int         EndPrice        { get; set; }
        String      StartPriceCustom{ get; set; }
        String      EndPriceCustom  { get; set; }
    }

    #endregion
}
