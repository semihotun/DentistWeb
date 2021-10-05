
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Currencies.Queries
{
    public class GetCurrencyQuery : IRequest<IDataResult<Currency>>
    {
        public int Id { get; set; }

        public class GetCurrencyQueryHandler : IRequestHandler<GetCurrencyQuery, IDataResult<Currency>>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly IMediator _mediator;

            public GetCurrencyQueryHandler(ICurrencyRepository currencyRepository, IMediator mediator)
            {
                _currencyRepository = currencyRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Currency>> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
            {
                var currency = await _currencyRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Currency>(currency);
            }
        }
    }
}
