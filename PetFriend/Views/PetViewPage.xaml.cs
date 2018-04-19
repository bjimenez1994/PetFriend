using System;
using System.Collections.Generic;
using SQLite;
using System.Linq;
using PetFriend.Models;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class PetViewPage : ContentPage
    {
        byte[] image;
        public PetViewPage()
        {
            InitializeComponent();
            OnCall();
            Init();
        }

        int tempid;

        void OnCall()
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
            conn.Close();
        }

        void Init()
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
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

        async void EditImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not supported on your device", "Ok");
                return;
            }

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync();

            if (selectedImageFile == null)
            {
                //await DisplayAlert("Error", "There was an error when trying to get your image", "Ok");
                return;
            }


            image_entry.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
            var stream = selectedImageFile.GetStream();
            image = ConvertImage(stream);

        }

        public static byte[] ConvertImage(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        async void DoneEdit(object sender, EventArgs e)
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
