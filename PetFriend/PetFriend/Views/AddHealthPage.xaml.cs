using System;
using System.Collections.Generic;
using PetFriend.Models;

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
            
            await Navigation.PopToRootAsync();
        }
    }
}
