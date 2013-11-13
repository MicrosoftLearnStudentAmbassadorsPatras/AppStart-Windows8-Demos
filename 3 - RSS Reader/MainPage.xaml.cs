using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

namespace rss_demo
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            ReformLayout();
            Window.Current.SizeChanged += WindowSizeChanged;
        }

        private void WindowSizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            ReformLayout();
        }

        private void ReformLayout()
        {
            if (ApplicationView.Value == ApplicationViewState.Snapped)
            {
                Thickness margin = header.Margin;
                margin.Left = 20;
                header.Margin = margin;

                headerTitle.FontSize = 22;

                list.Visibility = Visibility.Collapsed;
                listSnapped.Visibility = Visibility.Visible;
            }
            else
            {
                Thickness margin = header.Margin;
                margin.Left = 135;
                header.Margin = margin;

                headerTitle.FontSize = 80;

                listSnapped.Visibility = Visibility.Collapsed;
                list.Visibility = Visibility.Visible;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode != NavigationMode.Back)
            {
                LoadingGrid.Visibility = Visibility.Visible;
                await Feeder.GetFeedAsync("http://feeds.arstechnica.com/arstechnica/index");
                LoadingGrid.Visibility = Visibility.Collapsed;
            }
            rssList.ItemsSource = listSnapped.ItemsSource = Feeder.FeedItems;
        }

        private void rssList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (rssList.SelectedIndex != -1)
            {
                FeedClass item = (FeedClass)rssList.SelectedItem;
                this.Frame.Navigate(typeof(ItemPage), item);
                rssList.SelectedIndex = -1;
            }
        }

        private void listSnapped_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (listSnapped.SelectedIndex != -1)
            {
                FeedClass item = (FeedClass)listSnapped.SelectedItem;
                this.Frame.Navigate(typeof(ItemPage), item);
                listSnapped.SelectedIndex = -1;
            }
        }

        private async void refresh_Click_1(object sender, RoutedEventArgs e)
        {
            rssList.ItemsSource = listSnapped.ItemsSource = null;
            LoadingGrid.Visibility = Visibility.Visible;
            await Feeder.GetFeedAsync("http://feeds.arstechnica.com/arstechnica/index");
            LoadingGrid.Visibility = Visibility.Collapsed;
            rssList.ItemsSource = listSnapped.ItemsSource = Feeder.FeedItems;
        }
    }

}
