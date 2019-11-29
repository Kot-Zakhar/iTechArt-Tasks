using Microsoft.EntityFrameworkCore;
using ShareMe.DataAccessLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShareMe.DataAccessLayer.UnitOfWork.Repositories
{
    public class PostRepository : Repository<Post>
    {
        private DbSet<Post> PostTypeSet => typeSet;

        public PostRepository(DbContext context) : base(context) { }

        public IQueryable<Post> GetByCategoryIdAndTagIds(Guid? categoryId, IList<Guid> tagIds)
        {
            IQueryable<Post> posts = PostTypeSet;
            if (categoryId.HasValue)
                posts = posts.Where(p => p.Category.Id == categoryId.Value);
            if (tagIds != null)
                posts = posts.Where(p => p.PostTags.Any(pt => tagIds.Contains(pt.Tag.Id)));
            return posts;
        }
    }
}
