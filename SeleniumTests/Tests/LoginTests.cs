using NUnit.Framework;
using SeleniumTests.Pages;
using SeleniumTests.Utilities;

namespace SeleniumTests.Tests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void SuccessfulLogin_ShouldNavigateToInventory()
        {
            var login = new LoginPage(Driver);
            login.GoTo();
            login.Login("standard_user", "secret_sauce");

            var inventory = new InventoryPage(Driver);
            Assert.IsTrue(inventory.IsLoaded(), "Inventory page should be loaded after successful login.");
        }

        [Test]
        public void LockedOutUser_ShouldShowErrorMessage()
        {
            var login = new LoginPage(Driver);
            login.GoTo();
            login.Login("locked_out_user", "secret_sauce");

            var error = login.GetErrorMessage();
            Assert.IsTrue(error.Contains("locked out"), "Locked out user should see an appropriate error message.");
        }

        [Test]
        public void InvalidPassword_ShouldShowErrorMessage()
        {
            var login = new LoginPage(Driver);
            login.GoTo();
            login.Login("standard_user", "wrong_password");

            var error = login.GetErrorMessage();
            Assert.IsTrue(!string.IsNullOrEmpty(error), "Invalid login should produce an error message.");
        }
    }
}
