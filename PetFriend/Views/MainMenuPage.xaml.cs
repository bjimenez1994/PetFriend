using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;
using System.Linq;


using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class MainMenuPage : TabbedPage
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<PetProfile>();
            Pet_List.ItemsSource = conn.Table<PetProfile>().ToList().Select(p => p.Name).ToList();
            conn.Close();



            if(Pet_List.ItemsSource == null)
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
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            conn.Insert(localdata);
            conn.Close();

            await Navigation.PushAsync(new PetViewPage());
        }

        async void AddPet(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPetPage());
        }

        async void AddReminder(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReminderPage());
        }


    }
}
