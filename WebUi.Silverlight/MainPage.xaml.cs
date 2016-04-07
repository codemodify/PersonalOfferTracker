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
using System.Windows.Threading;

namespace WebUi.Silverlight
{
    public partial class MainPage : UserControl
    {
        #region Accordion Data Srouce

        public class ContentItem
        {
            public string ItemText{ get; set; }

            #region Helpers

            public ContentItem( string itemText )
            {
                ItemText = itemText;
            }

            #endregion
        }

        public class AccordionItem
        {
            public string HeaderText{ get; set; }

            public List<ContentItem> Content{ get; set; }

            #region Helpers

            public AccordionItem( string headerText )
            {
                HeaderText = headerText;

                Content = new List<ContentItem>();
            }

            #endregion
        }

        #endregion

        delegate void TextBoxClickHandler();
        Dictionary<string,TextBoxClickHandler> HandleClick;

        public MainPage()
        {
            InitializeComponent();

            #region Init Accordion Navigation

            const string searches = "Searches";
            const string inbox = "Inbox";
            const string alerts = "Alerts";
            const string general = "General";

            HandleClick = new Dictionary<string,TextBoxClickHandler>();
            HandleClick.Add( searches, SearchesClick );
            HandleClick.Add( inbox, InboxClick );
            HandleClick.Add( alerts, AlertsClick );
            HandleClick.Add( general, GeneralClick );

            AccordionItem
                homeItem = new AccordionItem( "Home" );
                homeItem.Content.Add( new ContentItem( searches ) );
                homeItem.Content.Add( new ContentItem( inbox ) );
                homeItem.Content.Add( new ContentItem( alerts ) ); 
            
            AccordionItem
                settingsItems = new AccordionItem( "Settings" );
                settingsItems.Content.Add( new ContentItem( general ) );

            List<AccordionItem>
                accordionItems = new List<AccordionItem>();
                accordionItems.Add( homeItem );
                accordionItems.Add( settingsItems );

            NavigationAccordion.ItemsSource = accordionItems;

            #endregion
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {
            //Helper.ShowProgress( "Loading..." );

            //var timer = new DispatcherTimer();
            //    timer.Interval = new TimeSpan( 0, 0, 0, 2, 0 );
            //    timer.Tick += new EventHandler( TimerDelegate );
            //    timer.Start();
        }

        public void TimerDelegate( object o, EventArgs sender )
        {
            //Helper.HideProgress();
            //Helper.ShowProgress( "This is a work in progress, get back later to check things out..." );
        }

        #region Navigation 

        private void AccordionNavigationItemClicked( object sender, MouseButtonEventArgs e )
        {
            var textBlock = sender as TextBlock;

            HandleClick[ textBlock.Text ]();
        }

        private void SearchesClick()
        {
            FrameContent.Content = new SearchesPage();
        }
        
        private void InboxClick()
        {}
        
        private void AlertsClick()
        {}

        private void GeneralClick()
        {}

        #endregion
    }
}
