// See https://aka.ms/new-console-template for more information

namespace stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome8063();
            Welcome4406();

        }

        static partial void Welcome4406();
        private static void Welcome8063()
        {
            Console.WriteLine("Enter Your Name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
            Console.ReadKey();
        }
    }
}

