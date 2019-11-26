using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.Models.ApiModels;
using ShareMe.WebApplication.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<TagApiModel> GetByName(string name)
        {
            return TranslateToApiModel(await _tagRepository.GetByNameAsync(name));
        }

        public IQueryable<TagApiModel> GetTop(int count)
        {
            return _tagRepository.GetAll().Take(count).Select(t => TranslateToApiModel(t));
        }
    }
}
