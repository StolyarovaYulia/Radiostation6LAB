using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;

namespace Radiostation.DAL.Repositories
{
    public class TranslationRepository : BaseRepository<Translation>
    {
        public TranslationRepository(RadiostationContext context) : base(context)
        {
        }

        protected override IQueryable<Translation> IncludeChildrens(IQueryable<Translation> query)
        {
            return query
                .Include(t => t.Employee)
                .Include(t => t.Track)
                    .ThenInclude(t => t.Genre)
                .OrderByDescending(t => t.Date)
                .AsNoTracking();
        }

        protected override Expression<Func<Translation, bool>> GetByIdExpression(int id)
        {
            return translation => translation.Id == id;
        }
    }
}