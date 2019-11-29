using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        private DbSet<Tag> TagTypeSet => typeSet;

        public TagRepository(DbContext context) : base(context) { }

        public async Task<Tag> GetByNameAsync(string name)
        {
            return await TagTypeSet.SingleAsync(t => t.Name == name);
        }
    }
}
