
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

namespace Business.Handlers.AppointmentTimes.Queries
{

    public class GetAppointmentTimesQuery : IRequest<IDataResult<IEnumerable<AppointmentTime>>>
    {
        public class GetAppointmentTimesQueryHandler : IRequestHandler<GetAppointmentTimesQuery, IDataResult<IEnumerable<AppointmentTime>>>
        {
            private readonly IAppointmentTimeRepository _appointmentTimeRepository;
            private readonly IMediator _mediator;

            public GetAppointmentTimesQueryHandler(IAppointmentTimeRepository appointmentTimeRepository, IMediator mediator)
            {
                _appointmentTimeRepository = appointmentTimeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<AppointmentTime>>> Handle(GetAppointmentTimesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<AppointmentTime>>(await _appointmentTimeRepository.GetListAsync());
            }
        }
    }
}