
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.PatientOperations.Queries
{

    public class GetPatientOperationsQuery : IRequest<IDataResult<IEnumerable<PatientOperation>>>
    {
        public class GetPatientOperationsQueryHandler : IRequestHandler<GetPatientOperationsQuery, IDataResult<IEnumerable<PatientOperation>>>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;

            public GetPatientOperationsQueryHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<PatientOperation>>> Handle(GetPatientOperationsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<PatientOperation>>(await _patientOperationRepository.GetListAsync());
            }
        }
    }
}