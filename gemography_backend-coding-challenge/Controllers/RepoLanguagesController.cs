using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace gemography_backend_coding_challenge.Controllers
{   
    [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class RepoLanguagesController : ControllerBase
    {
        private readonly ILogger<RepoLanguagesController> _logger;

        public RepoLanguagesController(ILogger<RepoLanguagesController> logger)
        {
            _logger = logger;
        }
        //this action to return api json data from github
        //api end point =>https://localhost:44358/api/RepoLanguages
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await GetRepoDataAsync();
            return Ok(result);

        }
        //consume github api and return original result without any operations 
        async Task<string> GetRepoDataAsync()
        {
            //return recent first 100 repo in first page with current date
            
            string url = "https://api.github.com/search/repositories?q=created:%3E"+DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd").Trim()+"&sort=stars&order=desc&per_page=100";


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");//Set the User Agent to "request"

                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var Result = await response.Content.ReadAsStringAsync();

                    return Result;
                }
            }
        }

        //this action to return return our language list in json
        //api end point =>https://localhost:44358/api/RepoLanguages/LanguagesList
        [HttpGet("LanguagesList")]
        public async Task<IActionResult> LanguagesList()
        {

            return Ok(await GetRepoModifiedListAsync(await GetRepoDataAsync()));


        }
        //For every language,i return the follwing data  :
        //Number of repos using this language
        // The list of repos using the language
        async Task<List<RepoLists>> GetRepoModifiedListAsync(string JsonData)
        {
            RepoLanguagesModel _dataResponse = JsonConvert.DeserializeObject<RepoLanguagesModel>(JsonData);
            List<string> langs = new List<string>();
            for (int i = 0; i < _dataResponse.Items.Count; i++)
            {
                if (!langs.Contains(_dataResponse.Items[i].Language))
                    langs.Add(_dataResponse.Items[i].Language);
            }
            List<RepoLists> FinalLists = new List<RepoLists>();
            for (int i = 0; i < langs.Count; i++)
            {
                List<Items> FinalItems = _dataResponse.Items.FindAll(x => x.Language == langs[i]);
                RepoLists temp = new RepoLists();
                temp.Name = langs[i];
                temp.Count = FinalItems.Count;
                temp.RepoList = FinalItems;
                FinalLists.Add(temp);

            }
            return FinalLists;
        }
    }
}
