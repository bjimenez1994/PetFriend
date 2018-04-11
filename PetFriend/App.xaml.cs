using Xamarin.Forms;
using PetFriend.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using Plugin.LocalNotifications;
using SQLite;
using PetFriend.Models;

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

        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {

        }

        void checkReminders()
        {
            CrossLocalNotifications.Current.Show("title", "test");
            return;
        }




    }
}
