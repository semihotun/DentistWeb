
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

namespace Business.Handlers.DoctorTypes.Queries
{

    public class GetDoctorTypesQuery : IRequest<IDataResult<IEnumerable<DoctorType>>>
    {
        public class GetDoctorTypesQueryHandler : IRequestHandler<GetDoctorTypesQuery, IDataResult<IEnumerable<DoctorType>>>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;

            public GetDoctorTypesQueryHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DoctorType>>> Handle(GetDoctorTypesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DoctorType>>(await _doctorTypeRepository.GetListAsync());
            }
        }
    }
}