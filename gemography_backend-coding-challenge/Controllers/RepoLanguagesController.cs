using gemography_backend_coding_challenge.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace gemography_backend_coding_challenge.Controllers
{
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

        //For every language,i return the follwing data  :
         //Number of repos using this language
        // The list of repos using the language
        async Task<List<RepoLists>> GetRepoModifiedListAsync(string JsonData)
        {
            return null;    
        }
    }
}
