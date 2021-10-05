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

namespace Business.Handlers.Currencies.Queries
{

    public class GetCurrencyLookUpQuery : IRequest<IDataResult<IEnumerable<SelectionItem>>>
    {
        public class GetCurrencyLookUpQueryHandler : IRequestHandler<GetCurrencyLookUpQuery, IDataResult<IEnumerable<SelectionItem>>>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly IMediator _mediator;

            public GetCurrencyLookUpQueryHandler(ICurrencyRepository currencyRepository, IMediator mediator)
            {
                _currencyRepository = currencyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<SelectionItem>>> Handle(GetCurrencyLookUpQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<SelectionItem>>(await _currencyRepository.GetCurrencyLookUp());
            }
        }
    }
}