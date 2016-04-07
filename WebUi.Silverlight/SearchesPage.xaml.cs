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
using System.Windows.Navigation;

using WebUi.Silverlight.RequestorService;
using WebUi.Silverlight.AuthentificatorService;
using BingService = WebUi.Silverlight.BingService;


namespace WebUi.Silverlight
{
    public partial class SearchesPage : Page
    {
        #region Accordion Data Srouce

        #region Offer

        public class Offer : SearchResultEntityProxy
        {
            #region Helpers

            public Offer( SearchResultEntityProxy searchResultEntity )
            {
                Id                  = searchResultEntity.Id;
                SearchId            = searchResultEntity.SearchId;
                Offer               = searchResultEntity.Offer;
                Specifications      = searchResultEntity.Specifications;
                State               = searchResultEntity.State;
                StateCustom         = searchResultEntity.StateCustom;
                StartPrice          = searchResultEntity.StartPrice;
                EndPrice            = searchResultEntity.EndPrice;
                StartPriceCustom    = searchResultEntity.StartPriceCustom;
                EndPriceCustom      = searchResultEntity.EndPriceCustom;

            }

            #endregion

            #region SearchResultEntityProxy
            #endregion

            public String ProviderRank { get; set; }
        }

        #endregion

        #region AccordionItem

        public class AccordionItem : SearchEntityProxy
        {
            #region Helpers

            public AccordionItem( SearchEntityProxy searchEntity )
            {
                Keywords          = searchEntity.Keywords;
                Specifications    = searchEntity.Specifications;
                Category          = searchEntity.Category;
                CategoryCustom    = searchEntity.CategoryCustom;
                CategoryAsValue   = Enum.GetName( typeof(RequestorService.Category), searchEntity.Category );
                State             = searchEntity.State;
                StateCustom       = searchEntity.StateCustom;
                StartPrice        = searchEntity.StartPrice;
                EndPrice          = searchEntity.EndPrice;
                StartPriceCustom  = searchEntity.StartPriceCustom;
                EndPriceCustom    = searchEntity.EndPriceCustom;

                NotifyOnNextOffer = searchEntity.NotifyOnNextOffer;


                Status = "/Resources/status-red.png";
                Offers = new List<Offer>();
                BingSearches = new List<String>();
            }

            #endregion

            #region SearchEntityProxy
            #endregion

            public String Status { get; set; }
            public List<Offer> Offers { get; set; }

            public List<String> BingSearches { get; set; }

            public double HelperWidth1 { get; set; }
            public double HelperWidth2 { get; set; }
            public double HelperHeight2 { get; set; }
        }

        #endregion

        #endregion

        #region SearchesPage()

        public SearchesPage()
        {
            InitializeComponent();
        }

        #endregion

        #region OnNavigatedTo()

        protected override void OnNavigatedTo( NavigationEventArgs e )
        {
            RefreshSeaches();
        }

        #endregion

        #region NewSearch

        private void NewSearchButton_Click( object sender, RoutedEventArgs e )
        {
            var newSearchChildWindow = new NewSearchChildWindow();
                newSearchChildWindow.OnNewSearch += OnNewSearch;
                newSearchChildWindow.Show();
        }

        void OnNewSearch( SearchEntityProxy searchEntity )
        {
            var requestorServiceClient = new RequestorServiceClient();
                requestorServiceClient.AddNewSearchForUserCompleted += AddNewSearchForUserCompleted;
                requestorServiceClient.AddNewSearchForUserAsync( Helper.UserIdFromCookie, searchEntity );
        }

        void AddNewSearchForUserCompleted( object sender, System.ComponentModel.AsyncCompletedEventArgs e )
        {
            RefreshSeaches();
        }

        #endregion

        #region Refresh

        #region RefreshSeaches()

        public void RefreshSeachesHelperGetUserId()
        {
            var authentificatorServiceClient = new AuthentificatorServiceClient();
            authentificatorServiceClient.GetUserByCookieCompleted += RefreshSeachesHelperGetUserId_GetUserByCookieCompleted;
            authentificatorServiceClient.GetUserByCookieAsync( Guid.Parse( Helper.Cookie ) );
        }

        void RefreshSeachesHelperGetUserId_GetUserByCookieCompleted( object sender, GetUserByCookieCompletedEventArgs e )
        {
            Helper.UserIdFromCookie = e.Result.Id;

            RefreshSeachesHelper();
        }

        public void RefreshSeachesHelper()
        {
            var requestorServiceClient = new RequestorServiceClient();
            requestorServiceClient.GetSearchesForUserCompleted += RequestorServiceClient_GetSearchesForUserCompleted;
            requestorServiceClient.GetSearchesForUserAsync( Helper.UserIdFromCookie );
        }

