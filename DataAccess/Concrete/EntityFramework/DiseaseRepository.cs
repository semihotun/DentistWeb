
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
    public class DiseaseRepository : EfEntityRepositoryBase<Disease, ProjectDbContext>, IDiseaseRepository
    {
        public DiseaseRepository(ProjectDbContext context) : base(context)
        {
        }
        public async Task<List<SelectionItem>> GetDiseaseLookUp()
        {
            var lookUp = await (from entity in Context.Disease
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Name
                                }).ToListAsync();
            return lookUp;
        }
    }
}
