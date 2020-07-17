using Business;
using System;

namespace ProjectName.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            XJobUpdate handler = new XJobUpdate();

            XJobRequest request = new XJobRequest();

            var response = handler.Excute(request);

            Console.Read();
        }
    }
}
