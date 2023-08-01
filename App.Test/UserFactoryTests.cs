using LegacyApp.Models;
using LegacyApp.Repositories;
using LegacyApp.Services;
using Moq;

namespace App.Test;

public class UserFactoryTests
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IUserCreditService> _userCreditServiceMock;
    private UserFactory _userFactory;

    public UserFactoryTests()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _userCreditServiceMock = new Mock<IUserCreditService>();
        _userFactory = new UserFactory(_clientRepositoryMock.Object, _userCreditServiceMock.Object);
    }

    [Test]
    [TestCase(ClientNames.VeryImportantClient, false, 1)]
    [TestCase(ClientNames.ImportantClient, true, 2)]
    [TestCase("testing", true, 1)]
    public void UserFactoryClientNameTests(string clientName, bool hasCreditLimit, int creditLimit)
    {
        _clientRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Client() { Id = 1, Name = clientName });
        _userCreditServiceMock.Setup(x => x.GetCreditLimit(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1);
        var user = _userFactory.GetUser("test", "test", "test", DateTime.Today, 1);
        Assert.That(user.HasCreditLimit, Is.EqualTo(hasCreditLimit));
        Assert.That(user.CreditLimit, Is.EqualTo(creditLimit));
    }
}