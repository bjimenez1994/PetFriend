using System;
using System.Collections.Generic;
using System.Linq;
using PetFriend.Models;
using System.IO;


using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class ReminderViewPage : ContentPage
    {
        private string name;

        public ReminderViewPage()
        {
            InitializeComponent();
            Init();
        }

        public ReminderViewPage(string passedName)
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
                curDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                curDir = "/storage/sdcard0/Android/data/petFriend/";
            }

            if (curDir == null) return;// if device isn't iOS or android, should never happen

            string[] data = File.ReadAllLines(curDir + name + ".dat");
            name_entry.Text = data[0];
            description_entry.Text = data[1];
            priority_picker.Text = data[2];


        }

    }
}
