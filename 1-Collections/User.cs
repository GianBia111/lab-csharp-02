using System;

namespace Collections
{
    public class User : IUser
    {
        public string FullName { get; private set; }
        public uint? Age { get; private set; }

        public string Username { get; private set; }

        public User(string fullName, string username, uint? age)
        {
            this.FullName = FullName;
            this.Username = username;
            this.Age = age;
        }


        public bool IsAgeDefined => Age.HasValue;
        
        // TODO implement missing methods (try to autonomously figure out which are the necessary methods)
    }
}
