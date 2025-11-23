using System;

namespace EKartBL
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("[LOG] " + message);
        }
    }
}
