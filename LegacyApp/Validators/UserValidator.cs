using System;
using EmailValidation;

namespace LegacyApp.Services;

public class UserValidator : IUserValidator
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public UserValidator() : this(new DateTimeProvider())
    {
    }

    public UserValidator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public bool ValidateUserData(string firstName, string surname, string email, DateTime dateOfBirth)
    {
        if (ValidateNames(firstName, surname))
        {
            Console.WriteLine("Name not valid.");
            return false;
        }

        if (!EmailValidator.Validate(email))
        {
            Console.WriteLine("Mail not valid.");
            return false;
        }

        if (!ValidateAge(dateOfBirth))
        {
            Console.WriteLine("Date of birth not valid.");
            return false;
        }

        return true;
    }

    private bool ValidateNames(string firstName, string surname) =>
        string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(surname);

    private bool ValidateAge(DateTime dateOfBirth)
    {
        var currentDate = _dateTimeProvider.GetToday();
        var age = currentDate.Year - dateOfBirth.Year;
        if (currentDate.Month < dateOfBirth.Month || currentDate.Month == dateOfBirth.Month && currentDate.Day < dateOfBirth.Day)
        {
            age--;
        }

        return age >= 21;
    }
}

public interface IUserValidator
{
    bool ValidateUserData(string firstName,
        string surname,
        string email,
        DateTime dateOfBirth);
}