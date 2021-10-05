
using Business.BusinessAspects;
using Core.Utilities.Results;
using Core.Aspects.Autofac.Performance;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Dtos;

namespace Business.Handlers.DoctorTypes.Queries
{

    public class DoctorTypeLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class DoctorTypeLookUpQueryHandler : IRequestHandler<DoctorTypeLookUpQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly IDoctorTypeRepository _doctorTypeRepository;
            private readonly IMediator _mediator;

            public DoctorTypeLookUpQueryHandler(IDoctorTypeRepository doctorTypeRepository, IMediator mediator)
            {
                _doctorTypeRepository = doctorTypeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]          
           [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(DoctorTypeLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _doctorTypeRepository.GetDoctorTypeLookUp());
            }
        }
    }
}