
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.PatientOperations.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePatientOperationCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePatientOperationCommandHandler : IRequestHandler<DeletePatientOperationCommand, IResult>
        {
            private readonly IPatientOperationRepository _patientOperationRepository;
            private readonly IMediator _mediator;

            public DeletePatientOperationCommandHandler(IPatientOperationRepository patientOperationRepository, IMediator mediator)
            {
                _patientOperationRepository = patientOperationRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePatientOperationCommand request, CancellationToken cancellationToken)
            {
                var patientOperationToDelete = _patientOperationRepository.Get(p => p.Id == request.Id);

                _patientOperationRepository.Delete(patientOperationToDelete);
                await _patientOperationRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

