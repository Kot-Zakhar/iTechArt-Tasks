using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace ShareMe.DataAccessLayer.Entity
{
    public abstract class Entity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Entity(Guid id)
        {
            Id = id;
        }
    }
}
