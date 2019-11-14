using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.WebApplication.Services
{
    public class TagService : Service<Tag>
    {
        protected IRepository<Tag> TagRepository { get => Repository; }

        public TagService(UnitOfWork unitOfWork) : base(unitOfWork.TagRepository)
        {}

        public Tag GetByName(string name)
        {
            return TagRepository.GetAll().Single(tag => tag.Name == name);
        }

        public override IQueryable<Tag> GetAll()
        {
            return TagRepository.GetAll();
        }
    }
}
