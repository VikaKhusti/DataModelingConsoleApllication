using System;
using System.Collections.Generic;

namespace DataModelingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //csse_covid_19_data/csse_covid_19_daily_reports
            string owner = "CSSEGISandData";
            string repo = "COVID-19";
            string path = "csse_covid_19_data/";
            DisplayContent(owner, repo, path);
        }

        static void DisplayContent(string owner, string repo, string path)
        {
            var dm = new DataModelingLibrary.DataService();
            var count = 1;
            Dictionary<int, string> urls = new Dictionary<int, string>();
            try
            {
                var contents = dm.listAsync(owner, repo, path).Result;
                foreach (var file in contents)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    var fileType = (string)file["type"];
                    if (fileType == "dir")
                    {
                        var directoryContentsUrl = (string)file["url"];
                        urls.Add(count, directoryContentsUrl);
                        Console.WriteLine($" #{count} | DIR: {directoryContentsUrl}");
                        Console.WriteLine("");

                    }
                    else if (fileType == "file")
                    {
                        var downloadUrl = (string)file["download_url"];
                        urls.Add(count, downloadUrl);
                        Console.WriteLine($" #{count} | DOWNLOAD: {downloadUrl}");
                        Console.WriteLine("");
                    }
                    count++;
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Type 0 to exit or '..' to go back");
                Console.WriteLine("Type # to open item (0 to exit)");
                
                try
                {
                    var input = Console.ReadLine().Trim();
                    if (input.ToUpper() == "EXIT") return;
                    else
                    {
                        var item = urls[Convert.ToInt32(input)];
                        Console.WriteLine(item);
                        Console.WriteLine(SplitUrl(item));
                        String[] arr = SplitUrl(item);
                        foreach (var i in arr)
                        {
                            Console.WriteLine(i);
                        }

                    }

                } catch (Exception e)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid # entered. Try again? y/n");
                    var answer = Console.ReadLine().Trim();
                    try
                    {
                        if (answer == "n") return;
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine(e1);
                    }
                }                

            } catch (AggregateException ae)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Enable to display data");
            }
            
            Console.ResetColor();

        }

        public static String[] SplitUrl(string url)
        {
            String[] point = url.Split('/');
            return point;
        }


    }
}
