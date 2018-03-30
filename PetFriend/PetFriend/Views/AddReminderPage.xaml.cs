using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //Reminders reminders = new Reminders()
            //{
              string Title = reminder_entry.Text;
              string Description = description_entry.Text;
              string Priority = priority_picker.SelectedItem.ToString();
            //};

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
            System.IO.StreamWriter appender = System.IO.File.AppendText(curDir + "Reminders.dat");//writes the new reminder to the main list of reminders
            appender.Write(Title);
            appender.Flush();
            appender.Close();

            System.IO.FileStream newReminder = System.IO.File.Create(curDir + Title + ".dat");
            Byte[] toWrite = Encoding.ASCII.GetBytes(Title + "\n" + Description + "\n" + Priority + "\n");
            newReminder.Write(toWrite, 0, 0);
            newReminder.Close();


            await Navigation.PopToRootAsync();
        }

    
    }
}
