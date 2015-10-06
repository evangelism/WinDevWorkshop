using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Advertising.Models;
using Microsoft.Advertising.WinRT.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Advertising
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private InterstitialAd _interstitialAd;

        bool _showAds = true;
        public bool ShowAds { get { return _showAds; } set { _showAds = value; } }

        bool _viewedFullInterstitial = true;
        public bool ViewedFullInterstitial { get { return _viewedFullInterstitial; } set { _viewedFullInterstitial = value; } }
        public MainPage()
        {
            this.InitializeComponent();

            if (ShowAds)
            {
                // initialize the interstitial class
                _interstitialAd = new InterstitialAd();

                // wire up all 4 events
                _interstitialAd.AdReady += interstitialAd_AdReady;
                _interstitialAd.Cancelled += interstitialAd_Cancelled;
                _interstitialAd.Completed += interstitialAd_Completed;
                _interstitialAd.ErrorOccurred += interstitialAd_ErrorOccurred;

                RequestAd();
            }
            else
            {
                // start normally
            }

        }

        private void RequestAd()
        {
            _interstitialAd.RequestAd(AdType.Video, DemoAds.VideoAdUnit.AppId, DemoAds.VideoAdUnit.AdUnitId);
        }

        private async void interstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
            // handle errors here
            var dialog = new ContentDialog
            {
                Title = "An Error",
                Content = e.ErrorMessage,
                PrimaryButtonText = "OK",
                IsPrimaryButtonEnabled = true
            };

            await dialog.ShowAsync();
        }

        private void interstitialAd_Completed(object sender, object e)
        {
            // raised when the user has watched the full video
            ViewedFullInterstitial = true;

        }

        private async void interstitialAd_Cancelled(object sender, object e)
        {
            // raised if the user interrupts the video
            //var dialog = new ContentDialog
            //{
            //    Title = "Ad Interrupted",
            //    Content = "You must watch the complete ad!",
            //    PrimaryButtonText = "OK",
            //    IsPrimaryButtonEnabled = true
            //};

            //await dialog.ShowAsync();

            //RequestAd();
        }

        private void interstitialAd_AdReady(object sender, object e)
        {
            //raised when an ad is ready to show
            // This is just for demoing - you should handle this differently in a production app
            if (_interstitialAd.State == InterstitialAdState.Ready)
            {
                _interstitialAd.Show();
            }
        }

        
    }
}
