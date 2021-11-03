using AutoFixture;
using Core.Models;
using Core.Repositories;
using Core.UseCases;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CoreTests
{
    public class CreateOrderTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IOrderTypeRepository> _orderTypeRepositoryMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public CreateOrderTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderTypeRepositoryMock = new Mock<IOrderTypeRepository>();
        }
        [Fact]
        public void HandleAsync_WhenNullOrder_ShouldThrowException()
        {
            #region Arrange
            var _sut = new CreateOrder(_orderTypeRepositoryMock.Object, _orderRepositoryMock.Object);
            #endregion

            #region Act
            Action res = () => _sut.HandleAsync(null).GetAwaiter().GetResult();
            #endregion

            #region Assert
            res.Should().Throw<ArgumentNullException>();
            #endregion
        }

        [Fact]
        public void HandleAsync_WhenOrderWithoutLines_ShouldStoreWithoutLines()
        {
            #region Arrange
            _fixture.Customize<Order>(c => c.Without(o => o.OrderLines));
            var order = _fixture.Create<Order>();
            var expectedOrderId = _fixture.Create<int>();
            _orderRepositoryMock
                .Setup(or => or.CreateOrderAsync(It.IsAny<Order>()))
                .Returns(Task.FromResult(expectedOrderId));

            var _sut = new CreateOrder(_orderTypeRepositoryMock.Object, _orderRepositoryMock.Object);
            #endregion

            #region Act
            var res = _sut.HandleAsync(order).GetAwaiter().GetResult();
            #endregion

            #region Assert
            res.Should().Be(expectedOrderId);

            _orderRepositoryMock
                .Verify(otr => otr.CreateOrderAsync(It.IsAny<Order>()), Times.Once);

            _orderTypeRepositoryMock
                .Verify(otr => otr.GetProductTypeAsync(It.IsAny<string>()), Times.Never);

            _orderRepositoryMock
                .Verify(otr => otr.AddOrderLineToOrder(It.IsAny<int>(), It.IsAny<OrderLine>()), Times.Never);
            #endregion
        }

        [Fact]
        public void HandleAsync_WhenOrderLineWithQuantityZero_ShouldStoreWithoutLines()
        {
            #region Arrange
            _fixture.Customize<OrderLine>(c => c.With(o => o.Quantity, 0));
            var order = _fixture.Create<Order>();
            var expectedOrderId = _fixture.Create<int>();
            _orderRepositoryMock
                .Setup(or => or.CreateOrderAsync(It.IsAny<Order>()))
                .Returns(Task.FromResult(expectedOrderId));

            var _sut = new CreateOrder(_orderTypeRepositoryMock.Object, _orderRepositoryMock.Object);
            #endregion

            #region Act
            var res = _sut.HandleAsync(order).GetAwaiter().GetResult();
            #endregion

            #region Assert
            res.Should().Be(expectedOrderId);

            _orderRepositoryMock
                .Verify(otr => otr.CreateOrderAsync(It.IsAny<Order>()), Times.Once);

            _orderTypeRepositoryMock
                .Verify(otr => otr.GetProductTypeAsync(It.IsAny<string>()), Times.Never);

            _orderRepositoryMock
                .Verify(otr => otr.AddOrderLineToOrder(It.IsAny<int>(), It.IsAny<OrderLine>()), Times.Never);
            #endregion
        }

        [Fact]
        public void HandleAsync_WhenOrderLineWithPositiveQuantity_ShouldStoreWithLines()
        {
            #region Arrange
            var order = _fixture.Create<Order>();
            var expectedOrderId = _fixture.Create<int>();
            _orderRepositoryMock
                .Setup(or => or.CreateOrderAsync(It.IsAny<Order>()))
                .Returns(Task.FromResult(expectedOrderId));

            _orderRepositoryMock
                .Setup(or => or.AddOrderLineToOrder(It.IsAny<int>(), It.IsAny<OrderLine>()))
                .Returns(Task.FromResult(_fixture.Create<int>()));

            _orderTypeRepositoryMock
                .Setup(or => or.GetProductTypeAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(_fixture.Create<ProductType>()));

            var _sut = new CreateOrder(_orderTypeRepositoryMock.Object, _orderRepositoryMock.Object);
            #endregion

            #region Act
            var res = _sut.HandleAsync(order).GetAwaiter().GetResult();
            #endregion

            #region Assert
            res.Should().Be(expectedOrderId);

            _orderRepositoryMock
                .Verify(otr => otr.CreateOrderAsync(It.IsAny<Order>()), Times.Once);

            _orderTypeRepositoryMock
                .Verify(otr => otr.GetProductTypeAsync(It.IsAny<string>()), Times.Exactly(order.OrderLines.Count));

            _orderRepositoryMock
                .Verify(otr => otr.AddOrderLineToOrder(expectedOrderId, It.IsAny<OrderLine>()), Times.Exactly(order.OrderLines.Count));
            #endregion
        }
    }
}
