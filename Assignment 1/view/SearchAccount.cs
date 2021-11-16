using System;
using System.IO;
using System.Linq;
using Assignment_1.model;

namespace Assignment_1.view
{
    class SearchAccount : View
    {
        //Content Value and Content Title
        private String[] ContentValue = new string[] { "" };
        private String[] Content = new string[] { "Account Number: " };

        //File path consist all account
        private const string dataFilePath = @"..\..\..\account\";

        //Load Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();

        //Finish Response of the (y/n) statement
        private Boolean finishResponse = true;

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            write.WriteHeading("SEARCH AN ACCOUNT");
            write.makingContentBorder(4, 2);
            write.WriteSubheading("ENTER THE DETAILS", 3);
            write.WriteAt("Account Number: " + ContentValue[0], 2, 5);
            //While loop to keep checking user input details
            while (readInputForUser())
            {
                write.ClearAtY(8);
                write.WriteAt("Please Check Your Input", 0, 8);
            }
            //Check User
            findUser();
            write.WriteAt("Check Another Account ? (y/n)", 0, 13);

            //While loop to keep checking user input either [y] or [n]
            while (finishResponse)
            {
                ConsoleKeyInfo readKeyFinal = Console.ReadKey(true);
                if(readKeyFinal.Key == ConsoleKey.Y)
                {
                    Console.Clear();
                    this.ContentValue[0] = "";
                    this.Load();
                    break;
                }    
                else if(readKeyFinal.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    View NextView = new MainMenu();
                    NextView.Load();
                    break;
                }
            }
        }
        //Check user txt
        private void findUser()
        {
            int userId = Int32.Parse(ContentValue[0]);
            try
            {
                Console.Clear();
                write.WriteHeading("ACCOUNT DETAILS");
                write.makingContentBorder(9,2);
                if (File.Exists(dataFilePath + userId + ".txt"))
                {
                    string[] dataLine = File.ReadAllLines(dataFilePath + userId + ".txt");
                    for (int i = 0; i < dataLine.Length; i++)
                    {
                        if(i == 7)
                        {
                            break;
                        }
                        write.ClearAtY(4 + i);
                        write.WriteAt(dataLine[i].Replace("|", ": "), 2, 4 + i);
                    }
                }
                else
                {
                    Console.Clear();
                    write.ClearAtY(8);
                    write.WriteAt("No Account Found", 0, 8);
                    this.Load();
                }
            }
            catch (Exception Ex)
            {
                write.ClearAtY(8);
                write.WriteAt(Ex.Message, 0, 8);
                this.Load();
            }
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
                //If they press enter it will go down or submit the form
                else if (readKeyResult.Key == ConsoleKey.Enter)
                    LoopActive = false;
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
                        if (ContentValue[indexContentValue].Length < 10)
                        {
                            ContentValue[indexContentValue] += readKeyResult.KeyChar;
                        }
                        else
                        {
                            write.ClearAtY(8);
                            write.WriteAt("Maximum Input allowed is 10 character", 0, 8);
                        }

                    }
                    //clear UI if there is any changes on contentValue
                    write.ClearAtY(5 + indexContentValue);
                }
                //Rewrite UI if there is any changes on contentValue (similar to React)
                write.WriteAt(Content[indexContentValue] + ContentValue[indexContentValue], 2, 5 + indexContentValue);
            } while (LoopActive);
            //If the input is int (from View Class)
            if (checkInt(ContentValue[0]))
                return false;
            return true;
        }
    }
}