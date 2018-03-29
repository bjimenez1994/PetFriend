using System;
using System.Collections.Generic;
using System.Linq;
using PetFriend.Models;

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

        string temp; //global declaration

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
            

        }
        /*
        async void DoneEdit(object ender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Query<PetProfile>("select * from PetProfile where Name=?", temp);
            PetProfile petprofile = new PetProfile()
            {
                Name = name_entry.Text,
                Gender = gender_picker.SelectedItem.ToString(),
                Type = type_picker.SelectedItem.ToString(),
                Age = age_picker.SelectedItem.ToString(),
                RFID = rfid_entry.Text
            };
            conn.Update(petprofile);
            conn.Close();


            await DisplayAlert("Success", "Pet information saved", "Ok");

            await Navigation.PopToRootAsync();
        }
*/

    }
}
