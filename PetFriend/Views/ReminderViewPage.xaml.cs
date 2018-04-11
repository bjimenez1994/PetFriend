using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using PetFriend.Models;
using SQLite.Extensions;


using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class ReminderViewPage : ContentPage
    {
        public ReminderViewPage()
        {
            InitializeComponent();
            Init();
        }

        int tempid;

        void Init()
        {
            priority_picker.Items.Add("Low");
            priority_picker.Items.Add("Normal");
            priority_picker.Items.Add("Important");
            priority_picker.Items.Add("Critical");
            status_picker.Items.Add("True");
            status_picker.Items.Add("False");

            /* getting data from selected reminder */

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            var check = from s in conn.Table<LocalData>()
                        select s;
            tempid = check.Last().tempname;
            conn.DropTable<LocalData>();

            var output = conn.Query<Reminders>("select * from Reminders where id=?", tempid);

            tempid = output.Last().id;
            name_entry.Text = output.Last().Title;
            description_entry.Text = output.Last().Description;
            priority_picker.SelectedItem = output.Last().Priority;
            status_picker.SelectedItem = output.Last().isActivated.ToString();
            datePicker.Date = output.Last().Date.Date;
            timePicker.Time = output.Last().Date.TimeOfDay;


            conn.Close();


        }

        async void DoneEdit(object ender, EventArgs e)
        {
            string tobool = status_picker.SelectedItem.ToString();
            bool val = Boolean.Parse(tobool);
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Query<Reminders>("select * from Reminders where id=?", tempid);

            Reminders reminders = new Reminders()
            {
                id = tempid,
                Title = name_entry.Text,
                Description = description_entry.Text,
                Priority = priority_picker.SelectedItem.ToString(),
                Date = datePicker.Date + timePicker.Time,
                isActivated = val
            };

            conn.Update(reminders);
            conn.Close();


            await DisplayAlert("Success", "Pet reminder saved", "Ok");

            await Navigation.PopToRootAsync();
        }

    }
}
