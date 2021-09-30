
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


namespace Business.Handlers.DoctorTypes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDoctorTypeCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDoctorTypeCommandHandler : IRequestHandler<DeleteDoctorTypeCommand, IResult>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;

            public DeleteDoctorTypeCommandHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDoctorTypeCommand request, CancellationToken cancellationToken)
            {
                var doctorTypeToDelete = _doctorTypeRepository.Get(p => p.Id == request.Id);

                _doctorTypeRepository.Delete(doctorTypeToDelete);
                await _doctorTypeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

