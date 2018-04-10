using System;
namespace PetFriend.Models
{
    public class Reminders
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime Date { get; set; }
        public bool isActivated { get; set; }


    }
}
