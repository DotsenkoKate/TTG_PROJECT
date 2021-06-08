using System;
using System.Collections.Generic;
using System.Text;

namespace Model_staticDB
{
     public class Owner
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
    public class Way
    {
        public int Number { get; set; }
        public int Price { get; set; }
        public int OwnerId { get; set; }
    }
    public class Waystation
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Place { get; set; }
    }
    public class Auto
    {
        public int Id { get; set; }
        public String Number { get; set; }
        public String Brand { get; set; }
        public String Model { get; set; }
        public String Status { get; set; }
        public String ReleaseDate { get; set; }
        public int Capacity { get; set; }
        public int DriverId { get; set; }
        public int WayNumber { get; set; }
    }
    public class Driver
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Birthday { get; set; }
        public String Passport { get; set; }
        public String Place { get; set; }
        public String Phonenumber { get; set; }

    }
    public class Worktime
    {
        public int Id { get; set; }
        public String Weekday { get; set; }
        public String StartTime { get; set; }
        public String FinishTime { get; set; }
    }
}
