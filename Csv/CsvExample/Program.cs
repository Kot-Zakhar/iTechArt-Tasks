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
        public int Bar { get; set; }
    }
    class Program
    {
        static string filename = "./some.csv";
        static void CreateFile()
        {
            List<SomeClass> someList = new List<SomeClass>
            {
                new SomeClass{ Foo = 10, Bar = 7 },
                new SomeClass{ Foo = 18, Bar = 6 },
                new SomeClass{ Foo = 13, Bar = 5 },
                new SomeClass{ Foo = 15, Bar = 4 },
                new SomeClass{ Foo = 17, Bar = 3 },
                new SomeClass{ Foo = 13, Bar = 2 },
                new SomeClass{ Foo = 14, Bar = 1 },
            };

            using (CsvWriter writer = new CsvWriter(new StreamWriter(Path.GetFullPath(filename), false, Encoding.Unicode)))
            {
                writer.WriteRecords(someList);
            }

        }

        static void ReadFile()
        {
            for (int i = 0; i < 2; i++)
            {
                var someList = new CsvCollection<SomeClass>(Path.GetFullPath(filename));
                foreach(var item in someList)
                    Console.WriteLine($"{{bar: {item.Bar}; foo: {item.Foo};}}");
                Console.WriteLine("Time to change file.");
                Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            CreateFile();
            ReadFile();
            Console.WriteLine("Press F to exit...");
            Console.ReadKey();
        }
    }
}
