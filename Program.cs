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

        }

        static void Inject()
        {

        }
    }
}