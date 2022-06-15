#pragma warning disable CS8604 // Possible null reference argument.

using System;
using System.IO;
using System.IO.Compression;
using Figgle;

namespace IsaacsSuitcase
{
    internal class Program
    {
        static void Main()
        {

        selectionLoop:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("v1.0.1 | Written by OpenSauce");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(FiggleFonts.Ivrit.Render("Isaac's Suitcase"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1: Backup save");
            Console.WriteLine("2: Restore save");
            Console.WriteLine("3: Reset save data location");
            Console.Write(">");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Backup();
                    break;
                case "2":
                    Restore();
                    break;
                case "3":
                    ResetModSaveLocation();
                    break;
                default:
                    goto selectionLoop;
            }
        }
        static Boolean CheckModSaveLocation(String location)
        {
            if (File.Exists(Path.Combine(location, "../isaac-ng.exe"))) {
                return true;
            }
            return false;
        }
        static void ResetModSaveLocation()
        {
            var pathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IsaacsSuitcase.path");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Input the location of \"data\" folder inside your Isaac folder");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(">");
            var savePath = Console.ReadLine();
            while (!CheckModSaveLocation(savePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Path is invalid, please try again");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(">");
                savePath = Console.ReadLine();
            }
            File.WriteAllTextAsync(pathFile, savePath);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Finished.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Press any key to restart the program");
            Console.ReadKey();
            Main();
        }

        static String GetModSaveLocation()
        {
            var pathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IsaacsSuitcase.path");
            if (File.Exists(pathFile)) {
                return File.ReadAllText(pathFile);
            }
            // File does not exist
            Console.Write("Input the location of \"data\" folder inside your Isaac folder (you only need to do this once per computer)\n>");
            var savePath = Console.ReadLine();
            while (!CheckModSaveLocation(savePath))
            {
                Console.WriteLine("Path is invalid, please try again");
                Console.Write(">");
                savePath = Console.ReadLine();
            }
            File.WriteAllTextAsync(pathFile, savePath);
            return savePath;
        }

        static void Backup()
        {
            if (File.Exists("./IsaacSuitcase.isave"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThis will overwrite the existing backup currently stored in ./IsaacSuitcase.isave");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Are you sure? [Y/N]>");
                if (!YN())
                { Abort(); }
            }
            var saveLocation = GetModSaveLocation();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Extracting mod save files to ./IsaacSuitcase.isave...");
            Console.ForegroundColor = ConsoleColor.White;
            File.Delete("IsaacSuitcase.isave");
            ZipFile.CreateFromDirectory(saveLocation, "IsaacSuitcase.isave");
            Finish();
        }

        static Boolean YN() {
            if (Console.ReadLine().ToUpper() == "Y") {
                return true;
            }
            return false;
        }

        static void Restore()
        {
            if (File.Exists("./IsaacSuitcase.isave"))
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("IsaacSuitcase.isave found.");
                Console.ForegroundColor = ConsoleColor.White;
            } else
			{
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("IsaacSuitcase.isave not found!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please either place the file in the same directory as this program and/or use the Backup function to obtain the file from the computer you want to transfer your save from.");
                Console.ForegroundColor = ConsoleColor.White;
                Abort();
            }
            var saveLocation = GetModSaveLocation();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(FiggleFonts.CyberMedium.Render("This will overwrite"));
            Console.WriteLine(FiggleFonts.CyberMedium.Render("your current save data"));
            Console.WriteLine(FiggleFonts.CyberMedium.Render("for all of your mods"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Are you sure? [Y/N]>");
            if (YN())
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Injecting mod save files from ./IsaacSuitcase.isave...");
                Console.ForegroundColor = ConsoleColor.White;
                if (Directory.Exists(saveLocation))
                {
                    Directory.Delete(saveLocation, true);
                }
                ZipFile.ExtractToDirectory("IsaacSuitcase.isave", saveLocation);
                Finish();
            } else
			{
                Abort();
			}
        }

        static void Abort()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Aborted.");
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine(" Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
        static void Finish()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Finished.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" Press any key to exit");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}