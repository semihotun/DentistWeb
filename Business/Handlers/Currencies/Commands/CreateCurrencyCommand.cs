
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Currencies.ValidationRules;

namespace Business.Handlers.Currencies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateCurrencyCommand : IRequest<IResult>
    {

        public string Abbreviation { get; set; }


        public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, IResult>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly IMediator _mediator;
            public CreateCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMediator mediator)
            {
                _currencyRepository = currencyRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateCurrencyValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
            {
                var isThereCurrencyRecord = _currencyRepository.Query().Any(u => u.Abbreviation == request.Abbreviation);

                if (isThereCurrencyRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedCurrency = new Currency
                {
                    Abbreviation = request.Abbreviation,

                };

                _currencyRepository.Add(addedCurrency);
                await _currencyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}