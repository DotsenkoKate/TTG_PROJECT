using System;
using System.Collections.Generic;
using Model_staticDB;
using MainController;

namespace ttg_server_ver1
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost;Username=postgres;Password=passwordDB1;Database=ttg_static_1";

            Controller controller = new Controller(connectionString);

            Worktime t = new Worktime();
            t.Id = 1;
            t.Weekday = "Mon";
            t.StartTime = "07:00:00";
            t.FinishTime = "17:30:00";

            controller.UpdateWorktime(t);

            Console.ReadKey(true);
        }
    }
}
