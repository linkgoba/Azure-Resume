# Azure Resume

## Pre-requisites

- Azure Account
- Visual Studio Code on Ubuntu
- .Net Core 6.0
- Azure CLI
- Azure Functions in Visual Code
- C# extension

## Frontend

1. Create Git respository
2. Copy respository locally
   - On Linux type git clone `git@github.com:linkgoba/azure-resume.git`
3. Copy sample code from the original repository
4. Type: Code . to open Visual Studio
5. Update index.html within the Frontend folder to reflect CV content
6. Create main.js within the Frontend folder with the following code:

   ```
   window.addEventListener('DOMContentLoaded', (event) => {getVisitCount();}) 
   const functionAPIUrl = 'https://getresumecounterkelly.azurewebsites.net/GetResumeCounter?code=Fk81OM7TRAulctda272poAkCvKRuPr0Bs6ogLI2SjBEqAzFu-IzjnA==';  
   const LocalFunctionApi = 'http://localhost:7071/GetResumeCounter'; 
   const getVisitCount = () =>
      {
      let count =30;
      fetch(functionAPIUrl).then(response =>
         { 
         return response.json() 
         }).then (response =>
         { 
         console.log("Website called funtion API."); 
         count = response.count; 
         document.getElementById("counter").innerText = count; 
      }).catch(function(error)
      { 
      console.log(error); 
      }); 
   return count; 
   } 
   ```
The URL in functionAPIURL is created by Azure function on the portal. 
The URL in LocalFunctionApi is the Azure function URL running locally
7. Update GitHub:
   - git add -A
   - git commit -m "updated..."

## Backend

### CosmosDB

1. Create CosmosDB account on Azure portal.
2. Go to Data Explorer to create the database called AzureResume
3. Inside the database create a container called Counter with Partition Key called ID
4. Add item to the container. Go to container and click on Items -> New items and replace id =1 and count = 0

### Azure Function

1. In Visual Code, go to Azure Functions and create a local project (as HTTP Trigger) called GetResumeCounter and add the following code:

   ```
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
   ([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, [CosmosDB(databaseName:"AzureResume",collectionName: "Counter", ConnectionStringSetting="AzureResumeConnectionString", Id="1", PartitionKey = "1")] Counter counter, [CosmosDB(databaseName:"AzureResume",collectionName: "Counter", ConnectionStringSetting="AzureResumeConnectionString", Id="1", PartitionKey = "1")] out Counter updatedcounter, ILogger log) 
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
   ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the
   request body for a personalized response." 
   : $"Hello, {name}. This HTTP triggered function executed successfully.";*/

   return new HttpResponseMessage(System.Net.HttpStatusCode.OK) 
   { 
   Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/jason") 
   }; 
   } 
   }
   ```

2. RUn "func host start"
3. Test local URL: `localhost:7071/api/GetResumeCounter`
4. Edit localsettings.json file to add the connection string

   ```
   { 
   "IsEncrypted": false, 
   "Values": { 
   "AzureWebJobsStorage": "", 
   "FUNCTIONS_WORKER_RUNTIME": "dotnet", 
   "AzureResumeConnectionString": "AccountEndpoint=https://kiutba
   database.documents.azure.com:443/;AccountKey=IRmwOxtbMHMRrRKXb2tiBC5gqRz7oS3jdXGh
   hGS7Q6Z6ASHkbgTWuMPt12xhNPNIh9bs1iWR5G7DACDbPkCSxQ==;" 
   }, 
    "Host": 
   { 
   "CORS": "*" 
   } 
   }
   ```
   The connection string is the same key (Primary Connection string) used in the Cosmos account under 'key settings'

5. Create Counter.cs file with the following code:

   ```
   using Newtonsoft.Json; 
   using Microsoft.AspNetCore.Mvc.NewtonsoftJson; 
   namespace Company.Function 
   { 
   public class Counter 
   { 
   [JsonProperty(PropertyName="id")] 
   public string Id {get; set;} 
   [JsonProperty(PropertyName="count")] 
   public int Count {get; set;} 
   } 
   }
   ```

### Deploy to Azure

1. Use the option in Visual Code to create an Azure Function on Azure 
2. Go to Azure portal -> GetResumeCounter function and add Application setting called AzureResumeConnectionString, use the value from the localsettings.json.
3. Enable CORS on the Azure Function
4. Make sure Azure Storage is installed on Visual Code. Right click on the frontend folder  and select: Deploy to static website ia Azure Storage 
5. Add the URL in CORS to make sure the function links to storage account: (https://kiutstorage.z6.web.core.windows.net) This URL is generated during storage account deployment. 
6. Create Azure CDN azureresumekelly 
   
