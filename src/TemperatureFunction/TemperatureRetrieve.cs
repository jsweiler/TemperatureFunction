using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Flurl.Http;
using Flurl;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TemperatureFunction
{
    public static class TemperatureRetrieve
    {
        [FunctionName("TemperatureRetrieve")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("* */5 * * * *")]TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");

            var url = "http://api.openweathermap.org/data/2.5/weather?zip=17517&appid=085424cf14a8269a2c0cbff5820fe231&units=imperial";
            var weather = await url
                .GetJsonAsync<Rootobject>();

            var config = new ConfigurationBuilder()
              .SetBasePath(context.FunctionAppDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
              .AddEnvironmentVariables()
              .Build();
            var val = config.GetConnectionString("LocalDb");

            var storageAccount = CloudStorageAccount.Parse(val);
            var tableClient = storageAccount.CreateCloudTableClient();
            var tempTable = tableClient.GetTableReference("TemperatureRecords");
            await tempTable.CreateIfNotExistsAsync();
            var insert = TableOperation.Insert(new TemperatureEntity(weather.main.temp_min.ToString()));
            await tempTable.ExecuteAsync(insert);
            

            log.Info("done.");
        }
    }
}
