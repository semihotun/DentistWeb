
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.AppointmentTimes.Queries
{
    public class GetAppointmentTimeQuery : IRequest<IDataResult<AppointmentTime>>
    {
        public int Id { get; set; }

        public class GetAppointmentTimeQueryHandler : IRequestHandler<GetAppointmentTimeQuery, IDataResult<AppointmentTime>>
        {
            private readonly IAppointmentTimeRepository _appointmentTimeRepository;
            private readonly IMediator _mediator;

            public GetAppointmentTimeQueryHandler(IAppointmentTimeRepository appointmentTimeRepository, IMediator mediator)
            {
                _appointmentTimeRepository = appointmentTimeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<AppointmentTime>> Handle(GetAppointmentTimeQuery request, CancellationToken cancellationToken)
            {
                var appointmentTime = await _appointmentTimeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<AppointmentTime>(appointmentTime);
            }
        }
    }
}
