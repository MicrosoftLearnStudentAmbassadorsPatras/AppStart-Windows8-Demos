using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace xaml_basics
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool changed_color = false;
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void color_Click_1(object sender, RoutedEventArgs e)
        {
            if (changed_color == false)
            {
                down_left.Background = new SolidColorBrush(Windows.UI.Colors.YellowGreen);
                color.Content = "gray";
                changed_color = true;
            }
            else
            {
                down_left.Background = new SolidColorBrush(Windows.UI.Colors.Gray);
                color.Content = "yellow_green";
                changed_color = false;
            }
        }

        private void start_Click_1(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void pause_Click_1(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void stop_Click_1(object sender, RoutedEventArgs e)
        {
            player.Stop();
        }
    }
}
