using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;

namespace ResumeSharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Diagnostics.Trace.Listeners.Add(new ConsoleTraceListener());
            string json = File.ReadAllText("anders-marzi-tornblad.json");
            var resume = Serializer.Deserialize(json);
            var isValid = Validator.Validate(resume, out var results);
            Console.WriteLine(isValid);
            foreach (var result in results)
            {
                Console.WriteLine($"    {result.ToString()}");
            }
        }
    }
}