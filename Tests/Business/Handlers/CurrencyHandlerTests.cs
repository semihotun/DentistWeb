
using Business.Handlers.Currencies.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Currencies.Queries.GetCurrencyQuery;
using Entities.Concrete;
using static Business.Handlers.Currencies.Queries.GetCurrenciesQuery;
using static Business.Handlers.Currencies.Commands.CreateCurrencyCommand;
using Business.Handlers.Currencies.Commands;
using Business.Constants;
using static Business.Handlers.Currencies.Commands.UpdateCurrencyCommand;
using static Business.Handlers.Currencies.Commands.DeleteCurrencyCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CurrencyHandlerTests
    {
        Mock<ICurrencyRepository> _currencyRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _currencyRepository = new Mock<ICurrencyRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Currency_GetQuery_Success()
        {
            //Arrange
            var query = new GetCurrencyQuery();

            _currencyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Currency, bool>>>())).ReturnsAsync(new Currency()
//propertyler buraya yazılacak
//{																		
//CurrencyId = 1,
//CurrencyName = "Test"
//}
);

            var handler = new GetCurrencyQueryHandler(_currencyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CurrencyId.Should().Be(1);

        }

        [Test]
        public async Task Currency_GetQueries_Success()
        {
            //Arrange
            var query = new GetCurrenciesQuery();

            _currencyRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Currency, bool>>>()))
                        .ReturnsAsync(new List<Currency> { new Currency() { /*TODO:propertyler buraya yazılacak CurrencyId = 1, CurrencyName = "test"*/ } });

            var handler = new GetCurrenciesQueryHandler(_currencyRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Currency>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Currency_CreateCommand_Success()
        {
            Currency rt = null;
            //Arrange
            var command = new CreateCurrencyCommand();
            //propertyler buraya yazılacak
            //command.CurrencyName = "deneme";

            _currencyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Currency, bool>>>()))
                        .ReturnsAsync(rt);

            _currencyRepository.Setup(x => x.Add(It.IsAny<Currency>())).Returns(new Currency());

            var handler = new CreateCurrencyCommandHandler(_currencyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _currencyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Currency_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCurrencyCommand();
            //propertyler buraya yazılacak 
            //command.CurrencyName = "test";

            _currencyRepository.Setup(x => x.Query())
                                           .Returns(new List<Currency> { new Currency() { /*TODO:propertyler buraya yazılacak CurrencyId = 1, CurrencyName = "test"*/ } }.AsQueryable());

            _currencyRepository.Setup(x => x.Add(It.IsAny<Currency>())).Returns(new Currency());

            var handler = new CreateCurrencyCommandHandler(_currencyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Currency_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCurrencyCommand();
            //command.CurrencyName = "test";

            _currencyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Currency, bool>>>()))
                        .ReturnsAsync(new Currency() { /*TODO:propertyler buraya yazılacak CurrencyId = 1, CurrencyName = "deneme"*/ });

            _currencyRepository.Setup(x => x.Update(It.IsAny<Currency>())).Returns(new Currency());

            var handler = new UpdateCurrencyCommandHandler(_currencyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _currencyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Currency_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCurrencyCommand();

            _currencyRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Currency, bool>>>()))
                        .ReturnsAsync(new Currency() { /*TODO:propertyler buraya yazılacak CurrencyId = 1, CurrencyName = "deneme"*/});

            _currencyRepository.Setup(x => x.Delete(It.IsAny<Currency>()));

            var handler = new DeleteCurrencyCommandHandler(_currencyRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _currencyRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

