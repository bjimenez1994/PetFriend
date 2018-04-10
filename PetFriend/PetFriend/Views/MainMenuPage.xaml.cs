using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.LocalNotifications;


using Xamarin.Forms;
using System.Text;

namespace PetFriend.Views
{
    public partial class MainMenuPage : TabbedPage
    {

        DateTime startDate;

        public MainMenuPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#38ada9");
        }

        async void Init()
        {
            dateLabel.Text = DateTime.Now.ToString();
            int result;
            DateTime now;
            string relationship;
            while (true)
            {
                await Task.Delay(10000);
                now = DateTime.Now;
                result = DateTime.Compare(now, startDate);
                if (result < 0)
                    relationship = " is earlier than ";
                else if (result == 0)
                    relationship = " is the same time as ";
                else
                    relationship = " is later than ";

                dateLabel.Text = now.ToString() + relationship + startDate.ToString();

                CrossLocalNotifications.Current.Show("title", relationship);


            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";
                
            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            if (System.IO.File.Exists(curDir + "PetFriendInfo.dat"))
            {
                //use this area if the files need to be accessed when the app starts up
            }
            else//first time starting or files were deleted/removed/corrupted
            {//create the directory and base files used to store data
                Directory.CreateDirectory(curDir);

                FileStream x = File.Create(curDir + "PetFriendInfo.dat");//currently only used to check if the app has been opened before
                x.Close();
                x = File.Create(curDir + "Reminders.dat");  //stores a list of reminder names
                x.Close();
                x = File.Create(curDir + "Records.dat");    //stores a list of health record names
                x.Close();
                x = File.Create(curDir + "Profiles.dat");   //stores a list of pet names
                x.Close();

            }

            string[] Reminders = File.ReadAllLines(curDir + "Reminders.dat");//Reads data from Reminders list file
            Reminders_List.ItemsSource = Reminders; //displays the data read in the listview
            string[] Profiles = File.ReadAllLines(curDir + "Profiles.dat");
            Pet_List.ItemsSource = Profiles;
            string[] Records = File.ReadAllLines(curDir + "Records.dat");
            Health_List.ItemsSource = Records;

            if (Pet_List.ItemsSource == null)
            {
                Pet_List.IsVisible = false;
                pet_label.IsVisible = true;
            }
            else
            {
                Pet_List.IsVisible = true;
                pet_label.IsVisible = false;
            }

            if(Reminders_List.ItemsSource == null)
            {   Reminders_List.IsVisible = false;
                reminder_label.IsVisible = true;
            }
            else
            {
                Reminders_List.IsVisible = true;
                reminder_label.IsVisible = false;
            }

            if (Health_List.ItemsSource == null)
            {
                Health_List.IsVisible = false;
                health_label.IsVisible = true;
            }
            else
            {
                Health_List.IsVisible = true;
                health_label.IsVisible = false;
            }

        }

        async void PetSelection(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            string temp = e.Item.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp 
            };

            await Navigation.PushAsync(new PetViewPage(e.Item.ToString()));
        }

        private async void ReminderSelection(object sender, ItemTappedEventArgs e)//TODO FIX THE FIRST TWO COMMENTED LINES
        {
            //var remm = e.Item as Reminders;
            //await DisplayAlert("Reminder selected", "Reminder: " + remm.Title, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = e.Item.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp
            };

            await Navigation.PushAsync(new ReminderViewPage(e.Item.ToString()));
        }

        async void HealthSelection(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = e.Item.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp
            };

            await Navigation.PushAsync(new HealthViewPage(e.Item.ToString()));
        }

        void removeReminder(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";

            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            string[] Reminders = File.ReadAllLines(curDir + "Reminders.dat");//Reads data from Reminders list file

            File.Delete(curDir + "Reminders.dat");

            //TODO: ADD CHECK FOR DUPLICATE FILE NAMES==========================================================================================================================================
            StreamWriter appender = File.AppendText(curDir + "Reminders.dat");

            string[] RemindersNew = new string[Reminders.Length - 1];

            int j = 0;
            for(int i=0; i<Reminders.Length; i++)
            {
                if (Reminders[i] != (string)mi.CommandParameter)
                {
                    RemindersNew[j] = (string)mi.CommandParameter;
                    appender.Write(Reminders[i] + "\n");
                    j++;
                }
            }


            Reminders_List.ItemsSource = RemindersNew; //displays the data read in the listview

            appender.Flush();
            appender.Close();

            File.Delete(curDir + (string)mi.CommandParameter + ".dat");

        }

        void removeHealth(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";

            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            string[] Records = File.ReadAllLines(curDir + "Records.dat");//Reads data from Reminders list file

            File.Delete(curDir + "Records.dat");

            //TODO: ADD CHECK FOR DUPLICATE FILE NAMES==========================================================================================================================================
            StreamWriter appender = File.AppendText(curDir + "Records.dat");

            string[] HealthNew = new string[Records.Length - 1];

            int j = 0;
            for (int i = 0; i < Records.Length; i++)
            {
                if (Records[i] != (string)mi.CommandParameter)
                {
                    HealthNew[j] = (string)mi.CommandParameter;
                    appender.Write(Records[i] + "\n");
                    j++;
                }
            }

            Health_List.ItemsSource = HealthNew; //displays the data read in the listview

            appender.Flush();
            appender.Close();

            File.Delete(curDir + (string)mi.CommandParameter + ".dat");

        }

        void removeProfile(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);

            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";

            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            string[] Profiles = File.ReadAllLines(curDir + "Profiles.dat");//Reads data from Reminders list file

            File.Delete(curDir + "Profiles.dat");

            //TODO: ADD CHECK FOR DUPLICATE FILE NAMES==========================================================================================================================================
            StreamWriter appender = File.AppendText(curDir + "Profiles.dat");

            string[] ProfilesNew = new string[Profiles.Length - 1];

            int j = 0;
            for (int i = 0; i < Profiles.Length; i++)
            {
                if (Profiles[i] != (string)mi.CommandParameter)
                {
                    ProfilesNew[j] = (string)mi.CommandParameter;
                    appender.Write(Profiles[i] + "\n");
                    j++;
                }
            }


            Pet_List.ItemsSource = ProfilesNew; //displays the data read in the listview

            appender.Flush();
            appender.Close();

            File.Delete(curDir + (string)mi.CommandParameter + ".dat");

        }

        async void AddPet(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPetPage());
        }

        async void AddHealthRecord(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddHealthPage());
        }

        async void AddReminder(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReminderPage());
        }

        void DateClicked(object ender, EventArgs e)
        {
            var ben = DateTime.Now;

            startDate = datePicker.Date + timePicker.Time;
            dateLabel.Text = startDate.ToString();
            //CrossLocalNotifications.Current.Show("title", "test", 100, DateTime.Now.AddSeconds(10));
        }
    }
}
