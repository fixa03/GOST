using System;
using System.Collections.Generic;
using System.IO;
using NumberSystem;

namespace FeistelNetwork
{
    class gost
    {
        string[] text_bytes;
        string[] key_bytes;
        const int key_len = 16; //64 числа
        string[,] swap_table;

        public void StrToInt(ref int[] array, string str)
        {

            array = new int[str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case 'A':
                        array[i] = 10;
                        break;
                    case 'B':
                        array[i] = 11;
                        break;
                    case 'C':
                        array[i] = 12;
                        break;
                    case 'D':
                        array[i] = 13;
                        break;
                    case 'E':
                        array[i] = 14;
                        break;
                    case 'F':
                        array[i] = 15;
                        break;
                    default:
                        array[i] = Convert.ToInt16(Char.GetNumericValue(str[i]));
                        break;

                }
            }

        }

        
        public gost() 
        {
            string text = String.Empty, key = String.Empty;
            // --EXAMPLE--
            //text_len = 4;
            //string text_exmaple =           "041A041C04170418";
            //string secret_text_exmaple =    "AB145CF4F57C9034";
            //string key_exmaple = "643411DAA851111DB53B6541D81AA2592625352720B6FFF43FF9CB60014702D4";
            //text_bytes = new string[4];
            //key_bytes = new string[16];
            //for (int i = 0; i < 4; i++)
            //{
            //    text_bytes[i] = secret_text_exmaple.Substring(i * 4, 4);
            //}

            //for (int i = 0; i < 16; i++)
            //{
            //    key_bytes[i] = key_exmaple.Substring(i * 4, 4);
            //}
            // --EXAMPLE--

            using (StreamReader fs = new StreamReader("input.txt"))
            {
                key = fs.ReadLine();
                while (!fs.EndOfStream)
                    text += fs.ReadLine();

            }

            text.Replace(' ', '_');

            text_bytes = new string[text.Length];
            key_bytes = new string[key_len];

            for (int i = 0; i < key_len; i++)
            {
                key_bytes[i] = ConvertSystem.To16( ConvertSystem.CharToCode(key[i]));
            }

            for (int i = 0; i < text.Length; i++)
                text_bytes[i] = ConvertSystem.To16(ConvertSystem.CharToCode(text[i]));

            swap_table = new string[8, 16];
            int l = 0, j = 0;

            using (StreamReader fs = new StreamReader("swap_table.txt"))
            {
                while (!fs.EndOfStream)
                {
                    foreach (string s in fs.ReadLine().Split(' '))
                    {
                        swap_table[l, j] = s;
                        j++;
                    }
                    l++; j = 0;
                }

            }

            //for (int i = 0; i < 8; i++)
            //{

            //    for (int k = 0; k < 16; k++)
            //        Console.Write(swap_table[i, k] + "\t");
            //    Console.WriteLine();
            //}


        }

        private string swap(int RowIndex, char symbol)
        {
            string res = String.Empty;
            switch (symbol)
            {
                case 'A':
                    res = swap_table[RowIndex, 10];
                    break;
                case 'B':
                    res = swap_table[RowIndex, 11];
                    break;
                case 'C':
                    res = swap_table[RowIndex, 12];
                    break;
                case 'D':
                    res = swap_table[RowIndex, 13];
                    break;
                case 'E':
                    res = swap_table[RowIndex, 14];
                    break;
                case 'F':
                    res = swap_table[RowIndex, 15];
                    break;
                default:
                    res = swap_table[RowIndex, (Convert.ToInt16(Char.GetNumericValue(symbol)))];
                    break;
            }

            switch (res)
            {
                case "10": res = "A"; break;
                case "11": res = "B"; break;
                case "12": res = "C"; break;
                case "13": res = "D"; break;
                case "14": res = "E"; break;
                case "15": res = "F"; break;
            }

            return res;

        }

        private void stepInit(int TextIndex, ref string[] left, ref string[] right)
        {
            string[] str = new string[4];
            TextIndex *= 4;
            Array.Copy(text_bytes, TextIndex, str, 0, 4);
            
            left = new string[2];
            right = new string[2];
            Array.Copy(str, 0, left, 0, 2);
            Array.Copy(str, 2, right, 0, 2);

        }

        private void step(int KeyIndex, int TextIndex, ref string[] left, ref string[] right)
        {

            string[] right_orign = new string[2];
            Array.Copy(right, right_orign, 2);

            string[] SelectKey = new string[2];
            KeyIndex *= 2;
            Array.Copy(key_bytes, KeyIndex , SelectKey, 0, 2);


            int swap_index = 0;
            string number = String.Empty;
            for (int i = 0; i < 2; i++)
            {
                long a = ConvertSystem.From16To10(right[i]) + ConvertSystem.From16To10(SelectKey[i]);
                right[i] = ConvertSystem.To16( ConvertSystem.From16To10(right[i]) + ConvertSystem.From16To10(SelectKey[i]) );

                foreach (char c in right[i])
                {
                    number += swap(swap_index % 8, c);
                    swap_index++;
                    //res += swap_table[swap_index, ]
                }
            }

            number = ConvertSystem.To2(ConvertSystem.From16To10(number), 32);
            number = new String(number.ToCharArray(), 11, 21) + new String(number.ToCharArray(), 0, 11);

            string number2 = ConvertSystem.To2( ConvertSystem.From16To10(left[0].ToString() + left[1].ToString()), 32);
            string res = BinarySystem.XOR(number, number2);
            
            res = ConvertSystem.To16(ConvertSystem.From2To10(res), 8);

            for (int i = 0; i < 2; i++)
                right[i] = res.Substring(i * 4, 4);

            Array.Copy(right_orign, left, 2);
        }

        private void SaveFile(string s)
        {

            using (StreamWriter fs = new StreamWriter("output.txt"))
            {
                fs.WriteLine(ConvertSystem.From16ToStr(ref key_bytes));
                fs.WriteLine(s);
            }

        }

        public void encrypt()
        {
            string[] left = null;
            string[] right = null;

            string res = String.Empty;
            for (int j = 0; j < text_bytes.Length / 4; j++)
            {
                stepInit(j, ref left, ref right);
                for (int i = 0; i < 8; i++)
                    step(i, j, ref left, ref right);

                for (int i = 0; i < 8; i++)
                    step(i, j, ref left, ref right);

                for (int i = 0; i < 8; i++)
                    step(i, j, ref left, ref right);

                for (int i = 7; i >= 0; i--)
                    step(i, j, ref left, ref right);

                //Console.Write( From16ToStr(ref left, ref right) );
                res += ConvertSystem.From16ToStr(ref left);
                res += ConvertSystem.From16ToStr(ref right);

            }

            SaveFile(res);

        }

        public void decrypt()
        {
            string[] left = null;
            string[] right = null;

            string res = String.Empty;
            for (int j = 0; j < text_bytes.Length / 4; j++)
            {
                stepInit(j, ref right, ref left);
                for (int i = 0; i < 8; i++)
                    step(i, j, ref left, ref right);
                for (int i = 7; i >= 0; i--)
                    step(i, j, ref left, ref right);
                for (int i = 7; i >= 0; i--)
                    step(i, j, ref left, ref right);
                for (int i = 7; i >= 0; i--)
                    step(i, j, ref left, ref right);

                res += ConvertSystem.From16ToStr(ref right);
                res += ConvertSystem.From16ToStr(ref left);

            }
            SaveFile(res);
        }

    }
}
