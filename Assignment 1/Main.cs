using Assignment_1.view;
using System;

namespace Assignment_1
{
    class main
    {
        static void Main(string[] args)
        {
            Login loginView = new Login();
            Console.Clear();
            loginView.Load();
        }
    }
}
