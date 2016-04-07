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
    #region FacebookFriend

    public class FacebookFriend
    {
        public String OnlineStatus { get; set; }
        public String FriendFullName { get; set; }
        public String FriendId { get; set; }
    }

    #endregion

    public partial class FacebookFriendListControl : UserControl
    {
        private Facebook.FacebookClient fb = null;

        public delegate void FacebookFriendDoubleClicked( String facebookUserId );
        public event FacebookFriendDoubleClicked FacebookFriendDoubleClickedDelegate = delegate { };

        private DateTime _lastClickTimeStamp;

        public FacebookFriendListControl()
        {
            InitializeComponent();

            _lastClickTimeStamp = DateTime.Now;

            FacebookFriendList.MouseLeftButtonUp += FacebookFriendList_MouseLeftButtonUp;
        }

        void FacebookFriendList_MouseLeftButtonUp( object sender, MouseButtonEventArgs e )
        {
            DateTime now = DateTime.Now;
            TimeSpan delta = now - _lastClickTimeStamp;

            if // double click
            (
                delta.Milliseconds < 500 
                && 
                delta.Seconds == 0 
                && 
                delta.Minutes == 0 
                && 
                delta.Hours == 0 
            )
            {
                var facebookFriend = FacebookFriendList.SelectedItem as FacebookFriend;

                FacebookFriendList.Visibility = System.Windows.Visibility.Collapsed;

                FacebookFriendDoubleClickedDelegate( facebookFriend.FriendId );
            }

            _lastClickTimeStamp = now;
        }

        void fb_GetCompleted( object sender, Facebook.FacebookApiEventArgs e )
        {
            if( e.Error == null )
            {
                var result = (IDictionary<string, object>) e.GetResultData();

                var facebookFriendList = new List<FacebookFriend>();

                if( result.Count != 0 )
                {
                    var array = result[ "data" ] as Facebook.JsonArray;

                    for( int i=0; i < array.Count; i++ )
                    {
                        var facebookData = array[ i ] as Facebook.JsonObject;

                        var facebookFriend = new FacebookFriend();
                        facebookFriend.OnlineStatus = "";
                        facebookFriend.FriendFullName = facebookData[ "name" ] as string;
                        facebookFriend.FriendId = facebookData[ "id" ] as string;

                        facebookFriendList.Add( facebookFriend );
                    }
                }

                Dispatcher.BeginInvoke( () => FacebookFriendList.ItemsSource = facebookFriendList );
            }
            else
            {
                // TODO: Need to let the user know there was an error
                //failedLogin();
            }
        }

        public void LoadFriendList()
        {
            if( fb == null )
            {
                fb = new Facebook.FacebookClient( Helper.FBCookie );
                fb.GetCompleted += new EventHandler<Facebook.FacebookApiEventArgs>( fb_GetCompleted );
            }

            fb.GetAsync( "/me/friends" );
        }
    }
}
