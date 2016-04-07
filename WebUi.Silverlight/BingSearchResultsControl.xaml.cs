using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace WebUi.Silverlight
{
    #region WebSeachResult

    public class WebSeachResult
    {
        public String Label { get; set; }
        public String Url { get; set; }
    }

    #endregion

    public partial class BingSearchResultsControl : UserControl
    {
        const string AppId = "643E9B768D2D0191FD1EF35571DECCA7133FB3B0";

        public BingSearchResultsControl()
        {
            InitializeComponent();
        }

        public void SearchForKeywords( String keywords )
        {
            var bingSearchResult = new List<WebSeachResult>();

            try
            {
                var request = new BingService.SearchRequest();
                    request.AppId = AppId;      // Common request fields (required)
                    request.Query = keywords;
                    request.Sources = new BingService.SourceType[] { BingService.SourceType.RelatedSearch };
                    request.Version = "2.0";    // Common request fields (optional)
                    request.Market = "en-us";
                    request.Options = new BingService.SearchOption[] {};

                var bingSearchClient = new BingService.BingPortTypeClient();
                    bingSearchClient.SearchCompleted += BingSearchClient_SearchCompleted;
                    bingSearchClient.SearchAsync( request );
            }
            catch( Exception )
            {

            }
        }

        void BingSearchClient_SearchCompleted( object sender, BingService.SearchCompletedEventArgs e )
        {
            var response = e.Result as BingService.SearchResponse;
            if( response == null || response.RelatedSearch == null )
                return;

            var resultList = new List<WebSeachResult>();

            foreach( BingService.RelatedSearchResult result in response.RelatedSearch.Results )
            {
                var webSeachResult = new WebSeachResult();
                    webSeachResult.Label = result.Title;
                    webSeachResult.Url   = result.Url;

                resultList.Add( webSeachResult );
            }

            BingSearchResults.ItemsSource = resultList;

            this.Height = resultList.Count * 30;
        }
    }
}
