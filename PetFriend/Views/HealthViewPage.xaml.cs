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
        }

        string temp;

        void Init()
        {


            /* getting data from selected pet */

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            var check = from s in conn.Table<LocalData>()
                        select s;
            temp = check.Last().tempname;
            conn.DropTable<LocalData>();
            conn.CreateTable<HealthData>();

            var output = conn.Query<HealthData>("select * from HealthData where Date=?", temp);

            vetVisit_entry.Text = output.Last().vetVisited;
            type_entry.Text = output.Last().TypeVisit;
            date_entry.Text = output.Last().Date;
            vaccinations_entry.Text = output.Last().Vaccinations;
            weight_entry.Text = output.Last().Weight;
            comments_entry.Text = output.Last().vetComments;

            conn.Close();

        }
    }
}
