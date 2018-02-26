using Agilitic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilitic.Utils
{
    internal abstract class Screen
    {
        internal static void DispaySeparator()
        {
            string line = new string('-', 100);
            Console.Write($"{line}\n");
        }

        internal static void DisplayColoredText(ConsoleColor col,string text)
        {
            Console.ForegroundColor = col;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal static void DisplayText(string text)
        {
            Console.Write(text);
        }

        internal static void AppendNewLine()
        {
            Console.WriteLine();
        }

        internal static void DisplayHeader()
        {
            DisplayColoredText(ConsoleColor.DarkCyan, "[A]");
            DisplayColoredText(ConsoleColor.Cyan, "agilitic\n");
            DisplayColoredText(ConsoleColor.DarkCyan, "Solutions logicielles innovantes\n");
            AppendNewLine();
            DisplayText("Sylvain Jalowoï\t Tel: 06 61 17 16 75\t" +
                "Site web: www.agilitic.fr\n");
            DispaySeparator();
            AppendNewLine();
        }

        internal static string AwaitInput()
        {
            return Console.ReadLine();
        }

        internal static void PopulateList(List<Appointment> apts,int range,int curminrange,int index)
        {
            int count = apts.Count;
            int coef = count / range + 1;
            for (int i = curminrange; i < count && i < curminrange + range; i++)
            {
                Appointment apt = apts[i];
                Screen.DisplayText($"ID: {apt.ID}\nDate de debut: {apt.StartTime}\tDate de fin: {apt.EndTime}\n");
                Screen.DispaySeparator();
            }
            Screen.DisplayText($"Page: {index}/{coef}\n");
        }

        internal static void PopulateMenu(List<(string,Action)> options,Action<bool> display,bool badchoice)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Screen.DisplayText($"{i + 1} - {options[i].Item1}\t");
            }
            if (badchoice)
            {
                Screen.DisplayColoredText(ConsoleColor.Red, "\nVotre choix est invalide.\n" +
                    "Veuillez selectionner une option disponible.");
            }
            Screen.DisplayText("\nChoix: ");
            string input = Screen.AwaitInput();
            if (int.TryParse(input, out int choice))
            {
                if (choice > options.Count || choice - 1 < 0)
                {
                    display(true);
                }
                else
                {
                    (string, Action) menuitem = options[choice - 1];
                    menuitem.Item2();
                }
            }
            else
            {
                display(true);
            }
        }
    }
}
