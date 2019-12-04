using Microsoft.AspNetCore.Identity;
using Bogus;
using System;

namespace PermissionsAttribute.WebApp.Models
{
    public class IdentityProfile : IdentityUser
    {
        private void InitPerson()
        {
            Person person = new Person(seed: Id.GetHashCode());
            FirstName = person.FirstName;
            LastName = person.LastName;
            UserName = person.UserName;
            Email = person.Email;
            Birthday = person.DateOfBirth;
            PhoneNumber = person.Phone;
        }
        public IdentityProfile() : base()
        {
            InitPerson();
        }

        public IdentityProfile(string userName) : base(userName)
        {
            InitPerson();
            UserName = userName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public ProfileStatus Status { get; set; } = ProfileStatus.Submitted;
    }
}
