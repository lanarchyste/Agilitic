using System;
using System.Collections.Generic;
using System.Text;
using Agilitic.Utils;
using Agilitic.Models;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Agilitic
{
    internal class DataAccessLayer
    {
        private static List<Appointment> _Appointments = new List<Appointment>();

        internal static async Task Initialize()
        {
            string[] paths = Directory.GetFiles("Data/");
            foreach (string path in paths)
            {
                try
                {
                    string json = await File.ReadAllTextAsync(path);
                    Appointment[] aptms = JSON.Deserialize<Appointment[]>(json);
                    aptms.ToList().ForEach(x => _Appointments.Add(x));
                    _Appointments.Sort();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        internal static List<Appointment> Appointments
        {
            get => _Appointments;
        }
    }
}
