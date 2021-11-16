using Assignment_1.model;
using System;

namespace Assignment_1.view
{
    class Login : View
    {
        //Load Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();

        //Content Value and Content Title
        private String[] ContentValue = { "", ""};

        //Password View to save Password in '*' format
        private String PasswordView = "";

        //Get all user details
        private Users setupReadTxt;

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            setupReadTxt = new Users();
            write.WriteHeading("WELCOME TO BANKING SYSTEM");
            write.makingContentBorder(4, 3);
            write.WriteSubheading("LOGIN TO START", 3);
            write.WriteAt("Password:  ", 2, 6);
            write.WriteAt("User Name: ", 2, 5);

            //While loop to keep checking user input details
            while (readInputForUser())
            {
                write.ClearAtY(8);
                write.WriteAt("Please Check Your Input", 0, 8);
            }
            veifyUser(ContentValue[0], ContentValue[1]);
        }
        private Boolean readInputForUser()
        {
            //Boolean loopActive to maintain the loop
            Boolean LoopActive = true;
            //IndexContentValue to see which content are user on. (Based on Content Title)
            int indexContentValue = 0;
            do
            {
                //Prevent any unnecessary key
                ConsoleKeyInfo readKeyResult = Console.ReadKey(true);
                if (readKeyResult.Key == ConsoleKey.LeftArrow ||
                readKeyResult.Key == ConsoleKey.RightArrow ||
                readKeyResult.Key == ConsoleKey.F1 ||
                readKeyResult.Key == ConsoleKey.F2 ||
                readKeyResult.Key == ConsoleKey.F3 ||
                readKeyResult.Key == ConsoleKey.F4 ||
                readKeyResult.Key == ConsoleKey.F5 ||
                readKeyResult.Key == ConsoleKey.F6 ||
                readKeyResult.Key == ConsoleKey.F7 ||
                readKeyResult.Key == ConsoleKey.F8 ||
                readKeyResult.Key == ConsoleKey.F9 ||
                readKeyResult.Key == ConsoleKey.F10 ||
                readKeyResult.Key == ConsoleKey.Home ||
                readKeyResult.Key == ConsoleKey.F12 ||
                readKeyResult.Key == ConsoleKey.LeftWindows ||
                readKeyResult.Key == ConsoleKey.RightWindows ||
                readKeyResult.Key == ConsoleKey.Tab ||
                readKeyResult.Key == ConsoleKey.Escape)
                { }
                //If they press up arrow to go up in the content
                else if (readKeyResult.Key == ConsoleKey.UpArrow)
                {
                    indexContentValue--;
                    indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                }
                //If they press up arrow to go down in the content
                else if (readKeyResult.Key == ConsoleKey.DownArrow)
                {
                    indexContentValue++;
                    indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                }
                //If they press enter it will go down or submit the form
                else if (readKeyResult.Key == ConsoleKey.Enter)
                {
                    if (indexContentValue == ContentValue.Length - 1)
                    {
                        LoopActive = false;
                    }
                    else
                    {
                        indexContentValue++;
                        indexContentValue = checkCurrentInputLine(indexContentValue, ContentValue.Length);
                    }
                }
                else
                {
                    //To delete and do backspace on the contentValue
                    if (readKeyResult.Key == ConsoleKey.Delete || readKeyResult.Key == ConsoleKey.Backspace)
                    {
                        if (ContentValue[indexContentValue].Length != 0)
                        {
                            ContentValue[indexContentValue] = ContentValue[indexContentValue].Remove(ContentValue[indexContentValue].Length - 1, 1);
                        }
                    }
                    //To add details on content Value
                    else
                    {
                        if (ContentValue[indexContentValue].Length <= 12)
                        {
                            ContentValue[indexContentValue] += readKeyResult.KeyChar;
                        }
                        else
                        {
                            write.ClearAtY(12);
                            write.WriteAt("Maximum Input allowed is 12 character", 0, 12);
                        }

                    }
                    //clear UI if there is any changes on contentValue
                    write.ClearAtY(5 + indexContentValue);
                }
                //Rewrite UI if there is any changes on contentValue (similar to React)
                if (indexContentValue == 0)
                {
                    write.WriteAt("User Name: " + ContentValue[indexContentValue], 2, 5 + indexContentValue);
                }    
                else if(indexContentValue == 1)
                {
                    string tempPass = new string('*', ContentValue[1].Length);
                    write.WriteAt("Password:  " + tempPass, 2, 5 + indexContentValue);
                }
            } while (LoopActive);
            //If the input is int (from View Class)
            if (!isNull(ContentValue[0]) &&
                !isNull(ContentValue[1]))
                return false;
            return true;
        }

        //Verify user input details
        private void veifyUser(String username, String password)
        {
            //Load method to check user exist or not from users class
            if (setupReadTxt.UserExist(username, password))
            {
                Console.Clear();
                MainMenu menuView = new MainMenu();
                menuView.Load();
            }
            else
            {
                write.WriteAt("Wrong Username or Password please try again ", 1, 9);
                Load();
            }
        }
    }
}
