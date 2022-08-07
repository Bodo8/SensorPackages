﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorPackages.Library.Models
{
    public class ImportedObject
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string? DataType { get; set; }
        public bool? IsNullable { get; set; }
        public double? NumberOfChildren { get; set; }
    }
}
