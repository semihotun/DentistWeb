
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


namespace Business.Handlers.GridSettingses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGridSettingsCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteGridSettingsCommandHandler : IRequestHandler<DeleteGridSettingsCommand, IResult>
        {
            private readonly IGridSettingsRepository _gridSettingsRepository;
            private readonly IMediator _mediator;

            public DeleteGridSettingsCommandHandler(IGridSettingsRepository gridSettingsRepository, IMediator mediator)
            {
                _gridSettingsRepository = gridSettingsRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGridSettingsCommand request, CancellationToken cancellationToken)
            {
                var gridSettingsToDelete = _gridSettingsRepository.Get(p => p.Id == request.Id);

                _gridSettingsRepository.Delete(gridSettingsToDelete);
                await _gridSettingsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

