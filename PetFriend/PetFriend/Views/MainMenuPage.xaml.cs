using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.Linq;


using Xamarin.Forms;

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

            await Navigation.PushAsync(new PetViewPage());
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

            await Navigation.PushAsync(new ReminderViewPage());
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

            await Navigation.PushAsync(new HealthViewPage());
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
