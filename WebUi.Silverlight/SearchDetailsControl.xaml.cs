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

using RequestorService = WebUi.Silverlight.RequestorService;
using WebUi.Silverlight.RequestorService;

namespace WebUi.Silverlight
{
    public partial class SearchDetailsControl : UserControl
    {
        //#region Properties for XAML Data Template Binding

        //public static DependencyProperty DependencyPropertyKeywords         = DependencyProperty.Register( "PropertyKeywords"           , typeof( String )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertySpecifications   = DependencyProperty.Register( "PropertySpecifications"     , typeof( String )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyCategory         = DependencyProperty.Register( "PropertyCategory"           , typeof( int    )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyCategoryCustom   = DependencyProperty.Register( "PropertyCategoryCustom"     , typeof( String )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyState            = DependencyProperty.Register( "PropertyState"              , typeof( int    )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyStateCustom      = DependencyProperty.Register( "PropertyStateCustom"        , typeof( String )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyStartPrice       = DependencyProperty.Register( "PropertyStartPrice"         , typeof( int    )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyStartPriceCustom = DependencyProperty.Register( "PropertyStartPriceCustom"   , typeof( String )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyEndPrice         = DependencyProperty.Register( "PropertyEndPrice"           , typeof( int    )  , typeof( SearchDetailsControl ), null );
        //public static DependencyProperty DependencyPropertyEndPriceCustom   = DependencyProperty.Register( "PropertyEndPriceCustom"     , typeof( String )  , typeof( SearchDetailsControl ), null );

        //public String PropertyKeywords          { get{return String.Empty;} set{Keywords.Text = value;                                              } }

        //public String PropertySpecifications    { get{return String.Empty;} set{Specifications.SelectAll(); Specifications.Selection.Text = value;  } }

        //public int    PropertyCategory          { get{return -1;          } set{Category.SelectedIndex = value;                                     } }
        //public String PropertyCategoryCustom    { get{return String.Empty;} set{CategoryCustom.Text = value;                                        } }

        //public int    PropertyState             { get{return -1;          } set{State.SelectedIndex = value;                                        } }
        //public String PropertyStateCustom       { get{return String.Empty;} set{StateCustom.Text = value;                                           } }

        //public int    PropertyStartPrice        { get{return -1;          } set{StartPrice.SelectedIndex = value;                                   } }
        //public String PropertyStartPriceCustom  { get{return String.Empty;} set{StartPriceCustom.Text = value;                                      } }

        //public int    PropertyEndPrice          { get{return -1;          } set{EndPrice.SelectedIndex = value;                                     } }
        //public String PropertyEndPriceCustom    { get{return String.Empty;} set{EndPriceCustom.Text = value;                                        } }




        //public int KeywordsssssProperty
        //{
        //    get { return ( int ) GetValue( KeywordsssssPropertyProperty ); }
        //    set { SetValue( KeywordsssssPropertyProperty, value ); }
        //}

        //// Using a DependencyProperty as the backing store for KeywordsssssProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty 
        //    KeywordsssssPropertyProperty = DependencyProperty.Register( "KeywordsssssProperty", typeof( int ), typeof( SearchDetailsControl ), null );
        

        //#endregion

        #region SearchDetailsControl()

        public SearchDetailsControl()
        {
            InitializeComponent();

            // Category
            for( int i=(int)RequestorService.Category.Start + 1; i < (int)RequestorService.Category.End; i++ )
            {
                this.Category.Items.Add( Enum.GetName( typeof( RequestorService.Category ), i ) );
            }

            // State
            for( int j=(int)RequestorService.State.Start + 1; j < (int)RequestorService.State.End; j++ )
            {
                this.State.Items.Add( Enum.GetName( typeof( RequestorService.State ), j ) );
            }

            // Start Price + End Price
            for( int k=(int)RequestorService.PriceRange.Start + 1; k < (int)RequestorService.PriceRange.End; k++ )
            {
                this.StartPrice.Items.Add( Enum.GetName( typeof( RequestorService.PriceRange ), k ).Replace("P_","") );
                this.EndPrice  .Items.Add( Enum.GetName( typeof( RequestorService.PriceRange ), k ).Replace("P_","") );
            }

            // Set initial value
            this.Category.SelectedIndex = 0;
            this.State.SelectedIndex = 0;
            this.StartPrice.SelectedIndex = 0;
            this.EndPrice.SelectedIndex = 0;
        }

