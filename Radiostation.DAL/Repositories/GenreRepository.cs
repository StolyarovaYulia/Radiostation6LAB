using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;

namespace Radiostation.DAL.Repositories
{
    public class GenreRepository : BaseRepository<Genre>
    {
        public GenreRepository(RadiostationContext context) : base(context)
        {
        }

        protected override IQueryable<Genre> IncludeChildrens(IQueryable<Genre> query)
        {
            return query
                .Include(t => t.Tracks)
                .AsNoTracking();
        }

        protected override Expression<Func<Genre, bool>> GetByIdExpression(int id)
        {
            return genre => genre.Id == id;
        }
    }
}