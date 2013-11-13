using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace facebook_animations_demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool animated = false;
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void animButton_Click(object sender, RoutedEventArgs e)
        {
            if (animated == false)
            {
                AnimationStart.Begin();
                animated = true;
            }
            else
            {
                AnimationClose.Begin();
                animated = false;
            }
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            upAnimationStart.Begin();
        }

        private void fade_Click(object sender, RoutedEventArgs e)
        {
            fadeAnimationStart.Begin();
        }

        private void size_Click(object sender, RoutedEventArgs e)
        {
            sizeAnimationStart.Begin();
        }

        private async void harlem_Click(object sender, RoutedEventArgs e)
        {
            player.Play();
            await Task.Delay(TimeSpan.FromSeconds(2));
            harlemFirst.Begin();
            await Task.Delay(TimeSpan.FromSeconds(13));
            harlemSecond.Begin();
            await Task.Delay(TimeSpan.FromSeconds(14));
            harlemFirst.Stop();
            harlemSecond.Stop();
        }
    }
}
