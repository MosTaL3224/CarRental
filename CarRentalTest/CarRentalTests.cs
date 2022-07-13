using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.DBTools;
using CarRental.Utils;
namespace CarRentalTest
{
    [TestClass]
    public class CarRentalTests
    {
        [TestMethod]
        public void CheckDatabaseConnection()
        {
            CarRentalDbContext context = new CarRentalDbContext();
            Assert.AreEqual(true, context.Database.CanConnect());
        }
        [TestMethod]
        public void CheckDatabase()
        {
            CarRentalDbContext context = new CarRentalDbContext();
            Assert.AreEqual("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);
        }
        [TestMethod]
        public void CheckRents()
        {
            CarRentalDbContext context = new CarRentalDbContext();
            DbUtils dbUtils = new DbUtils(context);
            var rentList = dbUtils.GetRentContainers();
            Assert.AreEqual(true, rentList.Count > 0);
        }
        [TestMethod]
        public void CheckPrice()
        {
            var price = CarRentalUtils.GetRentPrice(300, 200, 10);
            Assert.AreEqual(5000, price);
        }
        [TestMethod]
        public void CheckDoubleValidator()
        {
            var result = CarRentalUtils.CheckValueIsDouble("5000");
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void CheckDoubleValidator2()
        {
            var result = CarRentalUtils.CheckValueIsDouble("Test");
            Assert.AreEqual(false, result);
        }
    }
}