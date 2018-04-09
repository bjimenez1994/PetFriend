using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.LocalNotifications;


using Xamarin.Forms;

namespace PetFriend.Views
{
    public partial class MainMenuPage : TabbedPage
    {
        DateTime startDate;

        public MainMenuPage()
        {
            InitializeComponent();
            Init();
            BarBackgroundColor = Color.FromHex("#38ada9");
        }

        async void Init()
        {
            dateLabel.Text = DateTime.Now.ToString();
            int result;
            DateTime now;
            string relationship;
            while (true)
            {
                await Task.Delay(10000);
                now = DateTime.Now;
                result = DateTime.Compare(now, startDate);
                if (result < 0)
                    relationship = " is earlier than ";
                else if (result == 0)
                    relationship = " is the same time as ";
                else
                    relationship = " is later than ";

                dateLabel.Text = now.ToString() + relationship + startDate.ToString();

                  //  CrossLocalNotifications.Current.Show("title", relationship);


            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            int i;
            List<string> remtemp = new List<string>();
            List<string> proftemp = new List<string>();
            List<string> healthtemp = new List<string>();

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<PetProfile>();
            proftemp = conn.Table<PetProfile>().ToList().Select(p => p.Name).ToList();
            conn.CreateTable<Reminders>();
            remtemp = conn.Table<Reminders>().ToList().Select(r => r.Title).ToList();
            conn.CreateTable<HealthData>();
            healthtemp = conn.Table<HealthData>().ToList().Select(r => r.Date).ToList();
            conn.Close();


            if (remtemp != null)
            {
                var remm = new List<Reminders>
                {
                    //new Reminders{Title = remtemp[0]}
                };
                for (i = 0; i < remtemp.Count(); i++)
                {
                    remm.Add(new Reminders { Title = remtemp[i] });
                }
                Reminders_List.ItemsSource = remm;
            }
            if (healthtemp != null)
            {
                var heal = new List<HealthData>
                {
                    //new HealthData{Date = "hello"}
                };
                for (i = 0; i < healthtemp.Count(); i++)
                {
                    heal.Add(new HealthData { Date = healthtemp[i] });
                }
                Health_List.ItemsSource = heal;
            }
            if (proftemp != null)
            {
                var prof = new List<PetProfile>
                {
                    //new PetProfile{Name = "hello"}
                };
                for (i = 0; i < proftemp.Count(); i++)
                {
                    prof.Add(new PetProfile { Name = proftemp[i] });
                }
                Pet_List.ItemsSource = prof;
            }
        



            if(Pet_List.ItemsSource == null)
            {
                Pet_List.IsVisible = false;
                pet_label.IsVisible = true;
            }
            else
            {
                Pet_List.IsVisible = true;
                pet_label.IsVisible = false;
            }

            if(Reminders_List.ItemsSource == null)
            {   Reminders_List.IsVisible = false;
                reminder_label.IsVisible = true;
            }
            else
            {
                Reminders_List.IsVisible = true;
                reminder_label.IsVisible = false;
            }

            if (Health_List.ItemsSource == null)
            {
                Health_List.IsVisible = false;
                health_label.IsVisible = true;
            }
            else
            {
                Health_List.IsVisible = true;
                health_label.IsVisible = false;
            }

        }

        async void PetSelection(object sender, ItemTappedEventArgs e)
        {
            var prof = e.Item as PetProfile;
            await DisplayAlert("Pet selected", "Pet: " + prof.Name, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = prof.Name;
            LocalData localdata = new LocalData()
            {
                tempname = temp 
            };
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            conn.Insert(localdata);
            conn.Close();

            await Navigation.PushAsync(new PetViewPage());
        }

        private async void ReminderSelection(object sender, ItemTappedEventArgs e)
        {

            var remm = e.Item as Reminders;
            await DisplayAlert("Reminder selected", "Reminder: " + remm.Title, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = remm.Title;



            LocalData localdata = new LocalData()
            {
                tempname = temp
            };
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            conn.Insert(localdata);
            conn.Close();

            await Navigation.PushAsync(new ReminderViewPage());


        }

        async void HealthSelection(object sender, ItemTappedEventArgs e)
        {
            var heal = e.Item as HealthData;
            await DisplayAlert("Vet date selected", "Date: " + heal.Date, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            string temp = heal.Date;
            LocalData localdata = new LocalData()
            {
                tempname = temp
            };
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<LocalData>();
            conn.Insert(localdata);
            conn.Close();

            await Navigation.PushAsync(new HealthViewPage());
        }

        async void AddPet(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPetPage());
        }

        async void AddHealthRecord(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddHealthPage());
        }

        async void AddReminder(object ender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReminderPage());
        }

        void DateClicked(object ender, EventArgs e)
        {
            var ben = DateTime.Now;

            startDate = datePicker.Date + timePicker.Time;
            dateLabel.Text = startDate.ToString();
            CrossLocalNotifications.Current.Show("title", "test", 100, DateTime.Now.AddSeconds(10));
        }


    }
}