        public void RefreshSeaches()
        {
            Helper.ShowProgress( "Loading Searches..." );

            if( Helper.UserIdFromCookie == null )
            {
                RefreshSeachesHelperGetUserId();
            }
            else
            {
                RefreshSeachesHelper();
            }
        }

        #endregion

        #region RequestorServiceClient_GetSearchesForUserCompleted()

        void RequestorServiceClient_GetSearchesForUserCompleted( object sender, GetSearchesForUserCompletedEventArgs e )
        {
            Helper.HideProgress();

            var searchesForCurrentUser = e.Result; // as Dictionary<SearchEntityProxy, ObservableObjectCollection<SearchResultEntityProxy>>;

            var accordionItems = new List<AccordionItem>();

            foreach( SearchEntityProxy searchEntity in searchesForCurrentUser.Keys )
            {
                var accordionItem = new AccordionItem( searchEntity );

                // header
                accordionItem.Status = searchEntity.CurrentHash.Equals( searchEntity.LastHash ) ? "/Resources/status-green.png" : "/Resources/status-red.png";

                // content
                foreach( SearchResultEntityProxy searchResultEntity in searchesForCurrentUser[ searchEntity ] )
                {
                    var offer = new Offer( searchResultEntity );
                    offer.ProviderRank = String.Format( "{0}", (new Random()).Next( 10 ) );

                    accordionItem.Offers.Add( offer );
                }

                // Bing Searches

                //////accordionItem.BingSearches = 

                // helpers
                accordionItem.HelperWidth1 = SearchListSkin.ActualWidth - 20;
                accordionItem.HelperWidth2 = SearchListSkin.ActualWidth;
                accordionItem.HelperHeight2 = SearchListSkin.ActualHeight - 30 - 25 * (searchesForCurrentUser.Keys.Count-1);

                accordionItems.Add( accordionItem );
            }

            SearchesData.ItemsSource = null;
            SearchesData.ItemsSource = accordionItems;
        }

        #endregion

        #region Refresh_MouseLeftButtonUp()

        private void Refresh_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            RefreshSeaches();
        }

        #endregion

        #endregion

        #region Data Bind

        private void SearchDetailsControl_Loaded( object sender, RoutedEventArgs e )
        {
            var searchDetailsControl = sender as SearchDetailsControl;
            if( searchDetailsControl == null )
                return;

            var accordionItem = searchDetailsControl.DataContext as AccordionItem;
            if( accordionItem == null )
                return;

            searchDetailsControl.Category.SelectedIndex     = accordionItem.Category    - 1;
            searchDetailsControl.State.SelectedIndex        = accordionItem.State       - 1;
            searchDetailsControl.StartPrice.SelectedIndex   = accordionItem.StartPrice  - 1;
            searchDetailsControl.EndPrice.SelectedIndex     = accordionItem.EndPrice    - 1;
        }

        private void SearchResultDetailsControl_Loaded( object sender, RoutedEventArgs e )
        {
            var searchResultDetailsControl = sender as SearchResultDetailsControl;
            if( searchResultDetailsControl == null )
                return;

            var offer = searchResultDetailsControl.DataContext as Offer;
            if( offer == null )
                return;

            searchResultDetailsControl.State.SelectedIndex      = offer.State       - 1;
            searchResultDetailsControl.StartPrice.SelectedIndex = offer.StartPrice  - 1;
            searchResultDetailsControl.EndPrice.SelectedIndex   = offer.EndPrice    - 1;
        }

        private void BingSearchResultsControl_Loaded( object sender, RoutedEventArgs e )
        {
            var bingSearchResultsControl = sender as BingSearchResultsControl;
            if( bingSearchResultsControl == null )
                return;

            var accordionItem = bingSearchResultsControl.DataContext as AccordionItem;
            if( accordionItem == null )
                return;

            bingSearchResultsControl.SearchForKeywords( accordionItem.Keywords );
        }

        #endregion

        #region Facebook Integration

        private void Facebook_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            FacebookFriendList.Visibility = 
                ( FacebookFriendList.Visibility == System.Windows.Visibility.Visible ) ?
                System.Windows.Visibility.Collapsed :
                System.Windows.Visibility.Visible;
        }

        private void FacebookFriendList_Loaded( object sender, RoutedEventArgs e )
        {
            FacebookFriendList.FacebookFriendDoubleClickedDelegate += FacebookFriendList_FacebookFriendDoubleClickedDelegate;
            FacebookFriendList.LoadFriendList();

            RefreshSeaches();
        }

        void FacebookFriendList_FacebookFriendDoubleClickedDelegate( string facebookUserId )
        {
            FacebookFriendList.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void AskFriendButton_Loaded( object sender, RoutedEventArgs e )
        {
            var button = sender as Button;
                button.MouseLeftButtonUp += new MouseButtonEventHandler( button_MouseLeftButtonUp );
        }

        void button_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            Facebook_MouseLeftButtonUp( null, null );
        }

        #endregion

    }
}
