
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

namespace Business.Handlers.Doctors.Queries
{

    public class GetDoctorsQuery : IRequest<IDataResult<IEnumerable<Doctor>>>
    {
        public class GetDoctorsQueryHandler : IRequestHandler<GetDoctorsQuery, IDataResult<IEnumerable<Doctor>>>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMediator _mediator;

            public GetDoctorsQueryHandler(IDoctorRepository doctorRepository, IMediator mediator)
            {
                _doctorRepository = doctorRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Doctor>>> Handle(GetDoctorsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Doctor>>(await _doctorRepository.GetListAsync(x=>x.Deleted==false));
            }
        }
    }
}