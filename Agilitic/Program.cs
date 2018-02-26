using Agilitic.Models;
using Agilitic.Screens;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Agilitic
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] arg)
        {
            await DataAccessLayer.Initialize();
            HomeScreen.Display();
        }
    }
}
