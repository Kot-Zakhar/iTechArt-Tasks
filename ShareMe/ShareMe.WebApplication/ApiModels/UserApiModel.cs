using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.ApiModels
{
    public class UserApiModel : ApiModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public UserApiModel() { }
        public UserApiModel(User user) : base(user)
        {
            Username = user.Username;
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
        }

    }
}
