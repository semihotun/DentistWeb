
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class AppointmentTimeRepository : EfEntityRepositoryBase<AppointmentTime, ProjectDbContext>, IAppointmentTimeRepository
    {
        public AppointmentTimeRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
