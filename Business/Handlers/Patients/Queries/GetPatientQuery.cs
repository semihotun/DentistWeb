
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Patients.Queries
{
    public class GetPatientQuery : IRequest<IDataResult<Patient>>
    {
        public int Id { get; set; }

        public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, IDataResult<Patient>>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMediator _mediator;

            public GetPatientQueryHandler(IPatientRepository patientRepository, IMediator mediator)
            {
                _patientRepository = patientRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Patient>> Handle(GetPatientQuery request, CancellationToken cancellationToken)
            {
                var patient = await _patientRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Patient>(patient);
            }
        }
    }
}
