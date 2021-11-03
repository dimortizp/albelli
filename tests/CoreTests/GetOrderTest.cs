using AutoFixture;
using Core.Models;
using Core.Repositories;
using Core.UseCases;
using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace CoreTests
{
    public class GetOrderTest
    {
        private readonly Fixture _fixture = new Fixture();
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public GetOrderTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
        }

        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(2, 1, 1, 2)]
        [InlineData(2, 2, 1, 1)]
        [InlineData(3, 2, 1, 2)]
        [InlineData(1, 1, 2, 2)]
        [InlineData(2, 1, 2, 4)]
        [InlineData(2, 2, 2, 2)]
        [InlineData(3, 2, 2, 4)]
        public void HandleAsync_OneOrderLineShouldCalculate_CorrectlyBandWidth(int quantity, int stackAmount, decimal width, decimal expected)
        {
            #region Arrange
            _fixture.RepeatCount = 1;
            _fixture.Customize<OrderLine>(
                c =>
                    c.With(x => x.Quantity, quantity)
                );
            _fixture.Customize<ProductType>(
                c =>
                    c
                        .With(x => x.StackAmount, stackAmount)
                        .With(x => x.Width, width)
                );

            var order = _fixture.Create<Order>();
            var orderId = _fixture.Create<int>();

            _orderRepositoryMock
                .Setup(or => or.GetOrderAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(order));
            var _sut = new GetOrder(_orderRepositoryMock.Object);
            #endregion

            #region Act
            var res = _sut.HandleAsync(orderId).GetAwaiter().GetResult();
            #endregion

            #region Assert
            res.RequiredBinWidth.Should().Be(expected);
            #endregion
        }
    }
}
