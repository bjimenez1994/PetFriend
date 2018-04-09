using Xamarin.Forms;
using PetFriend.Views;
using System;
using System.Linq;
using System.Diagnostics;
using Plugin.LocalNotifications;

namespace PetFriend
{
    public partial class App : Application
    {
        public static Stopwatch stopWatch = new Stopwatch();
        public const int defaultTimespan = 10;
        public static string DatabaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainMenuPage())
            {
                BarBackgroundColor = Color.FromHex("#38ada9")
            };
        }

        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainMenuPage())
            {
                BarBackgroundColor = Color.FromHex("#38ada9")
            };
            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
            if (!stopWatch.IsRunning)
            {
                stopWatch.Start();
            }
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                // Logic for logging out if the device is inactive for a period of time.

                if (stopWatch.IsRunning && stopWatch.Elapsed.Seconds >= defaultTimespan)
                {
                    //prepare to perform your data pull here as we have hit the 1 minute mark   

                    // Perform your long running operations here.



                    stopWatch.Restart();
                }

                // Always return true as to keep our device timer running.
                return true;
            });
        }

        protected override void OnSleep()
        {
            CrossLocalNotifications.Current.Show("title", "test", 100, DateTime.Now.AddSeconds(10));

            stopWatch.Reset();
        }

        protected override void OnResume()
        {
            stopWatch.Start();
        }
    }
}
