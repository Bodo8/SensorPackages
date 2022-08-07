using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Infrastructure.Mappings
{
    public class CsvImportedObjectMap : ClassMap<ImportedObject>
    {
        public CsvImportedObjectMap()
        {
            Map(m => m.Name).Name("Name");
            Map(m => m.Type).Name("Type");
            Map(m => m.Schema).Name("Schema");
            Map(m => m.ParentName).Name("ParentName");
            Map(m => m.ParentType).Name("ParentType");
            Map(m => m.DataType).Name("DataType");
            Map(m => m.IsNullable).TypeConverter<BooleanConverterCsv>();
        }
    }
}
