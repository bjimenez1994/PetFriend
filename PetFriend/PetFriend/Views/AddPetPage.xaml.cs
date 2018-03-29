using System;
using System.Collections.Generic;
using PetFriend.Models;


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

            await Navigation.PopToRootAsync();
        }

    }
}
