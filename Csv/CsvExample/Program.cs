using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvEnumerable;

namespace CsvExample
{
    class SomeClass
    {
        public int Foo { get; set; }
        public int Bar { get; set; } = 10;
    }
    class Program
    {
        static string filename = "./some.csv";
        static void CreateFile()
        {
            List<SomeClass> someList = new List<SomeClass>
            {
                new SomeClass{ Foo = 10 },
                new SomeClass{ Foo = 18 },
                new SomeClass{ Foo = 13 },
                new SomeClass{ Foo = 15 },
                new SomeClass{ Foo = 17 },
                new SomeClass{ Foo = 13 },
                new SomeClass{ Foo = 14 },
            };

            using (CsvWriter writer = new CsvWriter(new StreamWriter(Path.GetFullPath(filename), false, Encoding.Unicode)))
            {
                writer.WriteRecords(someList);

            }

        }

        static void ReadFile()
        {
            //using (CsvCollection<SomeClass> collection = new CsvCollection<SomeClass>(new StreamReader(Path.GetFullPath(filename), Encoding.Unicode)))
            //{
            //    foreach(var some in collection)
            //    {
            //        Console.WriteLine(some);
            //    }
            //}

        }

        static void Main(string[] args)
        {
            ReadFile();
            Console.WriteLine("Press F to exit...");
            Console.ReadKey();
        }
    }
}
