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
    public partial class Progress : ChildWindow
    {
        String _message = null;
        String _currentChar = null;
        DispatcherTimer _timer = null;

        public Progress( String message )
        {
            _message = message;
            _currentChar = String.Empty;

            InitializeComponent();
        }

        private void ChildWindow_Loaded( object sender, RoutedEventArgs e )
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan( 0, 0, 0, 0, 100 );
            _timer.Tick += new EventHandler( TimerDelegate );
            _timer.Start();
        }

        public void TimerDelegate( object o, EventArgs sender )
        {
            UpdateCurrentChar();

            MessageTextBlock.Text = _message + _currentChar;
        }

        private void UpdateCurrentChar()
        {
            if( _currentChar.Equals( String.Empty ) ) _currentChar = @"-";
            else if( _currentChar.Equals( @"-" ) ) _currentChar = @"\";
            else if( _currentChar.Equals( @"\" ) ) _currentChar = @"|";
            else if( _currentChar.Equals( @"|" ) ) _currentChar = @"/";
            else if( _currentChar.Equals( @"/" ) ) _currentChar = @"-";
        }
    }
}

