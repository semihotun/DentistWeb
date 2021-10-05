
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

namespace Business.Handlers.Currencies.Queries
{

    public class GetCurrenciesQuery : IRequest<IDataResult<IEnumerable<Currency>>>
    {
        public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, IDataResult<IEnumerable<Currency>>>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly IMediator _mediator;

            public GetCurrenciesQueryHandler(ICurrencyRepository currencyRepository, IMediator mediator)
            {
                _currencyRepository = currencyRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Currency>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Currency>>(await _currencyRepository.GetListAsync());
            }
        }
    }
}