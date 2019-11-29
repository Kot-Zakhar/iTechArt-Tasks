using System.Collections.Generic;
using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShareMe.WebApplication.Services
{
    public class TagService : Service<TagApiModel, Tag>, ITagService
    {
        protected TagRepository _tagRepository;

        public TagService(UnitOfWork unitOfWork) : base(unitOfWork.TagRepository)
        {
            _tagRepository = unitOfWork.TagRepository;
        }
        protected override TagApiModel TranslateToApiModel(Tag tag)
        {
            return new TagApiModel(tag);
        }

        public async Task<TagApiModel> GetByNameAsync(string name)
        {
            return TranslateToApiModel(await _tagRepository.GetByNameAsync(name));
        }

        public async Task<IList<TagApiModel>> GetTopAsync(int count)
        {
            return (await _tagRepository.GetAll().Take(count).ToListAsync()).Select(t => TranslateToApiModel(t)).ToList();
        }
    }
}
