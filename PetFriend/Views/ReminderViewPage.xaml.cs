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

        string temp;

        void Init()
        {
            /* filling out the pickers */
            //string age;
            /*
            for (int i = 0; i < 100; i++)
            {
                age = i.ToString();
                age_picker.Items.Add(age);
            }

            type_picker.Items.Add("Dog");
            type_picker.Items.Add("Cat");
            type_picker.Items.Add("Horse");
            type_picker.Items.Add("Bird");
            type_picker.Items.Add("Reptile");

            gender_picker.Items.Add("Male");
            gender_picker.Items.Add("Female");
            */
            /**/

            /* getting data from selected pet */

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            var check = from s in conn.Table<LocalData>()
                        select s;
            temp = check.Last().tempname;
            conn.DropTable<LocalData>();
            conn.CreateTable<PetProfile>();

            var output = conn.Query<Reminders>("select * from Reminders where Title=?", temp);

            name_entry.Text = output.Last().Title;
            description_entry.Text = output.Last().Description;
            priority_picker.Text = output.Last().Priority;

            conn.Close();

        }

    }
}
