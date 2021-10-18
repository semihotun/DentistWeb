
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

namespace Business.Handlers.Diseases.Queries
{

    public class GetDiseaseLookUpQuery : IRequest<IDataResult<IList<SelectionItem>>>
    {
        public class GetDiseaseLookUpQueryHandler : IRequestHandler<GetDiseaseLookUpQuery, IDataResult<IList<SelectionItem>>>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;

            public GetDiseaseLookUpQueryHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
                     [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IList<SelectionItem>>> Handle(GetDiseaseLookUpQuery request, CancellationToken cancellationToken)
            {
                var data =await _diseaseRepository.GetDiseaseLookUp();
                return new SuccessDataResult<List<SelectionItem>>(data);
            }
        }
    }
}