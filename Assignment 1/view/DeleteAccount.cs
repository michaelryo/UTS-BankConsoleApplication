using System;
using System.IO;
using System.Linq;
using Assignment_1.model;

namespace Assignment_1.view
    {
    class DeleteAccount : View
    {
        //Content Value and Content Title
        private String[] ContentValue = new string[] { "" };
        private String[] Content = new string[] { "Account Number: " };
        //File path consist all account
        private const string dataFilePath = @"..\..\..\account\";

        // Loadd Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();
        //Finish Response of the (y/n) statement
        private Boolean finishResponse = true;

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            write.WriteHeading("DELETE AN ACCOUNT");
            write.makingContentBorder(4, 2);
            write.WriteSubheading("ENTER THE DETAILS", 3);
            write.WriteAt("Account Number: " + ContentValue[0], 2, 5);

            //While loop to keep checking user input details
            while (readInputForUser())
            {
                write.ClearAtY(8);
                write.WriteAt("Please Check Your Input", 0, 8);
            }
            //Find user also checking userId existances
            findUser();
            write.WriteAt("Delete This Account ? (y/n)", 0, 13);

            //While loop to keep checking user input either [y] or [n]
            while (finishResponse)
            {
                ConsoleKeyInfo readKeyFinal = Console.ReadKey(true);
                if (readKeyFinal.Key == ConsoleKey.Y)
                {
                    deleteUser();
                    write.ClearAtY(13);
                    write.WriteAt("Account Deleted", 0, 13);
                    write.ClearAtY(14);
                    write.WriteAt("Press Any Key to Continue...", 0, 14);
                    Console.ReadKey();
                    Console.Clear();
                    View nextView = new MainMenu();
                    nextView.Load();
                    break;
                }
                else if (readKeyFinal.Key == ConsoleKey.N)
                {
                    Console.Clear();
                    this.ContentValue[0] = "";
                    this.Load();
                    break;
                }
            }
        }
        //Find user also checking userId existances function
        private void findUser()
        {
            //Convert ID to int using 32 because 16 has low maximum number
            int userId = Int32.Parse(ContentValue[0]);
            try
            {
                //Clear console to write account details UI
                Console.Clear();
                write.WriteHeading("ACCOUNT DETAILS");
                write.makingContentBorder(9, 2);
                if (File.Exists(dataFilePath + userId + ".txt"))
                {
                    string[] dataLine = File.ReadAllLines(dataFilePath + userId + ".txt");
                    for (int i = 0; i < dataLine.Length; i++)
                    {
                        if (i == 7)
                        {
                            break;
                        }
                        write.ClearAtY(4 + i);
                        write.WriteAt(dataLine[i].Replace("|", ": "), 2, 4 + i);
                    }
                }
                else
                {
                    //Clear console to write error message and load this class UI
                    Console.Clear();
                    write.ClearAtY(8);
                    write.WriteAt("No Account Found", 0, 8);
                    this.Load();
                }
            }
            catch (Exception Ex)
            {
                //Clear console to write error message and load this class UI
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
                readKeyResult.Key == ConsoleKey.Escape ||
                readKeyResult.Key == ConsoleKey.UpArrow ||
                readKeyResult.Key == ConsoleKey.DownArrow)
                { }
                //If they press enter mean they submit
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

        /*
         * Delete User function
         */
        private void deleteUser()
        {
            //Check userId
            int userId = Int32.Parse(ContentValue[0]);
            try
            {
                //Check if file exists
                if (File.Exists(dataFilePath + userId + ".txt"))
                {
                    //Delete file
                    File.Delete(dataFilePath + userId + ".txt");
                }
                else
                {
                    //Clear console to write error message and load this class UI
                    write.ClearAtY(13);
                    write.WriteAt("No Data found", 0, 13);
                    this.Load();
                }
            }
            catch (Exception Ex)
            {
                //Clear console to write error message and load this class UI
                write.ClearAtY(8);
                write.WriteAt(Ex.Message, 0, 13);
                this.Load();
            }
        }
    }
}
