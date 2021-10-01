
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Diseases.Queries
{
    public class GetDiseaseQuery : IRequest<IDataResult<Disease>>
    {
        public int Id { get; set; }

        public class GetDiseaseQueryHandler : IRequestHandler<GetDiseaseQuery, IDataResult<Disease>>
        {
            private readonly IDiseaseRepository _diseaseRepository;
            private readonly IMediator _mediator;

            public GetDiseaseQueryHandler(IDiseaseRepository diseaseRepository, IMediator mediator)
            {
                _diseaseRepository = diseaseRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Disease>> Handle(GetDiseaseQuery request, CancellationToken cancellationToken)
            {
                var disease = await _diseaseRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Disease>(disease);
            }
        }
    }
}
