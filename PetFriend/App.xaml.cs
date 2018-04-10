using Xamarin.Forms;
using PetFriend.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using Plugin.LocalNotifications;

namespace PetFriend
{
    public partial class App : Application
    {
        
        bool run = true;
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
            while(run == true)
            {
                var task = Task.Delay(60000);
                task.Wait();
                checkReminders();
            }


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
