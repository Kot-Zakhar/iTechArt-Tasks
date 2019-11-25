using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class PostRepository : Repository<Post>
    {
        private DbSet<Post> _postTypeSet { get => typeSet; }

        public PostRepository(DbContext context) : base(context) { }


    }
}
