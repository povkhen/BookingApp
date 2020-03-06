using BookingApp.ConsoleUI.Pages;
using EasyConsole;

namespace BookingApp.ConsoleUI
{
    class DemoProgram : Program
    {
        public DemoProgram()
            : base("Main Console", breadcrumbHeader: true)
        {            AddPage(new MainPage(this));
            AddPage(new Page1(this));
            AddPage(new Page2(this));
            SetPage<MainPage>();
        }


    }
}
