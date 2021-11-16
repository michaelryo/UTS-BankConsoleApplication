using System;

namespace Assignment_1.model
{
    /*
     * This is User Class
     * User is account used to login
     * have only 2 attribute username and password
     */
    class User
    {
        private readonly String username;
        private readonly String password;

        //User Constructor
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        //Get User Username
        public String getUsername()
        {
            return username;
        }

        //Get User Password
        public String getPassword()
        {
            return password;
        }
    }
}
