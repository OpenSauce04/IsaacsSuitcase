using System;
using System.IO;
using System.IO.Compression;
using Figgle;

namespace IsaacsSuitcase
{
    internal class Program
    {
        static void Main(string[] args)
        {

            selectionLoop:
            Console.Clear();
            Console.WriteLine(FiggleFonts.Ivrit.Render("Isaac's Suitcase"));
            Console.WriteLine("1: Extract save");
            Console.WriteLine("2: Inject save");
            Console.Write(">");
            var input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    Extract();
                    break;
                case "2":
                    Inject();
                    break;
                default:
                    goto selectionLoop;
            }
        }
        
        static String GetModSaveLocation()
		{
            Console.Write("Input the location of your data folder>");
            return Console.ReadLine();
		}

        static void Extract()
        {
            var saveLocation = GetModSaveLocation();
            File.Delete("IsaacSuitcase.save");
            ZipFile.CreateFromDirectory(saveLocation, "IsaacSuitcase.save");
            Finish();
        }

        static void Inject()
        {
            if (File.Exists("./IsaacSuitcase.save"))
            {
                Console.WriteLine("IsaacSuitcase.save found.");
			} else
			{
                Console.WriteLine("IsaacSuitcase.save not found!");
                Abort();
            }
            var saveLocation = GetModSaveLocation();
            Console.WriteLine(FiggleFonts.CyberMedium.Render("This will overwrite"));
            Console.WriteLine(FiggleFonts.CyberMedium.Render("your current save data"));
            Console.WriteLine(FiggleFonts.CyberMedium.Render("for all of your mods"));
            Console.Write("Are you sure? [Y/N]>");
            if (Console.ReadLine().ToUpper() == "Y")
            {
                Directory.Delete(saveLocation, true);
                ZipFile.ExtractToDirectory("IsaacSuitcase.save", saveLocation);
                Finish();
            } else
			{
                Abort();
			}
        }

        static void Abort()
        {
            Console.WriteLine("Aborted. Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void Finish()
        {
            Console.WriteLine("Operation finished. Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}