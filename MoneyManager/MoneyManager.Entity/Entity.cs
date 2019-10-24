using System;

namespace MoneyManager.Entity
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }
        public Entity(Guid Id)
        {
            this.Id = Id;
        }
        public Guid Id { get; set; }
    }
}
