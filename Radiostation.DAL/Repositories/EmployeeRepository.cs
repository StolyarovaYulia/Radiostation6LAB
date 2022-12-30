using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Radiostation.DAL.Entities;

namespace Radiostation.DAL.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(RadiostationContext context) : base(context)
        {
        }

        protected override IQueryable<Employee> IncludeChildrens(IQueryable<Employee> query)
        {
            return query
                .Include(t => t.Translations)
                .AsNoTracking();
        }

        protected override Expression<Func<Employee, bool>> GetByIdExpression(int id)
        {
            return employee => employee.Id == id;
        }
    }
}