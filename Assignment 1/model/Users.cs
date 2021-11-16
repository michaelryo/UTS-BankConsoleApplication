using System;
using System.Collections.Generic;
using System.IO;

namespace Assignment_1.model
{
    /*
     * This is Users Class
     * Users contain list of User Account
     */
    class Users
    {
        private string[][] listUser;
        //Path to the login.txt
        static readonly string textFile = @"..\..\..\login.txt";
        // Read entire text file content in one string  
        public Users()
        {
            // Read entire text file read by line  
            string[] lines = File.ReadAllLines(textFile);
            foreach (string line in lines)
            {
                //Split it by the '|' symbol following tutor format
                listUser.Add(new User(line.Split('|')[0], line.Split('|')[1]));
            }
        }

        //checking if user exists.
        public Boolean UserExist(String username, String password)
        {
            for (int UserDB = 0; UserDB < listUser.Count; UserDB++)
            {
                if (listUser[UserDB].getUsername() == username &&
                   listUser[UserDB].getPassword() == password)
                    return true;
            }
            return false;
        }
    }
}
