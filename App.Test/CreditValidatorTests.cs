using LegacyApp.Models;
using LegacyApp.Validators;

namespace App.Test;

public class CreditValidatorTests
{
    private readonly ICreditValidator _creditValidator = new CreditValidator();

    [Test]
    [TestCase(true, 300, false)]
    [TestCase(true, 500, true)]
    [TestCase(true, 499, false)]
    [TestCase(false, 300, true)]
    [TestCase(false, 500, true)]
    [TestCase(false, 499, true)]
    public void CreditLimitTests(bool hasCreditLimit, int creditLimit, bool isCreditValidExpected)
    {
        var isCreditValidActual = _creditValidator.ValidateCredit(hasCreditLimit, creditLimit);
        Assert.That(isCreditValidActual, Is.EqualTo(isCreditValidExpected));
    }
}