using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sharedFunctions
{
    public class Encoder
    {
        public static String stringToHex(String input)
        {
            String temp = "";
            foreach (char letter in input.ToCharArray())
            {
                temp += String.Format("{0:X}", Convert.ToInt32(letter)) + " ";
            }
            return temp.TrimEnd(' ');
        }
 
        public static String stringToHex(Stream input)
        {
            return stringToHex(new StreamReader(input).ReadToEnd());
        }

        public static String hexToString(String input)
        {
            String[] inputSplit = input.Split(' ');
            String temp = "";
            foreach (String hex in inputSplit)
            {
                temp += Char.ConvertFromUtf32(Convert.ToInt32(hex, 16));
            }
            return temp;
        }

        public static String hexToString(Stream input)
        {
            return hexToString(new StreamReader(input).ReadToEnd());
        }
    }
}
