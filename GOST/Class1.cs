using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feistel_network
{
    class gost
    {
        string[] text_bytes;
        string[] key_bytes;

        const int key_len = 64;

        public string To16(int value)
        {
            string res = String.Empty;

            List<int> array = new List<int>();

            while (value > 0)
            {
                array.Add(value % 16);
                value /= 16;
            }

            array.Reverse();

            for (int i = 0; i < array.Count; i++)
            {
                switch (array[i])
                {
                    case 10:
                        res += "A";
                        break;
                    case 11:
                        res += "B";
                        break;
                    case 12:
                        res += "C";
                        break;
                    case 13:
                        res += "D";
                        break;
                    case 14:
                        res += "E";
                        break;
                    case 15:
                        res += "F";
                        break;
                    default:
                        res += array[i].ToString();
                        break;
                }
            }

            return res;
        }

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

        public string Addition16(string left, string right)
        {
            string res = String.Empty;

            int[] left_numbers = null;
            int[] right_numbers = null;

            StrToInt(ref left_numbers, left);
            StrToInt(ref right_numbers, right);

            Array.Reverse(left_numbers);
            Array.Reverse(right_numbers);

            int[] array = new int[Math.Max(left.Length, right.Length)];
            //array.Initialize();

            for (int i = 0, temp = 0; i < array.Length; i++)
            {
                //if(temp == 0)
                //    temp = (i < left_numbers.Length ? left_numbers[i] : 0) + (i < right_numbers.Length ? right_numbers[i] : 0);
                //else
                    temp = (i < left_numbers.Length ? left_numbers[i] : 0) + (i < right_numbers.Length ? right_numbers[i] : 0);
                array[i] += temp % 16;

                if (temp >= 16)
                    array[i + 1] = 1;
                    
            }

            Array.Reverse(array);

            foreach (int i in array)
            {

                switch (i)
                {
                    case 10:
                        res += "A";
                        break;
                    case 11:
                        res += "B";
                        break;
                    case 12:
                        res += "C";
                        break;
                    case 13:
                        res += "D";
                        break;
                    case 14:
                        res += "E";
                        break;
                    case 15:
                        res += "F";
                        break;
                    default:
                        res += i.ToString();
                        break;

                }

            }
                

            return res;
        
        }

        public gost()
        { }

        public gost(string text, string key) 
        {
            text_bytes = new string[text.Length];
            key_bytes = new string[key_len];

            for (int i = 0; i < key_len; i++)
                key_bytes[i] = To16(key[i]);


        }
    }
}
