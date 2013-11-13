using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Windows.Data.Html;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace rss_demo
{
    public sealed partial class ItemPage : Page
    {
        Uri url;
        public ItemPage()
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
                Thickness margin = article.Margin;
                margin.Left = 20;
                article.Margin = margin;
                article.Width = 280;
                itemContent.FontSize = 16;

                itemTitle.FontSize = 16;
                itemTitle.Width = 200;
                itemTitle.TextWrapping = TextWrapping.Wrap;
            }
            else
            {
                Thickness margin = article.Margin;
                margin.Left = 50;
                article.Margin = margin;
                article.Width = double.NaN;
                itemContent.FontSize = 16;

                itemTitle.FontSize = 26;
                itemTitle.Width = double.NaN;
                itemTitle.TextWrapping = TextWrapping.NoWrap;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FeedClass item = (FeedClass)e.Parameter;
            itemTitle.Text = item.Title;

            if (item.Image != null)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(item.Image, UriKind.RelativeOrAbsolute));
                bitmap.ImageOpened += bitmap_ImageOpened;
                itemImage.Visibility = Visibility.Collapsed;
                itemImage.Source = bitmap;
            }

            if (item.Author != null)
                itemAuthor.Text = item.Author;

            if (item.PubDate != null)
                itemDate.Text = item.PubDate.ToString();

            if (item.Content != null)
                itemContent.Text = HtmlUtilities.ConvertToText(item.Content);

            if (item.Link != null)
                url = item.Link;
        }

        void bitmap_ImageOpened(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = sender as BitmapImage;
            itemImage.Height = bitmap.PixelHeight;
            itemImage.Width = bitmap.PixelWidth;
            itemImage.Visibility = Visibility.Visible;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(url);
        }

       private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