        #endregion

        #region InitFromSearchEntity()

        public void InitFromSearchEntity( SearchEntityProxy searchEntity )
        {
            if( searchEntity != null )
            {
                this.Keywords.Text              = searchEntity.Keywords;

                this.Specifications.Text        = searchEntity.Specifications;

                this.Category.SelectedIndex     = (int)searchEntity.Category + 1;
                this.CategoryCustom.Text        = searchEntity.Keywords;

                this.State.SelectedIndex        = (int)searchEntity.State + 1;
                this.StateCustom.Text           = searchEntity.StateCustom;

                this.StartPrice.SelectedIndex   = (int)searchEntity.StartPrice + 1;
                this.EndPrice.SelectedIndex     = (int)searchEntity.EndPrice + 1;
                this.StartPriceCustom.Text      = searchEntity.StartPriceCustom;
                this.EndPriceCustom.Text        = searchEntity.EndPriceCustom;
            }
        }

        #endregion

        #region ToSearchEntity()

        public SearchEntityProxy ToSearchEntity()
        {
            var searchEntity = new SearchEntityProxy();

                searchEntity.Keywords           = this.Keywords.Text                ;
                                                  this.Specifications.SelectAll()   ;
                searchEntity.Specifications     = this.Specifications.Text          ;

                RequestorService.Category category = RequestorService.Category.Start;
                Enum.TryParse<RequestorService.Category>( Category.SelectedValue as String, out category );
                searchEntity.Category           = (int)category                     ;
                searchEntity.CategoryCustom     = this.CategoryCustom.Text          ;

                RequestorService.State state    = RequestorService.State.Start      ;
                Enum.TryParse<RequestorService.State>( State.SelectedValue as String, out state );
                searchEntity.State              = (int)state                        ;
                searchEntity.StateCustom        = this.StateCustom.Text             ;

                RequestorService.PriceRange price1 = RequestorService.PriceRange.Start;
                RequestorService.PriceRange price2 = RequestorService.PriceRange.Start;
                String price1AsString = StartPrice.SelectedValue as String;
                String price2AsString = EndPrice.SelectedValue as String;
                Enum.TryParse<RequestorService.PriceRange>( String.Format( "{0}{1}", price1AsString.Equals( "Custom" ) ? "" : "P_", price1AsString ), out price1 );
                Enum.TryParse<RequestorService.PriceRange>( String.Format( "{0}{1}", price2AsString.Equals( "Custom" ) ? "" : "P_", price2AsString ), out price2 );
                searchEntity.StartPrice         = (int)price1                       ;
                searchEntity.EndPrice           = (int)price2                       ;
                searchEntity.StartPriceCustom   = this.StartPriceCustom.Text        ;
                searchEntity.EndPriceCustom     = this.EndPriceCustom.Text          ;

                searchEntity.CurrentHash        = String.Empty;
                searchEntity.LastHash           = String.Empty;

            return searchEntity;
        }

        #endregion

        #region Combo Boxes changed

        private void Category_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            RequestorService.Category selectedCategory = RequestorService.Category.Start;
            Enum.TryParse<RequestorService.Category>( Category.SelectedValue as String, out selectedCategory );

            CategoryCustom.Visibility = ( selectedCategory == RequestorService.Category.Custom ) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void State_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            RequestorService.State selectedCategory = RequestorService.State.Start;
            Enum.TryParse<RequestorService.State>( State.SelectedValue as String, out selectedCategory );

            StateCustom.Visibility = ( selectedCategory == RequestorService.State.Custom ) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void StartPrice_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            RequestorService.PriceRange selectedCategory = RequestorService.PriceRange.Start;
            Enum.TryParse<RequestorService.PriceRange>( StartPrice.SelectedValue as String, out selectedCategory );

            StartPriceCustom.Visibility = ( selectedCategory == RequestorService.PriceRange.Custom ) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void EdnPrice_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            RequestorService.PriceRange selectedCategory = RequestorService.PriceRange.Start;
            Enum.TryParse<RequestorService.PriceRange>( EndPrice.SelectedValue as String, out selectedCategory );

            EndPriceCustom.Visibility = ( selectedCategory == RequestorService.PriceRange.Custom ) ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion
    }
}
