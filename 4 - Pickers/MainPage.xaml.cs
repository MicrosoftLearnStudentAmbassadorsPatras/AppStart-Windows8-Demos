using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace pickers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                headerTitle.FontSize = 22;

                Thickness margin = header.Margin;
                margin.Left = 10;
                margin.Top = 80;
                header.Margin = margin;


                scroll.Width = 300;
            }
            else
            {
                headerTitle.FontSize = 80;
                Thickness margin = header.Margin;
                margin.Left = 60;
                margin.Top = 30;
                header.Margin = margin;

                scroll.Width = 750;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                input.Text = (string)e.Parameter;
            }
        }

        private async void load_Click_1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.List;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                    input.Text = await Windows.Storage.FileIO.ReadTextAsync(file);
                }
                catch
                {
                    // error while reading txt -> propably unsupported unicode characters
                }
            }
        }

        private async void save_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.Text != null && input.Text != "")
            {
                FileSavePicker savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                savePicker.SuggestedFileName = "New Document";
                StorageFile file = await savePicker.PickSaveFileAsync();
                if (file != null)
                {
                    CachedFileManager.DeferUpdates(file);
                    await FileIO.WriteTextAsync(file, input.Text);

                    // Let Windows know that we're finished changing the file so the other app can update the remote version of the file. 
                    // Completing updates may require Windows to ask for user input. 
                    FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                    if (status == FileUpdateStatus.Complete)
                    {
                        //saved
                    }
                    else
                    {
                        // couldn't be saved
                    }
                }
                else
                {
                    //Operation cancelled
                } 
            }
        }

        private void input_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.ViewportHeight);
        }

        private void textSmall_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.FontSize - 2 >= 9)
            {

                input.FontSize = input.FontSize - 2;
            }
        }

        private void textBig_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.FontSize + 2 <= 60)
            {
                input.FontSize = input.FontSize + 2;
            }
        }

        private void navigate_Click_1(object sender, RoutedEventArgs e)
        {
            if (input.Text != null && input.Text != "")
            {
                this.Frame.Navigate(typeof(second), input.Text);
            }
        }
    }
}
