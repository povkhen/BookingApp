using EasyConsole;
using System;

namespace BookingApp.ConsoleUI.Pages
{
    class MainPage : MenuPage
    {
        public MainPage(Program program) : base("Головна сторінка", program,
            new Option("Пошук поїздок по маршруту та даті", () => program.NavigateTo<Page1>()),
            new Option("Детальна інформація про маршрут", () => program.NavigateTo<Page2>()),
            new Option("Вихід", () => Environment.Exit(0)))
        {
        }
    }
}
