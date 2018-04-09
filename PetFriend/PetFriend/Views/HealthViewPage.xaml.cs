using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.Linq;
using System.IO;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class HealthViewPage : ContentPage
    {
        public string name;

        public HealthViewPage()
        {
            InitializeComponent();
            Init();
        }

        public HealthViewPage(string passedName)
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
            vetVisit_entry.Text = data[0];
            type_entry.Text = data[1];
            date_entry.Text = data[2];
            weight_entry.Text = data[3];
            vaccinations_entry.Text = data[4];
            comments_entry.Text = data[5];

            /* getting data from selected pet */

        }
    }
}
