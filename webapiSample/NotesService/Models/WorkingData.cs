using System;
using System.Collections.Generic;

namespace NotesServiceApp.Models
{
    class WorkingData
    {
        public List<DateTime> dates { get; set; }
        public List<UsersData> data { get; set; }

        public WorkingData()
        {
            dates = new List<DateTime>();
            data = new List<UsersData>();
        }
    }

    internal class UsersData
    {
        public string userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<Dates> dates { get; set; }

        public UsersData()
        {
            dates = new List<Dates>();
        }
    }

    class Dates
    {
        public DateTime workDate { get; set; }
        public int workTime { get; set; }
    }

    public static class SampleInit
    {
        public static void Init()
        {
            var date1 = DateTime.Now;
            var date2 = DateTime.Now.AddDays(1);
            var date3 = DateTime.Now.AddDays(2);

            var data = new WorkingData();
            data.dates.Add(date1);
            data.dates.Add(date2);
            data.dates.Add(date3);

            var userData1 = new UsersData();
            userData1.firstName = "star";
            userData1.lastName = "monkey";
            userData1.userId = "";
            userData1.dates = new List<Dates>();
            userData1.dates.Add(new Dates() { workDate = date1, workTime = 2 });
            userData1.dates.Add(new Dates() { workDate = date2, workTime = 4 });
            userData1.dates.Add(new Dates() { workDate = date3, workTime = 6 });

            var userData2 = new UsersData();
            userData2.firstName = "star";
            userData2.lastName = "monkey";
            userData2.userId = "";
            userData2.dates = new List<Dates>();
            userData2.dates.Add(new Dates() { workDate = date1, workTime = 1 });
            userData2.dates.Add(new Dates() { workDate = date2, workTime = 3 });
            userData2.dates.Add(new Dates() { workDate = date3, workTime = 5 });
        }

    }
}