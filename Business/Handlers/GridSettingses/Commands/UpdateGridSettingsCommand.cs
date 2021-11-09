
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.GridSettingses.ValidationRules;


namespace Business.Handlers.GridSettingses.Commands
{
    public class UpdateGridSettingsCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string PropertyInfo { get; set; }

        public class UpdateGridSettingsCommandHandler : IRequestHandler<UpdateGridSettingsCommand, IResult>
        {
            private readonly IGridSettingsRepository _gridSettingsRepository;
            private readonly IMediator _mediator;

            public UpdateGridSettingsCommandHandler(IGridSettingsRepository gridSettingsRepository, IMediator mediator)
            {
                _gridSettingsRepository = gridSettingsRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGridSettingsValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGridSettingsCommand request, CancellationToken cancellationToken)
            {
                var isThereGridSettingsRecord = await _gridSettingsRepository.GetAsync(u => u.Id == request.Id);

                isThereGridSettingsRecord.Id = request.Id;
                isThereGridSettingsRecord.Path = request.Path;
                isThereGridSettingsRecord.PropertyInfo = request.PropertyInfo;

                _gridSettingsRepository.Update(isThereGridSettingsRecord);
                await _gridSettingsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

