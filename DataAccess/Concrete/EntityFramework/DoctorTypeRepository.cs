
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
    public class DoctorTypeRepository : EfEntityRepositoryBase<DoctorType, ProjectDbContext>, IDoctorTypeRepository
    {
        public DoctorTypeRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetDoctorTypeLookUp()
        {
            var lookUp = await (from entity in Context.DoctorType
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Name
                                }).ToListAsync();
            return lookUp;
        }
    }
}
