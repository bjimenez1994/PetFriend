using System;
using System.Collections.Generic;
using PetFriend.Models;
using SQLite;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Plugin.LocalNotifications;
using System.IO;
using ImageCircle.Forms.Plugin;


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
            List<int> hid = new List<int>();
            List<int> rid = new List<int>();
            List<int> pid = new List<int>();

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<PetProfile>();
            conn.CreateTable<Reminders>();
            conn.CreateTable<HealthData>();

            proftemp = conn.Table<PetProfile>().ToList().Select(p => p.Name).ToList();
            remtemp = conn.Table<Reminders>().ToList().Select(r => r.Title).ToList();
            healthtemp = conn.Table<HealthData>().ToList().Select(r => r.Date).ToList();
            pid = conn.Table<PetProfile>().ToList().Select(p => p.id).ToList();
            rid = conn.Table<Reminders>().ToList().Select(r => r.id).ToList();
            hid = conn.Table<HealthData>().ToList().Select(r => r.id).ToList();


            if (remtemp != null)
            {
                var remm = new List<Reminders>
                {
                    //new Reminders{Title = remtemp[0]}
                };
                for (i = 0; i < remtemp.Count(); i++)
                {
                    remm.Add(new Reminders { Title = remtemp[i], id = rid[i] });
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
                    heal.Add(new HealthData { Date = healthtemp[i], id = hid[i] });
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
                    if (i == 0)
                    {
                        var temp = conn.Query<PetProfile>("select * from PetProfile where id=?", pid[i]);
                        profilePic.Source = ImageSource.FromStream(() => new MemoryStream(temp.Last().Image));
                        profileLabel.Text = "The coolest " + temp.Last().Type + " there ever was!";
                        nameLabel.Text = temp.Last().Name;
                    }
                    prof.Add(new PetProfile { Name = proftemp[i], id = pid[i] });
                }
                Pet_List.ItemsSource = prof;
            }
            conn.Close();

        }

        async void PetSelection(object sender, ItemTappedEventArgs e)
        {
            var prof = e.Item as PetProfile;
            //await DisplayAlert("Pet selected", "Pet: " + prof.Name, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            int temp = prof.id;
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            var output = conn.Query<PetProfile>("select * from PetProfile where id=?", temp);
            profilePic.Source = ImageSource.FromStream(() => new MemoryStream(output.Last().Image));
            profileLabel.Text = "The coolest " + output.Last().Type + " there ever was!";
            nameLabel.Text = output.Last().Name;

            ((ListView)sender).SelectedItem = null; // de-select the row

        }

        private async void ReminderSelection(object sender, ItemTappedEventArgs e)
        {

            var remm = e.Item as Reminders;
            await DisplayAlert("Reminder selected", "Reminder: " + remm.Title, "OK");

            if (e.Item == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }
            int temp = remm.id;



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
            int temp = heal.id;
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

        async void AddPet(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPetPage());
        }

        async void AddHealthRecord(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddHealthPage());
        }

        async void AddReminder(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReminderPage());
        }

        void DateClicked(object sender, EventArgs e)
        {
            var ben = DateTime.Now;

            startDate = datePicker.Date + timePicker.Time;
            dateLabel.Text = startDate.ToString();
            CrossLocalNotifications.Current.Show("title", "test");


        }

        void DeletePet(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            int i = Int32.Parse(mi.CommandParameter.ToString());
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Delete<PetProfile>(i);

            OnAppearing();
        }

        void DeleteReminder(object sender, EventArgs e)
        {
            
            var mi = ((MenuItem)sender);
            int i = Int32.Parse(mi.CommandParameter.ToString());
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Delete<Reminders>(i);
           
            OnAppearing();
        }

        void DeleteHealth(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            int i = Int32.Parse(mi.CommandParameter.ToString());
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.Delete<HealthData>(i);
            OnAppearing();
        }

        async void EditPet(object sender, EventArgs e)
        {

            var mi = ((MenuItem)sender);
            int i = Int32.Parse(mi.CommandParameter.ToString());
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);

            LocalData localdata = new LocalData()
            {
                tempname = i 
            };
            conn.CreateTable<LocalData>();
            conn.Insert(localdata);
            conn.Close();

            await Navigation.PushAsync(new PetViewPage());

        }

        void test()
        {
            int i, result;
            List<string> remtemp = new List<string>();
            List<DateTime> temp = new List<DateTime>();
            List<int> tempid = new List<int>();

            DateTime now;
            now = DateTime.Now;

            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            remtemp = conn.Table<Reminders>().ToList().Select(r => r.Title).ToList();
            temp = conn.Table<Reminders>().ToList().Select(n => n.Date).ToList();
            tempid = conn.Table<Reminders>().ToList().Select(n => n.id).ToList();
            if (tempid != null)
            {
                for (i = 0; i < remtemp.Count(); i++)
                {
                    result = DateTime.Compare(now, temp[i]);
                    if (result < 0)
                    {

                    }
                    else
                    {
                        CrossLocalNotifications.Current.Show("Reminder", "It's time to " + remtemp[i] + ".");
                        var output = conn.Query<Reminders>("select * from Reminders where id=?", tempid[i+1]);

                        Reminders reminders = new Reminders()
                        {
                            id = output.Last().id,
                            Title = output.Last().Title,
                            Description = output.Last().Description,
                            Priority = output.Last().Priority,
                            Date = output.Last().Date.Date + output.Last().Date.TimeOfDay,
                            isActivated = false
                        };

                        conn.Update(reminders);
                    }

                }
            }
            conn.Close();

        }
    }
}
