using System;
using SQLite;
namespace PetFriend.Models
{
    public class HealthData
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string vetVisited { get; set; }
        public string Weight { get; set; }
        public string vetComments { get; set; }
        public string Vaccinations { get; set; }
        public string TypeVisit { get; set; }
        public string Date { get; set; }

    }
}
