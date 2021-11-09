
using Business.Handlers.Doctors.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Doctors.Queries.GetDoctorQuery;
using Entities.Concrete;
using static Business.Handlers.Doctors.Queries.GetDoctorsQuery;
using static Business.Handlers.Doctors.Commands.CreateDoctorCommand;
using Business.Handlers.Doctors.Commands;
using Business.Constants;
using static Business.Handlers.Doctors.Commands.UpdateDoctorCommand;
using static Business.Handlers.Doctors.Commands.DeleteDoctorCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using Core.Utilities.File;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DoctorHandlerTests
    {
        Mock<IDoctorRepository> _doctorRepository;
        Mock<IMediator> _mediator;
        Mock<IFileService> _fileHelper;

        [SetUp]
        public void Setup()
        {
            _doctorRepository = new Mock<IDoctorRepository>();
            _mediator = new Mock<IMediator>();
            _fileHelper = new Mock<IFileService>();
        }

        [Test]
        public async Task Doctor_GetQuery_Success()
        {
            //Arrange
            var query = new GetDoctorQuery();

            _doctorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Doctor, bool>>>())).ReturnsAsync(new Doctor()
//propertyler buraya yazılacak
//{																		
//DoctorId = 1,
//DoctorName = "Test"
//}
);

            var handler = new GetDoctorQueryHandler(_doctorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DoctorId.Should().Be(1);

        }

        [Test]
        public async Task Doctor_GetQueries_Success()
        {
            //Arrange
            var query = new GetDoctorsQuery();

            _doctorRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                        .ReturnsAsync(new List<Doctor> { new Doctor() { /*TODO:propertyler buraya yazılacak DoctorId = 1, DoctorName = "test"*/ } });

            var handler = new GetDoctorsQueryHandler(_doctorRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Doctor>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Doctor_CreateCommand_Success()
        {
            Doctor rt = null;
            //Arrange
            var command = new CreateDoctorCommand();
            //propertyler buraya yazılacak
            //command.DoctorName = "deneme";

            _doctorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                        .ReturnsAsync(rt);

            _doctorRepository.Setup(x => x.Add(It.IsAny<Doctor>())).Returns(new Doctor());

            var handler = new CreateDoctorCommandHandler(_doctorRepository.Object, _mediator.Object,_fileHelper.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _doctorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Doctor_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDoctorCommand();
            //propertyler buraya yazılacak 
            //command.DoctorName = "test";

            _doctorRepository.Setup(x => x.Query())
                                           .Returns(new List<Doctor> { new Doctor() { /*TODO:propertyler buraya yazılacak DoctorId = 1, DoctorName = "test"*/ } }.AsQueryable());

            _doctorRepository.Setup(x => x.Add(It.IsAny<Doctor>())).Returns(new Doctor());

            var handler = new CreateDoctorCommandHandler(_doctorRepository.Object, _mediator.Object,_fileHelper.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Doctor_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDoctorCommand();
            //command.DoctorName = "test";

            _doctorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                        .ReturnsAsync(new Doctor() { /*TODO:propertyler buraya yazılacak DoctorId = 1, DoctorName = "deneme"*/ });

            _doctorRepository.Setup(x => x.Update(It.IsAny<Doctor>())).Returns(new Doctor());

            //var handler = new UpdateDoctorCommandHandler(_doctorRepository.Object, _mediator.Object);
            //var x = await handler.Handle(command, new System.Threading.CancellationToken());

            //_doctorRepository.Verify(x => x.SaveChangesAsync());
            //x.Success.Should().BeTrue();
            //x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Doctor_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDoctorCommand();

            _doctorRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Doctor, bool>>>()))
                        .ReturnsAsync(new Doctor() { /*TODO:propertyler buraya yazılacak DoctorId = 1, DoctorName = "deneme"*/});

            _doctorRepository.Setup(x => x.Delete(It.IsAny<Doctor>()));

            var handler = new DeleteDoctorCommandHandler(_doctorRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _doctorRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

