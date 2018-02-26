using Agilitic.Models;
using Agilitic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilitic.Screens
{
    internal class ResultScreen
    {
        private static List<Appointment> _Appointments;
        private static List<(string, Action)> _Options = new List<(string, Action)>
        {
            ("Chercher a nouveau",() => SearchScreen.Display()),
            ("Retour au menu principal",() => HomeScreen.Display()),
        };

        internal static void WithData(List<Appointment> apts)
        {
            _Appointments = apts;
        }

        internal static void Display(bool badchoice=false)
        {
            Console.Clear();
            Screen.DisplayHeader();
            Screen.PopulateList(_Appointments, 4, 0, 1);
            Screen.PopulateMenu(_Options, Display, badchoice);
        }
    }
}
