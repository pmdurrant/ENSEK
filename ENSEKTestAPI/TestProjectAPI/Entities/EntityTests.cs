using ENSEK.Entities.Models;

namespace TestProjectAPI.Entities
{
    public class EntityTests
    {
        [Fact]
        public void Create_Entity()
        {
            var expected = new MeterReading() { AccountId = "2344", MeterReadingDateTime = DateTime.Parse("2/04/2019 09:24"), MeterReadValue = "1002" };

            MeterReading meterreading = new MeterReading("2344", DateTime.Parse("2/04/2019 09:24"), "1002");

            Assert.Equal(expected.Id, meterreading.Id);

            Assert.Equal(expected.MeterReadValue, meterreading.MeterReadValue);

            Assert.Equal(expected.MeterReadingDateTime, meterreading.MeterReadingDateTime);


        }


        [Fact]


        public void Bad_Entity()

        {
            var expected = new MeterReading() { MeterReadingDateTime = DateTime.Parse("2/04/2019 09:24"), MeterReadValue = "1002" };

            MeterReading meterreading = new MeterReading("", DateTime.Parse("2/04/2019 09:24"), "1002");

            Assert.Equal(meterreading.Id, 0);

            Assert.Equal(expected.MeterReadValue, meterreading.MeterReadValue);

            Assert.Equal(expected.MeterReadingDateTime, meterreading.MeterReadingDateTime);

        }
    }

}
