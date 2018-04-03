using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.Linq;
using System.IO;


using Xamarin.Forms;
using System.Text;

namespace PetFriend.Views
{
    public partial class MainMenuPage : TabbedPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#38ada9");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = "";//ADD 
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

        async void PetSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            string temp = e.SelectedItem.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp 
            };

            await Navigation.PushAsync(new PetViewPage(e.SelectedItem.ToString()));
        }

        async void ReminderSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = e.SelectedItem.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp
            };

            await Navigation.PushAsync(new ReminderViewPage(e.SelectedItem.ToString()));
        }

        async void HealthSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = e.SelectedItem.ToString();
            LocalData localdata = new LocalData()
            {
                tempname = temp
            };

            await Navigation.PushAsync(new HealthViewPage(e.SelectedItem.ToString()));
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


    }
}
