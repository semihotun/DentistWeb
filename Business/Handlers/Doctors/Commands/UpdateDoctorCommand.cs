
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
using Business.Handlers.Doctors.ValidationRules;


namespace Business.Handlers.Doctors.Commands
{


    public class UpdateDoctorCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Adress { get; set; }
        public string Telephone { get; set; }
        public int DoctorTypeId { get; set; }
        public System.DateTime StartDateOfWork { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, IResult>
        {
            private readonly IDoctorRepository _doctorRepository;
            private readonly IMediator _mediator;

            public UpdateDoctorCommandHandler(IDoctorRepository doctorRepository, IMediator mediator)
            {
                _doctorRepository = doctorRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDoctorValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(MsSqlLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
            {
                var isThereDoctorRecord = await _doctorRepository.GetAsync(u => u.Id == request.Id);


                isThereDoctorRecord.Name = request.Name;
                isThereDoctorRecord.Surname = request.Surname;
                isThereDoctorRecord.Adress = request.Adress;
                isThereDoctorRecord.Telephone = request.Telephone;
                isThereDoctorRecord.DoctorTypeId = request.DoctorTypeId;
                isThereDoctorRecord.StartDateOfWork = request.StartDateOfWork;
                isThereDoctorRecord.Active = request.Active;
                isThereDoctorRecord.Deleted = request.Deleted;


                _doctorRepository.Update(isThereDoctorRecord);
                await _doctorRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

