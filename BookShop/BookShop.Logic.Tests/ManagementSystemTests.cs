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
        [Test]
        public void SellBookTest_()
        {
            var book = new Book(0, string.Empty, string.Empty, new Genre[0]);
            const decimal price = 100m;
            var instance = new BookInstance(DateTime.Now, price, book, 1);
            var repository = new Mock<IBookShopRepository>();
            repository.Setup(r => r.DeleteBook(1)).Returns(instance);
            
            var system = new ManagementSystem(repository.Object, Mock.Of<IBookProvider>());
            system.Balance.Should().Be(price);
        }
    }
}