
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
using Business.Handlers.Diseases.ValidationRules;


namespace Business.Handlers.Diseases.Commands
{


    public class UpdateDiseaseCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CurrencyId { get; set; }

        public class UpdateDiseaseCommandHandler : IRequestHandler<UpdateDiseaseCommand, IResult>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;

            public UpdateDiseaseCommandHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDiseaseValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDiseaseCommand request, CancellationToken cancellationToken)
            {
                var isThereDiseaseRecord = await _diseaseRepository.GetAsync(u => u.Id == request.Id);


                isThereDiseaseRecord.Name = request.Name;
                isThereDiseaseRecord.Price = request.Price;
                isThereDiseaseRecord.CurrencyId = request.CurrencyId;


                _diseaseRepository.Update(isThereDiseaseRecord);
                await _diseaseRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

