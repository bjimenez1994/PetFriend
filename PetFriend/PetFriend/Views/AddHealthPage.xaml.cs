using System;
using System.Collections.Generic;
using PetFriend.Models;
using System.Text;
using System.IO;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class AddHealthPage : ContentPage
    {
        public AddHealthPage()
        {
            InitializeComponent();
        }

        async void Done(object ender, EventArgs e)
        {
            //HealthData healthdata = new HealthData()
            //{
            string vetVisited = vetVisit_entry.Text;
            string vetComments = comments_entry.Text;
            string TypeVisit = type_entry.Text;
            string Date = date_entry.Text;
            string Vaccinations = vaccinations_entry.Text;
            string Weight = weight_entry.Text;
            //};

            if (vetVisited == null) vetVisited = "noVet";//TODO make sure there's a vet
            if (vetComments == null) vetComments = "";
            if (TypeVisit == null) TypeVisit = "";
            if (Date == null) Date = "noDate";//TODO make sure there's a date
            if (Vaccinations == null) Vaccinations = "";
            if (Weight == null) Weight = "";

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

            //TODO: ADD CHECK FOR DUPLICATE FILE NAMES==========================================================================================================================================
            System.IO.StreamWriter appender = System.IO.File.AppendText(curDir + "Records.dat");//writes the new reminder to the main list of reminders
            appender.Write(vetVisited + "-" + Date + "\n");
            appender.Flush();
            appender.Close();

            try
            {
                File.Delete(curDir + vetVisited + "-" + Date + ".dat");
            }
            catch (FileNotFoundException fnf)
            {
                
            }

            var temp = File.Create(curDir + vetVisited + "-" + Date + ".dat");
            temp.Close();
            StreamWriter newHealth = File.AppendText(curDir + vetVisited + "-" + Date + ".dat");
            newHealth.Write(vetVisited + "\n" + vetComments + "\n" + TypeVisit + "\n" + Date + "\n" + Vaccinations + "\n" + Weight);
            newHealth.Close();

            await Navigation.PopToRootAsync();
        }
    }
}
