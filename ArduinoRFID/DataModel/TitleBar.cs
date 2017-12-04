using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ArduinoRFID.DataModel
{
    public class TitleBar : Control
    {
        Button _closeButton;
        Button _maxButton;
        Button _minButton;

        public TitleBar()
        {
            MouseLeftButtonDown += OnTitleBarLeftButtonDown;
            MouseDoubleClick += TitleBar_MouseDoubleClick;
            Loaded += TitleBar_Loaded;
        }

        private void TitleBar_Loaded(object sender, RoutedEventArgs e)
        {
            _closeButton = (Button)Template.FindName("CloseButton", this);
            _minButton = (Button)Template.FindName("MinButton", this);
            _maxButton = (Button)Template.FindName("MaxButton", this);

            _closeButton.Click += CloseButton_Click;
            _minButton.Click += MinButton_Click;
            _maxButton.Click += MaxButton_Click;
        }

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void TitleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }

        static TitleBar()
        {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
        }

        //event handler
        private void OnTitleBarLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = TemplatedParent as Window;
            if ( window != null )
            {
                window.DragMove();
            }
        }

        void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            var window = TemplatedParent as Window;
            if ( window != null )
            {
                window.Close();
            }
        }

        void MinButton_Click(object sender, RoutedEventArgs e)
        {
            var window = TemplatedParent as Window;
            if ( window != null )
            {
                window.WindowState = WindowState.Minimized;
            }
        }

        //properties
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }


        #region dependency properties

        public static readonly DependencyProperty TitleProperty =
           DependencyProperty.Register(
               "Title", typeof(string), typeof(TitleBar));

        public static readonly DependencyProperty IconProperty =
           DependencyProperty.Register(
               "Icon", typeof(ImageSource), typeof(TitleBar));

        #endregion

    }
}
