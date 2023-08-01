using LegacyApp.Services;
using Moq;

namespace App.Test;

public class UserValidatorTests
{
    private IUserValidator _userValidator;
    private Mock<IDateTimeProvider> _dateTimeProvider;

    public UserValidatorTests()
    {
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _userValidator = new UserValidator(_dateTimeProvider.Object);
    }

    [Test]
    public void DateOfBirthProvided30Years_UserIsValid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "test", "test@test.pl", DateTime.Today.AddYears(-30));
        Assert.IsTrue(isUserValid);
    }

    [Test]
    public void DateOfBirthProvided21YearsMinus3Months_UserIsValid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "test", "test@test.pl", DateTime.Today.AddYears(-21).AddMonths(-3));
        Assert.IsTrue(isUserValid);
    }

    [Test]
    public void DateOfBirthProvided21Years_UserIsInvalid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "test", "test@test.pl", DateTime.Today.AddYears(-21));
        Assert.IsTrue(isUserValid);
    }

    [Test]
    public void DateOfBirthProvided19Years_UserIsInvalid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "test", "test@test.pl", DateTime.Today.AddYears(-19));
        Assert.IsFalse(isUserValid);
    }


    [Test]
    public void DateOfBirthProvidedWrongEmail_UserIsInvalid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "test", "test@testpl", DateTime.Today.AddYears(-19));
        Assert.IsFalse(isUserValid);
    }

    [Test]
    public void DateOfBirthProvidedWrongFirstName_UserIsInvalid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("", "test", "test@testpl", DateTime.Today.AddYears(-19));
        Assert.IsFalse(isUserValid);
    }

    [Test]
    public void DateOfBirthProvidedWrongFirstSurname_UserIsInvalid()
    {
        _dateTimeProvider.Setup(x => x.GetToday()).Returns(DateTime.Now);
        var isUserValid = _userValidator.ValidateUserData("test", "", "test@testpl", DateTime.Today.AddYears(-19));
        Assert.IsFalse(isUserValid);
    }
}