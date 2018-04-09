using System;
using System.Collections.Generic;
using System.Linq;
using PetFriend.Models;
using System.IO;

using Xamarin.Forms;
using System.Text;

namespace PetFriend.Views
{
    public partial class PetViewPage : ContentPage
    {
        private string name;

        public PetViewPage()
        {
            InitializeComponent();
            Init();
        }

        public PetViewPage(string passedName)
        {
            name = passedName;
            InitializeComponent();
            Init();
        }


        void Init()
        {
            string curDir = null;

            if (Device.RuntimePlatform == Device.iOS)
            {
                curDir = "";//ADD 
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";

            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            string[] data = File.ReadAllLines(curDir + name + ".dat");
            name_entry.Text = data[0];
            type_picker.Text = data[1];
            gender_picker.Text = data[2];
            age_picker.Text = data[3];
            rfid_entry.Text = data[4];


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
