using System;
using LegacyApp.Repositories;
using LegacyApp.Validators;

namespace LegacyApp.Services
{
    public class UserService
    {
        private readonly IUserValidator _userValidator;
        private readonly IUserFactory _userFactory;
        private readonly ICreditValidator _creditValidator;
        private readonly IUserDataAccess _userDataAccess;

        public UserService() : this(new UserValidator(), new UserFactory(), new CreditValidator(), new UserDataAccessProxy())
        {
        }

        public UserService(IUserValidator userValidator, IUserFactory userFactory, ICreditValidator creditValidator, IUserDataAccess userDataAccess)
        {
            _userValidator = userValidator;
            _userFactory = userFactory;
            _creditValidator = creditValidator;
            _userDataAccess = userDataAccess;
        }

        public bool AddUser(string firstname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var isUserValid = _userValidator.ValidateUserData(firstname, surname, email, dateOfBirth);
            if (!isUserValid)
            {
                return false;
            }

            var user = _userFactory.GetUser(firstname, surname, email, dateOfBirth, clientId);

            if (!_creditValidator.ValidateCredit(user.HasCreditLimit, user.CreditLimit))
            {
                return false;
            }

            try
            {
                _userDataAccess.AddUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return true;
        }
    }
}