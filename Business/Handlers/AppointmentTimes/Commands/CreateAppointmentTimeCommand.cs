
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.AppointmentTimes.ValidationRules;

namespace Business.Handlers.AppointmentTimes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAppointmentTimeCommand : IRequest<IResult>
    {

        public int Hour { get; set; }
        public int Minutes { get; set; }


        public class CreateAppointmentTimeCommandHandler : IRequestHandler<CreateAppointmentTimeCommand, IResult>
        {
            private readonly IAppointmentTimeRepository _appointmentTimeRepository;
            private readonly IMediator _mediator;
            public CreateAppointmentTimeCommandHandler(IAppointmentTimeRepository appointmentTimeRepository, IMediator mediator)
            {
                _appointmentTimeRepository = appointmentTimeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAppointmentTimeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAppointmentTimeCommand request, CancellationToken cancellationToken)
            {
                var isThereAppointmentTimeRecord = _appointmentTimeRepository.Query().Any(u => u.Hour == request.Hour);

                if (isThereAppointmentTimeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAppointmentTime = new AppointmentTime
                {
                    Hour = request.Hour,
                    Minutes = request.Minutes,

                };

                _appointmentTimeRepository.Add(addedAppointmentTime);
                await _appointmentTimeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}