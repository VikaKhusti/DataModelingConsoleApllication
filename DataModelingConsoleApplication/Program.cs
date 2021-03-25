using System;

namespace DataModelingConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var dm = new DataModelingLibrary.DataService();
            var hello = dm.hello();
            Console.WriteLine(hello);
        }
    }
}
