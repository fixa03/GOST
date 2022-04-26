using System;
using System.Collections.Generic;

namespace NumberSystem
{
    class ConvertSystem
    {
        static public string To16(int value, int len = 4)
        {
            string res = String.Empty;

            List<int> array = new List<int>();

            while (value > 0)
            {
                array.Add(value % 16);
                value /= 16;
            }

            if (len > array.Count)
                for (int i = 0; i < (len - array.Count); i++)
                    array.Add(0);

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

        static public int From16To10(string value)
        {
            List<int> array = new List<int>(value.Length);
            int res = 0;

            foreach (char c in value)
            {
                switch (c)
                {
                    case 'A':
                        array.Add(10);
                        break;
                    case 'B':
                        array.Add(11);
                        break;
                    case 'C':
                        array.Add(12);
                        break;
                    case 'D':
                        array.Add(13);
                        break;
                    case 'E':
                        array.Add(14);
                        break;
                    case 'F':
                        array.Add(15);
                        break;
                    default:
                        array.Add(Convert.ToInt16(Char.GetNumericValue(c)));
                        break;
                }

            }

            array.Reverse();

            for (int i = 0; i < array.Count; i++)
            {
                res += array[i] * Convert.ToInt16(Math.Pow(16, i));
            }

            return res;
        }


        static public int From2To10(string value)
        {
            List<int> array = new List<int>(value.Length);
            int res = 0;

            foreach (char c in value)
            {
                array.Add(Convert.ToInt16(Char.GetNumericValue(c)));
            }

            array.Reverse();

            for (int i = 0; i < array.Count; i++)
            {
                res += array[i] * Convert.ToInt16(Math.Pow(2, i));
            }

            return res;

        }

        static public string From10To2(int value, int len)
        {
            string res = String.Empty;

            List<int> array = new List<int>();

            while (value > 0)
            {
                array.Add(value % 2);
                value /= 2;
            }

            if (array.Count < len)
                for (int i = array.Count; i < len; i++)
                    array.Add(0);

            array.Reverse();

            foreach (int i in array)
                res += i.ToString();


            return res;
        }

    }
}
