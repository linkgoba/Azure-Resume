using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure;
using Microsoft.Azure.WebJobs.Extensions.Http;
//using Microsoft.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Table;
//using Microsoft.Azure.DocumentDB.Core;
//using Microsoft.Azure.WebJobs.Extensions.Storage;
//using Microsoft.Azure.Functions.Worker.Extensions.ServiceBus;
using System.Net.Http;
using System.Text;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace Company.Function
{
public static class GetResumeCounter
{
    [FunctionName("GetResumeCounter")]
    public static /*async Task<IActionResult>*/HttpResponseMessage Run
    ([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
    [CosmosDB(databaseName:"AzureResume",collectionName: "Counter", ConnectionStringSetting ="AzureResumeConnectionString", Id="1", PartitionKey = "1")] Counter counter,
    [CosmosDB(databaseName:"AzureResume",collectionName: "Counter", ConnectionStringSetting ="AzureResumeConnectionString", Id="1", PartitionKey = "1")] out Counter updatedcounter,
    
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        updatedcounter = counter;
        updatedcounter.Count += 1;

        var jsonToReturn = JsonConvert.SerializeObject (counter);
        /*string name = req.Query["name"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);
        name = name ?? data?.name;

        string responseMessage = string.IsNullOrEmpty(name)
            ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Hello, {name}. This HTTP triggered function executed successfully.";*/

        return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/jason")
        };
    }
}

}
