using System;

namespace DataModelingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DisplayContent();
        }

        static void DisplayContent()
        {
            var dm = new DataModelingLibrary.DataService();

            string owner = "CSSEGISandData";
            string repo = "COVID-19";
            string path = "csse_covid_19_data";

            var contents = dm.listAsync(owner, repo, path).Result;
            var count = 1;
            foreach (var file in contents)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                var fileType = (string)file["type"];
                if (fileType == "dir")
                {
                    var directoryContentsUrl = (string)file["url"];
                    // use this URL to list the contents of the folder
                    //count++;
                    Console.WriteLine($" #{count} | DIR: {directoryContentsUrl}");
                    Console.WriteLine("");
                    
                }
                else if (fileType == "file")
                {
                    var downloadUrl = (string)file["download_url"];
                    // use this URL to download the contents of the file
                    //count++;
                    
                    Console.WriteLine($" #{count} | DOWNLOAD: {downloadUrl}");
                    Console.WriteLine("");
                }
                count++;
            }
            Console.ResetColor();

        }


    }
}
