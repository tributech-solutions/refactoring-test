using LegacyApp.Models;

namespace LegacyApp.Validators;

public class CreditValidator : ICreditValidator
{
    public bool ValidateCredit(bool hasCreditLimit, int creditLimit)
    {
        return !hasCreditLimit || creditLimit >= 500;
    }
}

public interface ICreditValidator
{
    bool ValidateCredit(bool hasCreditLimit, int creditLimit);
}