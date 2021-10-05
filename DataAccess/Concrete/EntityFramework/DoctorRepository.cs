
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DoctorRepository : EfEntityRepositoryBase<Doctor, ProjectDbContext>, IDoctorRepository
    {
        public DoctorRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
