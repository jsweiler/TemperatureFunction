using System;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace TemperatureAlert
{
    public static class TemperatureAlert
    {
        [FunctionName("TemperatureAlert")]
        public static void Run([CosmosDBTrigger(databaseName: "TablesDB", 
            collectionName: "TemperatureRecords",
            ConnectionStringSetting = "LocalDb")]IReadOnlyList<Document> documents, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {documents}");

            if(documents?.Count > 0)
            {

            }
        }
    }
}
