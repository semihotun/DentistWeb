
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

namespace Business.Handlers.Diseases.Queries
{

    public class GetDiseasesQuery : IRequest<IDataResult<IEnumerable<Disease>>>
    {
        public class GetDiseasesQueryHandler : IRequestHandler<GetDiseasesQuery, IDataResult<IEnumerable<Disease>>>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;

            public GetDiseasesQueryHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Disease>>> Handle(GetDiseasesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Disease>>(await _diseaseRepository.GetListAsync());
            }
        }
    }
}