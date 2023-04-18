using ENSEK.Contracts;
using ENSEK.Entities.Models;

namespace TestProjectAPI
{
    public class MeterReadingServiceFake : IMeterReadingRepository
    {
        private readonly List<MeterReading> _meterReadings;

        public MeterReadingServiceFake()
        {
            _meterReadings = new List<MeterReading>()
            {
            new MeterReading( "2344",DateTime.Parse("2/04/2019 09:24"),  "1002"),
              new MeterReading(    "2233",DateTime.Parse("2/04/2019 12:25"),    "323"),
                  new MeterReading(  "8766",DateTime.Parse("2/04/2019 12:25"),    "3440"),
                      new MeterReading(  "2344",DateTime.Parse("2/04/2019 12:25"),    "1002")


            };
        }

        public IEnumerable<MeterReading> GetMeterReadings()
        {
            return _meterReadings;
        }

        public List<MeterReading> UpdateMeterReadings(List<MeterReading> newItems)
        {

            _meterReadings.AddRange(_meterReadings);
            return _meterReadings;
        }

        List<MeterReading> IMeterReadingRepository.GetMeterReadings()
        {
            return _meterReadings;

        }

        void IMeterReadingRepository.UpdateMeterReadings(List<MeterReading> meterReadings)
        {
            _meterReadings.AddRange(meterReadings);
        }
    }
}
