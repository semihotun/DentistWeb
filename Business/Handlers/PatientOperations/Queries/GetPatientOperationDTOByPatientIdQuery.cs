
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;

namespace Business.Handlers.PatientOperations.Queries
{

    public class GetPatientOperationDTOByPatientIdQuery : IRequest<IDataResult<IList<PatientOperationDTO>>>
    {
        public int PatientId { get; set; }
        public class GetPatientOperationDTOByPatientIdHandler : IRequestHandler<GetPatientOperationDTOByPatientIdQuery, IDataResult<IList<PatientOperationDTO>>>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;

            public GetPatientOperationDTOByPatientIdHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IList<PatientOperationDTO>>> Handle(GetPatientOperationDTOByPatientIdQuery request, CancellationToken cancellationToken)
            {
                var data =await _patientOperationRepository.GetPatientOperationDTOByPatientId(request.PatientId);
                return new SuccessDataResult<IList<PatientOperationDTO>>(data);
            }
        }
    }
}