
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Currencies.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteCurrencyCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, IResult>
        {
            private readonly ICurrencyRepository _currencyRepository;
            private readonly IMediator _mediator;

            public DeleteCurrencyCommandHandler(ICurrencyRepository currencyRepository, IMediator mediator)
            {
                _currencyRepository = currencyRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
            {
                var currencyToDelete = _currencyRepository.Get(p => p.Id == request.Id);

                _currencyRepository.Delete(currencyToDelete);
                await _currencyRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

