
using Business.Handlers.AppointmentTimes.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.AppointmentTimes.Queries.GetAppointmentTimeQuery;
using Entities.Concrete;
using static Business.Handlers.AppointmentTimes.Queries.GetAppointmentTimesQuery;
using static Business.Handlers.AppointmentTimes.Commands.CreateAppointmentTimeCommand;
using Business.Handlers.AppointmentTimes.Commands;
using Business.Constants;
using static Business.Handlers.AppointmentTimes.Commands.UpdateAppointmentTimeCommand;
using static Business.Handlers.AppointmentTimes.Commands.DeleteAppointmentTimeCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class AppointmentTimeHandlerTests
    {
        Mock<IAppointmentTimeRepository> _appointmentTimeRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _appointmentTimeRepository = new Mock<IAppointmentTimeRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task AppointmentTime_GetQuery_Success()
        {
            //Arrange
            var query = new GetAppointmentTimeQuery();

            _appointmentTimeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AppointmentTime, bool>>>())).ReturnsAsync(new AppointmentTime()
//propertyler buraya yazılacak
//{																		
//AppointmentTimeId = 1,
//AppointmentTimeName = "Test"
//}
);

            var handler = new GetAppointmentTimeQueryHandler(_appointmentTimeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.AppointmentTimeId.Should().Be(1);

        }

        [Test]
        public async Task AppointmentTime_GetQueries_Success()
        {
            //Arrange
            var query = new GetAppointmentTimesQuery();

            _appointmentTimeRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<AppointmentTime, bool>>>()))
                        .ReturnsAsync(new List<AppointmentTime> { new AppointmentTime() { /*TODO:propertyler buraya yazılacak AppointmentTimeId = 1, AppointmentTimeName = "test"*/ } });

            var handler = new GetAppointmentTimesQueryHandler(_appointmentTimeRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<AppointmentTime>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task AppointmentTime_CreateCommand_Success()
        {
            AppointmentTime rt = null;
            //Arrange
            var command = new CreateAppointmentTimeCommand();
            //propertyler buraya yazılacak
            //command.AppointmentTimeName = "deneme";

            _appointmentTimeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AppointmentTime, bool>>>()))
                        .ReturnsAsync(rt);

            _appointmentTimeRepository.Setup(x => x.Add(It.IsAny<AppointmentTime>())).Returns(new AppointmentTime());

            var handler = new CreateAppointmentTimeCommandHandler(_appointmentTimeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _appointmentTimeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task AppointmentTime_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateAppointmentTimeCommand();
            //propertyler buraya yazılacak 
            //command.AppointmentTimeName = "test";

            _appointmentTimeRepository.Setup(x => x.Query())
                                           .Returns(new List<AppointmentTime> { new AppointmentTime() { /*TODO:propertyler buraya yazılacak AppointmentTimeId = 1, AppointmentTimeName = "test"*/ } }.AsQueryable());

            _appointmentTimeRepository.Setup(x => x.Add(It.IsAny<AppointmentTime>())).Returns(new AppointmentTime());

            var handler = new CreateAppointmentTimeCommandHandler(_appointmentTimeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task AppointmentTime_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateAppointmentTimeCommand();
            //command.AppointmentTimeName = "test";

            _appointmentTimeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AppointmentTime, bool>>>()))
                        .ReturnsAsync(new AppointmentTime() { /*TODO:propertyler buraya yazılacak AppointmentTimeId = 1, AppointmentTimeName = "deneme"*/ });

            _appointmentTimeRepository.Setup(x => x.Update(It.IsAny<AppointmentTime>())).Returns(new AppointmentTime());

            var handler = new UpdateAppointmentTimeCommandHandler(_appointmentTimeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _appointmentTimeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task AppointmentTime_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteAppointmentTimeCommand();

            _appointmentTimeRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<AppointmentTime, bool>>>()))
                        .ReturnsAsync(new AppointmentTime() { /*TODO:propertyler buraya yazılacak AppointmentTimeId = 1, AppointmentTimeName = "deneme"*/});

            _appointmentTimeRepository.Setup(x => x.Delete(It.IsAny<AppointmentTime>()));

            var handler = new DeleteAppointmentTimeCommandHandler(_appointmentTimeRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _appointmentTimeRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

