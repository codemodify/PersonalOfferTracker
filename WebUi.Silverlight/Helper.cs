using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Theming;

namespace WebUi.Silverlight
{
    public class Helper
    {
        public static string Cookie { get; set; }
        public static string FBCookie { get; set; }
        public static string UserIdFromCookie { get; set; }

        #region Progress

        private static Progress _progress = null;
        public static void ShowProgress( string message )
        {
            HideProgress();

            _progress = new Progress( message );
            _progress.Show();
        }

        public static void HideProgress()
        {
            if( _progress != null )
            {
                _progress.Close();
            }
        }

        #endregion

        #region Themes

        public static UIElement ApplyTheme( UserControl controlToApplyThemeTo )
        {
            ContentControl 
                theme = new WhistlerBlueTheme() as ContentControl;
                theme.Content = controlToApplyThemeTo;

            return theme;
        }

        #endregion
    }
}
