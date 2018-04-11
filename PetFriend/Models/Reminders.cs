using System;
using SQLite;
namespace PetFriend.Models
{
    public class Reminders
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Date { get; set; }
        public bool isActivated { get; set; }


    }
}
