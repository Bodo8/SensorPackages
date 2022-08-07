
using SensorPackages.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorPackages.Library.Infrastructure.FileProcessors.Interfaces
{
    public interface ISourceProcessor
    {
        List<ImportedObject> GetItems(string fileName);
        void SaveItems(List<ImportedObject> items, string fileName);
    }
}
