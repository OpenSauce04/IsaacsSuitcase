using Figgle;

namespace IsaacsSuitcase
{
    internal class Program
    {
        private static String input;
        static void Main(string[] args)
        {
            selectionLoop:
            Console.Clear();
            Console.WriteLine(FiggleFonts.Ivrit.Render("Isaac's Suitcase"));
            Console.WriteLine("1: Extract save");
            Console.WriteLine("2: Inject save");
            Console.Write(">");
            input = Console.ReadLine();
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

        static void Extract()
        {

        }

        static void Inject()
        {

        }
    }
}