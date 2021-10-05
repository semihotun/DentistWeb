
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
using Business.Handlers.Diseases.ValidationRules;

namespace Business.Handlers.Diseases.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDiseaseCommand : IRequest<IResult>
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }


        public class CreateDiseaseCommandHandler : IRequestHandler<CreateDiseaseCommand, IResult>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;
            public CreateDiseaseCommandHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDiseaseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDiseaseCommand request, CancellationToken cancellationToken)
            {
                var isThereDiseaseRecord = _diseaseRepository.Query().Any(u => u.Name == request.Name);

                if (isThereDiseaseRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDisease = new Disease
                {
                    Name = request.Name,
                    Price = request.Price,
                    CurrencyId = request.CurrencyId,
                };

                _diseaseRepository.Add(addedDisease);
                await _diseaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}