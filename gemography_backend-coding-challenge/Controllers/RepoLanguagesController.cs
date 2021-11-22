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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await GetRepoDataAsync();
            return Ok(result);

        }
        //consume github api and return original result without any operations 
        async Task<string> GetRepoDataAsync()
        {
            string url = $"https://api.github.com/search/repositories?q=created:%3E2021-11-11&sort=stars&order=desc&per_page=100";


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
         
    }
}
