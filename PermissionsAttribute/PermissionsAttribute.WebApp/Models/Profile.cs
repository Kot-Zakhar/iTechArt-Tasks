using System;

namespace PermissionsAttribute.WebApp.Models
{
    public class Profile
    {
        private void InitPerson(Bogus.Person person)
        {
            FirstName = person.FirstName;
            LastName = person.LastName;
            UserName = person.UserName;
            Email = person.Email;
            Birthday = person.DateOfBirth;
            Phone = person.Phone;
        }
        public Profile()
        {
            Id = Guid.NewGuid();
            InitPerson(new Bogus.Person(seed: Id.GetHashCode()));
        }

        public Profile(Guid id)
        {
            Id = id;
            InitPerson(new Bogus.Person(seed: Id.GetHashCode()));
        }

        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public ProfileStatus Status { get; set; } = ProfileStatus.Submitted;
    }
}
