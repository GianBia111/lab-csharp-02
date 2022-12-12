using System;
using System.Collections.Generic;

namespace Collections
{
    public class SocialNetworkUser<TUser> : User, ISocialNetworkUser<TUser>
        where TUser : IUser
    {

        private Dictionary<string, List<TUser>> _groups = new Dictionary<string, List<TUser>>();


        public SocialNetworkUser(string fullName, string username, uint? age) : base(fullName, username, age)
        {
        
        }

        public bool AddFollowedUser(string group, TUser user)
        {
            if (this._groups.ContainsKey(group))
            {
                List<TUser> list = this._groups[group];
                list.Add(user);
                this._groups[group] = list;
            }
            else
            {
                List<TUser> list = new List<TUser>();
                list.Add(user);
                this._groups.Add(group, list);
            }

            return true;
        }

        public IList<TUser> FollowedUsers
        {
            get
            {
                IList<TUser> li = new List<TUser>();

                foreach (List<TUser> list in this._groups.Values)
                    foreach (TUser user in list)
                        li.Add(user);

                return li;
                  
            }
        }

        public ICollection<TUser> GetFollowedUsersInGroup(string group)
        {
            if (this._groups.ContainsKey(group))
            {
                return this._groups[group];
            }
            else
            {
                return new List<TUser>();
            }
        }
    }
}
