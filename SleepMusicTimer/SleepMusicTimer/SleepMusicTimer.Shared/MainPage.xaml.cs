using SleepMusicTimer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SleepMusicTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public DispatcherTimer Timer { get; set; }

        public TimeSpan TimeTimer { get; set; }


        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            this.DataContext = new MainContext();

            AdjustWindowsPhoneLayout();


        }

        private void AdjustWindowsPhoneLayout()
        {
#if WINDOWS_PHONE_APP

            this.appBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;

            var statusBar = StatusBar.GetForCurrentView();

            statusBar.BackgroundColor = Colors.Teal;
            statusBar.BackgroundOpacity = 1;
            statusBar.ForegroundColor = Colors.White;

            statusBar.ProgressIndicator.Text = "Sleeping Music Timer";
            statusBar.ProgressIndicator.ProgressValue = 0;
            statusBar.ForegroundColor = Colors.White;
            statusBar.ProgressIndicator.ShowAsync();

#endif
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void bdMinutes_Tapped(object sender, TappedRoutedEventArgs e)
        {
            cbMinutes.IsDropDownOpen = true;            
        }

        private void bdHours_Tapped(object sender, TappedRoutedEventArgs e)
        {
            cbHours.IsDropDownOpen = true;      
        }

        private void cbHours_DropDownClosed(object sender, object e)
        {
            StopTimer();
        }

        private void cbMinutes_DropDownClosed(object sender, object e)
        {
            StopTimer();
        }

        private void StopTimer()
        {

            TimeTimer = TimeSpan.Zero;

            if (Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }

            txtTimer.Text = "Not running";

            KeepScreenActive(false);

            

        }

        private void KeepScreenActive(bool p)
        {
            try
            {
                Windows.System.Display.DisplayRequest KeepScreenOnRequest = new Windows.System.Display.DisplayRequest();

                if (p)
                {
                    KeepScreenOnRequest.RequestActive();
                }
                else
                {
                    KeepScreenOnRequest.RequestRelease();
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void StartTimer()
        {

            StopTimer();

            if (cbHours.SelectedItem != null)
            {
                var hours = (int)cbHours.SelectedValue;

                TimeTimer = TimeTimer.Add(TimeSpan.FromHours(hours));
            }

            if (cbMinutes.SelectedItem != null)
            {
                var minutes = (int)cbMinutes.SelectedValue;

                TimeTimer = TimeTimer.Add(TimeSpan.FromMinutes(minutes));
            }

            if (TimeTimer.TotalSeconds > 0)
            {
                Timer = new DispatcherTimer();
                Timer.Interval = TimeSpan.FromSeconds(1);
                Timer.Tick += Timer_Tick;
                Timer.Start();

                KeepScreenActive(true);
            }

            


        }

        void Timer_Tick(object sender, object e)
        {
            TimeTimer = TimeTimer.Subtract(TimeSpan.FromSeconds(1));

            txtTimer.Text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            TimeTimer.Hours,
                            TimeTimer.Minutes,
                            TimeTimer.Seconds);

            if (TimeTimer <= TimeSpan.Zero)
            {
                StopTimer();
                StopMusic();
            }


        }

        private void StopMusic()
        {

            mdMedia.Play();

            
        }

        private void bdStartTimer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StartTimer();
        }

        
    }


}

