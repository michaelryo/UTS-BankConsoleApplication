using Assignment_1.model;
using System;

namespace Assignment_1.view
{
    class MainMenu : View
    {
        //Load Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();

        //Check if its Int format
        private int emptyInt;

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            write.WriteHeading("WELCOME TO BANKING SYSTEM");
            write.makingContentBorder(7, 3);
            write.WriteAt("1. Create a new account", 2, 3);
            write.WriteAt("2. Search for an account", 2, 4);
            write.WriteAt("3. Deposit", 2, 5);
            write.WriteAt("4. Withdraw", 2, 6);
            write.WriteAt("5. A/C statement", 2, 7);
            write.WriteAt("6. Delete account ", 2, 8);
            write.WriteAt("7. Exit", 2, 9);
            write.makingContentBorder(1, 11);
            write.WriteAt("Enter your choice (1-7): ", 2, 11);
            validateInputNumber(Console.ReadKey(true).KeyChar.ToString());
        }
        //Check input validation
        private void validateInputNumber(String input)
        {
            if (!int.TryParse(input, out emptyInt))
            {
                write.WriteAt("Please input number only from 1 to 7", 0, 13);
                write.ClearAtY(11);
                write.WriteAt("Enter your choice (1-7): ", 2, 11);
                validateInputNumber(Console.ReadKey(true).KeyChar.ToString());
            }
            else if (Int32.Parse(input) > 7 || Int32.Parse(input) < 1)
            {
                write.WriteAt("Please input number only from 1 to 7", 0, 13);
                write.ClearAtY(11);
                write.WriteAt("Enter your choice (1-7): ", 2, 11);
                validateInputNumber(Console.ReadKey(true).KeyChar.ToString());
            }
            else
            {
                RedirectPage(Int32.Parse(input));
            }
        }

        //Redirect Page based on user input
        private void RedirectPage(int input)
        {
            View nextView = new MainMenu();
            switch (input)
            {
                case 1:
                    nextView = new CreateAccount();//Create Account
                    break;
                case 2:
                    nextView = new SearchAccount();//Search Account
                    break;
                case 3:
                    nextView = new Deposit();//Deposit
                    break;
                case 4:
                    nextView = new Withdraw();//Withdraw
                    break;
                case 5:
                    nextView = new AccountStatement();//Account Statement
                    break;
                case 6:
                    nextView = new DeleteAccount();//Delete Account
                    break;
                case 7:
                    Environment.Exit(0);//Exit
                    break;
                default:
                    Console.WriteLine("Something Went Wrong Please Try Again");
                    nextView = new Login();
                    break;
            }
            Console.Clear();
            nextView.Load();
        }
    }
}
