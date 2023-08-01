using System;
using LegacyApp.Models;
using LegacyApp.Repositories;

namespace LegacyApp.Services;

public class UserFactory : IUserFactory
{
    //this should be injected
    private readonly IClientRepository _clientRepository;
    private readonly IUserCreditService _userCreditService;

    public UserFactory(IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        _clientRepository = clientRepository;
        _userCreditService = userCreditService;
    }

    public UserFactory() : this(new ClientRepository(), new UserCreditServiceClient())
    {
    }

    public User GetUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
    {
        var client = _clientRepository.GetById(clientId);

        var (hasCreditLimit, creditFactor) = client.Name switch
        {
            ClientNames.VeryImportantClient => (false, 1),
            ClientNames.ImportantClient => (true, 2),
            _ => (true, 1)
        };

        return new User()
        {
            Client = client,
            DateOfBirth = dateOfBirth,
            EmailAddress = email,
            Firstname = firstname,
            Surname = surname,
            HasCreditLimit = hasCreditLimit,
            CreditLimit = _userCreditService.GetCreditLimit(firstname, surname, dateOfBirth) * creditFactor
        };
    }
}

public interface IUserFactory
{
    User GetUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId);
}