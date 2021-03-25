using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DataModelingLibrary
{
    public class DataService
    {
         public string hello() {
            var hello = "hello from lib";
            return hello;
         }

        /// <summary>
        /// Returns the contents of a GitRepo and displays the URL to download each file 
        /// </summary>
        /// <remarks>
        /// Takes string values like: 
        /// <c>string owner = "CSSEGISandData", repo = "CSSEGISandData", path = "csse_covid_19_data";</c>
        /// </remarks>

        public async System.Threading.Tasks.Task<JArray> listAsync(string owner, string repo, string path = "")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(
                new ProductInfoHeaderValue("MyApplication", "1"));
            //var repo = "markheath/azure-deploy-manage-containers";
            //var repo = "CSSEGISandData/COVID-19";
            //var path = "csse_covid_19_data/csse_covid_19_daily_reports";
            var contentsUrl = $"https://api.github.com/repos/{owner}/{repo}/contents/{path}";
           // var contentsUrl = $"https://api.github.com/repos/{repo}/git/trees/master";
            var contentsJson = await httpClient.GetStringAsync(contentsUrl);
            var contents = (JArray)JsonConvert.DeserializeObject(contentsJson);

            return contents;
        } 
    }
}
