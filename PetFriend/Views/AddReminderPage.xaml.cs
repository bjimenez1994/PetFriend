using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using PetFriend.Models;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class AddReminderPage : ContentPage
    {
        public AddReminderPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            priority_picker.Items.Add("Low");
            priority_picker.Items.Add("Normal");
            priority_picker.Items.Add("Important");
            priority_picker.Items.Add("Critical");
            //System.Diagnostics.Debug.WriteLine("TEST: " + datePicker.ToString());

        }

        async void Done(object ender, EventArgs e)
        {
            Reminders reminders = new Reminders()
            {
                Title = reminder_entry.Text,
                Description = description_entry.Text,
                Priority = priority_picker.SelectedItem.ToString(),
                Date = datePicker.Date + timePicker.Time
            };

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Reminders>();
            int rows = conn.Insert(reminders);
            conn.Close();

            if (rows > 0)
                await DisplayAlert("Success", "You have added a reminder", "Ok");
            else
                await DisplayAlert("Failure", "Your reminder was not added", "OK");

            await Navigation.PopToRootAsync();
        }

    
    }
}
