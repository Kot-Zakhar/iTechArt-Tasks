using ShareMe.DataAccessLayer.Entity;
using ShareMe.DataAccessLayer.UnitOfWork;
using ShareMe.DataAccessLayer.UnitOfWork.Repositories;
using ShareMe.WebApplication.ApiModels;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.WebApplication.Services
{
    public class TagService : Service<TagApiModel, Tag>
    {
        protected TagRepository _tagRepository;

        public TagService(UnitOfWork unitOfWork) : base(unitOfWork.TagRepository)
        {
            _tagRepository = unitOfWork.TagRepository;
        }
        protected override TagApiModel Translate(Tag t)
        {
            return new TagApiModel(t);
        }

        public async Task<TagApiModel> GetByName(string name)
        {
            return Translate(await _tagRepository.GetByNameAsync(name));
        }

        public IQueryable<TagApiModel> GetTop(int count)
        {
            return _tagRepository.GetAll().Take(count).Select(t => Translate(t));
        }
    }
}
