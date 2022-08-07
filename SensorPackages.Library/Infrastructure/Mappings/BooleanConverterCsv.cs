using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace SensorPackages.Library.Infrastructure.Mappings
{
    public class BooleanConverterCsv : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            return text == "1" ? true : false;
        }

        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return (bool)value == true ? "1" : "0";
        }
    }
}