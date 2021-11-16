using System;

namespace Assignment_1.model
{
    /*
     * Write Console Class
     * This will help to reuse code and structure
     */
    class WriteConsole
    {
        //Original Row and Column is [Cursor y] and [Cursor x] position
        private static int origRow, origCol;
        // Max width of the console
        private static int maxWidth = 60;
        public void MainMenuUI()
        {
            origRow = Console.CursorTop;//Get Cursor Y position
            origCol = Console.CursorLeft;//Get Cursor X position
        }

        //To write heading in each view
        public void WriteHeading(String text, int y = 0)
        {
            for (int i = y ; i <= 2; i++)
            {
                if (i != 1)
                {
                    for (int j = maxWidth; j > 0; j--)
                    {
                        WriteAt("=", j, i);
                    }
                }
                else
                {
                    WriteAt("|", 0, i);
                    WriteAt("|", maxWidth, i);
                    WriteAt(text, (maxWidth / 2) - (text.Length / 2), i);
                }
            }
        }
        //To write Subheading in each view
        public void WriteSubheading(String text, int y)
        {
            WriteAt("|", 0, y);
            WriteAt("|", maxWidth, y);
            WriteAt(text, (maxWidth / 2) - (text.Length / 2), y);
        }

        //To make border for the console content
        public void makingContentBorder(int space, int startLine)
        {
            // 2 for heading
            // 1 for bottom border
            space += startLine; 
            for (int i = 2; i <= space; i++)
            {
                if (i == 0 || i == space)
                {
                    for (int j = maxWidth; j > 0; j--)
                    {
                        WriteAt("=", j, i);
                    }
                }
                else
                {
                    WriteAt("|", 0, i);
                    WriteAt("|", maxWidth, i);
                }
            }
        }
        //Write At function to write string in [X,Y]
        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        //Clear At Y function to clear string in [Y]
        public void ClearAtY(int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + 1, origRow + y);
                Console.Write(new string(' ', maxWidth - 1));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
