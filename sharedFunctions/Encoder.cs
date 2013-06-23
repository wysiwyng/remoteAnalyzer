using System;
using System.IO;

namespace sharedFunctions
{
    /// <summary>
    /// a class for encoding and decoding strings
    /// </summary>
    public static class Encoder
    {
        /// <summary>
        /// converts a string to its hex representation
        /// </summary>
        /// <param name="input">the string</param>
        /// <returns>its hex representation</returns>
        public static String stringToHex(String input)
        {
            String temp = "";
            foreach (char letter in input.ToCharArray())
            {
                temp += String.Format("{0:X}", Convert.ToInt32(letter)) + " ";
            }
            return temp.TrimEnd(' ');
        }
 
        /// <summary>
        /// converts a stream into its hex representation
        /// </summary>
        /// <param name="input">the stream</param>
        /// <returns>its hex representation as string</returns>
        public static String stringToHex(Stream input)
        {
            return stringToHex(new StreamReader(input).ReadToEnd());
        }

        /// <summary>
        /// converts a hex representation back into a string
        /// </summary>
        /// <param name="input">a string of hex characters</param>
        /// <returns>the actual string depicted by the hex input</returns>
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

        /// <summary>
        /// converts a stream of hex data to the string representing it
        /// </summary>
        /// <param name="input">the hex stream</param>
        /// <returns>the string representation</returns>
        public static String hexToString(Stream input)
        {
            return hexToString(new StreamReader(input).ReadToEnd());
        }
    }
}
