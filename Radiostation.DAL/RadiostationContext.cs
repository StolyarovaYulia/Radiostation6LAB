using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radiostation.DAL
{
    public class RadiostationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Performer> Performers { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Translation> Translations { get; set; }


        public RadiostationContext(DbContextOptions<RadiostationContext> options)
            : base(options)
        {
        }

        public void DetachEntities<TEntity>(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DetachEntity(entity);
            }
        }

        public void DetachEntity<TEntity>(TEntity entity)
        {
            Entry(entity).State = EntityState.Detached;
        }
    }
}
