using System;
using System.Text;

namespace BookingApp.ConsoleUI
{
    class Runner
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            new DemoProgram().Run();
        }
    }
}
