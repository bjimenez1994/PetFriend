using Xamarin.Forms;
using PetFriend.Views;
using System;


namespace PetFriend
{
    public partial class App : Application
    {
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
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
