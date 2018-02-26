using Agilitic.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilitic.Screens
{
    internal class HomeScreen
    {
        private static List<(string, Action)> _Options = new List<(string, Action)>
        {
            ("Voir les possibilités",() => ConsultScreen.Display()),
            ("Chercher une disponibilité ", () => SearchScreen.Display()),
            ("Quitter",() => { }),
        };

        internal static void Display(bool badchoice=false)
        {
            Console.Clear();
            Screen.DisplayHeader();
            PopulateMenu(badchoice);
        }

        private static void PopulateMenu(bool badchoice)
        {
            for(int i = 0; i < _Options.Count; i++)
            {
                Console.WriteLine($"\t{i + 1} - {_Options[i].Item1}");
            }

            Screen.AppendNewLine();
            Screen.DispaySeparator();
            if(badchoice)
            {
                Screen.DisplayColoredText(ConsoleColor.Red, "Votre choix est invalide.\n" +
                    "Veuillez selectionner une option disponible.\n");
            }
            Screen.DisplayText("Choix: ");
            string input = Screen.AwaitInput();
            if(int.TryParse(input,out int choice))
            {
                if(choice > _Options.Count || choice - 1 < 0)
                {
                    Display(true);
                }
                else
                {
                    (string,Action) menuitem = _Options[choice - 1];
                    menuitem.Item2();
                }
            }
            else
            {
                Display(true);
            }
        }
    }
}
