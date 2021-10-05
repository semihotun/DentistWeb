
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


namespace Business.Handlers.AppointmentTimes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteAppointmentTimeCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteAppointmentTimeCommandHandler : IRequestHandler<DeleteAppointmentTimeCommand, IResult>
        {
            private readonly IAppointmentTimeRepository _appointmentTimeRepository;
            private readonly IMediator _mediator;

            public DeleteAppointmentTimeCommandHandler(IAppointmentTimeRepository appointmentTimeRepository, IMediator mediator)
            {
                _appointmentTimeRepository = appointmentTimeRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteAppointmentTimeCommand request, CancellationToken cancellationToken)
            {
                var appointmentTimeToDelete = _appointmentTimeRepository.Get(p => p.Id == request.Id);

                _appointmentTimeRepository.Delete(appointmentTimeToDelete);
                await _appointmentTimeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

