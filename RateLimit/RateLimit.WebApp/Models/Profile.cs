using System;

namespace RateLimit.WebApp.Models
{
    public class Profile
    {
        private DateTime _birthday;
        private void InitPerson(Bogus.Person person)
        {
            Website = person.Website;
            FirstName = person.FirstName;
            LastName = person.LastName;
            FullName = person.FullName;
            UserName = person.UserName;
            Avatar = person.Avatar;
            Email = person.Email;
            Birthday = person.DateOfBirth.ToString();
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

        public string Website { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Birthday { 
            get {
                return _birthday.ToShortDateString();
            }
            set {
                _birthday = DateTime.Parse(value); 
            }
        }
        public string Phone { get; set; }
    }
}
