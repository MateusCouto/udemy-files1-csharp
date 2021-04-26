using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Course.Entities
{
    class UseFile
    {
        public string Folder { get; set; }
        public string SrcPath { get; set; }
        public string DstPath { get; set; }

        public UseFile()
        {
        }

        public UseFile(string folder, string srcPath, string dstPath)
        {
            Folder = folder;
            SrcPath = srcPath;
            DstPath = dstPath;
        }

        public void UsingFileInfo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            StringBuilder fo = new();
            int count = 0;
            try
            {
                FileInfo f = new(SrcPath);
                f.CopyTo(DstPath);

                string[] lines = File.ReadAllLines(SrcPath);
                fo.AppendLine("<Bloco UsingFileInfo>");
                foreach (string line in lines)
                {
                    fo.AppendLine($"Linhas: {count++} - {line}");
                }
                Console.WriteLine(fo.ToString());
            }
            catch (FileNotFoundException e)
            {
                GetClassInfo(e);
            }

            catch (IOException e)
            {

                Console.WriteLine($"Exception: An error occurred { e.Message}\n");

            }

        }

        public void UsingFinally()
        {

            Console.ForegroundColor = ConsoleColor.DarkGray;
            StreamReader sr = null;
            StringBuilder uf = new();
            int count = 0;
            try
            {
                sr = File.OpenText(SrcPath);
                uf.AppendLine("<Bloco Finally>");
                while (!sr.EndOfStream)
                {
                    uf.AppendLine($"Linhas: {count++} - {sr.ReadLine()}");

                }
                Console.WriteLine(uf.ToString());
            }
            catch (IOException e)
            {
                GetClassInfo(e);
            }
            finally
            {
                if (sr != null) sr.Close();
            }
        }

        public void UsingStreamReader()
        {
            int count = 0;
            StringBuilder str = new();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            try
            {
                using StreamReader sr = File.OpenText(SrcPath);
                str.AppendLine("<Bloco UsingStreamReader> ");
                while (!sr.EndOfStream)
                {
                    str.AppendLine($"Linhas: {count++} - {sr.ReadLine()}");
                }
                Console.WriteLine(str.ToString());
            }
            catch (FileNotFoundException e)
            {

                GetClassInfo(e);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void GetDirectoryList(bool check)
        {
            StringBuilder fd = new();
            IEnumerable<string> folders;
            try
            {
                if (check)
                {
                    folders = Directory.EnumerateDirectories(Folder, ".", SearchOption.AllDirectories);
                }
                else
                {
                    folders = Directory.EnumerateFiles(Folder, ".", SearchOption.AllDirectories);
                }

                foreach (string s in folders)
                {
                    fd.AppendLine(s);
                }
                Console.WriteLine(fd.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(e.Message);
            }
        }
        public void CreateDirectory(string create)
        {
            try
            {

                Directory.CreateDirectory(Folder + @"\" + create);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(e.Message);
            }
            finally { }
        }
        public void RemoveDirectory(string create)
        {
            try
            {

                Directory.Delete(Folder + @"\" + create);
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occured");
                Console.WriteLine(e.Message);
            }
            finally { }
        }

        public void ReadFileCvs(string outfile)
        {
            try
            {
                string[] lines = File.ReadAllLines(SrcPath);
                Directory.CreateDirectory(Folder + @"\" + outfile);

                using (StreamWriter sw = File.AppendText(Path.GetDirectoryName(SrcPath)))
                {
                    foreach (string line in lines)
                    {

                        string[] fields = line.Split(',');
                        string name = fields[0];
                        double price = double.Parse(fields[1], CultureInfo.InvariantCulture);
                        int quantity = int.Parse(fields[2]);

                        Product prod = new(name, price, quantity);

                        sw.WriteLine(prod.Name + "," + prod.Total().ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred");
                Console.WriteLine(e.Message);
            }
        }
        public void GetDirectoryInfo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            StringBuilder gdi = new();

            gdi.AppendLine("<Info GetDirectoryInfo> ");
            gdi.AppendLine("DirectorySeparatorChar: " + Path.DirectorySeparatorChar);
            gdi.AppendLine("SeparatorChar: " + Path.PathSeparator);
            gdi.AppendLine("GetDirectName: " + Path.GetDirectoryName(SrcPath));
            gdi.AppendLine("GetFileName: " + Path.GetFileName(SrcPath));
            gdi.AppendLine("GetExtension: " + Path.GetExtension(SrcPath));
            gdi.AppendLine("GetFileNameWithoutExtension: " + Path.GetFileNameWithoutExtension(SrcPath));
            gdi.AppendLine("GetFullPath: " + Path.GetFullPath(Folder));
            gdi.AppendLine("GetTempPath: " + Path.GetTempPath());

            Console.WriteLine(gdi.ToString());
        }

        public static void GetClassInfo(Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            StringBuilder se = new();
            se.AppendLine($"Classe: {System.Reflection.MethodBase.GetCurrentMethod().DeclaringType}");
            se.AppendLine($"Exception: {e.GetType().Name}");
            se.AppendLine($"Método: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            se.AppendLine($"Mensagem: {e.Message}");

            Console.WriteLine(se.ToString());
        }

        public override string ToString()
        {
            StringBuilder info = new();

            info.AppendLine("<Arquivos e Diretórios>");
            info.AppendLine($"Folder: {Folder}");
            info.AppendLine($"SrcPath: {SrcPath}");
            info.AppendLine($"DstPath: {DstPath}");

            return info.ToString();
        }
    }
}
