
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.PatientOperations.Queries
{
    public class GetPatientOperationQuery : IRequest<IDataResult<PatientOperation>>
    {
        public int Id { get; set; }

        public class GetPatientOperationQueryHandler : IRequestHandler<GetPatientOperationQuery, IDataResult<PatientOperation>>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;

            public GetPatientOperationQueryHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<PatientOperation>> Handle(GetPatientOperationQuery request, CancellationToken cancellationToken)
            {
                var patientOperation = await _patientOperationRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<PatientOperation>(patientOperation);
            }
        }
    }
}
