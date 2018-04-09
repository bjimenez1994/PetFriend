using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;


using Xamarin.Forms;
using System.Text;

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

            string name = name_entry.Text;
            string type = type_picker.SelectedItem.ToString();//TODO PICKER DEFAULT VALUE
            string Gender = gender_picker.SelectedItem.ToString();
            string Age = age_picker.SelectedItem.ToString();
            string RFID = rfid_entry.Text;

            if (name == null) name = "noName";//TODO MAKE SURE THERE'S A NAME
            if (type == null) type = "";
            if (Gender == null) Gender = "";
            if (Age == null) Age = "";
            if (RFID == null) RFID = "";

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

            //TODO: ADD CHECK FOR DUPLICATE FILE NAMES==========================================================================================================================================
            StreamWriter appender = File.AppendText(curDir + "Profiles.dat");//writes the new reminder to the main list of reminders
            appender.Write(name + "\n");
            appender.Flush();
            appender.Close();

            try
            {
                File.Delete(curDir + name + ".dat");
            }
            catch (FileNotFoundException fnf)
            {

            }

            var temp = File.Create(curDir + name + ".dat");
            temp.Close();
            StreamWriter newPet = File.AppendText(curDir + name + ".dat");
            newPet.Write(name + '\n' + type + '\n' + Gender + '\n' + Age + '\n' + RFID + '\n');
            newPet.Flush();
            newPet.Close();

            await Navigation.PopToRootAsync();
        }

        async void AddPicture(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not supported on your device", "Ok");
                return;
            }
            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile == null)
            {
                //await DisplayAlert("Error", "There was an error when trying to get your image", "Ok");
                return;
            }



            PetPic.Source = ImageSource.FromStream(() =>
            {
                var stream = selectedImageFile.GetStream();
                //selectedImageFile.Dispose();
                return stream;
            });
        }

    }
}
