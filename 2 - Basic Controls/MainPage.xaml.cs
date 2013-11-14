using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace basic_controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            border.PointerEntered += border_PointerEntered;
            border.PointerExited += border_PointerExited;
        }

        void border_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            
            
                border.Background = new SolidColorBrush(Colors.Green);
            
        }

        void border_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if ((bool)check.IsChecked)
            border.Background = new SolidColorBrush(Colors.Red);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void pass_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password == "basic_controls")
            {
                canvas.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                canvas.Background = new SolidColorBrush(Colors.Red);
            }
        }

        private void play_Click_1(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void stop_Click_1(object sender, RoutedEventArgs e)
        {
            media.Stop();
        }

        private void slide_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            try
            {
                media.Volume = slide.Value/100;
            }
            catch { } 
        }
    }
}
