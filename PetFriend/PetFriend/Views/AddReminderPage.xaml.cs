using System;
using System.Collections.Generic;
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

        }

        async void Done(object ender, EventArgs e)
        {
            Reminders reminders = new Reminders()
            {
                Title = reminder_entry.Text,
                Description = description_entry.Text,
                Priority = priority_picker.SelectedItem.ToString()
            };
            
            await Navigation.PopToRootAsync();
        }

    
    }
}
