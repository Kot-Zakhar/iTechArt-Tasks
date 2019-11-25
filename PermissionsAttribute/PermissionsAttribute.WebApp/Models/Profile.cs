using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebApp.Models
{
    public class Profile
    {
        public Profile() { }
        public Profile(IdentityProfile identity)
        {
            Id = Guid.Parse(identity.Id);
            UserName = identity.UserName;
            FirstName = identity.FirstName;
            LastName = identity.LastName;
            Email = identity.Email;
            Birthday = identity.Birthday;
            PhoneNumber = identity.PhoneNumber;
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public ProfileStatus? Status { get; set; } = ProfileStatus.Submitted;

    }
}
