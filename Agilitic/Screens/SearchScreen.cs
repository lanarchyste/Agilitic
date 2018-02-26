using Agilitic.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Agilitic.Models;

namespace Agilitic.Screens
{
    internal class SearchScreen
    {
        private static List<Appointment> _SearchResult = new List<Appointment>();
        private static List<(string, Action)> _Options = new List<(string, Action)>
        {
            ("Chercher",() => {
                ResultScreen.WithData(_SearchResult);
                ResultScreen.Display();
            }),
            ("Annuler",() => Display()),
            ("Retour au menu principal",() => HomeScreen.Display()),
        };

        private static DateTime PopulateDateSearch()
        {
            Screen.DisplayText("Date souhaitee: ");
            string input = Screen.AwaitInput();
            if (DateTime.TryParse(input, out DateTime res))
            {
                return res;
            }
            else
            {
                Screen.DisplayColoredText(ConsoleColor.Red, "Votre entree ne correspond pas a une date " +
                    "valide (yyyy-mm-dd)\nVeuillez reessayer.\n");
                return PopulateDateSearch();
            }
        }

        private static bool PopulatePreferenceSearch()
        {
            Screen.DisplayText("1 - Matin\t2 - Apres-midi\nChoix: ");
            string input = Screen.AwaitInput();
            if(int.TryParse(input,out int res))
            {
                if(res == 1)
                {
                    return true;
                }
                else if(res == 2)
                {
                    return false;
                }
                else
                {
                    Screen.DisplayColoredText(ConsoleColor.Red, "Le choix n'est pas valide! (1 ou 2)\n");
                    return PopulatePreferenceSearch();
                }
            }
            else
            {
                Screen.DisplayColoredText(ConsoleColor.Red, "Le choix n'est pas valide! (1 ou 2)\n");
                return PopulatePreferenceSearch();
            }
        }

        private static List<Appointment> Search(DateTime input,bool ismorning)
        {
            Appointment apt = DataAccessLayer.Appointments
                .FirstOrDefault(x => x.StartTime <= input && x.EndTime >= input);
            if(apt == null)
            {
                List<Appointment> clone = DataAccessLayer.Appointments;
                List<Appointment> res = new List<Appointment>();
                for(int i = 0; i < 3;i++)
                {
                    long closest = long.MaxValue;
                    int closestindex = -1;
                    for(int j = 0; j < clone.Count; j++)
                    {
                        Appointment a = clone[j];
                        if((ismorning && a.StartTime.Hour <= 12) || (!ismorning && a.StartTime.Hour > 12))
                        {
                            long ticks = Math.Abs((a.StartTime - input).Ticks);
                            if (ticks < closest)
                            {
                                closestindex = j;
                                closest = ticks;
                            }
                        }
                    }

                    res.Add(clone[closestindex]);
                    clone.RemoveAt(closestindex);
                }

                return res;
            }
            else
            {
                return new List<Appointment>(new[]{ apt });
            }
        }

        internal static void Display(bool badchoice=false)
        {
            Console.Clear();
            Screen.DisplayHeader();
            DateTime wished = PopulateDateSearch();
            bool ismorning = PopulatePreferenceSearch();
            _SearchResult = Search(wished,ismorning);
            Screen.PopulateMenu(_Options, Display, badchoice);
        }
    }
}
