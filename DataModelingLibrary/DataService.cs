using CsvHelper;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DataModelingLibrary
{
    public class DataService
    {
        
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

        public string GetCSVData(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;       
                      
        }

        public string ParseHTML(string page)
        {
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(page);
            var innerText = pageDocument.DocumentNode.SelectSingleNode("//table").InnerText;
            return innerText;
        }

        public void SaveFile(string fileName, string text)
        {
            //string fileName = @"C:\Users\user\source\repos\Libraries\DataModelingConsoleApplication\data.txt";
            //string path = String.Format(@"{0}foldername\Box.xml", AppDomain.CurrentDomain.BaseDirectory);

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.Write(text);
                }

                // Open the stream and read it back.    
                
                using (StreamReader sr1 = File.OpenText(fileName))
                {
                    string s = "";
                    while ((s = sr1.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }

        }

   
    }
}
