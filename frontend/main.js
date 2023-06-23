window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
})
const functionAPIUrl = 'https://getresumecounterkelly.azurewebsites.net/GetResumeCounter?code=Fk81OM7TRAulctda272poAkCvKRuPr0Bs6ogLI2SjBEqAzFu-IzjnA==';
const LocalFunctionApi = 'http://localhost:7071/GetResumeCounter';

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