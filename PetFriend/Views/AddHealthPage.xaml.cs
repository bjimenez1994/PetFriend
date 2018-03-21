using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class AddHealthPage : ContentPage
    {
        public AddHealthPage()
        {
            InitializeComponent();
        }

        async void Done(object ender, EventArgs e)
        {
            HealthData healthdata = new HealthData()
            {
                vetVisited = vetVisit_entry.Text,
                vetComments = comments_entry.Text,
                TypeVisit = type_entry.Text,
                Date = date_entry.Text,
                Vaccinations = vaccinations_entry.Text,
                Weight = weight_entry.Text
            };

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<HealthData>();
            int rows = conn.Insert(healthdata);
            conn.Close();

            if (rows > 0)
                await DisplayAlert("Success", "You have added a health record", "Ok");
            else
                await DisplayAlert("Failure", "Your health record was not added", "OK");

            await Navigation.PopToRootAsync();
        }
    }
}
