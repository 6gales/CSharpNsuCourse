using System;
using BookShop.Data;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace BookShop.Logic.Tests
{
    [TestFixture]
    public sealed class ManagementSystemTests
    {
    #warning закоменченый код - зло. если код не нужен - лучше его удалить
        //[Test]
        //public void SellBookTest_()
        //{
        //    var book = new Book(0, string.Empty, string.Empty, new Genre[0]);
        //    const decimal price = 100m;
        //    var instance = new BookInstance(DateTime.Now, price, book, 1);
        //    var repository = new Mock<IBookShopRepository>();
        //    repository.Setup(r => r.DeleteBook(1)).Returns(instance);

        //    var system = new ManagementSystem(repository.Object, Mock.Of<IBookProvider>());
        //    system.SellBook(1);
        //    system.Balance.Should().Be(price);
        //}

        //[Test]
        //public void SystemUpdateTest()
        //{
        //    var repository = new Mock<IBookShopRepository>();
        //    repository.SetupGet(r => r.Capacity).Returns(110);
        //    repository.SetupGet(r => r.Count).Returns(10);
        //    var provider = new Mock<IBookProvider>();
        //    provider.Setup(p => p.ProvideBooks(It.IsAny<decimal>(), It.IsAny<int>()))
        //        .Returns(new BookInstance[0])
        //        .Verifiable();

        //    var system = new ManagementSystem(repository.Object, provider.Object);
        //    system.SystemUpdate(DateTime.Parse("10/10/2020"), new Discount[0]);

        //    provider.Verify(p => p.ProvideBooks(It.IsAny<decimal>(), It.IsAny<int>()), Times.Once);
        //}
    }
}