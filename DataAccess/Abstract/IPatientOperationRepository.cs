
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IPatientOperationRepository : IEntityRepository<PatientOperation>
    {
        Task<IList<PatientOperationDTO>> GetPatientOperationDTOByPatientId(int patientId);
    }
}