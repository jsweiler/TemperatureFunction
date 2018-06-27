using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace TemperatureFunction
{
    public class TemperatureEntity : TableEntity
    {
        public TemperatureEntity(string temperature)
        {
            RowKey = Guid.NewGuid().ToString();
            PartitionKey = temperature;
            Temperature = temperature;
        }

        public string Temperature { get; set; }
    }
}
