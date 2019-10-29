using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManager.DataAccess.Entity
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Entity(Guid Id)
        {
            this.Id = Id;
        }

        [Required]
        public Guid Id { get; set; }
    }
}
