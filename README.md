# backend-coding-challenge-solution
My solution to Gemography "backend-coding-challenge" as a hiring process and it about calling github search api to get trending repositories 
and most starred repos created in the last 30 days ( from now ) in descending  order.
## Functional specs
- Develop a REST microservice that list the languages used by the 100 trending public repos on GitHub.
- For every language, you need to calculate the attributes below:
    - Number of repos using this language.
    - The list of repos using the language.
## Requirements
- DotNet SDK   https://dotnet.microsoft.com/download

## Things To Do Before Run The Project
- first go to local database file at gemography_backend-coding-challenge\DataLayer\Models\GemographyDatabase.mdf and copy its full path by these steps
![image](https://user-images.githubusercontent.com/17914516/143137285-9fca0bc4-7a18-4e16-840a-88097fde0e66.png)
![image](https://user-images.githubusercontent.com/17914516/143137325-82b6127c-f2bc-4062-bc16-9010a1947498.png)

- second go to 2 location and update full local database file at location 1 go to file at gemography_backend-coding-challenge\DataLayer\DBEntities\CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext.cs" then update AttachDbFilename full path with yout new path at this path 
![image](https://user-images.githubusercontent.com/17914516/143137177-734fdf9b-bc9a-4ad2-a91f-d88210e4a0ad.png)

- third go to the second file at gemography_backend-coding-challenge\gemography_backend-coding-challenge\appsettings.json and update full local database path at AttachDbFilename full path  at this picture
![image](https://user-images.githubusercontent.com/17914516/143137079-a99fdb0c-da03-4f3a-b5ca-c669ff8b8d8c.png)

gemography_backend-coding-challenge
## How to start project
1. go to the api project at path gemography_backend-coding-challenge\gemography_backend-coding-challenge shown at this image at command line to run the project
![image](https://user-images.githubusercontent.com/17914516/143137594-9055aa44-fd74-49b3-b005-99fa316eda8f.png)
 

2. run ```dotnet run``` to start project.
   - after run this command this message will apear in console at http ```Now listening on: http://localhost:5000``` or for https at ```Now listening on: https://localhost:5001``` shown at this image 
   ![image](https://user-images.githubusercontent.com/17914516/143138141-81635351-5a20-48e4-96a0-f95a13d8b0c3.png)

      
## API List
- ```https://localhost:5001/api/RepoLanguages``` display pure response from github in json format.
    - ![image](https://user-images.githubusercontent.com/17914516/143138447-48365abe-ba76-4286-8ca6-2b76dc64fbc6.png)
- ```https://localhost:5001/api/RepoLanguages/LanguagesList``` display languages list and it formated as object for every language and contain {```name: language name ,
count: repos number, 
items: language repos```}
    - ![image](https://user-images.githubusercontent.com/17914516/143138542-11d3f231-dcbd-45e8-9335-42ce23ccb929.png)

