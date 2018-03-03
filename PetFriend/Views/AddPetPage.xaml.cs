using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;


using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class AddPetPage : ContentPage
    {
        public AddPetPage()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
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

        }

        async void Done(object ender, EventArgs e)
        {
            PetProfile petprofile = new PetProfile()
            {
                Name = name_entry.Text,
                Type = type_picker.SelectedItem.ToString(),
                Gender = gender_picker.SelectedItem.ToString(),
                Age = age_picker.SelectedItem.ToString(),
                RFID = rfid_entry.Text
            };

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<PetProfile>();
            int rows = conn.Insert(petprofile);
            conn.Close();

            if (rows > 0)
                await DisplayAlert("Success", "You have added a pet", "Ok");
            else
                await DisplayAlert("Failure", "Your pet was not added", "OK");

            await Navigation.PopToRootAsync();
        }

    }
}
