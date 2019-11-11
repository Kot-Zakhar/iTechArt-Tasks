using System;
using CustomModelBinder.Api.Binders;
using Microsoft.AspNetCore.Mvc;

namespace CustomModelBinder.Api.Models
{
    [ModelBinder(BinderType = typeof(PersonEntityBuilder))]
    public class Person
    {
        private void InitPerson(Bogus.Person person)
        {
            Website = person.Website;
            FirstName = person.FirstName;
            LastName = person.LastName;
            FullName = person.FullName;
            UserName = person.UserName;
            Avatar = person.Avatar;
            Email = person.Email;
            DateOfBirth = person.DateOfBirth;
            Phone = person.Phone;
        }
        public Person()
        {
            Id = Guid.NewGuid();
            InitPerson(new Bogus.Person(seed: Id.GetHashCode()));
        }

        public Person(Guid id){
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
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
    }
}
