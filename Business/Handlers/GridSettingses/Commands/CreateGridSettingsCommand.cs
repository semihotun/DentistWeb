
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
using Business.Handlers.GridSettingses.ValidationRules;

namespace Business.Handlers.GridSettingses.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGridSettingsCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string PropertyInfo { get; set; }

        public class CreateGridSettingsCommandHandler : IRequestHandler<CreateGridSettingsCommand, IResult>
        {
            private readonly IGridSettingsRepository _gridSettingsRepository;
            private readonly IMediator _mediator;
            public CreateGridSettingsCommandHandler(IGridSettingsRepository gridSettingsRepository, IMediator mediator)
            {
                _gridSettingsRepository = gridSettingsRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGridSettingsValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGridSettingsCommand request, CancellationToken cancellationToken)
            {
                var isThereGridSettingsRecord = _gridSettingsRepository.Query().Any(u => u.Path == request.Path);

                if (isThereGridSettingsRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGridSettings = new GridSettings
                {
                    Path=request.Path,
                    Id=request.Id,
                    PropertyInfo = request.PropertyInfo
                };

                _gridSettingsRepository.Add(addedGridSettings);
                await _gridSettingsRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}