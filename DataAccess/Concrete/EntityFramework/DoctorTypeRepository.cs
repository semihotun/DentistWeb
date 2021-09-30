
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DoctorTypeRepository : EfEntityRepositoryBase<DoctorType, ProjectDbContext>, IDoctorTypeRepository
    {
        public DoctorTypeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
