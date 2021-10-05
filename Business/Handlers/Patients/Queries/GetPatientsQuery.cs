
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

namespace Business.Handlers.Patients.Queries
{

    public class GetPatientsQuery : IRequest<IDataResult<IEnumerable<Patient>>>
    {
        public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, IDataResult<IEnumerable<Patient>>>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMediator _mediator;

            public GetPatientsQueryHandler(IPatientRepository patientRepository, IMediator mediator)
            {
                _patientRepository = patientRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
           [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Patient>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Patient>>(await _patientRepository.GetListAsync());
            }
        }
    }
}