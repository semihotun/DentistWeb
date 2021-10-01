
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.AppointmentTimes.ValidationRules;


namespace Business.Handlers.AppointmentTimes.Commands
{


    public class UpdateAppointmentTimeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }

        public class UpdateAppointmentTimeCommandHandler : IRequestHandler<UpdateAppointmentTimeCommand, IResult>
        {
            private readonly IAppointmentTimeRepository _appointmentTimeRepository;
            private readonly IMediator _mediator;

            public UpdateAppointmentTimeCommandHandler(IAppointmentTimeRepository appointmentTimeRepository, IMediator mediator)
            {
                _appointmentTimeRepository = appointmentTimeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateAppointmentTimeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateAppointmentTimeCommand request, CancellationToken cancellationToken)
            {
                var isThereAppointmentTimeRecord = await _appointmentTimeRepository.GetAsync(u => u.Id == request.Id);


                isThereAppointmentTimeRecord.Hour = request.Hour;
                isThereAppointmentTimeRecord.Minutes = request.Minutes;


                _appointmentTimeRepository.Update(isThereAppointmentTimeRecord);
                await _appointmentTimeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

