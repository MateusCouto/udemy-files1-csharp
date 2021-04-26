using System.IO;
using System;
using Course.Entities;
using System.Collections.Generic;

namespace Course
{
    class Program
    {
        static void Main(string[] args)
        {

             string folder = @"K:\csharp\Tools";
            string srcPath = @"K:\csharp\Tools\teste.txt";
            string dstPath = @"K:\csharp\Tools\teste2.txt";
            //string srcPath = @"K:\csharp\Tools\summary.txt";


            UseFile fl = new(folder, srcPath, dstPath);

            /*
            Console.WriteLine(fl);

            fl.UsingFileInfo();

            fl.UsingFinally();

            fl.UsingStreamReader();

            fl.GetDirectoryList(true);

            fl.GetDirectoryInfo();*/

            fl.GetDirectoryList(false);

             //fl.CreateDirectory("testesasa");
            //fl.RemoveDirectory("testesasa");

            //fl.ReadFileCvs("saida");
        }
    }
}
