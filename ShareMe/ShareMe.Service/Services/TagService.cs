using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShareMe.Service
{
    class TagService : Service<Tag>
    {
        protected readonly UnitOfWork unitOfWork;

        protected IRepository<Tag> TagRepository { get => this.repository; }

        public TagService(UnitOfWork unitOfWork) : base(unitOfWork.TagRepository)
        {
            this.unitOfWork = unitOfWork;
        }

        public Tag GetByName(string name)
        {
            return TagRepository.GetAll().Single(tag => tag.Name == name);
        }
    }
}
