using Agilitic.Models;
using Agilitic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilitic.Screens
{
    internal class ConsultScreen
    {
        private static int _Range = 5;
        private static int _PageIndex = 1;
        private static int _CurrentMinRange = 0;
        private static List<(string, Action)> _Options = new List<(string, Action)>
        {
            ("Page precedente",() => {
                _PageIndex--;
                _CurrentMinRange = _CurrentMinRange - _Range;
                if(_CurrentMinRange < 0)
                {
                    int count = DataAccessLayer.Appointments.Count;
                    _CurrentMinRange = count - 1 - _Range;
                    _PageIndex = count / _Range;
                }
                Display();
            }),
            ("Page suivante",() => {
                _PageIndex++;
                _CurrentMinRange = _CurrentMinRange + _Range;
                if(_CurrentMinRange >= DataAccessLayer.Appointments.Count)
                {
                    _CurrentMinRange = 0;
                    _PageIndex = 1;
                }
                Display();
            }),
            ("Retour au menu principal",() => HomeScreen.Display()),
        };

        internal static void Display(bool badchoice=false)
        {
            Console.Clear();
            Screen.DisplayHeader();
            Screen.PopulateList(DataAccessLayer.Appointments, _Range, _CurrentMinRange, _PageIndex);
            Screen.PopulateMenu(_Options,Display,badchoice);
        }
    }
}
