using System;
using System.IO;
using System.Linq;
using Assignment_1.model;

namespace Assignment_1.view
{
    class Deposit : View
    {
        //Content Value and Content Title
        private String[] ContentValue = new string[] { "", "" };
        private String[] Content = new string[] { "Account Number: ", "Ammount: " };

        //File path consist all account
        private const string dataFilePath = @"..\..\..\account\";

        //Load Write Console to Write heading and stuff
        private WriteConsole write = new WriteConsole();

        /* 
         * Override method
         * Load Function to show all the console UI
        */
        public override void Load()
        {
            write.WriteHeading("DEPOSIT");
            write.makingContentBorder(4, 3);
            write.WriteSubheading("ENTER THE DETAILS", 3);
            for (int i = Content.Length - 1; i > -1; i--)
            {
                write.WriteAt(Content[i], 2, 5 + i);
            }

            //While loop to keep checking user input details
            while (readInputForUser())
            {
                write.ClearAtY(8);
                write.WriteAt("Please Check Your Input", 0, 8);
            }
            write.ClearAtY(8);
            write.WriteAt("Processing Input", 0, 8);
            updateUserData();
            Console.ReadKey();
            Console.Clear();
            View NextView = new MainMenu();
            NextView.Load();
        }

        //Update user data in txt
        private void updateUserData()
        {
            //get current date
            string today = (DateTime.Now.ToString("dd/MM/yyyy")).Replace("/", ".");
            string path = dataFilePath + ContentValue[0] + ".txt";
            try
            {
                //if file exists
                if (File.Exists(path))
                {
                    // Read entire text file content in one string    
                    string[] dataLine = File.ReadAllLines(path);
                    try
                    {
                        //Get account balance
                        dataLine[6] = "Balance |" + (Decimal.Parse(dataLine[6].Replace("Balance |", "")) + Decimal.Parse(ContentValue[1]));
                    }
                    catch (Exception Ex)
                    {
                        write.ClearAtY(8);
                        write.WriteAt(Ex.Message, 0, 8);
                    }
                    /*
                     * Only store 5 data 7 + 5 
                     * [7 = account details]
                     * [5 = last 5 transaction]
                     */
                    if (dataLine.Length >= 12)
                    {
                        for (int i = 7; i <= dataLine.Length - 2; i++)
                            dataLine[i] = dataLine[i + 1];
                        dataLine[11] = today + "|Deposit|" + ContentValue[1] + "|" + dataLine[6].Replace("Balance |", "") + "|";
                    }
                    else
                        dataLine = dataLine.Concat(new string[] { today + "|Deposit|" + ContentValue[1] + "|" + dataLine[6].Replace("Balance |", "") + "|" }).ToArray();

                    File.WriteAllLines(path, dataLine);
                    write.ClearAtY(8);
                    write.WriteAt("Success", 0, 8);
                    write.WriteAt("Please enter any key to continue...", 0, 9);
                }
                else
                {
                    write.ClearAtY(8);
                    write.WriteAt("No user found", 0, 8);
                    this.Load();
                }
            }
            catch(Exception ex)
            {
                write.ClearAtY(8);
                write.WriteAt(ex.Message, 0, 8);
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
            if (checkInt(ContentValue[0]) &&
                checkDouble(ContentValue[1]))
                return false;
            return true;
        }
    }
}
