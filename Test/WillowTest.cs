using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WillowTree;

namespace Test
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        public ConsoleOutput()
        {
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        public string GetOuput()
        {
            return stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }
    }

    [TestClass]
    public class WillowTest
    {
        private string GetOutputName(string outputDir, string fileName)
        {
            return outputDir + @"\" + fileName.Split('.')[0] + ".txt";
        }
        [TestMethod]
        public void ReadExtended()
        {
            var ws = new WillowSaveGame();

            string path = Directory.GetCurrentDirectory() + @"\ReadTest\Extended";

            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*.sav"); //Getting Text files
            var total = files.Length;
            int count = 0;
            ConsoleOutput consoleOutput;
            DirectoryInfo outputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs\Extended");
            foreach (var file in outputDir.GetFiles("*.txt"))
            {
                file.Delete();
            }
            foreach (var collection in files)
            {
                bool success = true;
                consoleOutput = new ConsoleOutput();
                try
                {
                    ws.LoadWsg(collection.FullName);
                    count++;
                    consoleOutput.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"========== Exception ==========");
                    Console.WriteLine(e);
                    
                    File.WriteAllText(GetOutputName(outputDir.FullName, collection.Name), consoleOutput.GetOuput());
                    consoleOutput.Dispose();
                    success = false;
                }
                Console.WriteLine(collection.Name + @" : " + success);
            }
            Console.WriteLine(count + @"/" + total + @"(" + ((float)count / total) * 100f + @"%)");
            Assert.AreEqual(total, count);
        }
        [TestMethod]
        public void ReadOld()
        {
            var ws = new WillowSaveGame();

            string path = Directory.GetCurrentDirectory() + @"\ReadTest\Vanilla";

            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*.sav"); //Getting Text files
            var total = files.Length;
            int count = 0;
            ConsoleOutput consoleOutput;
            DirectoryInfo outputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs\Vanilla");
            foreach (var file in outputDir.GetFiles("*.txt"))
            {
                file.Delete();
            }
            foreach (var collection in files)
            {
                bool success = true;
                consoleOutput = new ConsoleOutput();
                try
                {
                    ws.LoadWsg(collection.FullName);
                    count++;
                    consoleOutput.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"========== Exception ==========");
                    Console.WriteLine(e);

                    File.WriteAllText(GetOutputName(outputDir.FullName, collection.Name), consoleOutput.GetOuput());
                    consoleOutput.Dispose();
                    success = false;
                }
                Console.WriteLine(collection.Name + @" : " + success);
            }
            Console.WriteLine(count + @"/" + total + @"(" + ((float)count / total) * 100f + @"%)");
            Assert.AreEqual(total, count);
        }

        //[TestMethod]
        //public void ReadOne()
        //{
        //    var ws = new WillowSaveGame();

        //    string path = Directory.GetCurrentDirectory() + @"\ReadTest\Extended";

        //    DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
        //    FileInfo[] files = d.GetFiles("*.sav"); //Getting Text files

        //    ws.LoadWsg(files[1].FullName);

        //}

        [TestMethod]
        public void Write()
        {
            var ws = new WillowSaveGame();

            string path = Directory.GetCurrentDirectory() + @"\ReadTest\Extended";

            DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
            FileInfo[] files = d.GetFiles("*.sav"); //Getting Text files
            var total = files.Length;
            int count = 0;
            ConsoleOutput consoleOutput;
            DirectoryInfo outputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs\Extended");
            foreach (var file in outputDir.GetFiles("*.txt"))
            {
                file.Delete();
            }
            foreach (var collection in files)
            {
                bool success = true;
                consoleOutput = new ConsoleOutput();
                try
                {
                    ws.LoadWsg(collection.FullName);
                    ws.SaveWsg(outputDir.FullName + @"\" + collection.Name);
                    count++;
                    consoleOutput.Dispose();
                }
                catch (Exception e)
                {
                    Console.WriteLine(@"========== Exception ==========");
                    Console.WriteLine(e);

                    File.WriteAllText(GetOutputName(outputDir.FullName, collection.Name), consoleOutput.GetOuput());
                    consoleOutput.Dispose();
                    success = false;
                }
                Console.WriteLine(collection.Name + @" : " + success);
            }
            Console.WriteLine(count + @"/" + total + @"(" + ((float)count / total) * 100f + @"%)");
            Assert.AreEqual(total, count);
        }
        //[TestMethod]
        //public void ReOpen()
        //{
        //    var ws = new WillowSaveGame();

        //    string path = Directory.GetCurrentDirectory() + @"\ReadTest\Extended";

        //    DirectoryInfo d = new DirectoryInfo(path);//Assuming Test is your Folder
        //    FileInfo[] files = d.GetFiles("*.sav"); //Getting Text files
        //    var total = files.Length;
        //    int count = 0;
        //    ConsoleOutput consoleOutput;
        //    DirectoryInfo outputDir = Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\Logs\Extended");
        //    foreach (var file in outputDir.GetFiles("*.txt"))
        //    {
        //        file.Delete();
        //    }
        //    foreach (var collection in files)
        //    {
        //        bool success = true;
        //        consoleOutput = new ConsoleOutput();
        //        try
        //        {
        //            ws.LoadWsg(collection.FullName);
        //            ws.SaveWsg(outputDir.FullName + @"\" + collection.Name);
        //            ws.LoadWsg(outputDir.FullName + @"\" + collection.Name);
        //            count++;
        //            consoleOutput.Dispose();
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(@"========== Exception ==========");
        //            Console.WriteLine(e);

        //            File.WriteAllText(GetOutputName(outputDir.FullName, collection.Name), consoleOutput.GetOuput());
        //            consoleOutput.Dispose();
        //            success = false;
        //        }
        //        Console.WriteLine(collection.Name + @" : " + success);
        //    }

        //    Console.WriteLine(count + @"/" + total + @"(" + ((float)count / total) * 100f + @"%)");
        //    Assert.AreEqual(total, count);
        //}
    }
}
