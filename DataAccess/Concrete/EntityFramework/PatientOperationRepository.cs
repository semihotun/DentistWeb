
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Entities.Dtos;
using System.Collections.Generic;
using Core.Utilities.Results;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class PatientOperationRepository : EfEntityRepositoryBase<PatientOperation, ProjectDbContext>, IPatientOperationRepository
    {
        public PatientOperationRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IList<PatientOperationDTO>> GetPatientOperationDTOByPatientId(int patientId)
        {
            var query = from p in Context.PatientOperation
                        where p.PatientId == patientId
                        join dj in Context.Disease on p.DiseaseId equals dj.Id
                        select new PatientOperationDTO
                        {
                            DiseaseId=p.DiseaseId,
                            Id=p.Id,
                            DiseaseName= dj.Name,
                            PatientId=p.PatientId
                        };

            var result = await query.ToListAsync();
            return result;
        }


    }
}
