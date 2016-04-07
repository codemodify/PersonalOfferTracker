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

using WebUi.Silverlight.RequestorService;

namespace WebUi.Silverlight
{
    public partial class NewSearchChildWindow : ChildWindow
    {
        public delegate void NewSearch( SearchEntityProxy searchEntity );
        public event NewSearch OnNewSearch = delegate { };

        public NewSearchChildWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;

            OnNewSearch( SearchDetails.ToSearchEntity() );
        }

        private void CancelButton_Click( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
        }
    }
}

