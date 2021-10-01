
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class PatientRepository : EfEntityRepositoryBase<Patient, ProjectDbContext>, IPatientRepository
    {
        public PatientRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
