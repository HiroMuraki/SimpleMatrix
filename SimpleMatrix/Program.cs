using System;
using System.IO.MemoryMappedFiles;
using System.Reflection.Metadata;
using System.Xml.Schema;
using static System.Console;

namespace ConsoleApp4 { 
    class Program {
        static void Main(string[] args) {
            //TestData.MatrixTest();
            //TestData.RationalNumberTest();
            TestData.DeterminantTest();
            ReadKey();
        }
    }
}
