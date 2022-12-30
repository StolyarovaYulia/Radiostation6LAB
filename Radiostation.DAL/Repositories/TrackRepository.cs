using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;

namespace Radiostation.DAL.Repositories
{
    public class TrackRepository : BaseRepository<Track>
    {
        public TrackRepository(RadiostationContext context) : base(context)
        {
        }

        protected override IQueryable<Track> IncludeChildrens(IQueryable<Track> query)
        {
            return query
                .Include(t => t.Translations)
                .Include(t => t.Genre)
                .Include(t => t.Performer)
                .AsNoTracking();
        }

        protected override Expression<Func<Track, bool>> GetByIdExpression(int id)
        {
            return track => track.Id == id;
        }
    }
}