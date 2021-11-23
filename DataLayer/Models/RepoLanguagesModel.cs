using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class RepoLanguagesModel
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("incomplete_results")]
        public bool IncompleteResults { get; set; }
        [JsonProperty("items")]
        public List<Items> Items { get; set; }
    }

    public class Items
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }

    }
    public class RepoLists
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("repo_list")]
        public List<Items> RepoList { get; set; }
    }
}
