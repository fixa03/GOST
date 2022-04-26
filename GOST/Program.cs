using System;
using Feistel_network;

namespace GOST
{
    class Program
    {
        static void Main(string[] args)
        {
            //длина текста кратна 4 
            //длина ключа = 16 символов

            gost obj = new gost("кмзи", "оченьхорошийключ");
            //string str2 = obj.Addition16("144E","4B7");

            obj.step(0, 0);

            string str = Console.ReadLine();
            str = str.ToLower();

            int[] array_byte = new int[str.Length];
            for (int i = 0; i < array_byte.Length; i++)
                array_byte[i] = str[i];

        }
    }
}
