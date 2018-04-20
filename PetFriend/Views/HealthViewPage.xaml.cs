using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;
using System.Linq;

using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class HealthViewPage : ContentPage
    {
        public HealthViewPage()
        {
            InitializeComponent();
            Init();
        }

        int tempid;

        void Init()
        {


            /* getting data from selected pet */

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            var check = from s in conn.Table<LocalData>()
                        select s;
            tempid = check.Last().tempname;
            conn.DropTable<LocalData>();

            var output = conn.Query<HealthData>("select * from HealthData where id=?", tempid);

            tempid = output.Last().id;
            vetVisit_entry.Text = output.Last().vetVisited;
            type_entry.Text = output.Last().TypeVisit;
            date_entry.Text = output.Last().Date;
            weight_entry.Text = output.Last().Weight;
            vaccinations_entry.Text = output.Last().Vaccinations;
            comments_entry.Text = output.Last().vetComments;

            conn.Close();

        }

        async void DoneEdit(object ender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Query<HealthData>("select * from HealthData where id=?", tempid);

            HealthData healthdata = new HealthData()
            {
                id = tempid,
                vetVisited = vetVisit_entry.Text,
                TypeVisit = type_entry.Text,
                Date = date_entry.Text,
                Weight = weight_entry.Text,
                Vaccinations = vaccinations_entry.Text,
                vetComments = comments_entry.Text
            };

            conn.Update(healthdata);
            conn.Close();


            await DisplayAlert("Success", "Vet information saved", "Ok");

            await Navigation.PopToRootAsync();
        }
    }
}
