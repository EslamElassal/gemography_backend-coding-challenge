using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        async Task<string> GetRepoDataAsync()
        {
            return null;
        }
         
    }
}
