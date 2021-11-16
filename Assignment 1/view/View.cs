using System;

namespace Assignment_1.view
{
    abstract class View
    {
        //Abstract Method load (can be check in MainMenu)
        public abstract void Load();
        //Method to check current input in the corret Content index
        protected int checkCurrentInputLine(int index, int contentValueLength)
        {
            if (index > contentValueLength - 1)
                index = 0;
            else if (index < 0)
                index = contentValueLength - 1;
            return index;
        }
        //Method to check current input is int
        protected Boolean checkInt(String input)
        {
            int emptyInt;
            if (!int.TryParse(input, out emptyInt) || isNull(input))
                return false;
            else if (Int32.Parse(input) < 0)
                return false;
            return true;
        }
        //Method to check current input is Double
        protected Boolean checkDouble(String input)
        {
            Double emptyDouble;
            if (!Double.TryParse(input, out emptyDouble) || isNull(input))
                return false;
            else if (Double.Parse(input) < 0)
                return false;
            return true;
        }
        //Method to check current input is Null
        protected Boolean isNull(String input)
        {
            if (input.Length != 0)
                return false;
            return true;
        }
    }
}
