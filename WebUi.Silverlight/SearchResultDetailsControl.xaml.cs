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
    public partial class SearchResultDetailsControl : UserControl
    {
        public SearchResultDetailsControl()
        {
            InitializeComponent();

            // State
            for( int j=(int) RequestorService.State.Start + 1; j < (int) RequestorService.State.End; j++ )
            {
                this.State.Items.Add( Enum.GetName( typeof( RequestorService.State ), j ) );
            }

            // Start Price + End Price
            for( int k=(int) RequestorService.PriceRange.Start + 1; k < (int) RequestorService.PriceRange.End; k++ )
            {
                this.StartPrice.Items.Add( Enum.GetName( typeof( RequestorService.PriceRange ), k ).Replace( "P_", "" ) );
                this.EndPrice.Items.Add( Enum.GetName( typeof( RequestorService.PriceRange ), k ).Replace( "P_", "" ) );
            }

            // Set initial value
            this.State.SelectedIndex = 0;
            this.StartPrice.SelectedIndex = 0;
            this.EndPrice.SelectedIndex = 0;
        }
    }
}
