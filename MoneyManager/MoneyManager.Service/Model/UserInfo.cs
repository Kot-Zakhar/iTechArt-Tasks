using MoneyManager.DataAccess.Entity;
using System;

namespace MoneyManager.Service.Model
{
    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserInfo(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
}
