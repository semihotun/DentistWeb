
using Business.Handlers.DoctorTypes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.DoctorTypes.Queries.GetDoctorTypeQuery;
using Entities.Concrete;
using static Business.Handlers.DoctorTypes.Queries.GetDoctorTypesQuery;
using static Business.Handlers.DoctorTypes.Commands.CreateDoctorTypeCommand;
using Business.Handlers.DoctorTypes.Commands;
using Business.Constants;
using static Business.Handlers.DoctorTypes.Commands.UpdateDoctorTypeCommand;
using static Business.Handlers.DoctorTypes.Commands.DeleteDoctorTypeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DoctorTypeHandlerTests
    {
        Mock<IDoctorTypeRepository> _doctorTypeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _doctorTypeRepository = new Mock<IDoctorTypeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task DoctorType_GetQuery_Success()
        {
            //Arrange
            var query = new GetDoctorTypeQuery();

            _doctorTypeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DoctorType, bool>>>())).ReturnsAsync(new DoctorType()
//propertyler buraya yazılacak
//{																		
//DoctorTypeId = 1,
//DoctorTypeName = "Test"
//}
);

            var handler = new GetDoctorTypeQueryHandler(_doctorTypeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DoctorTypeId.Should().Be(1);

        }

        [Test]
        public async Task DoctorType_GetQueries_Success()
        {
            //Arrange
            var query = new GetDoctorTypesQuery();

            _doctorTypeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<DoctorType, bool>>>()))
                        .ReturnsAsync(new List<DoctorType> { new DoctorType() { /*TODO:propertyler buraya yazılacak DoctorTypeId = 1, DoctorTypeName = "test"*/ } });

            var handler = new GetDoctorTypesQueryHandler(_doctorTypeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<DoctorType>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task DoctorType_CreateCommand_Success()
        {
            DoctorType rt = null;
            //Arrange
            var command = new CreateDoctorTypeCommand();
            //propertyler buraya yazılacak
            //command.DoctorTypeName = "deneme";

            _doctorTypeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DoctorType, bool>>>()))
                        .ReturnsAsync(rt);

            _doctorTypeRepository.Setup(x => x.Add(It.IsAny<DoctorType>())).Returns(new DoctorType());

            var handler = new CreateDoctorTypeCommandHandler(_doctorTypeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _doctorTypeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task DoctorType_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDoctorTypeCommand();
            //propertyler buraya yazılacak 
            //command.DoctorTypeName = "test";

            _doctorTypeRepository.Setup(x => x.Query())
                                           .Returns(new List<DoctorType> { new DoctorType() { /*TODO:propertyler buraya yazılacak DoctorTypeId = 1, DoctorTypeName = "test"*/ } }.AsQueryable());

            _doctorTypeRepository.Setup(x => x.Add(It.IsAny<DoctorType>())).Returns(new DoctorType());

            var handler = new CreateDoctorTypeCommandHandler(_doctorTypeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task DoctorType_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDoctorTypeCommand();
            //command.DoctorTypeName = "test";

            _doctorTypeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DoctorType, bool>>>()))
                        .ReturnsAsync(new DoctorType() { /*TODO:propertyler buraya yazılacak DoctorTypeId = 1, DoctorTypeName = "deneme"*/ });

            _doctorTypeRepository.Setup(x => x.Update(It.IsAny<DoctorType>())).Returns(new DoctorType());

            var handler = new UpdateDoctorTypeCommandHandler(_doctorTypeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _doctorTypeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task DoctorType_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDoctorTypeCommand();

            _doctorTypeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<DoctorType, bool>>>()))
                        .ReturnsAsync(new DoctorType() { /*TODO:propertyler buraya yazılacak DoctorTypeId = 1, DoctorTypeName = "deneme"*/});

            _doctorTypeRepository.Setup(x => x.Delete(It.IsAny<DoctorType>()));

            var handler = new DeleteDoctorTypeCommandHandler(_doctorTypeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _doctorTypeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

