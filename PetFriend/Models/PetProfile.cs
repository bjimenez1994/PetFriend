using System;
using SQLite;
using System.Linq;
namespace PetFriend.Models
{
    public class PetProfile
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement] 
        public string Name { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string RFID { get; set; }
        //public string Image { get; set; }


    }
}
