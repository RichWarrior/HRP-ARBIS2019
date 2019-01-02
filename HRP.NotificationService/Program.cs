using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace HRP.NotificationService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Debugger.IsAttached)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Notification Service Started...");
                Startup.Start();
                Console.ReadKey();
            }
            Startup _startup = new Startup();
        }
    }
}
