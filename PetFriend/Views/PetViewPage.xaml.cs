using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using PetFriend.Models;
using SQLite.Extensions;
using System.IO;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class PetViewPage : ContentPage
    {
        public PetViewPage()
        {
            InitializeComponent();
            Init();
        }

        byte[] image;
        int tempid;

        void Init()
        {
            /* filling out the pickers */
            string age;

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


            /* getting data from selected pet */

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            var check = from s in conn.Table<LocalData>()
                        select s;
            tempid = check.Last().tempname;
            conn.DropTable<LocalData>();

            var output = conn.Query<PetProfile>("select * from PetProfile where id=?", tempid);

            tempid = output.Last().id;
            name_entry.Text = output.Last().Name;
            gender_picker.SelectedItem = output.Last().Gender;
            type_picker.SelectedItem = output.Last().Type;
            age_picker.SelectedItem = output.Last().Age;
            rfid_entry.Text = output.Last().RFID;
            image_entry.Source = ImageSource.FromStream(() => new MemoryStream(output.Last().Image));
            image = output.Last().Image;


            conn.Close();
        }

        async void DoneEdit(object ender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Query<PetProfile>("select * from PetProfile where id=?", tempid);

            PetProfile petprofile = new PetProfile()
            {
                id = tempid, 
                Name = name_entry.Text,
                Gender = gender_picker.SelectedItem.ToString(),
                Type = type_picker.SelectedItem.ToString(),
                Age = age_picker.SelectedItem.ToString(),
                RFID = rfid_entry.Text,
                Image = image
            };

            conn.Update(petprofile);
            conn.Close();


            await DisplayAlert("Success", "Pet information saved", "Ok");

            await Navigation.PopToRootAsync();
        }


    }
}
