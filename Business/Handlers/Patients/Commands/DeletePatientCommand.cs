
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


namespace Business.Handlers.Patients.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeletePatientCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, IResult>
        {
            private readonly IPatientRepository _patientRepository;
            private readonly IMediator _mediator;

            public DeletePatientCommandHandler(IPatientRepository patientRepository, IMediator mediator)
            {
                _patientRepository = patientRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
           [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
            {
                var patientToDelete = _patientRepository.Get(p => p.Id == request.Id);

                _patientRepository.Delete(patientToDelete);
                await _patientRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

