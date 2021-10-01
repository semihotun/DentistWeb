
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class CurrencyRepository : EfEntityRepositoryBase<Currency, ProjectDbContext>, ICurrencyRepository
    {
        public CurrencyRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
