
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


namespace Business.Handlers.Diseases.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDiseaseCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDiseaseCommandHandler : IRequestHandler<DeleteDiseaseCommand, IResult>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;

            public DeleteDiseaseCommandHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDiseaseCommand request, CancellationToken cancellationToken)
            {
                var diseaseToDelete = _diseaseRepository.Get(p => p.Id == request.Id);

                _diseaseRepository.Delete(diseaseToDelete);
                await _diseaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

