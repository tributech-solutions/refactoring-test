using LegacyApp.Models;
using LegacyApp.Repositories;
using LegacyApp.Services;
using LegacyApp.Validators;
using Moq;

namespace App.Test;

[TestFixture]
public class UserServiceTests
{
    private Mock<IUserFactory> _userFactoryMock;
    private Mock<IUserValidator> _userValidatorMock;
    private Mock<ICreditValidator> _creditValidatorMock;
    private Mock<IUserDataAccess> _userDataAccessMock;
    private UserService _userService;

    [SetUp]
    public void SetupBeforeEachTest()
    {
        _userFactoryMock = new Mock<IUserFactory>();
        _userValidatorMock = new Mock<IUserValidator>();
        _creditValidatorMock = new Mock<ICreditValidator>();
        _userDataAccessMock = new Mock<IUserDataAccess>();
        _userService = new UserService(_userValidatorMock.Object, _userFactoryMock.Object, _creditValidatorMock.Object, _userDataAccessMock.Object);
    }

    [Test]
    public void AddMethodCalledAndSuccess()
    {
        _userFactoryMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new User());
        _userValidatorMock.Setup(x => x.ValidateUserData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(true);
        _creditValidatorMock.Setup(x => x.ValidateCredit(It.IsAny<bool>(), It.IsAny<int>())).Returns(true);

        _userService.AddUser("f1", "su", "em", DateTime.Now, 1);
        _userDataAccessMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
    }

    [Test]
    public void AddMethodNotCalledUserInvalid()
    {
        _userFactoryMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new User());
        _userValidatorMock.Setup(x => x.ValidateUserData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(false);
        _creditValidatorMock.Setup(x => x.ValidateCredit(It.IsAny<bool>(), It.IsAny<int>())).Returns(true);

        _userService.AddUser("f1", "su", "em", DateTime.Now, 1);
        _userDataAccessMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public void AddMethodNotCalledCreditInvalid()
    {
        _userFactoryMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new User());
        _userValidatorMock.Setup(x => x.ValidateUserData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(true);
        _creditValidatorMock.Setup(x => x.ValidateCredit(It.IsAny<bool>(), It.IsAny<int>())).Returns(false);

        _userService.AddUser("f1", "su", "em", DateTime.Now, 1);
        _userDataAccessMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public void AddMethodNotCalledUserCreditInvalid()
    {
        _userFactoryMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new User());
        _userValidatorMock.Setup(x => x.ValidateUserData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(false);
        _creditValidatorMock.Setup(x => x.ValidateCredit(It.IsAny<bool>(), It.IsAny<int>())).Returns(false);

        _userService.AddUser("f1", "su", "em", DateTime.Now, 1);
        _userDataAccessMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Never);
    }
    
    
    [Test]
    public void AddMethodThrowsException()
    {
        _userFactoryMock.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(new User());
        _userValidatorMock.Setup(x => x.ValidateUserData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>())).Returns(true);
        _creditValidatorMock.Setup(x => x.ValidateCredit(It.IsAny<bool>(), It.IsAny<int>())).Returns(true);
        _userDataAccessMock.Setup(x => x.AddUser(It.IsAny<User>())).Throws(new Exception());

        Assert.Throws<Exception>(() => _userService.AddUser("f1", "su", "em", DateTime.Now, 1));
    }
}