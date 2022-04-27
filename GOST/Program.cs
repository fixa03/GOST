using System;
using FeistelNetwork;


namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine(Console.OutputEncoding);

            //длина текста кратна 4 
            //длина ключа = 16 символов


            while (true)
            {
                gost obj = new gost();
                Console.Write("Зашифровать(space) или расшировать(any key) > ");
                ConsoleKeyInfo choice = Console.ReadKey();

                if (choice.Key == ConsoleKey.Spacebar)
                {
                    obj.encrypt();
                    Console.WriteLine("Зашифровано!");
                }
                else 
                {
                    obj.decrypt();
                    Console.WriteLine("Расшифровано!");
                }
            }

        }
    }
}
