# Azure Resume

## Pre-requisites

- Azure Account
- Visual Studio Code
- .Net Core 6.0
- Azure CLI
- Azure Functions in Visual Code
- C# extension

## Frontend

1. Created Git respository
2. Copied respository locally
   - On Linux typed git clone (git@github.com:linkgoba/azure-resume.git)
3. Copied sample code from the original repository
4. Typed: Code . to open Visual Studio
5. Updated index.html within the Frontend folder to reflect CV content
6. Created main.js within the Frontend folder with the following code:
```
window.addEventListener('DOMContentLoaded', (event) => { 
getVisitCount(); 
}) 
const functionAPIUrl = '
 https://getresumecounterkelly.azurewebsites.net/GetResumeCounter?
 code=Fk81OM7TRAulctda272poAkCvKRuPr0Bs6ogLI2SjBEqAzFu-IzjnA==';  --> URL created by
 Azure function on the portal.  Inser  this after function is created. 
const LocalFunctionApi = '
 http://localhost:7071/GetResumeCounter'; -->local URL 
const getVisitCount = () => { 
let count =30; 
fetch(functionAPIUrl).then(response => { 
return response.json() 
}).then (response => { 
console.log("Website called funtion API."); 
count = response.count; 
document.getElementById("counter").innerText = count; 
}).catch(function(error){ 
console.log(error); 
}); 
return count; 
} 
```
