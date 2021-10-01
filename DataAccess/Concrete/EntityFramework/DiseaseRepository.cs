
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class DiseaseRepository : EfEntityRepositoryBase<Disease, ProjectDbContext>, IDiseaseRepository
    {
        public DiseaseRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
