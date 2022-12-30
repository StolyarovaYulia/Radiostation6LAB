using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;

namespace Radiostation.DAL.Repositories
{
    public class PerformerRepository : BaseRepository<Performer>
    {
        public PerformerRepository(RadiostationContext context) : base(context)
        {
        }

        protected override IQueryable<Performer> IncludeChildrens(IQueryable<Performer> query)
        {
            return query
                .Include(t => t.Tracks)
                .AsNoTracking();
        }

        protected override Expression<Func<Performer, bool>> GetByIdExpression(int id)
        {
            return performer => performer.Id == id;
        }
    }
}