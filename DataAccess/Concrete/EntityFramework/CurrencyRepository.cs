
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class CurrencyRepository : EfEntityRepositoryBase<Currency, ProjectDbContext>, ICurrencyRepository
    {
        public CurrencyRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetCurrencyLookUp()
        {
            var lookUp = await (from entity in Context.Currencies
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Abbreviation
                                }).ToListAsync();
            return lookUp;
        }
    }
}
